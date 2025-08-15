using MathNet.Numerics.IntegralTransforms;
using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestMQTTWindows.Models;
using System.Linq;
using System.Globalization;

namespace serial_FFT_plotter
{
    public partial class Form1 : Form
    {
        volatile bool get_new_fft_frame = false;

        int serial_state = 0;
        int receive_data_size = 0;
        int receive_data_counter = 0;

        const int stream_buffer_size = 1024;
        float[] stream_buffer = new float[stream_buffer_size];
        int stream_buffer_idx = 0;

        int capture_recieved_size = 0;
        float[] capture_buffer = new float[stream_buffer_size];
        int capture_line_idx = -1;
        int capture_line2_idx = -1;

        int FFT_recieved_size = 0;
        float[] FFT_buffer = new float[stream_buffer_size];

        int device_message_count = 0;
        int device_message_recieved_size = 0;
        byte[] device_message_buffer = new byte[stream_buffer_size];

        float serial_buffer_data_float;
        UInt32 serial_buffer_data;

        List<float> record_data;

        int open_size = 1024;

        bool record_enable = false;

        int cursor_x = 0;
        int cursor_y = 0;
        bool cursor_mode = false;

        bool clear_streamming_flag = false;
        bool clear_FFT_flag = false;

        bool num_bar_draw = false;

        bool auto_export_data_enable;
        string prefix_filename_auto_export = "";
        int auto_export_counter = 0;

        int FFT_style = 0;

        const int bt01_signal_size = 129;
        const int bt02_signal_size = 257;

        bool auto_explain_data_enable = false;
        aiexplainview explainview = new aiexplainview();

        public Form1()
        {

            InitializeComponent();

            system_message_box.Alignment = ToolStripItemAlignment.Right;

            //var tmr = new System.Threading.Timer(TimerCallback, null, 0, 10000);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            serial_port_name_box.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                serial_port_name_box.Items.Add(port);
            }
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Console.WriteLine(serialPort.BytesToRead);
            byte[] data = new byte[serialPort.BytesToRead];

            serialPort.Read(data, 0, data.Length);

            for (int i = 0; i < data.Length; i++)
            {
                process_serial(data[i]);

            }

        }

