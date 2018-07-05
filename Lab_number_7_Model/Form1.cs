using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Lab_number_7_Model.Modeling;

namespace Lab_number_7_Model
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            this.Size = resolution;
            this.tabControl1.Size = resolution;

            //this.panel5.Width = resolution.Width;
            //this.panel5.Height = resolution.Height - 300;


            //p.Y = panel6.Location.Y;
            //this.panel6.Location = p;


            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;


            DeactivateControls();
            set_settings_rb.Checked = true;
            IsVisibleHead();
            Program.modeling = new Modeling();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetPropertyOfDataGrid();


            try
            {
                using (FileStream FS = new FileStream(@"Modelings.bin", FileMode.Open))
                {

                    BinaryFormatter BF = new BinaryFormatter();
                    Program.modeling.testmod = (List<testModeling>)BF.Deserialize(FS);

                }
            }
            catch
            {
            }
            finally
            {
                UpdateDGV();
            }

        }

        private void SetPropertyOfDataGrid()
        {
            load_dataGridView.AutoGenerateColumns = false;

            load_dataGridView.Columns.Add("Name", "Название");
            load_dataGridView.Columns.Add("capacity", "Дата создания");
            //mainTab_dataGridView.Columns.Add("check", "Выбрать");


            //создание столбца Загрузить
            DataGridViewButtonColumn button_column1 = new DataGridViewButtonColumn();
            button_column1.Name = "check";
            button_column1.HeaderText = "Загрузить";
            button_column1.FlatStyle = FlatStyle.Popup;
            button_column1.DefaultCellStyle.BackColor = Color.LightGreen;
            button_column1.Text = "+";



            //создание столбца Удалить
            DataGridViewButtonColumn button_column2 = new DataGridViewButtonColumn();
            button_column2.Name = "check";
            button_column2.HeaderText = "Удалить";
            button_column2.FlatStyle = FlatStyle.Popup;
            button_column2.DefaultCellStyle.BackColor = Color.LightCoral;
            button_column2.Text = "-";

            load_dataGridView.Columns.Add(button_column1);
            load_dataGridView.Columns.Add(button_column2);

            load_dataGridView.Columns[0].DataPropertyName = "Name";
            load_dataGridView.Columns[1].DataPropertyName = "Dates";
        }

        public void UpdateDGV()
        {
            BindingSource source1 = new BindingSource();
            source1.DataSource = Program.modeling.testmod;
            load_dataGridView.DataSource = source1;
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            if (N_UpDown.Text.Equals(""))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            else
            {
                //делаем все элементы вкладки "Терминалы" неактивными, кроме кнопки Перезагрузка
                leave_btn.Enabled = false;
                start_btn.Enabled = false;
                N_UpDown.Enabled = false;
                this.restart_btn.Enabled = true;
                Program.modeling.ModelingWithN(Convert.ToInt32(N_UpDown.Text));
                tabControl1.SelectTab(1);
                ActivateControls(1);
                //переход во вторую вкладку
                FillingDataGrid(dataGridView1);
            }
        }

        private void FillingDataGrid(DataGridView dgv)
        {
            for (int i = 1; i <= Program.modeling.N; i++)
            {
                dgv.Rows.Add();
                dgv.Rows[i - 1].Cells[0].Value = i.ToString();
            }
        }

        private bool IsFull(DataGridView dgv)
        {
            bool isfull = true;
            for (int j = 0; j < dgv.Rows.Count; j++)
                for (int i = 0; i < dgv.Rows[j].Cells.Count; i++)
                {
                    if (dgv.Rows[j].Cells[i].Value == null)
                        isfull = false;
                }
            return isfull;

        }

        double[] Tp;
        double[] dTp;

        double[] Tobr;
        double[] dtobr;

        private void SecondTab()
        {
            bool correct = true;
            if (IsFull(dataGridView1))
            {
                double result;
                Tp = new double[Program.modeling.N];
                dTp = new double[Program.modeling.N];

                for (int i = 0; i < Tp.Length; i++)
                {
                    if (Double.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out result) && result >= 0)
                    {
                        Tp[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Некорректное введенное значение", "Ошибка заполнения ячеек таблицы - Модель вычислительной системы, СамГТУ 2018",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        correct = false;
                    }

                    if (Double.TryParse(dataGridView1.Rows[i].Cells[2].Value.ToString(), out result) && result >= 0)
                    {
                        dTp[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Некорректное введенное значение", "Ошибка заполнения ячеек таблицы - Модель вычислительной системы, СамГТУ 2018",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        correct = false;
                    }
                }
                if (correct)
                {
                    Program.modeling.InitializationOfTpost(Tp);
                    Program.modeling.InitializationOfdtpost(dTp);
                    ActivateControls(2);
                    tabControl1.SelectTab(2);

                    if (dataGridView2.Rows.Count == 0)
                        FillingDataGrid(dataGridView2);
                }
            }

            else
            {
                MessageBox.Show("Заполните все ячейки.", "Ошибка заполнения ячеек таблицы - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ThirdTab()
        {
            bool correct = true;
            if (IsFull(dataGridView2))
            {
                double result;
                Tobr = new double[Program.modeling.N];
                dtobr = new double[Program.modeling.N];

                for (int i = 0; i < Tobr.Length; i++)
                {
                    if (Double.TryParse(dataGridView2.Rows[i].Cells[1].Value.ToString(), out result) && result >= 0)
                    {
                        Tobr[i] = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Некорректное введенное значение", "Ошибка заполнения ячеек таблицы - Модель вычислительной системы, СамГТУ 2018",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        correct = false;
                    }

                    if (Double.TryParse(dataGridView2.Rows[i].Cells[2].Value.ToString(), out result) && result >= 0)
                    {
                        dtobr[i] = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Некорректное введенное значение", "Ошибка заполнения ячеек таблицы - Модель вычислительной системы, СамГТУ 2018",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        correct = false;
                    }
                }
                if (correct)
                {
                    Program.modeling.InitializationOfTobr(Tobr);
                    Program.modeling.InitializationOfdtobr(dtobr);
                    ActivateControls(3);
                    tabControl1.SelectTab(3);
                    IsVisible();
                    IsVisible2();
                    sigma_tb.Enabled = false;
                    label4.Enabled = false;
                    input_rb.Checked = true;
                    input_dT_rb.Checked = true;
                }
            }

            else
            {
                MessageBox.Show("Заполните все ячейки.", "Ошибка заполнения ячеек таблицы - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ForthTab()
        {
            double result;
            uint res;

            if (!T_tb.Text.Equals("") && !K_tb.Text.Equals(""))
            {
                if (Double.TryParse(T_tb.Text, out result) && uint.TryParse(K_tb.Text, out res))
                {
                    if (input_dT_rb.Checked)
                    {
                        if (!dT_tb.Text.Equals(""))
                        {
                            if (Double.TryParse(dT_tb.Text, out result))
                            {
                                //тоже
                                if (input_rb.Checked)
                                {
                                    if (!deltat_tb.Text.Equals(""))
                                    {
                                        if (Double.TryParse(deltat_tb.Text, out result))
                                        {
                                            //cтарт1
                                            Program.modeling.EnizializedForRun1(Convert.ToDouble(T_tb.Text),
                                                Convert.ToInt32(K_tb.Text), Convert.ToDouble(dT_tb.Text),
                                                Convert.ToDouble(deltat_tb.Text));
                                            ActivateControls(4);
                                            tabControl1.SelectTab(4);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Некорректное введенное значение",
                                                "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Заполните все поля.", "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    if (!sigma_tb.Text.Equals(""))
                                    {
                                        if (Double.TryParse(sigma_tb.Text.Replace('.', ','), out result))
                                        {
                                            //старт2
                                            Program.modeling.EnizializedForRun2(Convert.ToDouble(T_tb.Text),
                                                Convert.ToInt32(K_tb.Text), Convert.ToDouble(dT_tb.Text),
                                                Convert.ToDouble(sigma_tb.Text.Replace('.', ',')));
                                            ActivateControls(4);
                                            tabControl1.SelectTab(4);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Некорректное введенное значение",
                                                "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Заполните все поля.", "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Некорректное введенное значение", "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Заполните все поля.", "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        if (!tochnostK_tb.Text.Equals(""))
                        {
                            int ResultbyTochnost = 0;
                            if (int.TryParse(tochnostK_tb.Text, out ResultbyTochnost))
                            {
                                //тоже
                                if (input_rb.Checked)
                                {
                                    if (!deltat_tb.Text.Equals(""))
                                    {
                                        if (Double.TryParse(deltat_tb.Text, out result))
                                        {
                                            //cтарт3
                                            if (canstart)
                                            {
                                                Program.modeling.EnizializedForRun3(Convert.ToDouble(T_tb.Text),
                                                    Convert.ToInt32(K_tb.Text), Convert.ToDouble(deltat_tb.Text),
                                                    Convert.ToInt32(tochnostK_tb.Text.Replace('.', ',')));
                                                ActivateControls(4);
                                                tabControl1.SelectTab(4);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Неккоректное значение k",
                                                    "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Некорректное введенное значение",
                                                "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Заполните все поля.", "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    if (!sigma_tb.Text.Equals(""))
                                    {
                                        if (Double.TryParse(sigma_tb.Text.Replace('.', ','), out result))
                                        {
                                            //старт4
                                            if (canstart)
                                            {
                                                Program.modeling.EnizializedForRun4(Convert.ToDouble(T_tb.Text),
                                                    Convert.ToInt32(K_tb.Text),
                                                    Convert.ToDouble(sigma_tb.Text.Replace('.', ',')),
                                                    Convert.ToInt32(tochnostK_tb.Text.Replace('.', ',')));
                                                ActivateControls(4);
                                                tabControl1.SelectTab(4);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Неккоректное значение k",
                                                    "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Некорректное введенное значение",
                                                "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Заполните все поля.", "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Некорректное введенное значение", "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Заполните все поля.", "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Некорректное введенное значение", "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля.", "Ошибка заполнения полей ввода - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            if (tabControl1.SelectedIndex == 4)
            {
                if (!isModeling)
                {
                    for (int i = 0; i < Program.modeling.N; i++)
                    {
                        comboBox1.Items.Add("Терминал " + (i + 1).ToString());
                        comboBox2.Items.Add("Терминал " + (i + 1).ToString());
                    }

                    ActivateControls(5);
                    comboBox1.Items.Add("Показать все");

                    //Program.modeling.GetInformation(this.dataGridView1,0);
                    StartModelling(dataGridView3);
                    comboBox1.SelectedIndex = Program.modeling.N;
                    dt_label.Text += " " + Program.modeling.deltat;
                    dTlabel.Text += " " + Program.modeling.deltaT;
                    dT_tb.Enabled = false;
                    isModeling = true;
                    IsVisibleSaveButton();
                }
                else
                {
                    dt_label.Text = "Δt = ";
                    dTlabel.Text = "ΔT = ";
                    dt_label.Text += Program.modeling.deltat;
                    dTlabel.Text += Program.modeling.deltaT;
                    dataGridView3.Rows.Clear();
                    dataGridView4.Rows.Clear();
                    comboBox2.Items.Clear();
                    comboBox1.Items.Clear();


                    for (int i = 0; i < chart1.Series.Count; i++)
                    {
                        chart1.Series[i].Points.Clear();
                    }

                    for (int i = 0; i < Program.modeling.N; i++)
                    {
                        comboBox1.Items.Add("Терминал " + (i + 1).ToString());
                        comboBox2.Items.Add("Терминал " + (i + 1).ToString());
                    }

                    ActivateControls(5);
                    comboBox1.Items.Add("Показать все");
                    Program.modeling.ToRestart();
                    IsVisibleSaveButton();
                    StartModelling(dataGridView3);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ActivateControls(int i)
        {
            foreach (Control item in tabControl1.TabPages[i].Controls)
            {
                item.Enabled = true;
            }
        }



        private void DeactivateControls()
        {
            for (int i = 1; i < tabControl1.TabCount; i++)
            {
                foreach (Control item in tabControl1.TabPages[i].Controls)
                {
                    item.Enabled = false;
                }
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void next_btn_1_Click(object sender, EventArgs e)
        {
            SecondTab();
        }

        private void next_btn_2_Click(object sender, EventArgs e)
        {
            ThirdTab();
        }

        bool isModeling = false;

        private void run_btn_Click(object sender, EventArgs e)
        {
            ForthTab();
        }

        private void late_btn_1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void late_btn_2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void restart_btn_Click(object sender, EventArgs e)
        {
            DeactivateControls();
            ActivateControls(0);
            this.N_UpDown.Enabled = true;
            this.start_btn.Enabled = true;
            this.leave_btn.Enabled = true;
            ToNullPages();
            Program.modeling.NewModeling();
            isModeling = false;
            IsVisibleSaveButton();
        }

        public void ToNullPages()
        {
            deltat_tb.Text = "";
            dT_tb.Text = "";
            K_tb.Text = "";
            N_UpDown.Text = "";
            sigma_tb.Text = "";
            T_tb.Text = "";

            dt_label.Text = "Δt =";
            dTlabel.Text = "ΔT =";

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            IsVisible();
            IsVisible2();
            sigma_tb.Enabled = false;
            label4.Enabled = false;
            input_rb.Checked = true;
            input_dT_rb.Checked = true;
            comboBox2.Items.Clear();
            comboBox1.Items.Clear();

            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].Points.Clear();
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
            {
                Point location = new Point(547, 27);


                schema_panel.Location = location;
                //schema_panel.Width = 200;
                //schema_panel.Height = 488;
                StructuralScheme.Width = 739;
                StructuralScheme.Height = 474;
                groupBox5.Width = 430;
                groupBox5.Height = 366;
                load_dataGridView.Width = 398;
                load_dataGridView.Height = 320;

                schema_panel.AutoSize = true;
                schema_panel.Size = new Size(StructuralScheme.Width + 14, StructuralScheme.Height + 14);

            }
            else
            {
                groupBox5.Width = 430;
                groupBox5.Height = 170;
                load_dataGridView.Width = 398;
                load_dataGridView.Height = 125;

                Point location = new Point(70, 330);
                schema_panel.Location = location;

                schema_panel.Width = 430;


                StructuralScheme.Width = 430 - 14;
                StructuralScheme.Height = 266;
                schema_panel.Height = 266 + 14;


            }

            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            resolution.Width = resolution.Width / 2;




            //StructuralScheme.Width = this.Size.Width;
            this.MinimumSize = resolution;
            this.tabControl1.Size = this.Size;
            //this.StructuralScheme.Width = this.Size.Width - 2;
            //this.panel5.Width = this.Size.Width - 1;
            //this.panel5.Height = this.Size.Height - 300;
            //this.StructuralScheme.Height = this.Size.Height - 300;
            Point p = new Point();
            p.X = this.Size.Width / 2 - 300;
            //p.Y = panel6.Location.Y;
            //this.panel6.Location = p;
        }

        private void IsVisible()
        {
            if (input_rb.Checked)
            {
                sigma_tb.Text = "";
                dt_tp_4_label.Visible = false;

                if (T_tb.Text.Equals(""))
                {
                    deltat_tb.Enabled = false;
                    sigma_tb.Enabled = false;
                    label4.Enabled = false;
                }
                else
                {
                    deltat_tb.Enabled = true;
                    sigma_tb.Enabled = false;
                    label4.Enabled = false;
                }
            }
            else
            {
                dt_tp_4_label.Visible = true;

                if (T_tb.Text.Equals(""))
                {
                    deltat_tb.Enabled = false;
                    sigma_tb.Enabled = false;
                    label4.Enabled = false;
                }

                else
                {
                    deltat_tb.Enabled = false;
                    sigma_tb.Enabled = true;
                    label4.Enabled = true;
                }
            }
        }

        private void IsVisible2()
        {
            if (input_dT_rb.Checked)
            {
                canstart = true;
                k_inform_label.Text = "Введите значение k в диапазоне от 1 до ";
                tochnostK_tb.Text = "";
                deltaT_tp_4_label.Visible = false;
                if (T_tb.Text.Equals(""))
                {
                    dT_tb.Enabled = false;
                    tochnostK_tb.Enabled = false;
                    label8.Enabled = false;
                    k_inform_label.Visible = false;
                }
                else
                {
                    //if (calc_dT_rb.Checked)
                    //{
                    dT_tb.Enabled = true;
                    tochnostK_tb.Enabled = false;
                    label8.Enabled = false;
                    k_inform_label.Visible = false;

                    //здесь добавляем коэффициент k

                    //double T = double.Parse(T_tb.Text);
                    //double dT = Program.modeling.CalculationOfIncrementdTT();
                    //int kinf = (int)Math.Truncate(T / dT);

                    //k_inform_label.Text += " " + kinf;
                    //}
                }
            }
            else
            {
                deltaT_tp_4_label.Visible = true;
                if (T_tb.Text.Equals(""))
                {
                    dT_tb.Enabled = false;
                    tochnostK_tb.Enabled = false;
                    label8.Enabled = false;
                    k_inform_label.Visible = false;
                }
                else
                {
                    k_inform_label.Text = "Введите значение k в диапазоне от 1 до ";
                    dT_tb.Enabled = false;
                    tochnostK_tb.Enabled = true;
                    label8.Enabled = true;
                    k_inform_label.Visible = true;

                    double T = double.Parse(T_tb.Text);
                    double dT = Program.modeling.CalculationOfIncrementdTT();
                    int kinf = (int)Math.Truncate(T / dT);

                    k_inform_label.Text += " " + kinf;
                }
            }
        }




        private void input_dT_rb_CheckedChanged(object sender, EventArgs e)
        {
            IsVisible2();
        }

        private void input_rb_CheckedChanged(object sender, EventArgs e)
        {
            IsVisible();
        }

        private void calc_dT_rb_CheckedChanged(object sender, EventArgs e)
        {
            IsVisible2();
        }

        private void calculate_rb_CheckedChanged(object sender, EventArgs e)
        {
            IsVisible();
        }

        public void StartModelling(DataGridView dgv)
        {

            foreach (Terminal t in Program.modeling.terminals)
            {
                t.CalculationOfTp();
            }
            double dt = Program.modeling.deltaT;
            while (Program.modeling.t <= Program.modeling.T)
            {

                if (Program.modeling.t >= dt)
                {
                    //Информация
                    Program.modeling.GetInformation(dgv, dt);
                    dt = dt + Program.modeling.deltaT;
                }

                Program.modeling.t = Program.modeling.t + Program.modeling.deltat;

                Program.modeling.ReceiptOfBids();

                if (Program.modeling.ISEmployed())
                {
                    Program.modeling.PackageTreatment();
                    Program.modeling.IsConflict();
                    Program.modeling.ReducingWindows();
                }
                else
                {
                    Program.modeling.Processing();
                    Program.modeling.ReducingWindows();
                }

            }
            //Program.modeling.GetInformation(this.dataGridView1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();

            if (comboBox1.SelectedIndex < Program.modeling.N)
            {
                dataGridView4.Visible = true;
                for (int i = 0; i < Program.modeling.reserved[comboBox1.SelectedIndex].Count; i++)
                {
                    if (i == 0)
                    {
                        dataGridView4.Rows.Add(comboBox1.SelectedIndex + 1,
                            Program.modeling.reserved[comboBox1.SelectedIndex][i].deltaT,
                            Program.modeling.reserved[comboBox1.SelectedIndex][i].S,
                            Program.modeling.reserved[comboBox1.SelectedIndex][i].Q,
                            Program.modeling.reserved[comboBox1.SelectedIndex][i].R,
                            Program.modeling.reserved[comboBox1.SelectedIndex][i].balance);
                        if (Program.modeling.reserved[comboBox1.SelectedIndex][i].balance == 0)
                            dataGridView4.Rows[i].Cells[5].Style.BackColor = Color.LightGreen;
                        else dataGridView4.Rows[i].Cells[5].Style.BackColor = Color.IndianRed;
                    }
                    else
                    {
                        dataGridView4.Rows.Add("", Program.modeling.reserved[comboBox1.SelectedIndex][i].deltaT,
                            Program.modeling.reserved[comboBox1.SelectedIndex][i].S,
                            Program.modeling.reserved[comboBox1.SelectedIndex][i].Q,
                            Program.modeling.reserved[comboBox1.SelectedIndex][i].R,
                            Program.modeling.reserved[comboBox1.SelectedIndex][i].balance);
                        if (Program.modeling.reserved[comboBox1.SelectedIndex][i].balance == 0)
                            dataGridView4.Rows[i].Cells[5].Style.BackColor = Color.LightGreen;
                        else dataGridView4.Rows[i].Cells[5].Style.BackColor = Color.IndianRed;
                    }
                }
            }
            else
            {
                dataGridView4.Visible = false;
            }
        }

        private void late_btn_3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();

            chart1.ChartAreas[0].AxisX.Interval = Math.Truncate(Program.modeling.t / 10);

            foreach (SQR sqr in Program.modeling.reserved[comboBox2.SelectedIndex])
            {
                chart1.Series[0].Points.AddXY(sqr.deltaT, sqr.S);
                chart1.Series[1].Points.AddXY(sqr.deltaT, sqr.Q);
                chart1.Series[2].Points.AddXY(sqr.deltaT, sqr.R);
            }
        }

        private void k_inform_label_Click(object sender, EventArgs e)
        {
        }

        private void T_tb_TextChanged(object sender, EventArgs e)
        {
            IsVisible();
            IsVisible2();
            k_inform_label.Text = "Введите значение k в диапазоне от 1 до ";
            if (calc_dT_rb.Checked)
            {
                try
                {
                    double T = double.Parse(T_tb.Text);

                    double dT = Program.modeling.CalculationOfIncrementdTT();
                    int kinf = (int)Math.Truncate(T / dT);

                    k_inform_label.Text += " " + kinf;
                }
                catch
                {
                    k_inform_label.Text = "Введите значение k в диапазоне от 1 до ";
                }
            }
        }

        bool canstart = true;

        private void tochnostK_tb_TextChanged(object sender, EventArgs e)
        {
            deltaT_tp_4_label.Text = "ΔT = ";
            if (calc_dT_rb.Checked)
            {
                if (!tochnostK_tb.Text.Equals(""))
                {
                    double T = double.Parse(T_tb.Text);

                    double dT = Program.modeling.CalculationOfIncrementdTT();

                    if ((dT * Convert.ToDouble(tochnostK_tb.Text)) > Convert.ToDouble(T_tb.Text))
                    {
                        deltaT_tp_4_label.Text = "Значение k некорректно!";
                        canstart = false;
                    }
                    else
                    {
                        deltaT_tp_4_label.Text += (dT * Convert.ToDouble(tochnostK_tb.Text)).ToString();
                        canstart = true;
                    }
                }
                else
                {
                    deltaT_tp_4_label.Text = "ΔT = ";
                }
            }
            else
            {
                deltaT_tp_4_label.Text = "ΔT = ";
            }
        }

        private void dT_tb_TextChanged(object sender, EventArgs e)
        {
        }

        //регулярки
        bool jh;

        private void tochnostK_tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
                jh = true;
            else jh = false;
        }

        private void tochnostK_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (jh == false)
            {
                string c = e.KeyChar.ToString();
                if (Regex.Match(c, @"[\D]").Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void sigma_tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
                jh = true;
            else jh = false;
        }


        private void sigma_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && (ch != '.' || sigma_tb.Text.Contains(".")))
            {
                if (sigma_tb.Text.IndexOf('.') == 0) sigma_tb.Text = "0" + sigma_tb.Text;
                e.Handled = true;
            }
        }

        private void sigma_tb_Leave(object sender, EventArgs e)
        {
            if (sigma_tb.Text.IndexOf('.') == 0) sigma_tb.Text = "0" + sigma_tb.Text;
        }

        private void sigma_tb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dt_tp_4_label.Text = "Δt = ";
                if (calculate_rb.Checked)
                {
                    if (!sigma_tb.Text.Equals(""))
                    {
                        double dt = Program.modeling.CalculationOfIncrementdTT();

                        dt_tp_4_label.Text +=
                        (Program.modeling.CalculationOfIncrementModelTime(
                            Convert.ToDouble(sigma_tb.Text.Replace(".", ",")))).ToString();
                    }
                    else
                    {
                        dt_tp_4_label.Text = "Δt = ";
                    }
                }
                else
                {
                    dt_tp_4_label.Text = "Δt = ";
                }
            }
            catch
            {
            }
        }

        private void T_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && (ch != '.' || T_tb.Text.Contains(".")))
            {
                if (T_tb.Text.IndexOf('.') == 0) T_tb.Text = "0" + T_tb.Text;
                e.Handled = true;
            }
        }

        private void T_tb_Leave(object sender, EventArgs e)
        {
            if (T_tb.Text.IndexOf('.') == 0) T_tb.Text = "0" + T_tb.Text;
        }

        private void dT_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && (ch != '.' || dT_tb.Text.Contains(".")))
            {
                if (dT_tb.Text.IndexOf('.') == 0) dT_tb.Text = "0" + dT_tb.Text;
                e.Handled = true;
            }
        }

        private void dT_tb_Leave(object sender, EventArgs e)
        {
            if (dT_tb.Text.IndexOf('.') == 0) dT_tb.Text = "0" + dT_tb.Text;
        }

        private void deltat_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && (ch != '.' || deltat_tb.Text.Contains(".")))
            {
                if (deltat_tb.Text.IndexOf('.') == 0) deltat_tb.Text = "0" + deltat_tb.Text;
                e.Handled = true;
            }
        }

        private void deltat_tb_Leave(object sender, EventArgs e)
        {
            if (deltat_tb.Text.IndexOf('.') == 0) deltat_tb.Text = "0" + deltat_tb.Text;
        }

        //регулярка для K
        private void K_tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
                jh = true;
            else jh = false;
        }

        private void K_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (jh == false)
            {
                string c = e.KeyChar.ToString();
                if (Regex.Match(c, @"[\D]").Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void tochnostK_tb_Leave(object sender, EventArgs e)
        {
        }

        private void N_tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
                jh = true;
            else jh = false;
        }

        private void N_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (jh == false)
            {
                string c = e.KeyChar.ToString();
                if (Regex.Match(c, @"[\D]").Success)
                {
                    e.Handled = true;
                }
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.SetClip(e.Bounds);
            string text = tabControl1.TabPages[e.Index].Text;
            SizeF sz = e.Graphics.MeasureString(text, e.Font);

            bool bSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            using (SolidBrush b = new SolidBrush(bSelected ? Color.LightSkyBlue : SystemColors.Control))
                e.Graphics.FillRectangle(b, e.Bounds);

            using (SolidBrush b = new SolidBrush(bSelected ? SystemColors.ControlText : SystemColors.ControlText))
                e.Graphics.DrawString(text, e.Font, b, e.Bounds.X + 2, e.Bounds.Y + (e.Bounds.Height - sz.Height) / 2);

            if (tabControl1.SelectedIndex == e.Index)
            {
                e.DrawFocusRectangle();
            }

            e.Graphics.ResetClip();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void IsVisibleHead()
        {
            if (set_settings_rb.Checked)
            {
                tests_cb.Visible = false;
                load_test_btn.Visible = false;
            }
            else
            {
                tests_cb.Visible = true;
                load_test_btn.Visible = true;
            }
        }

        private void set_settings_rb_CheckedChanged(object sender, EventArgs e)
        {
            IsVisibleHead();
        }

        private void choose_settings_rb_CheckedChanged(object sender, EventArgs e)
        {
            IsVisibleHead();
        }

        private void IsVisibleSaveButton()
        {
            if (isModeling) saveToTest_btn.Enabled = true;
            else
            {
                saveToTest_btn.Enabled = false;
            }
        }

        private void saveToTest_btn_Click(object sender, EventArgs e)
        {
            if (!save_textBox.Text.Equals("") && !save_textBox.Text.Equals("Введите название"))
            {
                try
                {
                    Program.modeling.testmod.Add(new testModeling(Program.modeling, save_textBox.Text));
                    BinaryFormatter BF = new BinaryFormatter();

                    using (
                        Stream fStream = new FileStream(@"Modelings.bin", FileMode.Create, FileAccess.Write,
                            FileShare.None))
                    {
                        BF.Serialize(fStream, Program.modeling.testmod);
                        fStream.Close();
                    }

                    //проверить на существование аналогчисного объекта
                    tests_cb.Items.Clear();
                    for (int i = 0; i < Program.modeling.testmod.Count; i++)
                    {
                        tests_cb.Items.Add(Program.modeling.testmod[i].name);
                    }
                    MessageBox.Show("Данные успешно сохранены.", "Запись данных - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    save_textBox.Clear();

                }
                catch (IOException)
                {

                    MessageBox.Show("Ошибка при сохранении данных в файл", "Ошибка - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            UpdateDGV();

        }

        public void AllEnabled()
        {
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                foreach (Control item in tabControl1.TabPages[i].Controls)
                {
                    item.Enabled = true;
                }
            }

        }

        public void LoadAll()
        {
            leave_btn.Enabled = false;
            start_btn.Enabled = false;
            N_UpDown.Enabled = false;
            this.restart_btn.Enabled = true;

            Modeling m = Program.modeling;

            for (int i = 0; i < Program.modeling.terminals.Count; i++)
            {
                dataGridView1.Rows.Add((i + 1).ToString(), m.terminals[i].Tpost, m.terminals[i].dtpost);
            }

            for (int i = 0; i < Program.modeling.terminals.Count; i++)
            {
                dataGridView2.Rows.Add((i + 1).ToString(), m.terminals[i].Tobr, m.terminals[i].dtobr);
            }

            //AllEnabled();
            T_tb.Text = m.T.ToString();
            K_tb.Text = m.K.ToString();
            input_dT_rb.Checked = true;
            dT_tb.Text = m.deltaT.ToString();
            input_rb.Checked = true;
            deltat_tb.Text = m.deltat.ToString();
            AllEnabled();
            IsVisible();
            IsVisible2();


        }

        private void load_test_btn_Click(object sender, EventArgs e)
        {
            try
            {
                restart_btn_Click(sender, e);
                if (Program.modeling.testmod.Count > 0)
                {
                    Program.modeling.ModelingWithTest(Program.modeling.testmod[tests_cb.SelectedIndex]);
                }

                N_UpDown.Text = Program.modeling.N.ToString();
                LoadAll();
                run_btn_Click(sender, e);
                dT_tb.Enabled = true;
            }

            catch
            {

            }
        }

        private void save_textBox_TextChanged(object sender, EventArgs e)
        {
            if (!save_textBox.Text.Equals("") && !save_textBox.Text.Equals("Введите название"))
            {

            }

            else
            {

            }
        }

        private void save_textBox_Leave(object sender, EventArgs e)
        {
            if (save_textBox.Text == "")
            {
                save_textBox.Text = "Введите название";
                save_textBox.ForeColor = Color.Gray;
                save_textBox.PasswordChar = '\0';
            }
        }


        private void tabPage5_MouseClick(object sender, MouseEventArgs e)
        {
            focus_textBox.Focus();
        }

        private void save_textBox_Enter(object sender, EventArgs e)
        {
            save_textBox.Text = "";
            save_textBox.ForeColor = Color.Black;
        }

        private void param_groupBox_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void deltaT_tp_4_label_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void load_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {


                if (e.ColumnIndex == 2) //индекс столбца, в которой находится кнопка.
                {
                    try
                    {
                        restart_btn_Click(sender, e);
                        if (Program.modeling.testmod.Count > 0)
                        {
                            Program.modeling.ModelingWithTest(Program.modeling.testmod[e.RowIndex]);
                        }

                        N_UpDown.Text = Program.modeling.N.ToString();
                        LoadAll();
                        run_btn_Click(sender, e);
                        dT_tb.Enabled = true;
                    }
                    catch
                    {

                    }
                }

                if (e.ColumnIndex == 3) //Удалить исследование
                {
                    DialogResult result = MessageBox.Show(string.Format("Вы уверены, что хотите удалить исследование \"{0}\" от {1} ?", Program.modeling.testmod[e.RowIndex].name, Program.modeling.testmod[e.RowIndex].Date), "Удаление - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            restart_btn_Click(sender, e);
                            Program.modeling.testmod.RemoveAt(e.RowIndex);
                            try
                            {
                                BinaryFormatter BF = new BinaryFormatter();

                                using (
                                    Stream fStream = new FileStream(@"Modelings.bin", FileMode.Create, FileAccess.Write,
                                        FileShare.None))
                                {
                                    BF.Serialize(fStream, Program.modeling.testmod);
                                    fStream.Close();
                                }

                            }
                            catch (IOException)
                            {

                                MessageBox.Show("Ошибка при сохранении данных в файл", "Ошибка - Модель вычислительной системы, СамГТУ 2018", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }

                        catch
                        {

                        }

                        finally
                        {
                            UpdateDGV();
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        return;
                    }


                }
            }



        }

        private void leave_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void N_UpDown_ValueChanged(object sender, EventArgs e)
        {

            //if (!Valid(this.N_UpDown.Text))
            //    this.N_UpDown.BackColor = Color.LightCoral;
            //else
            //    this.N_UpDown.BackColor = Color.White;

        }

        bool Valid(string text)
        {
            int i = 0;
            if (!int.TryParse(text, out i))
                return false;
            if (i < 0 || i > 100)
                return false;
            else return true;
        }

        private void N_label_Click(object sender, EventArgs e)
        {

        }

  
    }
}

