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
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }


 
        public Button userControle_1_Button2 { get { return button2; } }

        public TextBox userControle_1_path { get { return path; } }

        public TextBox userControle_1_TextBox1 { get { return textBox1; } }

        public DataGridView userControle_1_DataGridView1 { get { return dataGridView1; } }

        public DataGridView userControle_1_DataGridView2 { get { return dataGridView2; } }
        public Form1 form1 { get; set; }

        

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void path_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            form1.button1_Click(sender, e);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(textBox1.Text);
                form1.button2_Click(sender, e); 
            }
            catch (Exception) { }
        }
    }
}
