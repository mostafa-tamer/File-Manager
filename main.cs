using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class main : UserControl
    {

        public Button mainButton1 { get { return button1; } }

      //  public Button userControle_1_Button2 { get { return button2; } }

        public TextBox mainpath { get { return path; } }

     //   public TextBox userControle_1_TextBox1 { get { return textBox1; } }

        public DataGridView mainDataGridView1 { get { return dataGridView1; } }

        public DataGridView mainDataGridView2 { get { return dataGridView2; } }


        public Form1 form1 { get; set; }
        public main()
        {
            InitializeComponent();

             


        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1.button1_Click(sender, e);

            if (dataGridView1.Rows.Count > 0)
            {
                form1.button2.Enabled = true;
                form1.button3.Enabled = true;
                form1.button4.Enabled = true;
            }
        }

        private void path_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
