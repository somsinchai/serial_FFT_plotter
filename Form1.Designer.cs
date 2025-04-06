
namespace serial_FFT_plotter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.capture_pic = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.streaming_pic = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.FFT_pic = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.export_bt01_bt02_but = new System.Windows.Forms.CheckBox();
            this.mark_index_129_but = new System.Windows.Forms.CheckBox();
            this.fft_log_scale_but = new System.Windows.Forms.CheckBox();
            this.ask_before_save_but = new System.Windows.Forms.CheckBox();
            this.fft_style_but = new System.Windows.Forms.Button();
            this.auto_export_but = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.serial_port_baud_box = new System.Windows.Forms.ComboBox();
            this.sampling_selected_max_text = new System.Windows.Forms.Label();
            this.track_bar_num = new System.Windows.Forms.NumericUpDown();
            this.button10 = new System.Windows.Forms.Button();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.streamming_max_box = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.serial_port_name_box = new System.Windows.Forms.ComboBox();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.stream_draw_timer = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog3 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.device_message_box = new System.Windows.Forms.ToolStripStatusLabel();
            this.system_message_box = new System.Windows.Forms.ToolStripStatusLabel();
            this.auto_export_save_dialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.capture_pic)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.streaming_pic)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FFT_pic)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_bar_num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.streamming_max_box)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.capture_pic);
            this.groupBox1.Location = new System.Drawing.Point(12, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1072, 213);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Captured Signal";
            // 
            // capture_pic
            // 
            this.capture_pic.BackColor = System.Drawing.Color.Black;
            this.capture_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capture_pic.Location = new System.Drawing.Point(3, 16);
            this.capture_pic.Name = "capture_pic";
            this.capture_pic.Size = new System.Drawing.Size(1066, 194);
            this.capture_pic.TabIndex = 0;
            this.capture_pic.TabStop = false;
            this.capture_pic.Paint += new System.Windows.Forms.PaintEventHandler(this.capture_pic_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.streaming_pic);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1072, 166);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Streaming Signal";
            // 
            // streaming_pic
            // 
            this.streaming_pic.BackColor = System.Drawing.Color.Black;
            this.streaming_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.streaming_pic.Location = new System.Drawing.Point(3, 16);
            this.streaming_pic.Name = "streaming_pic";
            this.streaming_pic.Size = new System.Drawing.Size(1066, 147);
            this.streaming_pic.TabIndex = 1;
            this.streaming_pic.TabStop = false;
            this.streaming_pic.Paint += new System.Windows.Forms.PaintEventHandler(this.streaming_pic_Paint);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.FFT_pic);
            this.groupBox3.Location = new System.Drawing.Point(9, 403);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1072, 264);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FFT";
            // 
            // FFT_pic
            // 
            this.FFT_pic.BackColor = System.Drawing.Color.Black;
            this.FFT_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FFT_pic.Location = new System.Drawing.Point(3, 16);
            this.FFT_pic.Name = "FFT_pic";
            this.FFT_pic.Size = new System.Drawing.Size(1066, 245);
            this.FFT_pic.TabIndex = 1;
            this.FFT_pic.TabStop = false;
            this.FFT_pic.Paint += new System.Windows.Forms.PaintEventHandler(this.FFT_pic_Paint);
            this.FFT_pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FFT_pic_MouseMove);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.export_bt01_bt02_but);
            this.groupBox4.Controls.Add(this.mark_index_129_but);
            this.groupBox4.Controls.Add(this.fft_log_scale_but);
            this.groupBox4.Controls.Add(this.ask_before_save_but);
            this.groupBox4.Controls.Add(this.fft_style_but);
            this.groupBox4.Controls.Add(this.auto_export_but);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.serial_port_baud_box);
            this.groupBox4.Controls.Add(this.sampling_selected_max_text);
            this.groupBox4.Controls.Add(this.track_bar_num);
            this.groupBox4.Controls.Add(this.button10);
            this.groupBox4.Controls.Add(this.radioButton4);
            this.groupBox4.Controls.Add(this.radioButton3);
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.button9);
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Controls.Add(this.trackBar1);
            this.groupBox4.Controls.Add(this.button6);
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.streamming_max_box);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.serial_port_name_box);
            this.groupBox4.Location = new System.Drawing.Point(12, 673);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1072, 116);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Setting";
            // 
            // export_bt01_bt02_but
            // 
            this.export_bt01_bt02_but.AutoSize = true;
            this.export_bt01_bt02_but.Location = new System.Drawing.Point(957, 70);
            this.export_bt01_bt02_but.Name = "export_bt01_bt02_but";
            this.export_bt01_bt02_but.Size = new System.Drawing.Size(113, 17);
            this.export_bt01_bt02_but.TabIndex = 32;
            this.export_bt01_bt02_but.Text = "export BT01 BT02";
            this.export_bt01_bt02_but.UseVisualStyleBackColor = true;
            // 
            // mark_index_129_but
            // 
            this.mark_index_129_but.AutoSize = true;
            this.mark_index_129_but.Location = new System.Drawing.Point(494, 74);
            this.mark_index_129_but.Name = "mark_index_129_but";
            this.mark_index_129_but.Size = new System.Drawing.Size(98, 17);
            this.mark_index_129_but.TabIndex = 31;
            this.mark_index_129_but.Text = "mark index 129";
            this.mark_index_129_but.UseVisualStyleBackColor = true;
            this.mark_index_129_but.CheckedChanged += new System.EventHandler(this.mark_index_129_but_CheckedChanged);
            // 
            // fft_log_scale_but
            // 
            this.fft_log_scale_but.AutoSize = true;
            this.fft_log_scale_but.Location = new System.Drawing.Point(494, 97);
            this.fft_log_scale_but.Name = "fft_log_scale_but";
            this.fft_log_scale_but.Size = new System.Drawing.Size(90, 17);
            this.fft_log_scale_but.TabIndex = 30;
            this.fft_log_scale_but.Text = "FFT log scale";
            this.fft_log_scale_but.UseVisualStyleBackColor = true;
            this.fft_log_scale_but.CheckedChanged += new System.EventHandler(this.fft_log_scale_but_CheckedChanged);
            // 
            // ask_before_save_but
            // 
            this.ask_before_save_but.AutoSize = true;
            this.ask_before_save_but.Location = new System.Drawing.Point(957, 91);
            this.ask_before_save_but.Name = "ask_before_save_but";
            this.ask_before_save_but.Size = new System.Drawing.Size(102, 17);
            this.ask_before_save_but.TabIndex = 29;
            this.ask_before_save_but.Text = "ask before save";
            this.ask_before_save_but.UseVisualStyleBackColor = true;
            // 
            // fft_style_but
            // 
            this.fft_style_but.Location = new System.Drawing.Point(645, 87);
            this.fft_style_but.Name = "fft_style_but";
            this.fft_style_but.Size = new System.Drawing.Size(144, 23);
            this.fft_style_but.TabIndex = 28;
            this.fft_style_but.Text = "switch FFT style (bar)";
            this.fft_style_but.UseVisualStyleBackColor = true;
            this.fft_style_but.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // auto_export_but
            // 
            this.auto_export_but.Location = new System.Drawing.Point(795, 87);
            this.auto_export_but.Name = "auto_export_but";
            this.auto_export_but.Size = new System.Drawing.Size(156, 23);
            this.auto_export_but.TabIndex = 25;
            this.auto_export_but.Text = "Auto export capture data";
            this.auto_export_but.UseVisualStyleBackColor = true;
            this.auto_export_but.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "BAUD :";
            // 
            // serial_port_baud_box
            // 
            this.serial_port_baud_box.FormattingEnabled = true;
            this.serial_port_baud_box.Items.AddRange(new object[] {
            "9600",
            "57600",
            "115200",
            "460800",
            "921600",
            "1000000"});
            this.serial_port_baud_box.Location = new System.Drawing.Point(92, 21);
            this.serial_port_baud_box.Name = "serial_port_baud_box";
            this.serial_port_baud_box.Size = new System.Drawing.Size(85, 21);
            this.serial_port_baud_box.TabIndex = 23;
            // 
            // sampling_selected_max_text
            // 
            this.sampling_selected_max_text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sampling_selected_max_text.AutoSize = true;
            this.sampling_selected_max_text.Location = new System.Drawing.Point(898, 60);
            this.sampling_selected_max_text.Name = "sampling_selected_max_text";
            this.sampling_selected_max_text.Size = new System.Drawing.Size(38, 13);
            this.sampling_selected_max_text.TabIndex = 22;
            this.sampling_selected_max_text.Text = "max(0)";
            // 
            // track_bar_num
            // 
            this.track_bar_num.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.track_bar_num.Enabled = false;
            this.track_bar_num.Location = new System.Drawing.Point(732, 57);
            this.track_bar_num.Name = "track_bar_num";
            this.track_bar_num.Size = new System.Drawing.Size(156, 20);
            this.track_bar_num.TabIndex = 21;
            this.track_bar_num.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.track_bar_num.ValueChanged += new System.EventHandler(this.track_bar_num_ValueChanged);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(337, 87);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(69, 23);
            this.button10.TabIndex = 20;
            this.button10.Text = "open FFT";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(412, 97);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(65, 17);
            this.radioButton4.TabIndex = 19;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "128 FFT";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(412, 81);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(65, 17);
            this.radioButton3.TabIndex = 18;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "256 FFT";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(412, 64);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(65, 17);
            this.radioButton2.TabIndex = 17;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "512 FFT";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(412, 47);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 17);
            this.radioButton1.TabIndex = 16;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1024 FFT";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // button9
            // 
            this.button9.Enabled = false;
            this.button9.Location = new System.Drawing.Point(337, 52);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(69, 23);
            this.button9.TabIndex = 15;
            this.button9.Text = "save FFT";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(412, 21);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(82, 23);
            this.button8.TabIndex = 14;
            this.button8.Text = "Crop data";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(570, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Range";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(337, 21);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(69, 23);
            this.button7.TabIndex = 12;
            this.button7.Text = "FFT";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Enabled = false;
            this.trackBar1.Location = new System.Drawing.Point(615, 19);
            this.trackBar1.Maximum = 10000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(362, 45);
            this.trackBar1.TabIndex = 11;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(254, 87);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(77, 23);
            this.button6.TabIndex = 10;
            this.button6.Text = "Open File";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(254, 53);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(77, 22);
            this.button5.TabIndex = 9;
            this.button5.Text = "Stop Record";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(254, 21);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Record";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(183, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(65, 89);
            this.button3.TabIndex = 7;
            this.button3.Text = "Refresh port";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // streamming_max_box
            // 
            this.streamming_max_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.streamming_max_box.DecimalPlaces = 1;
            this.streamming_max_box.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.streamming_max_box.Location = new System.Drawing.Point(986, 19);
            this.streamming_max_box.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.streamming_max_box.Name = "streamming_max_box";
            this.streamming_max_box.Size = new System.Drawing.Size(80, 20);
            this.streamming_max_box.TabIndex = 6;
            this.streamming_max_box.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.streamming_max_box.ValueChanged += new System.EventHandler(this.streamming_max_box_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(983, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "streamming Max";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "COM PORT:";
            // 
            // serial_port_name_box
            // 
            this.serial_port_name_box.FormattingEnabled = true;
            this.serial_port_name_box.Location = new System.Drawing.Point(92, 53);
            this.serial_port_name_box.Name = "serial_port_name_box";
            this.serial_port_name_box.Size = new System.Drawing.Size(85, 21);
            this.serial_port_name_box.TabIndex = 0;
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 460800;
            this.serialPort.PortName = "COM9";
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // stream_draw_timer
            // 
            this.stream_draw_timer.Enabled = true;
            this.stream_draw_timer.Tick += new System.EventHandler(this.stream_draw_timer_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Blue OAK file|*.pong;*.mang;*.tie|All files (*.*)|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "pong";
            this.saveFileDialog1.FileName = "datapoint";
            this.saveFileDialog1.Filter = "Pong file|*.pong";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.DefaultExt = "mang";
            this.saveFileDialog2.FileName = "datacrop";
            this.saveFileDialog2.Filter = "Mang file|*.mang";
            // 
            // saveFileDialog3
            // 
            this.saveFileDialog3.DefaultExt = "tie";
            this.saveFileDialog3.FileName = "fftpoint";
            this.saveFileDialog3.Filter = "Tie file|*.tie";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.device_message_box,
            this.system_message_box});
            this.statusStrip1.Location = new System.Drawing.Point(0, 792);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1096, 22);
            this.statusStrip1.TabIndex = 4;
            // 
            // device_message_box
            // 
            this.device_message_box.Name = "device_message_box";
            this.device_message_box.Size = new System.Drawing.Size(70, 17);
            this.device_message_box.Text = "no message";
            this.device_message_box.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // system_message_box
            // 
            this.system_message_box.Margin = new System.Windows.Forms.Padding(100, 3, 0, 2);
            this.system_message_box.Name = "system_message_box";
            this.system_message_box.Size = new System.Drawing.Size(39, 17);
            this.system_message_box.Text = "Ready";
            this.system_message_box.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // auto_export_save_dialog
            // 
            this.auto_export_save_dialog.DefaultExt = "csv";
            this.auto_export_save_dialog.FileName = "auto_export";
            this.auto_export_save_dialog.Filter = "CSV file|*.csv";
            this.auto_export_save_dialog.Title = "Auto export save file";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 814);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Serial FFT Plotter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.capture_pic)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.streaming_pic)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FFT_pic)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_bar_num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.streamming_max_box)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox capture_pic;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox streaming_pic;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox FFT_pic;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown streamming_max_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox serial_port_name_box;
        private System.Windows.Forms.Button button3;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Timer stream_draw_timer;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog3;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.NumericUpDown track_bar_num;
        private System.Windows.Forms.Label sampling_selected_max_text;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox serial_port_baud_box;
        private System.Windows.Forms.Button auto_export_but;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SaveFileDialog auto_export_save_dialog;
        private System.Windows.Forms.ToolStripStatusLabel device_message_box;
        private System.Windows.Forms.ToolStripStatusLabel system_message_box;
        private System.Windows.Forms.Button fft_style_but;
        private System.Windows.Forms.CheckBox ask_before_save_but;
        private System.Windows.Forms.CheckBox fft_log_scale_but;
        private System.Windows.Forms.CheckBox mark_index_129_but;
        private System.Windows.Forms.CheckBox export_bt01_bt02_but;
    }
}

