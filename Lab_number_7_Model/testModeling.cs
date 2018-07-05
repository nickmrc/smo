using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_number_7_Model
{
    [Serializable]
    class testModeling
    {
        public string name;
        public DateTime dt;

        public string Name
        {
            get { return this.name; }
        }

        public DateTime Date
        {
            get { return this.dt; }
        }

        public string Dates
        {
            get { return this.dt.ToString("F"); }
        }

        public List<Terminal> terminals;
        public double deltat { get; set; }
        public double T { get; set; }
        public double deltaT { get; set; }
        public int N { get; set; }
        public int K { get; set; }

        public testModeling(Modeling modeling)
        {
            this.InizializedTest(modeling);
        }


        public testModeling(Modeling modeling, String s)
        {
            this.name = s;
            this.dt = DateTime.Now;
            this.InizializedTest(modeling);
        }

        public void InizializedTest(Modeling modeling)
        {
            this.deltat = modeling.deltat;
            this.T = modeling.T;
            this.deltaT = modeling.deltaT;
            this.N = modeling.N;
            this.K = modeling.K;

            this.terminals = new List<Terminal>();
            for (int i = 0; i < modeling.terminals.Count; i++)
            {
                this.terminals.Add(new Terminal(modeling.terminals[i]));
                
            }

            
        }
    }
}
