using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Memory.Win64;
using Memory.Utils;
using System.Diagnostics;
using System.Linq;

namespace MetaHacks
{
    public partial class CodeVein : Form
    {
        Process p;
        MemoryHelper64 memo;
        //Global offesets
        ulong uLife, uBruma, uBB, uVigor;
        //Controladoras
        Int16 lbl = 0;
        float fLife, fVigor, fDB;
        Int32 iBruma;
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetAsyncKeyState(int pKey);
        public CodeVein()
        {
            InitializeComponent();
        }
        //Métodos
        private void Buscar()
        {
            try
            {
                p = Process.GetProcessesByName("CodeVein-Win64-Shipping").FirstOrDefault();
                memo = new MemoryHelper64(p);
                //Address 
                ulong baseAddr = memo.GetBaseAddress(0x03DB03D0);
                ulong AdrBB = memo.GetBaseAddress(0x0422D830);
                //Offsets
                uLife = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x120, 0x88, 0x538, 0x158, 0x2B8, 0x1D0, 0x1FC });
                uVigor = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x120, 0x88, 0x538, 0x158, 0x2B8, 0x1D0, 0x260 });
                uBruma = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x120, 0x88, 0x538, 0x158, 0x2B8, 0x1D0, 0x548 });
                uBB = MemoryUtils.OffsetCalculator(memo, AdrBB, new int[] { 0x30, 0x388, 0xD58, 0x20, 0x358, 0xA8, 0x500 });
            }
            catch
            {
                timer1.Stop();
                tSearch.Start();
            }
        }
        private void Vigor()
        {
            if (checkVigor.Checked == true)
            {
                try
                {
                    memo.WriteMemory<float>(uVigor, fVigor);
                }
                catch
                {
                    textBox1.Text = "020";
                    MessageBox.Show("Nop");
                }
            }
        }
        private void SangueNegro()
        {
            if (checkDB.Checked == true)
            {
                try
                {
                    memo.WriteMemory<float>(uBB, fDB);
                }
                catch
                {
                    textBox1.Text = "020";
                    MessageBox.Show("Nop");
                }
            }
        }
        private void Life()
        {
            fLife = 9999;
            if (checkHP.Checked == true)
            {
                try
                {
                    memo.WriteMemory<float>(uLife, fLife);
                }
                catch
                {
                    textBox1.Text = "020";
                    MessageBox.Show("Nop");
                }
            }
        }
        private void Bruma()
        {
            if(checkBruma.Checked == true)
            {
                try
                {
                    iBruma = Convert.ToInt16(textBox3.Text);
                }
                catch
                {
                    textBox3.Text = "9000";
                }
            }
        }
        //-----------------------------//-----------------
        bool MouseDwn;
        Point lastPt;
        private void CodeVein_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "English";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            string message = "Deseja encerrar o programa?";
            string title = "Encerrar aplicação";
            MessageBoxButtons bnt = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, bnt, MessageBoxIcon.Stop);
            if (result == DialogResult.Yes)
            {

                this.Close();

            }
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CodeVein_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDwn = true;
            lastPt = e.Location;
        }

        private void CodeVein_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDwn)
            {
                this.Location = new Point(this.Location.X - lastPt.X + e.X, this.Location.Y - lastPt.Y + e.Y);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            HotKeys();
            try
            {
                Buscar();
                iBruma = Convert.ToInt32(textBox3.Text);
                fVigor = float.Parse(textBox1.Text);
                fDB = float.Parse(textBox2.Text);

                Vigor();
                Bruma();
                SangueNegro();
                Life();
            }
            catch
            {
                tSearch.Start();
                timer1.Stop();
            }
        }

        private void CodeVein_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDwn = false;
        }
        private void HotKeys()
        {
            //Controlar as hotkeys
            for (int i = 0; i < 255; i++)
            {
                int KeyState = GetAsyncKeyState(i);
                string nova = i.ToString();
                if (KeyState != 0)
                {
                    //NumPad1
                    if (nova == "97")
                    {
                        if (checkHP.Checked == false)
                        {
                            checkHP.Checked = true;
                        }
                        else
                            checkHP.Checked = false;
                    }
                    //NumPad2
                    if (nova == "98")
                    {
                        if (checkVigor.Checked == false)
                        {
                            checkVigor.Checked = true;
                        }
                        else
                            checkVigor.Checked = false;
                    }
                    //NumPad3
                    if (nova == "99")
                    {
                        if (checkDB.Checked == false)
                        {
                            checkDB.Checked = true;
                        }
                        else
                            checkDB.Checked = false;
                    }
                    if(nova == "100")
                    {
                        if (checkBruma.Checked == false) checkBruma.Checked = true;
                        else
                            checkBruma.Checked = false;
                    }
                }
            }
        }
        private void tSearch_Tick(object sender, EventArgs e)
        {
           try
            {
                p = Process.GetProcessesByName("CodeVein-Win64-Shipping").FirstOrDefault();
                int ProcID = Process.GetCurrentProcess().Id;
                string name = Process.GetCurrentProcess().MachineName;
                if (p != null)
                {
                    
                    lblStatus.Text = name;
                    lblID.Text = p.ToString();
                    timer1.Start();
                    tSearch.Stop();
                }
                else
                {
                    lblStatus.Text = "Jogo Inativo";
                    lblID.Text = "---";
                    checkBruma.Checked = false;
                    checkHP.Checked = false;
                    checkDB.Checked = false;
                    checkVigor.Checked = false;
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
            if(lbl == 2)
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
    }
}
