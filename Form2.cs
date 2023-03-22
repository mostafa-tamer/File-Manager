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
    public partial class Form2 : Form
    {
        string[][] arrayOfPaths;

        DataGridView baseDataGrid;

        TextBox source;
        TextBox destintaion;
        public Form2(string[][] arrayOfPaths, DataGridView baseDataGrid,TextBox destintaion,TextBox source)
        {
            this.arrayOfPaths = arrayOfPaths;
            this.baseDataGrid = baseDataGrid;
            this.destintaion=destintaion;   
            this.source = source;

            InitializeComponent();
        }
        public void setDataGridRow(string RowPaths)
        {
            dataGridView1.Rows.Add(RowPaths);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int getArraySize = 0;

            for (int i = 0; i < arrayOfPaths.Length; i++) { for (int j = 0; j < arrayOfPaths[i].Length; j++) { getArraySize++; } }

            string[] arrayOfRows = new string[getArraySize];

            int index = 0 ;

            for (int i = 0; i < arrayOfPaths.Length; i++)
            {
                for (int j = 0; j < arrayOfPaths[i].Length; j++)
                {
                    if (arrayOfPaths[i][j]==null)
                    {
                        continue;
                    }
                    arrayOfRows[index]=arrayOfPaths[i][j];
                    index++;
                } 
            }

            for (int i = 0; i < arrayOfRows.Length; i++)
            {
                if (arrayOfRows[i] == null)
                {
                    continue;
                }
                 
                DirectoryInfo fileInfo = new DirectoryInfo(arrayOfRows[i]);   
                 
                foreach (string file in Directory.GetFiles(fileInfo.Parent.FullName,"*.*",SearchOption.AllDirectories))
                {
                    if (file.Contains(destintaion.Text))
                    {
                        continue;
                    }
                    File.Delete(file); 
                }
                 
                foreach (string folder in Directory.GetDirectories(source.Text))
                {
                    try
                    {
                        if (folder == destintaion.Text)
                        {
                            continue;
                        }

                        Directory.Delete(folder,true); 
                    }
                    catch (Exception) { }
                }  
            }
            MessageBox.Show("Done!");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
