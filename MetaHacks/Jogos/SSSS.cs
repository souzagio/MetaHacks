using System;
using System.Windows.Forms;
using Memory.Win32;
using Memory.Utils;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;

namespace MetaHacks
{
    public partial class SSSS : Form
    {
        bool gamestatus = false;
        Intermed mid = new Intermed();
        Process p;
        MemoryHelper32 memo;
        uint targetCosmo, targetCosmo2;
        uint targetHp, targetHp2;
        uint target7, target72;
        uint baseAddr, din;
        int lbl = 0;
        private void timerSearch_Tick(object sender, EventArgs e)
        {
            try
            {
                //Definir qual o nome do processo que será buscado.
                p = Process.GetProcessesByName("SSSS").FirstOrDefault();
                int en = Process.GetProcessesByName("SSSS").FirstOrDefault().Id;
                // Declarar nossa variável de acesso da memória.
                memo = new MemoryHelper32(p);
                //Definir a base do addr, seguido pelos offsets que formam o ponteiro.
                baseAddr = memo.GetBaseAddress(0x01BDDB94);
                din = memo.GetBaseAddress(0x1BE3618);
                //Definir nosso ponteiro.
                lblStatus.Text = "SSSS";
                lblID.Text = en.ToString();
                //Desativar os componentes ativos
                if (gamestatus == false)
                {
                    checkHP.Checked = false;
                    mc2.Checked = false;
                    mc3.Checked = false;
                    mc4.Checked = false;
                    mc5.Checked = false;
                    mc6.Checked = false;
                    gamestatus = true;
                }
                timerBatalha.Start();
                tControle.Start();
                
                //Alterar segundo Led

            }
            catch
            {
                lblStatus.Text = "Inativado";
                gamestatus = false;
                timerBatalha.Stop();
                tControle.Stop();
            }
        }
        private void OneHitP1()
        {
            float life = 1;

            if (memo.ReadMemory<float>(targetHp2) > 400 && mc4.Checked == true)
            {
              memo.WriteMemory<float>(targetHp2, life);
            }

        }
        private void OneHitP2()
        {
            float life = 1;
            if (memo.ReadMemory<float>(targetHp) > 400 && mc5.Checked == true)
            {
                memo.WriteMemory<float>(targetHp, life);
            }

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
                        if (mc2.Checked == false)
                        {
                            mc2.Checked = true;
                        }
                        else
                            mc2.Checked = false;
                    }
                    //NumPad3
                    if (nova == "99")
                    {
                        if (mc3.Checked == false && mc5.Checked == false)
                        {
                            mc3.Checked = true;
                        }
                        else
                            mc3.Checked = false;
                    }
                    //NumPad4
                    if (nova == "100")
                    {
                        if (mc4.Checked == false)
                        {
                            mc4.Checked = true;
                            mc9.Checked = false;
                            mc9.Enabled = false;
                        }
                        else
                        {
                            mc4.Checked = false;
                            mc9.Enabled = true;
                        }
                    }
                    //NumPad5
                    if (nova == "101")
                    {
                        if (mc5.Checked == false)
                        {
                            mc5.Checked = true;
                            mc3.Checked = false;
                            mc3.Enabled = false;
                        }
                        else
                        {
                            mc5.Checked = false;
                            mc3.Enabled = true;
                        }
                    }
                    //NumPad6
                    if (nova == "102")
                    {
                        if (mc6.Checked == false)
                        {
                            mc6.Checked = true;
                        }
                        else
                            mc6.Checked = false;
                    }
                    //NumPad7
                    if(nova == "103" )
                    {
                        if (mc7.Checked == false)
                        {
                            mc7.Checked = true;
                        }
                        else
                            mc7.Checked = false;
                    }
                    //NumPad8
                    if (nova == "104" )
                    {
                        if (mc8.Checked == false)
                        {
                            mc8.Checked = true;
                        }
                        else
                            mc8.Checked = false;
                    }
                    //NumPad9
                    if (nova == "105" && mc4.Checked == false)
                    {
                        if (mc9.Checked == false)
                        {
                            mc9.Checked = true;
                        }
                        else
                            mc9.Checked = false;
                    }
                }
            }
        }
        private void tControle_Tick(object sender, EventArgs e)
        {
            if(mc4.Checked == true)
            {
                mc9.Checked = false;
                mc9.Enabled = false;
            }
            else { mc9.Enabled = true; }
            if(mc5.Checked == true)
            {
                mc3.Checked = false;
                mc3.Enabled = false;
            }
            else { mc3.Enabled = true; }
            if(checkBox1.Checked == true)
            {
                checkHP.Enabled = false;
                checkHP.Checked = false;
                mc2.Enabled = false;
                mc2.Checked = false;
            }
            else { checkHP.Enabled = true; }
            if (checkBox2.Checked == true)
            {
                mc7.Enabled = false;
                mc7.Checked = false;
                mc8.Enabled = false;
                mc8.Checked = false;
            }
            else { checkHP.Enabled = true; }
            HotKeys();
            try
            {
                //Lugar Certo
                targetHp = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x48, 0x118, 0xF0, 0x10, 0x18, 0x4C4 });
                targetCosmo = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x48, 0x118, 0xF0, 0x10, 0x18, 0x4F8 });
                target7 = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x48, 0x118, 0xF0, 0x10, 0x18, 0x500 });
                targetHp2 = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x48, 0x8C, 0x10, 0xC, 0xF0, 0x4C4 });
                targetCosmo2 = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x48, 0x8C, 0x10, 0xC, 0xF0, 0x4F8 });
                target72 = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x48, 0x8C, 0x10, 0xC, 0xF0, 0x500 });
                //Controle
                float on2 = memo.ReadMemory<float>(targetHp2);
                float on = memo.ReadMemory<float>(targetHp);

                //Semi Morte
            }
            catch
            {
                MessageBox.Show("Error01");
            }
               
        }

        public SSSS()
        {
            InitializeComponent();
            this.Cursor = new Cursor(Properties.Resources.bac.Handle);
        }
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetAsyncKeyState(int pKey);

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Deseja sair?";
            string title = "Encerrar aplicação";
            MessageBoxButtons bnt = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, bnt, MessageBoxIcon.Stop);
            if (result == DialogResult.Yes) { this.Close(); }
            else { return; }
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

        private void tLed_Tick(object sender, EventArgs e)
        {
            //Leds
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

        private void siticoneTextBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void siticoneTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void siticoneTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.Enter && siticoneTextBox1.Text != "")
                {
                    if (Convert.ToInt32(siticoneTextBox1.Text) >= 1000) siticoneTextBox1.Text = "1000";
                    HpBar.Value = Convert.ToInt32(siticoneTextBox1.Text) / 10;
                }
            
        }

        private void siticoneTextBox1_Enter(object sender, EventArgs e)
        {
            siticoneTextBox1.Text = "";
        }

        private void siticoneTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && siticoneTextBox2.Text != "")
                {
                    if (Convert.ToInt32(siticoneTextBox2.Text) >= 1000) siticoneTextBox2.Text = "1000";
                    MetroBar3.Value = Convert.ToInt32(siticoneTextBox2.Text) / 10;
                }
            }
            catch
            {
                siticoneTextBox2.Text = "1";
            }
        }
        private void siticoneTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && siticoneTextBox3.Text != "")
                {
                    if (Convert.ToInt32(siticoneTextBox3.Text) >= 1000) siticoneTextBox3.Text = "1000";
                    MetroBar3.Value = Convert.ToInt32(siticoneTextBox3.Text) / 10;
                }
            }
            catch
            {
                siticoneTextBox3.Text = "1";
            }
        }

        private void siticoneTextBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && siticoneTextBox2.Text != "")
                {
                    if (Convert.ToInt32(siticoneTextBox2.Text) >= 1000) siticoneTextBox2.Text = "1000";
                    MetroBar2.Value = Convert.ToInt32(siticoneTextBox2.Text) / 10;
                }
            }
            catch
            {
                siticoneTextBox2.Text = "1";
            }
        }

        private void siticoneTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void siticoneTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void siticoneTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void siticoneTextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void siticoneTextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void siticoneTextBox2_Enter(object sender, EventArgs e)
        {
            
        }

        private void siticoneTextBox2_Enter_1(object sender, EventArgs e)
        {
            siticoneTextBox2.Text = "";
        }

        private void siticoneTextBox3_Enter(object sender, EventArgs e)
        {
            siticoneTextBox3.Text = "";
        }
        private void siticoneTextBox1_Leave(object sender, EventArgs e)
        {
            siticoneTextBox1.Text = (HpBar.Value*10).ToString();
        }

        private void siticoneTextBox2_Leave(object sender, EventArgs e)
        {
            siticoneTextBox2.Text = (MetroBar2.Value*10).ToString();
        }

        private void siticoneTextBox3_Leave(object sender, EventArgs e)
        {
            siticoneTextBox3.Text = (MetroBar3.Value*10).ToString();
        }
        private void siticoneTextBox6_Leave(object sender, EventArgs e)
        {
            
        }

        private void txt1_KeyDown(object sender, KeyEventArgs e)
        {
            
            
                if (e.KeyCode == Keys.Enter && txt1.Text != "")
                {
                    if (Convert.ToInt32(txt1.Text) >= 1000) txt1.Text = "1000";
                    Bar1.Value = Convert.ToInt32(txt1.Text) / 10;
                }
          
        }

        private void txt2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt2.Text != "")
            {
                if (Convert.ToInt32(txt2.Text) >= 1000) txt2.Text = "1000";
                Bar2.Value = Convert.ToInt32(txt2.Text) / 10;
            }
        }

        private void txt3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt3.Text != "")
            {
                if (Convert.ToInt32(txt3.Text) >= 1000) txt3.Text = "1000";
                Bar3.Value = Convert.ToInt32(txt3.Text) / 10;
            }
        }

        private void txt1_Enter(object sender, EventArgs e)
        {
            txt1.Clear();
        }

        private void txt1_Leave(object sender, EventArgs e)
        {
            txt1.Text = (Bar1.Value * 10).ToString();
        }

        private void txt2_Enter(object sender, EventArgs e)
        {
            txt2.Clear();
        }

        private void txt2_Leave(object sender, EventArgs e)
        {
            txt2.Text = (Bar2.Value * 10).ToString();
        }

        private void txt3_Enter(object sender, EventArgs e)
        {
            txt3.Clear();
        }

        private void txt3_Leave(object sender, EventArgs e)
        {
            txt3.Text = (Bar3.Value * 10).ToString();
        }

        private void SSSS_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void SSSS_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDwn)
            {
                this.Location = new Point(this.Location.X - lastPt.X + e.X, this.Location.Y - lastPt.Y + e.Y);
            }
        }
        bool MouseDwn;
        Point lastPt;

        private void SSSS_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDwn = true;
            lastPt = e.Location;
        }

        private void SSSS_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDwn = false;
        }

        private void SSSS_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Português";  
            timerSearch.Start();
            Games.auto = 1;
            if (Float.radio == 1) this.MaximizeBox = true;
            else this.MaximizeBox = false;
        }
       

        private void timerBatalha_Tick(object sender, EventArgs e)
        {
            try
            {
                Player1();
                Player2();
                OneHitP1();
                OneHitP2();
            }
            catch
            {
                timerBatalha.Stop();
            }
        }
        private void Player1()
        {
            Int32 iSen = 1000;//HpBar.Value;
            Int32 iCosmo = 1000;// MetroBar2.Value;
            Int32 iHp = 500;// MetroBar3.Value;
            float fSen = 1000;// ((float)iSen*10);
            float fCosmo = 1000;// ((float)iCosmo*10);
            float fHp = 500;// ((float)iHp*10);
            //Hack-Jogo
            if (checkHP.Checked == true)
            {
                memo.WriteMemory<float>(target7, fSen);
                float ffSeven = memo.ReadMemory<float>(target7);
                int iiSeven = (int)ffSeven / 10;
                HpBar.Value = iiSeven;
            }
            if(mc2.Checked == true)
            {
                memo.WriteMemory<float>(targetCosmo, fCosmo);
                float ffCosmo = memo.ReadMemory<float>(targetCosmo);
                int iicosmo = (int)ffCosmo / 10;
                MetroBar2.Value = iicosmo;
            }
            if (mc3.Checked == true)
            {
                memo.WriteMemory<float>(targetHp, fHp);
                float fvida = memo.ReadMemory<float>(targetHp);
                int iihp = (int)fvida / 10;
                MetroBar3.Value = iihp;
            }
        }
        private void Player2()
        {
            Int32 iSen = Bar1.Value;
            Int32 iCosmo = Bar2.Value;
            Int32 iHp = Bar3.Value;
            float fSen = 1000;// ((float)iSen * 10);
            float fCosmo = 1000;// ((float)iCosmo * 10);
            float fHp = 500;// ((float)iHp * 10);
            //Hack-Jogo
            if (mc7.Checked == true)
            {
                memo.WriteMemory<float>(target72, fSen);
                float ffSeven = memo.ReadMemory<float>(target72);
                int iiSeven = (int)ffSeven / 10;
                Bar1.Value = iiSeven;
            }
            if (mc8.Checked == true)
            {
                memo.WriteMemory<float>(targetCosmo2, fCosmo);
                float ffCosmo = memo.ReadMemory<float>(targetCosmo2);
                int iicosmo = (int)ffCosmo / 10;
                Bar2.Value = iicosmo;
            }
            if (mc9.Checked == true)
            {
                memo.WriteMemory<float>(targetHp2, fHp);
                float fvida = memo.ReadMemory<float>(targetHp2);
                int iihp = (int)fvida / 10;
                Bar3.Value = iihp;
            }
        }
    }
}
