using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serial_FFT_plotter
{
    public partial class saving_form : Form
    {
        public string file_name = "";

        public saving_form()
        {
            InitializeComponent();
        }

        public class ComboboxItem
        {
            public int Num { get; set; }
            public string Text { get; set; }
            public string Value { get; set; }

            public ComboboxItem(int num, string name, string val)
            {
                Num = num;
                Text = name;
                Value = val;
            }

            public override string ToString()
            {
                return Text;
            }
        };


        private void saving_form_Load(object sender, EventArgs e)
        {
            List<ComboboxItem> items1 = new List<ComboboxItem>();
            items1.Add(new ComboboxItem(0, "000", "000")); // unknown
            items1.Add(new ComboboxItem(1, "001", "001")); // Under the Khum Phucome wooden house
            items1.Add(new ComboboxItem(2, "002", "002")); // Parking beside the Khum Phucome wooden house
            items1.Add(new ComboboxItem(3, "003", "003")); // Parking under Khum Phucome Building
            items1.Add(new ComboboxItem(4, "004", "004")); // Beside Khum Phucome swimming pool
            items1.Add(new ComboboxItem(5, "005", "005")); // Parking in front of SYN Hotel

            format_box_1.DataSource = items1;

            List<ComboboxItem> items2 = new List<ComboboxItem>();
            /*items2.Add(new ComboboxItem(0, "N", "00")); // normal (no flooring laid over)
            items2.Add(new ComboboxItem(1, "L", "01")); // normal covered with laminate
            items2.Add(new ComboboxItem(2, "WN", "W00")); // wood normal (no flooring laid over)
            items2.Add(new ComboboxItem(3, "WL", "W01")); // wood covered with laminate*/
            items2.Add(new ComboboxItem(0, "00", "00")); // normal (no flooring laid over)
            items2.Add(new ComboboxItem(1, "01", "01")); // normal covered with laminate
            items2.Add(new ComboboxItem(2, "02", "02")); // wood normal (no flooring laid over)
            items2.Add(new ComboboxItem(3, "03", "03")); // wood covered with laminate

            format_box_2.DataSource = items2;

            List<ComboboxItem> items3 = new List<ComboboxItem>();
            items3.Add(new ComboboxItem(0, "00", "M00")); // none (don't used model)
            items3.Add(new ComboboxItem(1, "01", "M01")); // model_1
            items3.Add(new ComboboxItem(2, "02", "M02")); // model_2
            items3.Add(new ComboboxItem(3, "03", "M03")); // model_3
            items3.Add(new ComboboxItem(4, "04", "M04")); // model_4
            items3.Add(new ComboboxItem(5, "05", "M05")); // model_5
            items3.Add(new ComboboxItem(6, "06", "M06")); // model_6
            items3.Add(new ComboboxItem(7, "07", "M07")); // model_7

            format_box_3.DataSource = items3;

            List<ComboboxItem> items4 = new List<ComboboxItem>();
            items4.Add(new ComboboxItem(0, "H000 (stunt man)", "H000"));    // stunt man
            items4.Add(new ComboboxItem(1, "H001 (jump)", "H001"));         // jump
            items4.Add(new ComboboxItem(2, "H002 (stomp-left)", "H002"));   // stomp-left
            items4.Add(new ComboboxItem(3, "H003 (stomp-right)", "H003"));  // stomp-right
            items4.Add(new ComboboxItem(4, "H004 (walk)", "H004"));         // walk

            items4.Add(new ComboboxItem(5, "D001 (dummy 30 kg)", "D001"));  // dummy 30 kg
            items4.Add(new ComboboxItem(6, "D002 (dummy 45 kg)", "D002"));  // dummy 45 kg
            items4.Add(new ComboboxItem(7, "D003 (dummy 75 kg)", "D003"));  // dummy 75 kg

            items4.Add(new ComboboxItem(8, "O001 (basket)", "O001"));       // basket
            items4.Add(new ComboboxItem(9, "O002 (bin)", "O002"));          // bin
            items4.Add(new ComboboxItem(10, "O003 (book)", "O003"));        // book
            items4.Add(new ComboboxItem(11, "O004 (pillow)", "O004"));      // pillow
            items4.Add(new ComboboxItem(12, "O005 (shower)", "O005"));      // shower
            items4.Add(new ComboboxItem(13, "O006 (bottle of shower cream)", "O006"));  // bottle of shower cream
            items4.Add(new ComboboxItem(14, "O007 (umbrella)", "O007"));                // umbrella
            items4.Add(new ComboboxItem(15, "O008 (bottle of water )", "O008"));        // bottle of water 
            items4.Add(new ComboboxItem(16, "O009 (wood chair)", "O009"));              // wood chair

            format_box_4.DataSource = items4;

            List<ComboboxItem> items6 = new List<ComboboxItem>();
            items6.Add(new ComboboxItem(0, "000(1x)",   "G000")); // 1x
            items6.Add(new ComboboxItem(1, "001(2x)",   "G001")); // 2x
            items6.Add(new ComboboxItem(2, "010(4x)",   "G010")); // 4x
            items6.Add(new ComboboxItem(3, "011(8x)",   "G011")); // 8x
            items6.Add(new ComboboxItem(4, "100(16x)",  "G100")); // 16x
            items6.Add(new ComboboxItem(5, "101(32x)",  "G101")); // 32x
            items6.Add(new ComboboxItem(6, "110(64x)",  "G110")); // 64x
            items6.Add(new ComboboxItem(7, "111(128x)", "G111")); // 128x       

            format_box_6.DataSource = items6;

            List<ComboboxItem> items7 = new List<ComboboxItem>();
            items7.Add(new ComboboxItem(0, "20 cm (0025)",  "D0025")); // 20 cm
            items7.Add(new ComboboxItem(1, "50 cm (0050)",  "D0050")); // 50 cm
            items7.Add(new ComboboxItem(2, "75 cm (0075)",  "D0075")); // 75 cm
            items7.Add(new ComboboxItem(3, "100 cm (0100)", "D0100")); // 100 cm 
            items7.Add(new ComboboxItem(4, "125 cm (0125)", "D0125")); // 125 cm
            items7.Add(new ComboboxItem(5, "150 cm (0150)", "D0150")); // 150 cm
            items7.Add(new ComboboxItem(6, "175 cm (0175)", "D0175")); // 175 cm
            items7.Add(new ComboboxItem(7, "200 cm (0200)", "D0200")); // 200 cm
            items7.Add(new ComboboxItem(9, "300 cm (0300)", "D0300")); // 300 cm 
            items7.Add(new ComboboxItem(10,"400 cm (0400)", "D0400")); // 400 cm
            items7.Add(new ComboboxItem(11,"500 cm (0500)", "D0500")); // 500 cm 
            items7.Add(new ComboboxItem(12,"600 cm (0600)", "D0600")); // 600 cm
            items7.Add(new ComboboxItem(13,"700 cm (0800)", "D0900")); // 700 cm
            items7.Add(new ComboboxItem(14,"800 cm (0800)", "D0800")); // 800 cm
            items7.Add(new ComboboxItem(15,"900 cm (0900)", "D0900")); // 900 cm
            items7.Add(new ComboboxItem(15,"1000 cm (1000)", "D1000")); // 1000 cm

            format_box_7.DataSource = items7;

            List<ComboboxItem> items8 = new List<ComboboxItem>();
            items8.Add(new ComboboxItem(0, "1st trial (01)", "R01")); // 1st trial
            items8.Add(new ComboboxItem(1, "2nd trial (02)", "R02")); // 2nd trial
            items8.Add(new ComboboxItem(2, "3rd trial (03)", "R03")); // 3rd trial
            items8.Add(new ComboboxItem(3, "4th trial (04)", "R04")); // 4th trial
            items8.Add(new ComboboxItem(4, "5th trial (05)", "R05")); // 5th trial

            format_box_8.DataSource = items8;

            for(int i = 0; i < 9; i++)
            {
                refresh_text(i);
            }

            generate_file_name();
        }

        private void refresh_time_text()
        {
            format_text_5.Text = "T" + DateTime.Now.ToString("yyyyMMddHHmm");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            refresh_time_text();
            generate_file_name();
        }

        private void refresh_text(int num)
        {
            if(num == 1)
            {
                format_text_1.Text = ((ComboboxItem)format_box_1.SelectedItem).Value;
            }
            if (num == 2)
            {
                format_text_2.Text = ((ComboboxItem)format_box_2.SelectedItem).Value;
            }
            if (num == 3)
            {
                format_text_3.Text = ((ComboboxItem)format_box_3.SelectedItem).Value;
            }
            if (num == 4)
            {
                format_text_4.Text = ((ComboboxItem)format_box_4.SelectedItem).Value;
            }
            if (num == 5)
            {
                refresh_time_text();
            }
            if (num == 6)
            {
                format_text_6.Text = ((ComboboxItem)format_box_6.SelectedItem).Value;
            }
            if (num == 7)
            {
                format_text_7.Text = ((ComboboxItem)format_box_7.SelectedItem).Value;
            }
            if (num == 8)
            {
                format_text_8.Text = ((ComboboxItem)format_box_8.SelectedItem).Value;
            }

        }

        private void generate_file_name()
        {
            filename_box.Text = format_text_1.Text + "_" + format_text_2.Text + "_" +
                                format_text_3.Text + "_" + format_text_4.Text + "_" +
                                format_text_5.Text + "_" + format_text_6.Text + "_" +
                                format_text_7.Text + "_" + format_text_8.Text;

        }

        private void format_box_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_text(1);
            generate_file_name();
        }

        private void format_box_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_text(2);
            generate_file_name();
        }

        private void format_box_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_text(3);
            generate_file_name();
        }

        private void format_box_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_text(4);
            generate_file_name();
        }

        private void format_box_6_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_text(6);
            generate_file_name();
        }

        private void format_box_7_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_text(7);
            generate_file_name();
        }

        private void format_box_8_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_text(8);
            generate_file_name();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (using_formatter_box.CheckState == CheckState.Checked)
            {
                this.file_name = filename_box.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }
    }
}