        private void process_serial(byte data)
        {
            if (serial_state == 0) // idle
            {
                //Console.WriteLine(data);
                if ((data & 0xF0) == 0x40) // start of streamming
                {
                    serial_state = 1;
                    serial_buffer_data = 0;
                }
                if ((data & 0xF0) == 0x50) // start of receive capture signal
                {
                    serial_state = 20;
                }
                if ((data & 0xF0) == 0x60) // start of receive FFT signal
                {
                    serial_state = 30;
                }
                if ((data & 0xF0) == 0x70) // start of device message
                {
                    serial_state = 40;
                }
            }
            else if (serial_state >= 1 && serial_state <= 4) // receiving streamming data
            {
                serial_buffer_data |= (UInt32)data << ((4 - serial_state) * 8);
                if (serial_state == 4) // convert data
                {
                    serial_buffer_data_float = BitConverter.ToSingle(BitConverter.GetBytes(serial_buffer_data), 0);

                    if (record_enable)
                    {
                        record_data.Add(serial_buffer_data_float);
                    }

                    stream_buffer[stream_buffer_idx] = serial_buffer_data_float;
                    stream_buffer_idx++;
                    stream_buffer_idx %= stream_buffer_size;

                    serial_state = -1;
                }
                serial_state++;
            }
            else if (serial_state == 20) // receive capture signal size
            {
                receive_data_size = (int)Math.Pow(2, data);
                capture_recieved_size = receive_data_size;
                receive_data_counter = 0;
                serial_buffer_data = 0;
                serial_state = 21;
            }
            else if (serial_state == 21) // receiving capture signal
            {
                serial_buffer_data |= (UInt32)data << ((3 - (receive_data_counter % 4)) * 8);
                if (receive_data_counter % 4 == 3)
                {
                    if (receive_data_counter / 4 < capture_buffer.Length)
                    {
                        capture_buffer[receive_data_counter / 4] = BitConverter.ToSingle(BitConverter.GetBytes(serial_buffer_data), 0);
                    }
                    else
                    {
                        serial_state = 0; // error
                    }
                    serial_buffer_data = 0;
                }
                receive_data_counter++;
                if ((receive_data_counter / 4) >= receive_data_size) // finish receive capture signal (accroding to size)
                {
                    if (capture_recieved_size > 0)
                    {
                        capture_pic.Invalidate();
                        if (auto_export_data_enable == true)
                        {
                            bool save_require = true;
                            if (askBeforeSaveToolStripMenuItem.Checked == true)
                            {
                                DialogResult dialogResult = MessageBox.Show("Do you want to save this signal", "Save file confirm", MessageBoxButtons.YesNo);
                                if (dialogResult != DialogResult.Yes)
                                {
                                    save_require = false;
                                }
                            }

                            if (save_require == true)
                            {
                                string save_file_name = Path.GetDirectoryName(auto_export_save_dialog.FileName) + "\\" + Path.GetFileNameWithoutExtension(auto_export_save_dialog.FileName) + auto_export_counter.ToString("D5") + ".csv";
                                while (File.Exists(save_file_name) == true) // prevent same file name
                                {
                                    auto_export_counter++;
                                }
                                //StreamWriter outputFile = new StreamWriter(auto_export_save_dialog.FileName + auto_export_counter.ToString("D5") + ".csv");
                                StreamWriter outputFile = new StreamWriter(save_file_name);
                                for (int i = 0; i < capture_recieved_size; i++)
                                {
                                    outputFile.Write(capture_buffer[i]);
                                    if (i + 1 < capture_recieved_size)
                                    {
                                        outputFile.Write(",");
                                    }
                                }
                                outputFile.Close();

                                if (exportBT01BT02ToolStripMenuItem.Checked == true)
                                {
                                    string bt01_save_file_name = Path.GetDirectoryName(auto_export_save_dialog.FileName) + "\\" + Path.GetFileNameWithoutExtension(auto_export_save_dialog.FileName) + auto_export_counter.ToString("D5") + "_bt01.csv";
                                    StreamWriter bt01_outputFile = new StreamWriter(bt01_save_file_name);
                                    for (int i = 0; i < bt01_signal_size; i++)
                                    {
                                        bt01_outputFile.Write(FFT_buffer[i]);
                                        if (i + 1 < bt01_signal_size)
                                        {
                                            bt01_outputFile.Write(",");
                                        }
                                    }
                                    bt01_outputFile.Close();

                                    string bt02_save_file_name = Path.GetDirectoryName(auto_export_save_dialog.FileName) + "\\" + Path.GetFileNameWithoutExtension(auto_export_save_dialog.FileName) + auto_export_counter.ToString("D5") + "_bt02.csv";
                                    StreamWriter bt02_outputFile = new StreamWriter(bt02_save_file_name);
                                    for (int i = 0; i < bt02_signal_size; i++)
                                    {
                                        bt02_outputFile.Write(FFT_buffer[i] + bt01_signal_size);
                                        if (i + 1 < bt02_signal_size)
                                        {
                                            bt02_outputFile.Write(",");
                                        }
                                    }
                                    bt02_outputFile.Close();
                                }

                                system_message_box.Text = save_file_name + " saved!";
                                auto_export_counter++;


                            }
                        }
                    }
                    serial_state = 0;
                    /*
                    for (int i = 0; i < capture_recieved_size; i++)
                    {
                        Console.WriteLine(i.ToString() + ":" + capture_buffer[i]);
                    }*/
                    serial_buffer_data = 0;
                }
            }
            else if (serial_state == 30) // receive FFT signal size
            {
                receive_data_size = (int)Math.Pow(2, data);
                FFT_recieved_size = receive_data_size;
                receive_data_counter = 0;
                serial_buffer_data = 0;
                serial_state = 31;
            }
            else if (serial_state == 31) // receiving FFT signal
            {
                serial_buffer_data |= (UInt32)data << ((3 - (receive_data_counter % 4)) * 8);
                if (receive_data_counter % 4 == 3)
                {
                    if (receive_data_counter / 4 < FFT_buffer.Length)
                    {
                        FFT_buffer[receive_data_counter / 4] = BitConverter.ToSingle(BitConverter.GetBytes(serial_buffer_data), 0);
                    }
                    else
                    {
                        serial_state = 0; // error
                    }

                    serial_buffer_data = 0;
                }
                receive_data_counter++;
                if ((receive_data_counter / 4) >= receive_data_size) // finish receive FFT signal size
                {
                    if (FFT_recieved_size > 0)
                    {
                        FFT_pic.Invalidate();
                        if (auto_explain_data_enable == true)
                        {
                            Task.Run(() => inference_from_serial());
                        }

                    }
                    serial_state = 0;
                    /// test
                    /*
                    for (int i = 0; i < FFT_recieved_size; i++)
                    {
                        Console.WriteLine(i.ToString() + ":" + FFT_buffer[i]);
                    }
                    */
                    serial_buffer_data = 0;
                    get_new_fft_frame = true;
                    //TimerCallback(null);
                }
            }
            else if (serial_state == 40) // receive message size
            {
                receive_data_size = data;
                device_message_recieved_size = receive_data_size;
                receive_data_counter = 0;
                //device_message_buffer = "";
                serial_state = 41;
            }
            else if (serial_state == 41) // receiving message
            {

                device_message_buffer[receive_data_counter] = data;
                receive_data_counter++;
                if (receive_data_counter >= device_message_recieved_size) // finished receive message
                {
                    device_message_count++;
                    device_message_box.Text = device_message_count.ToString() + " : " + System.Text.Encoding.ASCII.GetString(device_message_buffer, 0, receive_data_counter);

                    serial_state = 0;
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serial_port_name_box.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                serial_port_name_box.Items.Add(port);
            }

            serial_port_baud_box.SelectedIndex = 3;

            stream_draw_timer.Start();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort.BaudRate = int.Parse(serial_port_baud_box.Items[serial_port_baud_box.SelectedIndex].ToString());
            serialPort.PortName = (string)serial_port_name_box.SelectedItem;
            serialPort.Open();
        }

        private void streaming_pic_Paint(object sender, PaintEventArgs e)
        {

            if (clear_streamming_flag == true)
            {
                e.Graphics.Clear(Color.Black);
                clear_streamming_flag = false;
            }


            Pen time_line_pen = new Pen(Brushes.Green, 1.0F);
            Pen base_line_pen = new Pen(Brushes.DarkGreen, 1.0F);

            float base_line_y = streaming_pic.Height / 2;
            float line_y_draw_multipiler = ((float)base_line_y) / (float)streamming_max_box.Value;

            e.Graphics.DrawLine(base_line_pen, 0, base_line_y, streaming_pic.Width, base_line_y);

            if (stream_buffer_idx >= 0)
            {
                e.Graphics.DrawLine(time_line_pen, stream_buffer_idx, 0, stream_buffer_idx, streaming_pic.Height);
            }
            /*
            for (int i=0;i< stream_buffer_size; i++)
            {

                float draw_y = base_line_y + (stream_buffer[i] * line_y_draw_multipiler);
                if (draw_y > 0 && draw_y < streaming_pic.Height)
                {
                    e.Graphics.FillEllipse(Brushes.Orange, i, draw_y, 2, 2);
                }
            }
            */

            /*
            for (int i = 1; i < capture_recieved_size; i++)
            {
                float draw_y = base_line_y + (capture_buffer[i] * line_y_draw_multipiler);
                if (i > 0)
                {
                    e.Graphics.FillEllipse(Brushes.LightBlue, i, draw_y, 2, 2);
                }
                
            }
            */

            float last_draw_y = 0;
            for (int i = 0; i < open_size; i++)
            {
                float draw_y = base_line_y + (stream_buffer[i] * line_y_draw_multipiler);
                if (i > 0)
                {
                    if (draw_y > 0 && draw_y < streaming_pic.Height && last_draw_y > 0 && last_draw_y < streaming_pic.Height)
                    {
                        //e.Graphics.FillEllipse(Brushes.Orange, i, draw_y, 2, 2);
                        e.Graphics.DrawLine(Pens.Orange, i - 1, last_draw_y, i, draw_y);
                    }
                }
                last_draw_y = draw_y;
            }


        }

        private void stream_draw_timer_Tick(object sender, EventArgs e)
        {
            streaming_pic.Invalidate();
        }

        private void capture_pic_Paint(object sender, PaintEventArgs e)
        {
            Pen base_line_pen = new Pen(Brushes.DarkGreen, 1.0F);
            float max = 0, min = 0, bound;

            for (int i = 0; i < capture_recieved_size; i++)
            {
                if (capture_buffer[i] > 0 && capture_buffer[i] > max)
                {
                    max = capture_buffer[i];
                }
                if (capture_buffer[i] < 0 && capture_buffer[i] < min)
                {
                    min = capture_buffer[i];
                }
            }
            bound = Math.Abs(max);
            if (Math.Abs(min) > bound)
            {
                bound = Math.Abs(min);
            }
            bound = bound * 1.2F;
            /*
            if(bound > 1024) // prevent overflow
            {
                bound = 1024;
            }*/

            float base_line_y = capture_pic.Height / 2;
            float line_y_draw_multipiler = ((float)base_line_y) / bound;

            e.Graphics.DrawLine(base_line_pen, 0, base_line_y, capture_pic.Width, base_line_y);



            /*
               for (int i = 0; i < capture_recieved_size; i++)
               {
                   float draw_y = base_line_y + (capture_buffer[i] * line_y_draw_multipiler);
                   e.Graphics.FillEllipse(Brushes.LightBlue, i, draw_y, 2, 2);
               }
            */
            float last_draw_y = 0;
            for (int i = 0; i < capture_recieved_size; i++)
            {
                float draw_y = base_line_y + (capture_buffer[i] * line_y_draw_multipiler);
                if (i > 0)
                {
                    if (draw_y > 500)
                    {
                        draw_y = 500;
                    }
                    if (draw_y < -500)
                    {
                        draw_y = -500;
                    }
                    //e.Graphics.FillEllipse(Brushes.LightBlue, i, draw_y, 2, 2);
                    e.Graphics.DrawLine(Pens.Cyan, i - 1, last_draw_y, i, draw_y);
                }
                last_draw_y = draw_y;
            }

            e.Graphics.DrawString("min =" + min.ToString("F4"), DefaultFont, Brushes.LightGreen, 10, capture_pic.Height - 30);
            e.Graphics.DrawString("max =" + max.ToString("F4"), DefaultFont, Brushes.LightGreen, 10, 10);

            if (capture_line_idx >= 0)
            {
                e.Graphics.DrawLine(Pens.Magenta, capture_line_idx, 0, capture_line_idx, capture_pic.Height);
            }
            if (capture_line2_idx >= 0)
            {
                e.Graphics.DrawLine(Pens.Red, capture_line2_idx, 0, capture_line2_idx, capture_pic.Height);
            }
        }

        private void FFT_pic_Paint(object sender, PaintEventArgs e)
        {
            if (clear_FFT_flag == true)
            {
                e.Graphics.Clear(Color.Black);
                clear_FFT_flag = false;
            }

            Pen base_line_pen = new Pen(Brushes.Gray, 1.0F);
            float max = 0;
            int max_idx = 0;

            for (int i = 2; i < FFT_recieved_size; i++)
            {
                if (FFT_buffer[i] > 0 && FFT_buffer[i] > max)
                {
                    max = FFT_buffer[i];
                    max_idx = i;
                }
            }

            if (max < 0.000001) // prevent overflow
            {
                max = 0.000001F;
            }

            if (fFTLogScaleToolStripMenuItem.Checked == true)
            {
                max = (float)Math.Log(max);
            }

            float base_line_y = FFT_pic.Height - 10;
            float line_y_draw_multipiler = ((float)base_line_y) / max;


            e.Graphics.DrawLine(base_line_pen, 0, base_line_y, FFT_pic.Width, base_line_y);

            if (FFT_style == 0) // bar chart (default)
            {
                for (int i = 2; i < open_size / 2; i++)
                {
                    float draw_y = base_line_y - (FFT_buffer[i] * line_y_draw_multipiler);
                    if (fFTLogScaleToolStripMenuItem.Checked == true)
                    {
                        draw_y = base_line_y - ((float)Math.Log(FFT_buffer[i]) * line_y_draw_multipiler);
                    }

                    if (draw_y > 1080)
                    {
                        draw_y = 1080;
                    }
                    if (draw_y < -1080)
                    {
                        draw_y = -1080;
                    }
                    //e.Graphics.FillEllipse(Brushes.LightBlue, i, draw_y, 2, 2);
                    e.Graphics.DrawLine(Pens.LightBlue, i, draw_y, i, FFT_pic.Height);
                }

                if (markIndex129BT01BT02ToolStripMenuItem.Checked == true)
                {
                    e.Graphics.DrawLine(Pens.LightBlue, 129, 0, 129, FFT_pic.Height);
                }
            }
            else // graph chart
            {
                float last_draw_y = base_line_y - (FFT_buffer[0] * line_y_draw_multipiler);
                if (fFTLogScaleToolStripMenuItem.Checked == true)
                {
                    last_draw_y = base_line_y - ((float)Math.Log(FFT_buffer[0]) * line_y_draw_multipiler);
                }

                for (int i = 1; i < open_size / 2; i++)
                {
                    float draw_y = base_line_y - (FFT_buffer[i] * line_y_draw_multipiler);
                    if (fFTLogScaleToolStripMenuItem.Checked == true)
                    {
                        draw_y = base_line_y - ((float)Math.Log(FFT_buffer[i]) * line_y_draw_multipiler);
                    }

                    if (draw_y > 1080)
                    {
                        draw_y = 1080;
                    }
                    if (draw_y < -1080)
                    {
                        draw_y = -1080;
                    }
                    //e.Graphics.FillEllipse(Brushes.LightBlue, i, draw_y, 2, 2);
                    e.Graphics.DrawLine(Pens.LightBlue, i * 2, draw_y, (i - 1) * 2, last_draw_y);

                    last_draw_y = draw_y;
                }

                if (markIndex129BT01BT02ToolStripMenuItem.Checked == true)
                {
                    e.Graphics.DrawLine(Pens.LightBlue, 129 * 2, 0, 129 * 2, FFT_pic.Height);
                }
            }

            e.Graphics.DrawString("max =" + max.ToString("F4") + "idx -> [" + max_idx + "]", DefaultFont, Brushes.Pink, 10, 10);

            if (cursor_mode == true) // draw cursor
            {
                var right_alignment = new StringFormat() { Alignment = StringAlignment.Far };
                e.Graphics.DrawString("(x -> " + cursor_x.ToString() + ",y -> " + cursor_y.ToString() + ")", DefaultFont, Brushes.Pink, FFT_pic.Width - 50, 12, right_alignment);
                e.Graphics.DrawString("(Frequency -> " + cursor_x.ToString() + " Hz)", DefaultFont, Brushes.Pink, FFT_pic.Width - 50, 24, right_alignment);
                if (cursor_x <= (open_size / 2))
                {
                    e.Graphics.DrawString("(Magnitude -> " + FFT_buffer[cursor_x] + ")", DefaultFont, Brushes.Pink, FFT_pic.Width - 50, 36, right_alignment);
                }

                e.Graphics.DrawLine(Pens.LightGreen, cursor_x - 10, cursor_y, cursor_x + 10, cursor_y);
                e.Graphics.DrawLine(Pens.LightGreen, cursor_x, cursor_y - 10, cursor_x, cursor_y + 10);
            }
        }

        float[] mqtt_capture_buffer;
        float[] mqtt_FFT_buffer;
        string mqtt_FFT_message;
        string mqtt_capture_message;

        private async void TimerCallback(Object o)
        {
            // Display the date/time when this method got called.
            Console.WriteLine("In TimerCallback: " + DateTime.Now);
            if (get_new_fft_frame == true)
            {
                mqtt_capture_buffer = new float[512];
                mqtt_FFT_buffer = new float[512];

                Array.Copy(FFT_buffer, mqtt_FFT_buffer, 512);
                mqtt_FFT_message = "[";
                for (int i = 0; i < 512; i++)
                {
                    mqtt_FFT_message += mqtt_FFT_buffer[i].ToString("0.0");
                    if (i < 511)
                    {
                        mqtt_FFT_message += ",";
                    }
                }
                mqtt_FFT_message += "]";

                Array.Copy(capture_buffer, mqtt_capture_buffer, 512);
                mqtt_capture_message = "[";
                for (int i = 0; i < 512; i++)
                {
                    mqtt_capture_message += mqtt_capture_buffer[i].ToString("0.0");
                    if (i < 511)
                    {
                        mqtt_capture_message += ",";
                    }
                }
                mqtt_capture_message += "]";

                get_new_fft_frame = false;
                await publish();

            }
        }

        public async Task publish()
        {
            /*
             * This sample pushes a simple application message including a topic and a payload.
             *
             * Always use builders where they exist. Builders (in this project) are designed to be
             * backward compatible. Creating an _MqttApplicationMessage_ via its constructor is also
             * supported but the class might change often in future releases where the builder does not
             * or at least provides backward compatibility where possible.
             */

            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer("iot.nipa.cloud")
                    .WithClientId("FFT_32")
                    .WithCredentials("FFT_32", "")
                    .Build();

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);



                var values = new BlueOakTelemetryData
                {
                    TData = mqtt_capture_message,
                    FData = mqtt_FFT_message
                };


                var telemetryData = new ThingsBoardTelemetryData
                {
                    Ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    Values = values,
                };

                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic("v1/devices/me/telemetry")
                    .WithPayload(JsonSerializer.Serialize(telemetryData))
                    .Build();

                await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

                await mqttClient.DisconnectAsync();

                Console.WriteLine("MQTT application message is published.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            record_data = new List<float>();


            button4.Enabled = false;
            button5.Enabled = true;
            record_enable = true;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            record_enable = false;

            saving_form formatter_form = new saving_form();
            DialogResult result = formatter_form.ShowDialog();
            if (result == DialogResult.OK)
            {
                saveFileDialog1.FileName = formatter_form.file_name;
            }
            else
            {
                saveFileDialog1.FileName = "datapoint";
            }

            result = saveFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, true, Encoding.ASCII);

            for (int i = 0; i < record_data.Count; i++)
            {
                //Console.WriteLine(record_data[i]);
                sw.WriteLine(record_data[i].ToString());
            }
            int data_cnt = record_data.Count;
            record_data.Clear();
            sw.Close();
            MessageBox.Show("export " + data_cnt + " data point completed!");

            button4.Enabled = true;
            button5.Enabled = false;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            record_data = new List<float>();

            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            StreamReader sr = new StreamReader(openFileDialog1.FileName);
            String line;
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the line to console window
                Console.WriteLine(line);
                float value;
                if (float.TryParse(line, out value))
                {
                    record_data.Add(value);
                }

                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
            MessageBox.Show("import " + record_data.Count.ToString() + " data point completed!");
            stream_draw_timer.Enabled = false;

            track_bar_num.Value = 0;
            if (record_data.Count > open_size)
            {
                track_bar_num.Maximum = record_data.Count - open_size;
            }
            else
            {
                track_bar_num.Maximum = 0;
            }

            sampling_selected_max_text.Text = "max(" + (record_data.Count - open_size).ToString() + ")";
            button7.Enabled = true;
            //button8.Enabled = true;
            trackBar1.Enabled = true;
            track_bar_num.Enabled = true;
            num_bar_draw = true;

            trackBar1.Value = 0;
            move_track_bar(0, open_size);
            streaming_pic.Invalidate();

            capture_recieved_size = stream_buffer_size;
            float plot_multipiler = (float)record_data.Count / (float)stream_buffer_size;
            for (int i = 0; i < stream_buffer_size; i++)
            {
                int idx = (int)(plot_multipiler * i);
                if (idx < record_data.Count)
                {
                    capture_buffer[i] = record_data[idx];
                }
                else
                {
                    capture_buffer[i] = 0;
                }
            }
            capture_pic.Invalidate();

        }

