using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PosAttDataGenerator
{
    public partial class PosAttDataGenerator : Form
    {
        public PosAttDataGenerator()
        {
            InitializeComponent();
            fc = new function();
        }

        public function fc;

        private void PosAttDataGenerator_Load(object sender, EventArgs e)
        {
            string now_path = System.Windows.Forms.Application.StartupPath;
            inputpath_label.Text = fc.inputfile_open(now_path);
            outputpath_label.Text = fc.outputpath_load(now_path);

        }

        private void transfer_button_Click(object sender, EventArgs e)
        {

            if (!time_textbox.Text.Equals("") && !interval_textbox.Text.Equals(""))
            {

                if (fc.transfer_and_save(inputpath_label.Text, outputpath_label.Text, time_textbox.Text, interval_textbox.Text))
                    MessageBox.Show("성공적으로 저장되었습니다.");

                else MessageBox.Show("저장에 실패하였습니다.");
            }

            else MessageBox.Show("빈칸이 있습니다");
      
        }


    }
}
