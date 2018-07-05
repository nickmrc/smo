namespace Lab_number_7_Model
{
    partial class ChangePass_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePass_Form));
            this.password_groupBox = new System.Windows.Forms.GroupBox();
            this.changepass_button = new System.Windows.Forms.Button();
            this.newpass_textBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.newpass_label = new System.Windows.Forms.Label();
            this.oldpass_label = new System.Windows.Forms.Label();
            this.password_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // password_groupBox
            // 
            this.password_groupBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.password_groupBox.Controls.Add(this.changepass_button);
            this.password_groupBox.Controls.Add(this.newpass_textBox);
            this.password_groupBox.Controls.Add(this.textBox1);
            this.password_groupBox.Controls.Add(this.newpass_label);
            this.password_groupBox.Controls.Add(this.oldpass_label);
            this.password_groupBox.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password_groupBox.ForeColor = System.Drawing.Color.DimGray;
            this.password_groupBox.Location = new System.Drawing.Point(12, 12);
            this.password_groupBox.Name = "password_groupBox";
            this.password_groupBox.Size = new System.Drawing.Size(440, 128);
            this.password_groupBox.TabIndex = 2;
            this.password_groupBox.TabStop = false;
            this.password_groupBox.Text = "Изменить пароль для входа";
            // 
            // changepass_button
            // 
            this.changepass_button.BackColor = System.Drawing.Color.LightSkyBlue;
            this.changepass_button.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.changepass_button.Location = new System.Drawing.Point(322, 93);
            this.changepass_button.Name = "changepass_button";
            this.changepass_button.Size = new System.Drawing.Size(81, 25);
            this.changepass_button.TabIndex = 3;
            this.changepass_button.Text = " Изменить";
            this.changepass_button.UseVisualStyleBackColor = false;
            this.changepass_button.Click += new System.EventHandler(this.changepass_button_Click);
            // 
            // newpass_textBox
            // 
            this.newpass_textBox.Location = new System.Drawing.Point(187, 62);
            this.newpass_textBox.Name = "newpass_textBox";
            this.newpass_textBox.PasswordChar = '*';
            this.newpass_textBox.Size = new System.Drawing.Size(216, 25);
            this.newpass_textBox.TabIndex = 2;
            this.newpass_textBox.TextChanged += new System.EventHandler(this.newpass_textBox_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(187, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(216, 25);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // newpass_label
            // 
            this.newpass_label.AutoSize = true;
            this.newpass_label.ForeColor = System.Drawing.Color.DarkBlue;
            this.newpass_label.Location = new System.Drawing.Point(31, 65);
            this.newpass_label.Name = "newpass_label";
            this.newpass_label.Size = new System.Drawing.Size(150, 18);
            this.newpass_label.TabIndex = 0;
            this.newpass_label.Text = "Введите новый пароль:";
            // 
            // oldpass_label
            // 
            this.oldpass_label.AutoSize = true;
            this.oldpass_label.ForeColor = System.Drawing.Color.DarkBlue;
            this.oldpass_label.Location = new System.Drawing.Point(31, 34);
            this.oldpass_label.Name = "oldpass_label";
            this.oldpass_label.Size = new System.Drawing.Size(156, 18);
            this.oldpass_label.TabIndex = 0;
            this.oldpass_label.Text = "Введите старый пароль:";
            // 
            // ChangePass_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(463, 160);
            this.Controls.Add(this.password_groupBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePass_Form";
            this.Text = "Сменить пароль";
            this.Load += new System.EventHandler(this.ChangePass_Form_Load);
            this.password_groupBox.ResumeLayout(false);
            this.password_groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox password_groupBox;
        private System.Windows.Forms.Button changepass_button;
        private System.Windows.Forms.TextBox newpass_textBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label newpass_label;
        private System.Windows.Forms.Label oldpass_label;
    }
}