        void move_track_bar(int pos, int size)
        {
            int start_idx;
            if (record_data.Count < size)
            {
                start_idx = 0;
            }
            else
            {
                start_idx = (int)((record_data.Count - size) * ((float)pos / trackBar1.Maximum));
            }
            //load file to data capture
            for (int i = 0; i < size; i++)
            {
                if (+i < record_data.Count)
                {
                    stream_buffer[i] = record_data[start_idx + i];
                }
                else
                {
                    stream_buffer[i] = 0;
                }
            }

            num_bar_draw = false;
            track_bar_num.Value = start_idx;
            num_bar_draw = true;

            stream_buffer_idx = -1; // disable time line in streamming view
            capture_line_idx = (int)((float)start_idx / (float)record_data.Count * (float)stream_buffer_size);
            capture_line2_idx = (int)((float)(start_idx + size) / (float)record_data.Count * (float)stream_buffer_size);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            move_track_bar(trackBar1.Value, open_size);

            streaming_pic.Invalidate();
            capture_pic.Invalidate();
        }

        private void streamming_max_box_ValueChanged(object sender, EventArgs e)
        {
            streaming_pic.Invalidate();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //stream_buffer;

            Complex[] buffer = new Complex[open_size];
            for (int i = 0; i < open_size; i++)
            {
                buffer[i] = stream_buffer[i];
            }
            Fourier.Forward(buffer, FourierOptions.Default);

            for (int i = 0; i < open_size; i++)
            {
                FFT_buffer[i] = (float)buffer[i].Magnitude;
            }
            FFT_recieved_size = open_size;

            cursor_mode = true;
            //button9.Enabled = true;
            FFT_pic.Invalidate();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            open_size = 1024;
            move_track_bar(trackBar1.Value, open_size);
            if (record_data.Count > open_size)
            {
                track_bar_num.Maximum = record_data.Count - open_size;
            }
            else
            {
                track_bar_num.Maximum = 0;
            }

            sampling_selected_max_text.Text = "max(" + (record_data.Count - open_size).ToString() + ")";

            clear_streamming_flag = false;
            clear_FFT_flag = false;
            streaming_pic.Invalidate();
            capture_pic.Invalidate();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            open_size = 512;
            move_track_bar(trackBar1.Value, open_size);
            if (record_data.Count > open_size)
            {
                track_bar_num.Maximum = record_data.Count - open_size;
            }
            else
            {
                track_bar_num.Maximum = 0;
            }

            sampling_selected_max_text.Text = "max(" + (record_data.Count - open_size).ToString() + ")";

            clear_streamming_flag = false;
            clear_FFT_flag = false;
            streaming_pic.Invalidate();
            capture_pic.Invalidate();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            open_size = 256;
            move_track_bar(trackBar1.Value, open_size);
            clear_streamming_flag = false;
            clear_FFT_flag = false;
            streaming_pic.Invalidate();
            capture_pic.Invalidate();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            open_size = 128;
            move_track_bar(trackBar1.Value, open_size);
            if (record_data.Count > open_size)
            {
                track_bar_num.Maximum = record_data.Count - open_size;
            }
            else
            {
                track_bar_num.Maximum = 0;
            }
            sampling_selected_max_text.Text = "max(" + (record_data.Count - open_size).ToString() + ")";

            clear_streamming_flag = false;
            clear_FFT_flag = false;
            streaming_pic.Invalidate();
            capture_pic.Invalidate();
        }

