using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_number_7_Model
{
    public partial class ChangePass_Form : Form
    {
        public ChangePass_Form()
        {
            InitializeComponent();
        }

      

        private void ChangePass_Form_Load(object sender, EventArgs e)
        {

        }

        private void changepass_button_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Password == textBox1.Text)
            {
                if (newpass_textBox.Text != "")
                {
                    Properties.Settings.Default.Password = newpass_textBox.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Пароль изменен", "Смена пароля - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    newpass_textBox.Clear();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось изменить пароль. Пароль должен состоять минимум из одного символа", "Смена пароля", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                if (Properties.Settings.Default.Password == textBox1.Text)
                {
                    textBox1.BackColor = Color.Aquamarine;
                    changepass_button.Enabled = true;

                }
                else
                {
                    textBox1.BackColor = Color.LightCoral;
                    changepass_button.Enabled = false;
                }
            }

            else
            {
                textBox1.BackColor = Color.White;
                changepass_button.Enabled = false;
            }
        }

        private void newpass_textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
