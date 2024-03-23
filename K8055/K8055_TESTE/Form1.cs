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
using System.Runtime.InteropServices;


namespace K8055_TESTE
{
    public partial class Form1 : Form
    {
        Thread t;
        public int aux = 0;
        public int aux2 = 0;
        public int ligado = -1;
        public Boolean board_open = false;
        public Form1()
        {
            InitializeComponent();
            Luzes.Text = "Luzes Desligadas";
        }

        public static class K8055
        {
            [DllImport("K8055D.dll")]
            public static extern int OpenDevice(int CardAdress);

            [DllImport("K8055D.dll")]
            public static extern void CloseDevice();

            [DllImport("K8055D.dll")]
            public static extern int ReadAnalogChannel(int Channel);

            [DllImport("K8055D.dll")]
            public static extern void ReadAllAnalog(ref int Data1, ref int Data2);

            [DllImport("K8055D.dll")]
            public static extern void ClearAnalogChannel(int Channel);

            [DllImport("K8055D.dll")]
            public static extern void ClearAllAnalog();

            [DllImport("K8055D.dll")]
            public static extern void OutputAnalogChannel(int Channel, int Data);

            [DllImport("K8055D.dll")]
            public static extern void OutputAllAnalog(int Data1, int Data2);

            [DllImport("K8055D.dll")]
            public static extern void SetAnalogChannel(int Channel);

            [DllImport("K8055D.dll")]
            public static extern void SetAllAnalog();

            [DllImport("K8055D.dll")]
            public static extern void ClearAllDigital();

            [DllImport("K8055D.dll")]
            public static extern void ClearDigitalChannel(int Channel);

            [DllImport("K8055D.dll")]
            public static extern void SetAllDigital();

            [DllImport("K8055D.dll")]
            public static extern void SetDigitalChannel(int Channel);

            [DllImport("K8055D.dll")]
            public static extern void WriteAllDigital(int Data);

            [DllImport("K8055D.dll")]
            public static extern bool ReadDigitalChannel(int Channel);

            [DllImport("K8055D.dll")]
            public static extern int ReadAllDigital();

            [DllImport("K8055D.dll")]
            public static extern int ReadCounter(int CounterNr);

            [DllImport("K8055D.dll")]
            public static extern void ResetCounter(int CounterNr);

            [DllImport("K8055D.dll")]
            public static extern void SetCounterDebounceTime(int CounterNr, int DebounceTime);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            K8055.OpenDevice(0); //Open communication with K8055 that has the device address 0
            
            Console.WriteLine("Board Iniciada!");
            //THREAD FOR READ THE INPUT DIGITAL CHANNELS
            t = new Thread(Check_Digital_Inputs);
            t.Start();
            Console.WriteLine("Thread Iniciada");
            
        }

        //Luzes de nevoeiro, médio e maximos (dependente da luz), luz matricula, luz interior,
        public void Check_Digital_Inputs()
        {

            while (true)
            {

                //Sensor Noite
                if (K8055.ReadDigitalChannel(1) && aux == 0)
                {
                    aux = 1;
                    
                    // Ligar luzes da matricula
                    K8055.SetDigitalChannel(1);
                    
                    // Se nenhuma luz estiver ligada, ligamos os medios
                    if(ligado == -1)
                    {
                        K8055.SetDigitalChannel(7);
                    }

                }
                else if (!K8055.ReadDigitalChannel(1) && aux == 1)
                {
                    aux = 0;
                    if(ligado == -1)
                    {
                        K8055.ClearDigitalChannel(7);
                        K8055.ClearDigitalChannel(8);
                    }
                    K8055.ClearDigitalChannel(1);
                }

                //Sensor Nevoeiro
                if (K8055.ReadDigitalChannel(2))
                {
                    aux2 = 0;
                    K8055.SetDigitalChannel(6);

                }
                else if (!K8055.ReadDigitalChannel(2) && aux2 == 0)
                {
                    aux2 = 1;
                    K8055.ClearDigitalChannel(6);

                }

                //Sensor Portas
                if (K8055.ReadDigitalChannel(3))
                {
                    K8055.SetDigitalChannel(2);

                }
                else
                {
                    K8055.ClearDigitalChannel(2);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            board_open = false;
            K8055.ClearDigitalChannel(1);
            K8055.ClearDigitalChannel(2);
            K8055.ClearDigitalChannel(3);
            K8055.ClearDigitalChannel(4);
            K8055.ClearDigitalChannel(5);
            K8055.ClearDigitalChannel(6);
            K8055.ClearDigitalChannel(7);
            K8055.ClearDigitalChannel(8);
            Luzes.Text = "Luzes Desligadas";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            K8055.CloseDevice(); //Closes communication with the K8055
            

        }

        //Mínimos, médios e máximos
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (Luzes.SelectedItem.Equals("Luzes Desligadas"))
                {
                    ligado = -1;
                    K8055.ClearDigitalChannel(7);
                    K8055.ClearDigitalChannel(8);
                }

                if (Luzes.SelectedItem.Equals("Mínimos"))
                {
                    ligado = 1;
                    K8055.ClearDigitalChannel(7);
                    K8055.SetDigitalChannel(8);
                }

                if (Luzes.SelectedItem.Equals("Médios"))
                {
                    ligado = 1;
                    K8055.ClearDigitalChannel(8);
                    K8055.SetDigitalChannel(7);
                }

                if (Luzes.SelectedItem.Equals("Máximos"))
                {
                    ligado = 1;
                    K8055.SetDigitalChannel(8);
                    K8055.SetDigitalChannel(7);
                }
            


        }


        //Luzes de Nevoeiro
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        { 
        
            
            if (checkBox1.Checked && !K8055.ReadDigitalChannel(2))
            {
                K8055.SetDigitalChannel(6);
            }
            
            else if (!checkBox1.Checked && !K8055.ReadDigitalChannel(2))
            {
                K8055.ClearDigitalChannel(6);
            }
        }

        //Travão
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                K8055.SetDigitalChannel(4);
            }
            else
            {
                K8055.ClearDigitalChannel(4);
            }
        }

        //Marcha atrás
        private async void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                while (checkBox3.Checked)
                {

                    K8055.SetDigitalChannel(5);

                    await Task.Delay(2000);
                    //Console.WriteLine("stop wait timer");

                    K8055.ClearDigitalChannel(5);

                    await Task.Delay(2000);
                    //Console.WriteLine("stop wait timer");
                }
            }
            else
            {
                K8055.ClearDigitalChannel(5);
            }
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                K8055.SetDigitalChannel(3);

            }
            else
            {
                K8055.ClearDigitalChannel(3);

            }        

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