        private void FFT_pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (cursor_mode == true)
            {
                cursor_x = e.X;
                cursor_y = e.Y;
                FFT_pic.Invalidate();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog2.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            StreamWriter sw = new StreamWriter(saveFileDialog2.FileName, true, Encoding.ASCII);


            for (int i = 0; i < open_size; i++)
            {
                sw.WriteLine(stream_buffer[i].ToString());
            }
            int data_cnt = open_size;
            sw.Close();
            MessageBox.Show("export " + data_cnt + " data point completed!");
        }

        private void button9_Click(object sender, EventArgs e)
        {

            DialogResult result = saveFileDialog3.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            StreamWriter sw = new StreamWriter(saveFileDialog3.FileName, true, Encoding.ASCII);


            for (int i = 0; i < open_size; i++)
            {
                sw.WriteLine(FFT_buffer[i].ToString());
            }
            int data_cnt = open_size;
            sw.Close();
            MessageBox.Show("export " + data_cnt + " data point completed!");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            record_data = new List<float>();

            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            StreamReader sr = new StreamReader(openFileDialog1.FileName);
            String line;
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the line to console window
                Console.WriteLine(line);
                float value;
                if (float.TryParse(line, out value))
                {
                    record_data.Add(value);
                }

                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
            MessageBox.Show("import " + record_data.Count.ToString() + " FFT data point completed!");

            for (int i = 0; i < record_data.Count; i++)
            {
                FFT_buffer[i] = record_data[i];
            }
            FFT_recieved_size = open_size;

            cursor_mode = true;
            //button9.Enabled = true;
            FFT_pic.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void track_bar_num_ValueChanged(object sender, EventArgs e)
        {
            if (num_bar_draw == true)
            {
                int start_idx = (int)track_bar_num.Value;

                //load file to data capture
                for (int i = 0; i < open_size; i++)
                {
                    if (start_idx + i < record_data.Count)
                    {
                        stream_buffer[i] = record_data[start_idx + i];
                    }
                    else
                    {
                        stream_buffer[i] = 0;
                    }
                }

                stream_buffer_idx = -1; // disable time line in streamming view
                capture_line_idx = (int)((float)start_idx / (float)record_data.Count * (float)stream_buffer_size);
                capture_line2_idx = (int)((float)(start_idx + open_size) / (float)record_data.Count * (float)stream_buffer_size);

                streaming_pic.Invalidate();
                capture_pic.Invalidate();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (auto_export_data_enable == false)
            {
                if (auto_export_save_dialog.ShowDialog() == DialogResult.OK)
                {
                    prefix_filename_auto_export = auto_export_save_dialog.FileName;

                    auto_export_counter = 0;
                    auto_export_data_enable = true;
                    auto_export_but.BackColor = Color.LightGreen;
                }
            }
            else
            {

                auto_export_data_enable = false;
                auto_export_but.BackColor = SystemColors.Control;
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (FFT_style == 0)
            {
                FFT_style = 1;
                switchFFTStylebarToolStripMenuItem.Text = "switch FFT style (graph)";
            }
            else
            {
                FFT_style = 0;
                switchFFTStylebarToolStripMenuItem.Text = "switch FFT style (bar)";
            }

            FFT_pic.Invalidate();
        }

        private void fft_log_scale_but_CheckedChanged(object sender, EventArgs e)
        {
            FFT_pic.Invalidate();
        }

        private void mark_index_129_but_CheckedChanged(object sender, EventArgs e)
        {
            FFT_pic.Invalidate();
        }

        private void openDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button6_Click(null, null);
        }

        private void openFFTFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button10_Click(null, null);
        }

        private void cropDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button8_Click(null, null);
        }

