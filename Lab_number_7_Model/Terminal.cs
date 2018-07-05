using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_number_7_Model
{
    [Serializable]
    class Terminal
    {
        public int S;
        public int Q;
        public int R;

        public int P;

        public double Tpost;
        public double dtpost;
        public double tp;

        public double Tobr { set; get; }
        public double dtobr { set; get; }
        public double to;

        public Terminal() { }

        public Terminal(Terminal t)
        {
            this.Tpost = t.Tpost;
            this.dtpost = t.dtpost;
            this.Tobr = t.Tobr;
            this.dtobr = t.dtobr;
            
        }
        public void ToRestartTerminal()
        {
            this.S = 0;
            this.Q = 0;
            this.R = 0;
            this.P = 0;
        }



        public void CalculationOfTp()
        {
            Random rand = new Random();
            this.tp = (this.Tpost - this.dtpost) + 2 * this.dtpost * rand.NextDouble();
        }


        public void CalculationOfto()
        {
            Random rand = new Random();
            this.to = (this.Tobr - this.dtobr) + 2 * this.dtobr *rand.NextDouble();
        }




    }
}
