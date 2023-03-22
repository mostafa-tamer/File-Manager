
namespace WinFormsApp3
{
    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
        }
        class SortFiles
        {
            FileInfo fileInfo;

            TextBox textBoxPath;

            DataGridView displayInDataGrid;
            DataGridView displayInMainDataGrid;

            int arraySize;

            string[][] fileAndExtention;
            string[][] numberOfExtentions;
            string[][] arrayOfOrderedPaths;

            public string[][] GetArray
            { get { return arrayOfOrderedPaths; } }

            public SortFiles(TextBox textBox_path, DataGridView displayInDataGrid, DataGridView displayInMainDataGrid)
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(textBox_path.Text);
                    arraySize = dir.GetFiles().Length;
                }
                catch (Exception) { MessageBox.Show("Wrong Path!"); }

                fileAndExtention = new string[arraySize][];
                numberOfExtentions = new string[arraySize][];

                textBoxPath = textBox_path;
                this.displayInDataGrid = displayInDataGrid;
                this.displayInMainDataGrid = displayInMainDataGrid;

                fillingFileAndExtention();

                fillingExtentionsAndNumbers();

                fillingArrayOfOrderedPaths();

                printingExtentionsWithNumbers();

                printInDataGridNames();
            }
            void fillingFileAndExtention()
            {
                for (int i = 0; i < arraySize; i++)
                {
                    fileAndExtention[i] = new string[2];
                }
                for (int i = 0; i < arraySize; i++)
                {
                    numberOfExtentions[i] = new string[2];
                    numberOfExtentions[i][1] = "1";
                }

                int index = 0;

                try
                {
                    foreach (string filePath in Directory.GetFiles(textBoxPath.Text))
                    {
                        fileInfo = new FileInfo(filePath);

                        fileAndExtention[index][0] = filePath;
                        fileAndExtention[index][1] = fileInfo.Extension;

                        index++;
                    }
                }
                catch (Exception) { }
            }

            void fillingExtentionsAndNumbers()
            {
                int x = 0;
                int y = 0;
                int lastIndex = 0;
                bool condition = true;

                int incrementer;

                while (y < arraySize)
                {
                    while (x < arraySize)
                    {
                        if (numberOfExtentions[x][0] == fileAndExtention[y][1] && fileAndExtention[y][1] != null)
                        {
                            condition = false;

                            incrementer = Convert.ToInt16(numberOfExtentions[x][1]);

                            numberOfExtentions[x][1] = Convert.ToString(++incrementer);

                            break;
                        }
                        x++;
                    }
                    if (condition == true)
                    {
                        numberOfExtentions[lastIndex][0] = fileAndExtention[y][1];
                        lastIndex++;
                    }
                    else
                    {
                        condition = true;
                    }
                    x = 0;
                    y++;
                }
            }
            void printingExtentionsWithNumbers()
            {
                string[] arr = new string[2];

                for (int i = 0; i < arraySize; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (numberOfExtentions[i][0] == null)
                        {
                            i = arraySize;
                            break;
                        }
                        arr[j] = numberOfExtentions[i][j];
                    }
                    if (i != arraySize)
                    {
                        displayInDataGrid.Rows.Add(arr);
                    }
                }
            }
            void fillingArrayOfOrderedPaths()
            {
                arrayOfOrderedPaths = new string[arraySize][];

                for (int q = 0; q < arraySize; q++)
                {
                    arrayOfOrderedPaths[q] = new string[Convert.ToInt16(numberOfExtentions[q][1])];
                }

                int last = 0;

                for (int i = 0; i < arraySize; i++)
                {
                    for (int j = 0; j < arraySize; j++)
                    {
                        if (fileAndExtention[j][1] == numberOfExtentions[i][0])
                        {
                            arrayOfOrderedPaths[i][last] = fileAndExtention[j][0];
                            last++;
                        }
                    }
                    last = 0;
                }
            }
            void printInDataGridNames()
            {
                string[] arr = new string[2];

                for (int i = 0; i < arraySize; i++)
                {
                    for (int j = 0; j < arrayOfOrderedPaths[i].Length; j++)
                    {
                        try
                        {
                            FileInfo fileInfo = new FileInfo(arrayOfOrderedPaths[i][j]);
                            arr[0] = arrayOfOrderedPaths[i][j];

                            if (fileInfo.Length < 1024 * 1024)
                            {
                                arr[1] = String.Format("{0:0.000}", (fileInfo.Length / (1024.0))) + " Kb";
                            }
                            else
                            {
                                arr[1] = String.Format("{0:0.000}", (fileInfo.Length / (1024 * 1024.0))) + " Mb";
                            }

                            displayInMainDataGrid.Rows.Add(arr);
                        }
                        catch (Exception) { }

                        if (arrayOfOrderedPaths[i][j] == null)
                        {
                            i = arraySize;
                            break;
                        }

                    }
                    if (i == arraySize)
                    {
                        break;
                    }

                }
            }
        }

        class SortDirectory
        {
            TextBox textBoxPath;

            DataGridView dataGridView;

            DataGridView dataGridViewCounter;

            double directorySize;

            string[][] Directories;

            public string[][] getDirectories { get { return Directories; } }

            public SortDirectory(TextBox textBoxPath, DataGridView dataGridView, DataGridView dataGridViewCounter)
            {
                this.textBoxPath = textBoxPath;
                this.dataGridView = dataGridView;
                this.dataGridViewCounter = dataGridViewCounter;

                
                Directories = new string[Directory.GetDirectories(textBoxPath.Text).Length][];

                for (int i = 0; i < Directories.Length; i++)
                {
                    Directories[i] = new string[3];
                }

                directoryReader();

                dataGridViewCounter.Rows.Add("Folders", directoryCounter());

                
            }
            void directoryReader()
            {
                int index = 0;

                try
                {
                    foreach (string item in Directory.GetDirectories(textBoxPath.Text))
                    {
                        string[] arr = new string[2];

                        arr[0] = item;

                        foreach (string file in Directory.GetFiles(item, "*.*", SearchOption.AllDirectories))
                        {
                            FileInfo fileInfo = new FileInfo(file);

                            directorySize += Convert.ToDouble(fileInfo.Length);
                        }
                         
                        arr[1] = directorySizeConverter();


                        Directories[index][0] = item;

                        Directories[index][1]= directorySize.ToString();

                        Directories[index][2] = arr[1];

                        dataGridView.Rows.Add(arr);

                        directorySize = 0;

                        index++;
                    }
                }
                catch (Exception) { }
            }
            string directorySizeConverter()
            {
                string size;

                if (directorySize < 1024 * 1024)
                {
                    size = String.Format("{0:0.000}", (directorySize / (1024.0))) + " Kb";
                }
                else if (directorySize < 1024 * 1024 * 1024 && directorySize >= 1024 * 1024)
                {
                    size = String.Format("{0:0.000}", (directorySize / (1024 * 1024.0))) + " Mb";
                }
                else if (directorySize >= 1024 * 1024 * 1024)
                {
                    size = String.Format("{0:0.000}", (directorySize / (1024 * 1024 * 1024.0))) + " GB";
                }
                else
                {
                    size = "Very Big Size";
                }

                return size;
            }
            int directoryCounter()
            {
                try
                {
                    return Directory.GetDirectories(textBoxPath.Text).Length;
                }
                catch (Exception) { }
                return 0;
            }

            public void sortingDirectories()
            { 
                string[] arrrayTemp;

                try
                {
                    for (int i = 0; i < Directories.Length; i++)
                    {
                        for (int j = 0; j < Directories.Length - 1; j++)
                        {
                            if (Convert.ToInt64(Directories[j][1]) > Convert.ToInt64(Directories[j + 1][1]))
                            {
                                arrrayTemp = Directories[j + 1];
                                Directories[j + 1] = Directories[j];
                                Directories[j] = arrrayTemp;
                            }
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        SortDirectory sortDirectory;

        SortFiles sort;


        public void button1_Click(object sender, EventArgs e)
        {


            main.mainDataGridView1.Rows.Clear();
            main.mainDataGridView2.Rows.Clear();

            sort = new SortFiles(main.mainpath, main.mainDataGridView2, main.mainDataGridView1);

            try
            {
                sortDirectory = new SortDirectory(main.mainpath, main.mainDataGridView1, main.mainDataGridView2);
            }
            catch (Exception) { }
        }

        class CopyInFolder
        {
            TextBox textBoxPathDest;
            TextBox TextBoxPathSource;

            Form2 formCopy;

            string[][] arrayOfPaths;
            public CopyInFolder(string[][] arrayOfPaths, TextBox textBoxPathDest, TextBox TextBoxPathSource, DataGridView mainDataGrid)
            {
                formCopy = new Form2(arrayOfPaths, mainDataGrid, textBoxPathDest, TextBoxPathSource);

                this.arrayOfPaths = arrayOfPaths;

                this.textBoxPathDest = textBoxPathDest;

                this.TextBoxPathSource = TextBoxPathSource;

                copyFiles();

                copyDirectories();

                formCopy.ShowDialog(); 
            }
            void copyFiles()
            {
                for (int i = 0; i < arrayOfPaths.Length; i++)
                {
                    for (int j = 0; j < arrayOfPaths[i].Length; j++)
                    {
                        if (arrayOfPaths[i][j] == null)
                        {
                            break;
                        }

                        try
                        {
                            FileInfo obj = new FileInfo(arrayOfPaths[i][j]);
                            if (j == 0)
                            {
                                Directory.CreateDirectory(textBoxPathDest.Text + @"\" + obj.Extension);
                            }

                            File.Copy(arrayOfPaths[i][j], textBoxPathDest.Text + @"\" + obj.Extension + @"\" + obj.Name);

                            formCopy.setDataGridRow(textBoxPathDest.Text + @"\" + obj.Extension + @"\" + obj.Name);

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                }
            }
            void copyDirectories()
            {
                bool firstTimeOnly = true;

                foreach (string Folder in Directory.GetDirectories(TextBoxPathSource.Text))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Folder);
                    try
                    { 

                        if (directoryInfo.FullName == textBoxPathDest.Text)
                        {
                            continue;
                        }

                        if (firstTimeOnly == true)
                        {
                            Directory.CreateDirectory(textBoxPathDest.Text + @"\Folders");

                            firstTimeOnly = false;
                        }

                        Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(Folder, textBoxPathDest.Text + @"\Folders\" + directoryInfo.Name);

                        formCopy.setDataGridRow(textBoxPathDest.Text + @"\Folders\" + directoryInfo.Name);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(directoryInfo.Name + " Is Already Exist");
                    }
                }

            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(userControl11.userControle_1_TextBox1.Text)&& userControl11.userControle_1_TextBox1.Text!= "Write Destination Path Here")
            { 
                CopyInFolder copyInFolder = new CopyInFolder(sort.GetArray, userControl11.userControle_1_TextBox1, main.mainpath, main.mainDataGridView2);
            }
            else
            {
                MessageBox.Show("Wrong Path!");
            }
            MessageBox.Show("Done!");
            button2.Enabled = false;    
            button3.Enabled = false;
            button4.Enabled = false;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
             
            userControl11.form1 = this;
            main.form1 = this;

        }
      
        private void button2_Click_1(object sender, EventArgs e)
        {
            sortDirectory.sortingDirectories();
            try
            {
                for (int i = 0; i < sortDirectory.getDirectories.Length; i++)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(sortDirectory.getDirectories[i][0]);

                    Directory.Move(sortDirectory.getDirectories[i][0], directoryInfo.Parent + @"\" + (i + 1).ToString() + @"  " + directoryInfo.Name + "    " + sortDirectory.getDirectories[i][2]);
                    
                }
                MessageBox.Show("Done!");
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            button2.Enabled = false;
        }
        private void path_TextChanged(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void panel2_Paint(object sender, PaintEventArgs e) { }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string Folder in Directory.GetDirectories(main.mainpath.Text, "*", SearchOption.AllDirectories).Reverse())
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles(Folder))
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            if (fileInfo.Length == 0)
                            {
                                File.Delete(fileInfo.FullName);
                            }
                        }

                        if (Directory.GetDirectories(Folder).Length == 0)
                        {
                            Directory.Delete(Folder);
                        }
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }

            foreach (string FolderRoot in Directory.GetFiles(main.mainpath.Text))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(FolderRoot);
                    if (fileInfo.Length == 0)
                    {
                        File.Delete(fileInfo.FullName);
                    }
                }
                catch (Exception) { }
            }
            MessageBox.Show("Done!");
        }

        private void button4_Click(object sender, EventArgs e)
        {


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void userControl11_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
             
            userControl11.dataGridView2.Rows.Clear();
            userControl11.dataGridView1.Rows.Clear();
             
            userControl11.path.Text = main.path.Text;

            userControl11.path.ReadOnly = true; 

            for (int i = 0; i < main.dataGridView1.Rows.Count-1; i++)
            { 
                userControl11.dataGridView1.Rows.Add(main.dataGridView1.Rows[i].Cells[0].Value, main.dataGridView1.Rows[i].Cells[1].Value); 
            }

            for (int i = 0; i < main.dataGridView2.Rows.Count-1; i++)
            {
                userControl11.dataGridView2.Rows.Add(main.dataGridView2.Rows[i].Cells[0].Value, main.dataGridView2.Rows[i].Cells[1].Value);
            } 

            userControl11.Show();
            main.Hide(); 
        }
       
        private void button1_Click_1(object sender, EventArgs e)
        { 
            main.Show();
            userControl11.Hide();
        }

        private void main_Load(object sender, EventArgs e)
        {

        }
    }
}