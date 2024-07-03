using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Importar bibliotecas importantes
using System.Windows.Forms;
using Memory.Win64;
using Memory.Utils;
using System.Diagnostics;

namespace MetaHacks.Jogos
{
    public partial class DigimonCS : Form
    {
        //Variável do processo
        Process p;
        //Variável da memória
        MemoryHelper64 memo;
        //Base da memória
        ulong uGame;
        //Controladores do form
        Int16 lbl;
        public DigimonCS()
        {
            InitializeComponent();
        }
        //Buscar pelo jogo

        private void tSearch_Tick(object sender, EventArgs e)
        {
            try
            {
                p = Process.GetProcessesByName("Digimon Story CS").FirstOrDefault();
                //Escrever nos labels
                if (p != null)
                {
                    lblID.Text = (p.Id).ToString();
                    lblStatus.Text = p.ProcessName;
                    timer1.Start();
                    tSearch.Stop();
                }
                else
                {
                    lblStatus.Text = "Jogo Inativo";
                    lblID.Text = "---";
                }
            }
            catch
            {
                lblStatus.Text = "Jogo Inativo";
                lblID.Text = "---";
            }
        }

        private void tControle_Tick(object sender, EventArgs e)
        {
            lbl++;
            if (lbl == 2)
            {
                lbl = 0;
            }
            if (lbl == 1)
            {
                lblID.ForeColor = Color.MistyRose;
                lblStatus.ForeColor = Color.MistyRose;
            }
            if (lbl == 0)
            {
                lblID.ForeColor = Color.Black;
                lblStatus.ForeColor = Color.Black;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Buscar();
        }

        void Buscar()
        {
            try
            {
                p = Process.GetProcessesByName("Digimon Story CS").FirstOrDefault();
                if (p == null)
                {
                    tSearch.Start();
                    timer1.Stop();
                }
                else
                {
                     memo = new MemoryHelper64(p);
                    ulong baseAddr = memo.GetBaseAddress(0x00F205c0);
                    uGame = memo.GetBaseAddress(0x1AD356);
                    int uSetGame = memo.ReadMemory<int>(uGame);
                    string baseAd = uGame.ToString("X");
                    lbAddr.Text = baseAd;
                    lbAdd.Text = uSetGame.ToString("X");
                }
            }
            catch
            {
                tSearch.Start();
                timer1.Stop();
            }
        }

        private void btnAct_Click(object sender, EventArgs e)
        {
            Int32 i = 1212174633;
            memo.WriteMemory<int>(uGame, i);
        }

        private void btnDec_Click(object sender, EventArgs e)
        {
            Int32 i = 1212174593;
            memo.WriteMemory<int>(uGame, i);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ulong var = 0x7ff665d50017;
            ushort i = memo.ReadMemory<ushort>(MemoryUtils.OffsetCalculator(memo, var, new int[] { 0x8C }));
            lbHex.Text = i.ToString();
            lbVar.Text = i.ToString("X");
        }
    }
}
