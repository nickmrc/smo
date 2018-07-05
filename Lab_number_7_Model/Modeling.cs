using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_number_7_Model
{
    [Serializable]
    class Modeling
    {
        public class SQR
        {
            public double deltaT;
            public int S;
            public int Q;
            public int R;
            public int balance;
            public SQR(double dt, int S, int Q, int R)
            {
                this.deltaT = dt;
                this.S = S;
                this.Q = Q;
                this.R = R;
                this.balance = S - Q - R;
            }
        }



        public void ToRestart()
        {
            foreach (Terminal t in this.terminals)
            {
                t.ToRestartTerminal();
            }
            this.F = -1;
            this.t = 0;

            this.massindex = new int[N];
            for (int i = 0; i < N; i++)
            {
                massindex[i] = -1;

            }
            reserved.Clear();
            for (int i = 0; i < N; i++)
            {
                reserved.Add(i, new List<SQR>());
            }
            this.k = 0;

        }
        public List<Terminal> terminals;
        public Dictionary<int, List<SQR>> reserved = new Dictionary<int, List<SQR>>();
        public double t { get; set; }
        public double deltat { get; set; }
        public double T { get; set; }
        public double deltaT { get; set; }
        public int N { get; set; }
        public int K { get; set; }
        public double ScalingFactor { get; set; }
        public int F { get; set; }
        public int k { get; set; }
        public int S { get; private set; }
        public List<testModeling> testmod = new List<testModeling>();

      

        public void LoadTest(testModeling tm)
        {
            this.N = tm.N;
            this.ToRestart();
            this.deltat = tm.deltat;
            this.deltaT = tm.deltaT;
            this.T = tm.T;
            this.K = tm.K;

            this.terminals = null;

            for (int i = 0; i < N; i++)
            {
                this.terminals.Add(new Terminal( tm.terminals[i]));
            }

        }

        public void ModelingWithTest(testModeling tm)
        {
            this.N = tm.N;
            F = -1;
            //this.ToRestart();
            this.deltat = tm.deltat;
            this.deltaT = tm.deltaT;
            this.T = tm.T;
            this.K = tm.K;

            this.terminals = new List<Terminal>();

            for (int i = 0; i < N; i++)
            {
                this.terminals.Add(new Terminal(tm.terminals[i]));
            }
            this.massindex = new int[N];
            for (int i = 0; i < N; i++)
            {
                massindex[i] = -1;

            }
            reserved.Clear();
            for (int i = 0; i < N; i++)
            {
                reserved.Add(i, new List<SQR>());
            }
            this.k = 0;
        }

        public int[] massindex;
        public void NewModeling()
        {
            terminals = new List<Terminal>();
            reserved.Clear();
            t = new double();
            deltat = new double();
            T = new double();
            deltaT = new double();
            N = new int();
            K = new int();
            ScalingFactor = new double();
       F = -1;
            k = new int();
            S = new int();
      
    }

        public Modeling()
        { }

        public void ModelingWithN(int N)
        {
            this.N = N;

            this.F = -1;
            this.t = 0;
            terminals = new List<Terminal>(this.N);

            for (int i = 0; i < N; i++)
            {
                terminals.Add(new Terminal());
            }


            this.t = 0;
            this.massindex = new int[N];
            for (int i = 0; i < N; i++)
            {
                massindex[i] = -1;

            }
            for (int i = 0; i < N; i++)
            {
                reserved.Add(i, new List<SQR>());
            }
        }
        public Modeling(int N)
        {
            this.N = N;

            this.F = -1;
            this.t = 0;
            terminals = new List<Terminal>(this.N);

            for (int i = 0; i < N; i++)
            {
                terminals.Add(new Terminal());
            }


            this.t = 0;
            this.massindex = new int[N];
            for (int i = 0; i < N; i++)
            {
                massindex[i] = -1;

            }
            for (int i = 0; i < N; i++)
            {
                reserved.Add(i, new List<SQR>());
            }

            


        }

        public void ToEnizializedModeling(double T, double deltaT, int K)
        {
            this.T = T;
            this.deltaT = deltaT;
            this.K = K;
        }

        public void EnizializedForRun1(double T, int K, double dT, double dt )
        {
            this.T = T;
            this.K = K;
            this.deltaT = dT;
            this.deltat = dt;
        }

        public void EnizializedForRun2(double T, int K, double dT, double sigma)
        {
            this.T = T;
            this.K = K;
            this.deltaT = dT;
            this.ScalingFactor = sigma;
            CalculationOfIncrementModelTime();
        }

        public void EnizializedForRun3(double T, int K, double dt, int tochnost)
        {
            this.T = T;
            this.K = K;
            this.deltat = dt;
            CalculationOfIncrementdT(tochnost);
        }

        public void EnizializedForRun4(double T, int K, double sigma, int tochnost)
        {
            this.T = T;
            this.K = K;
            this.ScalingFactor = sigma;
            CalculationOfIncrementModelTime();
            CalculationOfIncrementdT(tochnost);
        }



        //инициализация среднего времени поступления
        public void InitializationOfTpost(double[] Tpost)
        {
            for (int i = 0; i < Tpost.Length; i++)
            {
                this.terminals[i].Tpost = Tpost[i];
            }
        }

        //инициализация вероятного отклонения времени поступления заявок
        public void InitializationOfdtpost(double[] dtpost)
        {
            for (int i = 0; i < dtpost.Length; i++)
            {
                this.terminals[i].dtpost = dtpost[i];
            }
        }

        //инициализация времени обработки
        public void InitializationOfTobr(double[] Tobr)
        {
            for (int i = 0; i < Tobr.Length; i++)
            {
                this.terminals[i].Tobr = Tobr[i];
            }
        }

        //инициализация вероятного отклонения времени обработки
        public void InitializationOfdtobr(double[] dtobr)
        {
            for (int i = 0; i < dtobr.Length; i++)
            {
                this.terminals[i].dtobr = dtobr[i];
            }
        }

        //инициализация S,Q,R,P
        public void SQRPInitialization()
        {
            for (int i = 0; i < this.N; i++)
            {
                this.terminals[i].S = 0;
                this.terminals[i].Q = 0;
                this.terminals[i].R = 0;
                this.terminals[i].P = 0;
            }
        }

        //рассчет дельта-t
        public void CalculationOfIncrementModelTime()
        {
          
            double minimum = this.terminals[0].Tpost - this.terminals[0].dtpost;
            for (int i = 0; i < N; i++)
            {
                if (minimum > this.terminals[i].Tpost - this.terminals[i].dtpost)
                    minimum = this.terminals[i].Tpost - this.terminals[i].dtpost;

                if (minimum > this.terminals[i].Tobr - this.terminals[i].dtobr)
                    minimum = this.terminals[i].Tobr - this.terminals[i].dtobr;

                if (minimum <= 0) minimum = 1;

                this.deltat = minimum * this.ScalingFactor;
                
            }

        }

        public double CalculationOfIncrementModelTime(double scf)
        {

            double minimum = this.terminals[0].Tpost - this.terminals[0].dtpost;
            for (int i = 0; i < N; i++)
            {
                if (minimum > this.terminals[i].Tpost - this.terminals[i].dtpost)
                    minimum = this.terminals[i].Tpost - this.terminals[i].dtpost;

                if (minimum > this.terminals[i].Tobr - this.terminals[i].dtobr)
                    minimum = this.terminals[i].Tobr - this.terminals[i].dtobr;

                if (minimum <= 0) minimum = 1;

                this.deltat = minimum * scf;

            }

            return deltat;

        }
        public void CalculationOfIncrementdT()
        {
            
            double maximum = this.terminals[0].Tpost + this.terminals[0].dtpost;
            for (int i = 0; i < N; i++)
            {
                if (maximum < this.terminals[i].Tpost + this.terminals[i].dtpost)
                    maximum = this.terminals[i].Tpost + this.terminals[i].dtpost;

                if (maximum < this.terminals[i].Tobr + this.terminals[i].dtobr)
                    maximum = this.terminals[i].Tobr + this.terminals[i].dtobr;

                this.deltaT = maximum;
              
            }

        }

        public void CalculationOfIncrementdT(int tochnost)
        {

            double maximum = this.terminals[0].Tpost + this.terminals[0].dtpost;
            for (int i = 0; i < N; i++)
            {
                if (maximum < this.terminals[i].Tpost + this.terminals[i].dtpost)
                    maximum = this.terminals[i].Tpost + this.terminals[i].dtpost;

                if (maximum < this.terminals[i].Tobr + this.terminals[i].dtobr)
                    maximum = this.terminals[i].Tobr + this.terminals[i].dtobr;

                this.deltaT = maximum*tochnost;

            }

        }
        public double CalculationOfIncrementdTT()
        {

            double maximum = this.terminals[0].Tpost + this.terminals[0].dtpost;
            for (int i = 0; i < N; i++)
            {
                if (maximum < this.terminals[i].Tpost + this.terminals[i].dtpost)
                    maximum = this.terminals[i].Tpost + this.terminals[i].dtpost;

                if (maximum < this.terminals[i].Tobr + this.terminals[i].dtobr)
                    maximum = this.terminals[i].Tobr + this.terminals[i].dtobr;
                //надо-не надо?
                this.deltaT = maximum;
                
            }
            return maximum;
        }
        //проверка на конец
        public bool IsFinish()
        {
            if (this.t >= this.T) return true;
            else return false;
        }

        //вывод информации о состоянии системы
        public string ReturnInformation()
        {
            string s = "";
            return s;
        }

        //поступление заявок
        public void ReceiptOfBids()
        {

            for (int i = 0; i < N; i++)
            {
                terminals[i].tp = terminals[i].tp - this.deltat;
                if (terminals[i].tp <= 0)
                {
                    //if (terminals[i].P < 1)
                    //{
                        terminals[i].S++;
                        terminals[i].Q++;
                        this.terminals[i].CalculationOfTp();
                    //}
                }
            }
        }

        //проверка на занятость ЦЭВМ
        public bool ISEmployed()
        {
            if (this.F < 0) return true;
            else return false;
        }

        //продвижение обработки
        public void Processing()
        {
            this.terminals[F].to = this.terminals[F].to - this.deltat;
            if (terminals[F].to <= 0)
            {
                terminals[F].R++;

                terminals[F].Q--;
                F = -1;
            }
        }

        //уменьшение окон задержки
        public void ReducingWindows()
        {
            for (int i = 0; i < N; i++)
            {
                if (terminals[i].P > 0) terminals[i].P--;
            }
        }

        //формирование пакетов на обработку
        public void PackageTreatment()
        {
            this.k = 0;

            for (int i = 0; i < N; i++)
            {
                if (terminals[i].P < 1)
                {
                    if (terminals[i].Q > 0)
                    {

                        this.massindex[k] = i;
                        k = k + 1;
                    }
                }

            }
        }

        public void IsConflict()
        {
            Random rand = new Random();
            if (this.k > 0)
            {
                if (k > 1)
                {
                    List<int> pmas = new List<int>(K);
                    int KK = N;

                    if (K <= N)
                    {
                        for (int i = 0, j = 1; i < N; i++, j++)
                        {
                            pmas.Add(j);
                            if (j == K)
                            {
                                j = 1;
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0, j = 1; i < K; i++, j++)
                        {
                            pmas.Add(j);
                         

                        }
                    }

                    for (int i = 0; i < k; i++)
                    {

                        int randindex = rand.Next(0, pmas.Count);
                        terminals[massindex[i]].P = pmas[randindex];
                        pmas.RemoveAt(randindex);




                        massindex[i] = -1;
                    }
                }
                else { F = this.massindex[0]; terminals[F].CalculationOfto(); }
            }

        }

        public void control()
        {
            this.ToString();
        }

        public void GetInformation(DataGridView dgv, double dt)
        {
            for (int i = 0; i < Program.modeling.N; i++)
            {
                int balance = Program.modeling.terminals[i].S - Program.modeling.terminals[i].Q - Program.modeling.terminals[i].R;
                reserved[i].Add(new SQR(dt, terminals[i].S, terminals[i].Q, terminals[i].R));

                if (i == 0)
                {
                   
                    dgv.Rows.Add(dt, i + 1, Program.modeling.terminals[i].S, Program.modeling.terminals[i].Q, Program.modeling.terminals[i].R, balance);
                    if (balance == 0) dgv.Rows[dgv.Rows.Count-1].Cells[5].Style.BackColor = Color.LightGreen;
                    else dgv.Rows[dgv.Rows.Count - 1].Cells[5].Style.BackColor = Color.IndianRed;

                }
                else
                {
                    dgv.Rows.Add("", i + 1, Program.modeling.terminals[i].S, Program.modeling.terminals[i].Q, Program.modeling.terminals[i].R, balance);
                    if (balance == 0) dgv.Rows[dgv.Rows.Count - 1].Cells[5].Style.BackColor = Color.LightGreen;
                    else dgv.Rows[dgv.Rows.Count - 1].Cells[5].Style.BackColor = Color.IndianRed;
                }

            }
        }

        public void StartModelling()
        {

            foreach (Terminal t in Program.modeling.terminals)
            {
                t.CalculationOfTp();
            }
            double dt = Program.modeling.deltaT;
            while (Program.modeling.t >= Program.modeling.T)
            {

                if (t >= dt)
                {
                    //Информация

                    dt = dt + Program.modeling.deltaT;
                }
                Program.modeling.t = Program.modeling.t + Program.modeling.deltat;
                Program.modeling.ReceiptOfBids();

                if (Program.modeling.ISEmployed())
                {
                    PackageTreatment();
                    IsConflict();
                }
                else
                {
                    Processing();
                    ReducingWindows();
                }


            }
            //ВЫВЕСТИ ИНФОРМАЦИЮ ПЕРЕД ВЫХОДОМ


        }

    }
    //запуск процесса моделирования


}

