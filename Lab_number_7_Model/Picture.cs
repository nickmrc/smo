using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Lab_number_7_Model
{
    public partial class Picture : Form
    {
        public Picture()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.4;
            if (this.Opacity == 1) { timer1.Stop(); }
        }

        private void Picture_Load(object sender, EventArgs e){}
    }
}
