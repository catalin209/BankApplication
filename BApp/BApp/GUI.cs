using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BApp
{
    public partial class GUI : Form
    {
        Client c = null;
        private static int lastButton = 0;
        static int contor_auxiliar = 0;
        static int contor_auxiliar1 = 0;
        List<ClientBase> list = null;
        private char gender = ' ';

        /// <summary>
        /// Initializes a new instance of the <see cref="GUI"/> class.
        /// </summary>
        public GUI()
        {
            InitializeComponent();
            comboBox1.Items.Add("Count Number Of Females");
            comboBox1.Items.Add("Count Number Of Males");
            comboBox1.Items.Add("Compute Female Age Average");
            comboBox1.Items.Add("Compute Male Age Average");
            comboBox1.Items.Add("Compute Female Age Average With String Parameter");
            comboBox1.Items.Add("Compute Male Age Average With String Parameter");
            comboBox1.Items.Add("Compute Female Age Average With Age Parameter");
            comboBox1.Items.Add("Compute Male Age Average With Age Parameter");
            comboBox1.Items.Add("Compute Female Age Average Younger Than A Parameter");
            comboBox1.Items.Add("Compute Female Age Average In A Range");
            c = new Client();

        }

        /// <summary>
        /// Handles the Load event of the GUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GUI_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            lastButton = 1;

            c.ClientList.Clear();
            c.GetGenderStatic();
            c.GenerateRandomAge();

            c.ClientList.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));

            richTextBox1.SelectionProtected = true;
            richTextBox1.ReadOnly = true;

            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection col = new AutoCompleteStringCollection();

            foreach (var item in c.ClientList)
            {
                col.Add(item.FirstName);
            }

            textBox2.AutoCompleteCustomSource = col;
            textBox3.AutoCompleteCustomSource = col;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            list = new List<ClientBase>();
            if (lastButton.Equals(1))
            {
                list = c.ClientList;
            }
            else if (lastButton.Equals(2))
            {
                list = c.RClientList;
            }

            if (list.Count == 0) MessageBox.Show("Empty client list. Please generate the client list first.");
            else
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        textBox1.Text = "";
                        textBox1.Text = c.FemaleCount(list).ToString();
                        break;

                    case 1:
                        textBox1.Text = "";
                        textBox1.Text = c.MaleCount(list).ToString();
                        break;

                    case 2:
                        textBox1.Text = "";
                        textBox1.Text = c.FemaleAverage(list).ToString();
                        break;

                    case 3:
                        textBox1.Text = "";
                        textBox1.Text = c.MaleAverage(list).ToString();
                        break;

                    case 4:
                        textBox1.Text = "";
                        textBox3.Visible = true;
                        button9.Visible = true;
                        break;

                    case 5:
                        textBox1.Text = "";
                        textBox3.Visible = true;
                        button9.Visible = true;
                        break;

                    case 6:
                        textBox1.Text = "";
                        textBox3.Visible = true;
                        button9.Visible = true;
                        break;

                    case 7:
                        textBox1.Text = "";
                        textBox3.Visible = true;
                        button9.Visible = true;
                        break;

                    case 8:
                        textBox1.Text = "";
                        textBox3.Visible = true;
                        button9.Visible = true;
                        break;

                    case 9:
                        textBox1.Text = "";
                        textBox4.Visible = true;
                        textBox5.Visible = true;
                        button9.Visible = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the label1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the button3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            c.ClientList.Clear();

            string s = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string file = s + "\\name_list.txt";
            string[] lines = File.ReadAllLines(file);

            if (lines.Length > 50)
                MessageBox.Show("The list is to big for this option.Please reduce the number of element.");
            else
            {
                c.GetGender();
                c.GenerateRandomAge();
            }
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            if (c.ClientList.Count == 0 )
            {
                MessageBox.Show("Empty client list. Please generate the client list first.");
            }

            if (gender.Equals(' '))
                MessageBox.Show("The type of gender is not selected");
            else
            {
                foreach (var item in c.ClientList)
                {
                    if (gender.Equals('a'))
                    {
                        richTextBox1.AppendText("Name:" + item.FirstName + "\nSex:" + item.Gender + "\nAge:" + item.Age + "\n");
                        richTextBox1.AppendText("......................\n");
                    }
                    else
                    {
                        if (item.Gender.Equals(gender))
                        {
                            richTextBox1.AppendText("Name:" + item.FirstName + "\nSex:" + item.Gender + "\nAge:" + item.Age + "\n");
                            richTextBox1.AppendText("......................\n");
                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            c.RClientList.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));
            richTextBox1.Text = "";

            foreach (var item in c.RClientList)
            {
                if (gender.Equals('a'))
                {
                    richTextBox1.AppendText("Name:" + item.FirstName + "\nSex:" + item.Gender + "\nAge:" + item.Age + "\n");
                    richTextBox1.AppendText("......................\n");
                }
                else
                {
                    if (item.Gender.Equals(gender))
                    {
                        richTextBox1.AppendText("Name:" + item.FirstName + "\nSex:" + item.Gender + "\nAge:" + item.Age + "\n");
                        richTextBox1.AppendText("......................\n");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lastButton = 2;

            c.RClientList.Clear();
            c.GenerateRandomClients();

            if(c.ClientList.Count < 50)
            {
                MessageBox.Show("Not enough clients in the list.");
            }

            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            c.RClientList.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));       

            foreach (var item in c.RClientList)
            {
                col.Add(item.FirstName);
            }

            textBox2.AutoCompleteCustomSource = col;
            textBox3.AutoCompleteCustomSource = col;
        }

        /// <summary>
        /// Handles the Click event of the button6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            string s = textBox2.Text;

            if (IsAllLetters(s))
            {
                foreach (var item in c.ClientList)
                {
                    if (item.FirstName.Equals(s))
                    {
                        richTextBox1.AppendText("Name:" + item.FirstName + "\nSex:" + item.Gender + "\nAge:" + item.Age + "\n");
                        richTextBox1.AppendText("......................\n");
                    }
                }
            }
            else
            {
                MessageBox.Show("The name is invalid");
            }  
        }

        /// <summary>
        /// Handles the Click event of the button7 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button7_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Clear();
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            StreamReader sr = new StreamReader(myStream);
                            richTextBox1.Text = sr.ReadToEnd();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter sw = new StreamWriter(myStream);

                    for (int i = 0; i < richTextBox1.Lines.Length; i++)
                    {
                        string s = richTextBox1.Lines[i];
                        sw.WriteLine(s);
                    }
                    sw.Close();
                    myStream.Close();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox2_Click(object sender, EventArgs e)
        {
            if (contor_auxiliar == 0)
            {
                textBox2.Text = "";
                contor_auxiliar++;
            }
        }

        /// <summary>
        /// Handles the Leave event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (contor_auxiliar == 1 && string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Text = "Enter a name";
                contor_auxiliar--;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox3_Click(object sender, EventArgs e)
        {
            if (contor_auxiliar1 == 0)
            {
                textBox3.Text = "";
                contor_auxiliar1++;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the textBox2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the TextChanged event of the textBox4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the TextChanged event of the textBox5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the button9 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button9_Click(object sender, EventArgs e)
        {
            string pattern = null;
            int age = 0;

            if (IsAllLetters(textBox3.Text))
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 4:
                        pattern = textBox3.Text;
                        textBox1.Text = c.FemaleAverage(pattern, list).ToString();
                        break;

                    case 5:
                        pattern = textBox3.Text;
                        textBox1.Text = c.MaleAverage(pattern, list).ToString();
                        break;

                    case 6:
                        age = 0;
                        int.TryParse(textBox3.Text, out age);
                        textBox1.Text = c.FemaleCount(age, list).ToString();
                        break;

                    case 7:
                        age = 0;
                        int.TryParse(textBox3.Text, out age);
                        textBox1.Text = c.MaleCount(age, list).ToString();
                        break;

                    case 8:
                        age = 0;
                        int.TryParse(textBox3.Text, out age);
                        textBox1.Text = c.FemaleAverageYoungerThan(age, list).ToString();
                        break;

                    case 9:
                        int ageMin = 0;
                        int ageMax = 0;
                        int.TryParse(textBox4.Text, out ageMin);
                        int.TryParse(textBox5.Text, out ageMax);
                        textBox1.Text = c.FemaleAverageRange(ageMin, ageMax, list).ToString();
                        break;

                }
            }
            else
            {
                MessageBox.Show("Name is invalid");
            }
           
           
        }

        /// <summary>
        /// Handles the CheckedChanged event of the radioButton4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            gender = 'x';
        }

        /// <summary>
        /// Handles the CheckedChanged event of the radioButton3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            gender = 'f';
        }

        /// <summary>
        /// Handles the CheckedChanged event of the radioButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = 'a';
        }

        /// <summary>
        /// Handles the CheckedChanged event of the radioButton2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = 'm';
        }

        /// <summary>
        /// Handles the Leave event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (contor_auxiliar1 == 1 && string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Text = "Enter a name";
                contor_auxiliar1--;
            }
        }

        /// <summary>
        /// Determines whether [is all letters] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///   <c>true</c> if [is all letters] [the specified s]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAllLetters(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetter(c))
                    return false;
            }
            return true;
        }
    }
}