        private void saveFFTFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button9_Click(null, null);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            radioButton1_CheckedChanged(null, null);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            radioButton2_CheckedChanged(null, null);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            radioButton3_CheckedChanged(null, null);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            radioButton4_CheckedChanged(null, null);
        }

        private void fFTLogScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fFTLogScaleToolStripMenuItem.Checked = !fFTLogScaleToolStripMenuItem.Checked;
            FFT_pic.Invalidate();

        }

        private void markIndex129BT01BT02ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            markIndex129BT01BT02ToolStripMenuItem.Checked = !markIndex129BT01BT02ToolStripMenuItem.Checked;
            FFT_pic.Invalidate();
        }

        private void markIndex257BT030BT04ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            markIndex257BT030BT04ToolStripMenuItem.Checked = !markIndex257BT030BT04ToolStripMenuItem.Checked;
            FFT_pic.Invalidate();
        }

        private void switchFFTStylebarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FFT_style == 0)
            {
                FFT_style = 1;
                switchFFTStylebarToolStripMenuItem.Text = "switch FFT style (graph)";
            }
            else
            {
                FFT_style = 0;
                switchFFTStylebarToolStripMenuItem.Text = "switch FFT style (bar)";
            }

            FFT_pic.Invalidate();
        }

        private void SetaiMessage(string message)
        {
            ai_message_box.Text = message;
        }

        private void inferenceUsingBTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] inputData = new double[258];

            if (csv_opener.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] lines = File.ReadAllLines(csv_opener.FileName);

            if (lines.Length > 0)
            {
                string firstLine = lines[0];  // take the first row (or loop if you want batch inputs)
                inputData = firstLine
                    .Split(',')                                // split by comma
                    .Select(s => double.Parse(s, CultureInfo.InvariantCulture))  // convert to float
                    .ToArray();
            }

            double[] out_prob = new double[] { 0, 0 };

            out_prob[0] = Predict(
                inputData,
                Bio_ModelWeights.ScalerMean,
                Bio_ModelWeights.ScalerScale,
                Bio_ModelWeights.W0,
                Bio_ModelWeights.B0,
                Bio_ModelWeights.W1,
                Bio_ModelWeights.B1,
                Bio_ModelWeights.W2,
                Bio_ModelWeights.B2
            );

            out_prob[1] = 1.0f - out_prob[0];

            SetaiMessage("bio Inference result: [" + out_prob[1].ToString() + "," + out_prob[0].ToString() + "]");

            // show to FFT view
            for (int i = 0; i < 258; i++)
            {
                FFT_buffer[i] = (float)inputData[i];
            }
            open_size = 1024;
            FFT_recieved_size = 1024;
            FFT_pic.Invalidate();

            // calculate explaination
            //if (explainview.IsDisposed == false)
            //{
            //    explainview.set_bio_local_explain(
            //        LocalFeatureContribution(
            //        inputData,
            //        Bio_ModelWeights.ScalerMean,
            //        Bio_ModelWeights.ScalerScale,
            //        Bio_ModelWeights.W0,
            //        Bio_ModelWeights.B0,
            //        Bio_ModelWeights.W1,
            //        Bio_ModelWeights.B1,
            //        Bio_ModelWeights.W2,
            //        Bio_ModelWeights.B2
            //         )
            //    );
            //}
        }

        private void inferenceUsingBTFallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double[] inputData = new double[257];

            if (csv_opener.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] lines = File.ReadAllLines(csv_opener.FileName);

            if (lines.Length > 0)
            {
                string firstLine = lines[0];  // take the first row (or loop if you want batch inputs)
                inputData = firstLine
                    .Split(',')                                // split by comma
                    .Select(s => double.Parse(s, CultureInfo.InvariantCulture))  // convert to float
                    .ToArray();
            }

            double[] selected_input = new double[123];
            Array.Copy(inputData, 0, selected_input, 0, 20);
            Array.Copy(inputData, 155, selected_input, 20, 103);

            double[] out_prob = new double[] { 0, 0 };

            out_prob[0] = Predict(
                selected_input,
                Fall_ModelWeights.ScalerMean,
                Fall_ModelWeights.ScalerScale,
                Fall_ModelWeights.W0,
                Fall_ModelWeights.B0,
                Fall_ModelWeights.W1,
                Fall_ModelWeights.B1,
                Fall_ModelWeights.W2,
                Fall_ModelWeights.B2
            );

            out_prob[1] = 1.0f - out_prob[0];

            ai_message_box.Text = "fall Inference result: [" + out_prob[1].ToString() + "," + out_prob[0].ToString() + "]";

            // show to FFT view
            for (int i = 0; i < 257; i++)
            {
                FFT_buffer[257 + i] = (float)inputData[i];
            }
            open_size = 1024;
            FFT_recieved_size = 1024;
            FFT_pic.Invalidate();

            // calculate explaination
            //if (explainview.IsDisposed == false)
            //{
            //    explainview.set_fall_local_explain(
            //        LocalFeatureContribution(
            //        selected_input,
            //        Fall_ModelWeights.ScalerMean,
            //        Fall_ModelWeights.ScalerScale,
            //        Fall_ModelWeights.W0,
            //        Fall_ModelWeights.B0,
            //        Fall_ModelWeights.W1,
            //        Fall_ModelWeights.B1,
            //        Fall_ModelWeights.W2,
            //        Fall_ModelWeights.B2
            //         )
            //    );
            //}
        }

        public static double Relu(double x)
        {
            return x > 0 ? x : 0;
        }



        public static double Predict(
            double[] input,
            double[] mean,
            double[] scale,
            double[,] Layer0_Weights,
            double[] Layer0_Biases,
            double[,] Layer1_Weights,
            double[] Layer1_Biases,
            double[,] Layer2_Weights,
            double[] Layer2_Biases)
        {

            int input_length = Layer0_Weights.GetLength(0);
            int l1_length = Layer0_Weights.GetLength(1);
            int l2_length = Layer1_Weights.GetLength(1);

            double[] x = new double[input_length];
            double[] h1 = new double[l1_length]; // 32
            double[] h2 = new double[l2_length]; // 16

            // Normalize input
            for (int i = 0; i < input_length; i++)
                x[i] = (input[i] - mean[i]) / scale[i];

            // Layer 0: Input -> 32
            for (int j = 0; j < h1.Length; j++)
            {
                double sum = Layer0_Biases[j];
                for (int i = 0; i < input_length; i++)
                    sum += x[i] * Layer0_Weights[i,j];
                h1[j] = Relu(sum);
            }

            // Layer 1: 32 -> 16
            for (int j = 0; j < h2.Length; j++)
            {
                double sum = Layer1_Biases[j];
                for (int i = 0; i < h1.Length; i++)
                    sum += h1[i] * Layer1_Weights[i,j];
                h2[j] = Relu(sum);
            }

            // Layer 2: 16 -> 1
            double output = Layer2_Biases[0];
            for (int i = 0; i < h2.Length; i++)
                output += h2[i] * Layer2_Weights[i,0];

            // Sigmoid for probability
            return Sigmoid(output);

        }

        public static double Predict(
            double[] input,
            double[,] Layer0_Weights,
            double[] Layer0_Biases,
            double[,] Layer1_Weights,
            double[] Layer1_Biases,
            double[,] Layer2_Weights,
            double[] Layer2_Biases)
        {

            int input_length = Layer0_Weights.GetLength(0);
            int l1_length = Layer0_Weights.GetLength(1);
            int l2_length = Layer1_Weights.GetLength(1);

            double[] x = new double[input_length];
            double[] h1 = new double[l1_length]; // 32
            double[] h2 = new double[l2_length]; // 16

            // Layer 0: Input -> 32
            for (int j = 0; j < h1.Length; j++)
            {
                double sum = Layer0_Biases[j];
                for (int i = 0; i < input_length; i++)
                    sum += x[i] * Layer0_Weights[i, j];
                h1[j] = Relu(sum);
            }

            // Layer 1: 32 -> 16
            for (int j = 0; j < h2.Length; j++)
            {
                double sum = Layer1_Biases[j];
                for (int i = 0; i < h1.Length; i++)
                    sum += h1[i] * Layer1_Weights[i, j];
                h2[j] = Relu(sum);
            }

            // Layer 2: 16 -> 1
            double output = Layer2_Biases[0];
            for (int i = 0; i < h2.Length; i++)
                output += h2[i] * Layer2_Weights[i, 0];

            // Sigmoid for probability
            return Sigmoid(output);

        }

        public static double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));

        public static double[] LocalFeatureContribution(
        double[] input,            // input vector, size n
        double[] mean , double[] scale,
        double[,] W1, double[] b1, // weights and bias of layer 1 (W1: [n,h])
        double[,] W2, double[] b2, // weights and bias of layer 2 (W2: [h,o])
        double[,] W3, double[] b3 // weights and bias of layer 2 (W2: [h,o])
        )
        {

            int inputSize = input.Length;
            int hiddenSize = b1.Length;
            int outputSize = b2.Length;

            double[] hiddenRaw = new double[hiddenSize];
            double[] hidden = new double[hiddenSize];
            double[] outputRaw = new double[outputSize];
            double[] output = new double[outputSize];

            // Input → Hidden (tanh)
            for (int j = 0; j < hiddenSize; j++)
            {
                hiddenRaw[j] = b1[j];
                for (int i = 0; i < inputSize; i++)
                {
                    hiddenRaw[j] += input[i] * W1[i, j];
                }
                hidden[j] = Math.Tanh(hiddenRaw[j]); // activation for hidden layer
            }

            // Hidden → Output (sigmoid)
            for (int k = 0; k < outputSize; k++)
            {
                outputRaw[k] = b2[k];
                for (int j = 0; j < hiddenSize; j++)
                {
                    outputRaw[k] += hidden[j] * W2[j, k];
                }
                output[k] = Sigmoid(outputRaw[k]); // activation for output layer
            }

            // Compute contribution from input -> hidden -> output, ignoring tanh derivative
            double[] contribution = new double[inputSize];
            for (int i = 0; i < inputSize; i++)
            {
                double sum = 0;
                for (int j = 0; j < hiddenSize; j++)
                {
                    sum += W1[i, j] * W2[j, 0] * hidden[j];  // Use activated value
                }
                contribution[i] = input[i] * sum;
            }

            return contribution;
        }

        private void aIExplainableViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (explainview.IsDisposed == true)
            {
                explainview = new aiexplainview();
            }
            explainview.Show();
        }

        private void askBeforeSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            askBeforeSaveToolStripMenuItem.Checked = !askBeforeSaveToolStripMenuItem.Checked;
        }

        private void exportBT01BT02ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportBT01BT02ToolStripMenuItem.Checked = !exportBT01BT02ToolStripMenuItem.Checked;
        }

        private void exportBT03BT04ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportBT03BT04ToolStripMenuItem.Checked = !exportBT03BT04ToolStripMenuItem.Checked;
        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            if (auto_explain_data_enable == false)
            {
                auto_explain_data_enable = true;
                auto_explain_but.BackColor = Color.LightGreen;
            }
            else
            {

                auto_explain_data_enable = false;
                auto_explain_but.BackColor = SystemColors.Control;
            }
        }

        public void inference_from_serial()
        {
            double[,] out_prob = new double[,] { { 0, 0 }, { 0, 0 } };
            bool got_bt04 = true;
            if (FFT_buffer[258] == 0 && FFT_buffer[259] == 0 && FFT_buffer[300] == 0)
            {
                got_bt04 = false;
            }

            double[] bio_input = new double[258];
            Array.Copy(FFT_buffer, 0, bio_input, 0, 258);

            out_prob[0, 0] = Predict(
                bio_input,
                Bio_ModelWeights.ScalerMean,
                Bio_ModelWeights.ScalerScale,
                Bio_ModelWeights.W0,
                Bio_ModelWeights.B0,
                Bio_ModelWeights.W1,
                Bio_ModelWeights.B1,
                Bio_ModelWeights.W2,
                Bio_ModelWeights.B2
            );
            out_prob[0, 1] = 1.0f - out_prob[0, 0];

            // calculate explaination
            if (explainview.IsDisposed == false)
            {
                explainview.set_bio_local_explain(
                    LocalFeatureContribution(
                    bio_input,
                    Bio_ModelWeights.ScalerMean,
                    Bio_ModelWeights.ScalerScale,
                    Bio_ModelWeights.W0,
                    Bio_ModelWeights.B0,
                    Bio_ModelWeights.W1,
                    Bio_ModelWeights.B1,
                    Bio_ModelWeights.W2,
                    Bio_ModelWeights.B2
                     )
                );
            }

            if (got_bt04 == false)
            {
                SetaiMessage("Bio result: [" + out_prob[0, 1].ToString("F5") + "," + out_prob[0, 0].ToString("F5") + "] , []");
                explainview.valid_local_fall = 0;
            }
            else
            {
                double[] fall_input = new double[257];
                Array.Copy(FFT_buffer, 258, fall_input, 0, 257);

                out_prob[1, 0] = Predict(
                    fall_input,
                    Fall_ModelWeights.ScalerMean,
                    Fall_ModelWeights.ScalerScale,
                    Fall_ModelWeights.W0,
                    Fall_ModelWeights.B0,
                    Fall_ModelWeights.W1,
                    Fall_ModelWeights.B1,
                    Fall_ModelWeights.W2,
                    Fall_ModelWeights.B2
                );
                out_prob[1, 1] = 1.0f - out_prob[1, 0];

                // calculate explaination
                if (explainview.IsDisposed == false)
                {
                    explainview.set_fall_local_explain(
                        LocalFeatureContribution(
                        fall_input,
                        Fall_ModelWeights.ScalerMean,
                        Fall_ModelWeights.ScalerScale,
                        Fall_ModelWeights.W0,
                        Fall_ModelWeights.B0,
                        Fall_ModelWeights.W1,
                        Fall_ModelWeights.B1,
                        Fall_ModelWeights.W2,
                        Fall_ModelWeights.B2
                         )
                    );
                }

                SetaiMessage("Bio result: [" + out_prob[0, 1].ToString("F5") + "," + out_prob[0, 0].ToString("F5") + "] , Fall result: [" + out_prob[1, 1].ToString("F5") + "," + out_prob[1, 0].ToString("F5") + "]");

            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                    "Serial FFT Plotter v1.1\nCreated by Somsin Tongkrairat",
                    "About Serial FFT Plotter",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
            );
        }
    }
}
