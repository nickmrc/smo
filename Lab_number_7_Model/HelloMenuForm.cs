using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab_number_7_Model
{
    public partial class HelloMenuForm : Form
    {
        public HelloMenuForm()
        {
            InitializeComponent();

            button1.BackColor = Color.LightBlue;//начать моделирование
            button2.BackColor = Color.AliceBlue;//on
            button3.BackColor = Color.AliceBlue;//o программе
            button4.BackColor = Color.AliceBlue;//on
            button5.BackColor = SystemColors.Control;//off
            button6.BackColor = SystemColors.Control;//off
            
        }

        //кнопка Выполнение расчетов или запуск модели
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        //дата и время
        //private void HelloMenuForm_Load(object sender, EventArgs e)
        //{
        //    this.linkLabel1.Text = DateTime.Now.ToShortTimeString();
        //    this.linkLabel2.Text = DateTime.Now.ToShortDateString();
        //}

        //вывод основной части реферата и заключения
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.LoadFile(@"helpДИ.rtf");
                button2.Visible = false;
                button5.Visible = true;

                button4.Visible = true;
                button6.Visible = false;
            }
            catch
            {
                richTextBox1.Text = "Неправильно указан путь к файлу";
            }
        }

        //отчитстка richtextbox той же кнопкой
        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            button5.Visible = false;
            button2.Visible = true;

        }

        bool IsFormOpened<TForm>() where TForm : Form
        {
            return Application.OpenForms.OfType<TForm>().Any();
        }


        //кнопка о программе
        private void button3_Click(object sender, EventArgs e)
        {
            if (!IsFormOpened<AboutBox1>()) {
                AboutBox1 aboutbox = new AboutBox1();
                aboutbox.Show();
            }
            else
            {
                Application.OpenForms.OfType<AboutBox1>().First().Focus();
            }
            
        }

        //кнопка Структурная схема и алгоритм
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.LoadFile(@"HelloMenuFormButtonСхемыИАлгоритм.rtf");
                button4.Visible = false;
                button6.Visible = true;

                button2.Visible = true;
                button5.Visible = false;


            }
            catch 
            {
                richTextBox1.Text = "Неправильно указан путь к файлу";
            }
        }

        //отчитстка richtextbox той же кнопкой
        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            button6.Visible = false;
            button4.Visible = true;
        }

        private void HelloMenuForm_Load(object sender, EventArgs e)
        {

        }

        private void HelloMenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void change_pass_button_Click(object sender, EventArgs e)
        {
            if (!IsFormOpened<ChangePass_Form>())
            {
                ChangePass_Form aboutbox = new ChangePass_Form();
                aboutbox.Show();
            }
            else
            {
                Application.OpenForms.OfType<ChangePass_Form>().First().Focus();
            }
        }
    }
}
