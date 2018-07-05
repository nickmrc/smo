using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lab_number_7_Model;
using Lab_number_7_Model.Properties;

namespace StegProject
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
         
            if (!password_textBox.Text.Equals("")&& !password_textBox.Text.Equals("Введите пароль"))
            {
                if (Lab_number_7_Model.Properties.Settings.Default.Password == password_textBox.Text)
                {
                    password_textBox.ForeColor = Color.Black;
                    password_textBox.BackColor = Color.Aquamarine;
                   // this.BackgroundImage = Lab_number_7_Model.Properties.Resources.unlck;
                    this.BackgroundImage = Lab_number_7_Model.Properties.Resources.полет;
                    welcome_button.Enabled = true;

                }
                else
                {
                  //  this.BackgroundImage = Lab_number_7_Model.Properties.Resources.lck;
                    password_textBox.ForeColor = Color.Black;
                    password_textBox.BackColor = Color.LightCoral;
                    this.BackgroundImage = Lab_number_7_Model.Properties.Resources.logo;
                    welcome_button.Enabled = false;
                }
            }

            else
            {
                //this.BackgroundImage = Properties.Resources.lck;
                password_textBox.BackColor = Color.White;
                welcome_button.Enabled = false;
            }
        }

        private void welcome_button_Click(object sender, EventArgs e)
        {
            if (Lab_number_7_Model.Properties.Settings.Default.Password == password_textBox.Text)
            {
                this.Hide();
                HelloMenuForm helloMenu = new HelloMenuForm();
                helloMenu.Show();
              
                
            }
        }

   

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //this.BackColor = Color.FromArgb(234, 24, 70);
            password_textBox.Text = "Введите пароль";
            password_textBox.ForeColor = Color.Gray;
            welcome_button.Enabled = false;
            
            focus_textBox.Focus();
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (password_textBox.Text == "")
            {
                password_textBox.Text = "Введите пароль";
                password_textBox.ForeColor = Color.Gray;
                password_textBox.PasswordChar = '\0';
            }
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            password_textBox.Text = "";
            password_textBox.ForeColor = Color.Black;
            password_textBox.PasswordChar = '*';
        }

        private void LoginForm_MouseClick(object sender, MouseEventArgs e)
        {
            focus_textBox.Focus();
        }

        private void exit_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти из программы?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
             Application.Exit();
            }
            else if (result == DialogResult.No)
            {
                //...
            }
        }

        private void LoginForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) welcome_button.PerformClick();
        }

        private void focus_textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) welcome_button.PerformClick();
        }

        private void password_textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) welcome_button.PerformClick();
        }
    }
}
