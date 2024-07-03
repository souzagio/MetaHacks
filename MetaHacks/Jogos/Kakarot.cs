using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Media;
using Memory.Utils;
using Memory.Win64;
using System.Diagnostics;

namespace MetaHacks
{
    public partial class Kakarot : Form
    {
        
        public Kakarot()
        {
            InitializeComponent();
        }
        //*** ----- Variáveis locais e declarações de Dlls ------ ***//
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetAsyncKeyState(int pKey);
        SoundPlayer Act = new SoundPlayer(Properties.Resources.actve);
        SoundPlayer Dct = new SoundPlayer(Properties.Resources.deact);
        Process p;
        MemoryHelper64 memo;
        //Global
        bool gamestatus, MouseD, bBuff, bHP, bKi, bAwk, bHit;
        int lbl;
        float fBuff;
        Point Pt;
        //*** Variáveis de hack ***//
        ulong OrbAddr, BuffAddr, HitAddr;
        ulong uHit;
        ulong OrbB, OrbR, OrbG, OrbRain, OrbSuper, OrbSupre;
        ulong uVida, uKi, uAtkC, uAtkK, uDefC, uCT, uExp, uOrb;
        ulong utVida, utKi, utAtkC, utAtkK, utDefK, utCT, utExp, utOrb;
        ulong sgNv, sgVida, sgKi, sgAwk, sgRefHp, sgRefKi, sgAtk, sgDef, sgAtKi, sgDeKi;
        ulong svNv, svVida, svKi, svAwk, svRefHp, svRefKi, svAtk, svDef, svAtKi, svDeKi;

        private void Kakarot_MouseUp(object sender, MouseEventArgs e)
        {
            MouseD = false;
        }

        private void Kakarot_MouseDown(object sender, MouseEventArgs e)
        {
            MouseD = true;
            Pt = e.Location;
        }

        private void Kakarot_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseD)
            {
                this.Location = new Point(this.Location.X - Pt.X + e.X, this.Location.Y - Pt.Y + e.Y);
            }
        }

        ulong shNv, shVida, shKi, shAwk, shRefHp, shRefKi, shAtk, shDef, shAtKi, shDeKi;

        private void txtDefKi_MouseClick(object sender, MouseEventArgs e)
        {
            txtDefKi.Clear();
        }

        private void txtAtkKi_MouseClick(object sender, MouseEventArgs e)
        {
            txtAtkKi.Clear();
        }

        private void txtDef_MouseClick(object sender, MouseEventArgs e)
        {
            txtDef.Clear();
        }

        private void txtAtk_MouseClick(object sender, MouseEventArgs e)
        {
            txtAtk.Clear();
        }

        private void txtKi_MouseClick(object sender, MouseEventArgs e)
        {
            txtKi.Clear();
        }

        private void txtVida_MouseClick(object sender, MouseEventArgs e)
        {
            txtVida.Clear();
        }

        private void txtNv_MouseClick(object sender, MouseEventArgs e)
        {
            txtNv.Clear();
        }

        //*** -------- Métodos  -------- ***//
        private void Itens()
        {
            OrbB = MemoryUtils.OffsetCalculator(memo, OrbAddr, new int[] { 0x88, 0x8, 0x78, 0x50, 0x130, 0x8D0, 0x40 });
            OrbR = MemoryUtils.OffsetCalculator(memo, OrbAddr, new int[] { 0x88, 0x8, 0x78, 0x50, 0x130, 0x8D0, 0x44 });
            OrbG = MemoryUtils.OffsetCalculator(memo, OrbAddr, new int[] { 0x88, 0x8, 0x78, 0x50, 0x130, 0x8D0, 0x48 });
            OrbRain = MemoryUtils.OffsetCalculator(memo, OrbAddr, new int[] { 0x88, 0x8, 0x78, 0x50, 0x130, 0x8D0, 0x4C });
            OrbSuper = MemoryUtils.OffsetCalculator(memo, OrbAddr, new int[] { 0x88, 0x8, 0x78, 0x50, 0x130, 0x8D0, 0x50 });
            OrbSupre = MemoryUtils.OffsetCalculator(memo, OrbAddr, new int[] { 0x88, 0x8, 0x78, 0x50, 0x130, 0x8D0, 0x54 });
        }
        private void Buff()
        {
            uVida = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x18370 });
            uKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x1837C });
            uAtkC = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x18334 });
            uAtkK = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x18340 });
            uDefC = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x18358 });
            uCT = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x18364 });
            uExp = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x183A0 });
            uOrb = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x183AC });
            //Timer
            utVida = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x18374 });
            utKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x18380 });
            utAtkC = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x18338 });
            utAtkK = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x18344 });
            utDefK = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x1835C });
            utCT = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x18368 });
            utExp = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x183A4 });
            utOrb = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x183B0 });



        }
        private void TimeBuff()
        {
            if (bBuff == true)
            {
                lblInfBuff.ForeColor = Color.Red;
                memo.WriteMemory<float>(utVida, 300);
                memo.WriteMemory<float>(utKi, 300);
                memo.WriteMemory<float>(utAtkC, 300);
                memo.WriteMemory<float>(utAtkK, 300);
                memo.WriteMemory<float>(utDefK, 300);
                memo.WriteMemory<float>(utCT, 300);
                memo.WriteMemory<float>(utExp, 300);
                memo.WriteMemory<float>(utOrb, 300);
            }
            else
            {
                lblInfBuff.ForeColor = Color.Black;
            }

        }
        private void InfHP()
        {
            if (bHP)
            {
                lbnHp.ForeColor = Color.Red;
                memo.WriteMemory<int>(sgVida, 999999999);
                memo.WriteMemory<int>(svVida, 999999999);
                memo.WriteMemory<int>(shVida, 999999999);
            }
            else
            {
                lbnHp.ForeColor = Color.Black;
            }
        }
        private void InfKi()
        {
            if (bKi)
            {
                lbnKi.ForeColor = Color.Red;
                memo.WriteMemory<int>(sgKi, 9999);
                memo.WriteMemory<int>(svKi, 9999);
                memo.WriteMemory<int>(shKi, 9999);
            }
            else
            {
                lbnKi.ForeColor = Color.Black;
            }
        }
        private void InfAwk()
        {
            if (bAwk)
            {
                lbnAwk.ForeColor = Color.Red;
                memo.WriteMemory<int>(sgAwk, 1000);
                memo.WriteMemory<int>(svAwk, 1000);
                memo.WriteMemory<int>(shAwk, 1000);
            }
            else
            {
                lbnAwk.ForeColor = Color.Black;
            }
        }
        private void MaxHit()
        {
            if (bHit)
            {
                uHit = MemoryUtils.OffsetCalculator(memo, HitAddr, new int[] { 0xA0, 0xF8, 0x140, 0x2C0, 0x250, 0x140, 0x28 });
                int hit = memo.ReadMemory<int>(uHit);
                if (hit > 2 && hit < 998)
                {
                    memo.WriteMemory<int>(uHit, 999);
                }
                lbnHit.ForeColor = Color.Red;
            }
            else
            {
                lbnHit.ForeColor = Color.Black;
            }
        }
        private void Exibir()
        {
            //Valores dos Buffs
            float fVida = memo.ReadMemory<float>(uVida);
            float fKi = memo.ReadMemory<float>(uKi);
            float fAtkC = memo.ReadMemory<float>(uAtkC);
            float fAtkK = memo.ReadMemory<float>(uAtkK);
            float fDefK = memo.ReadMemory<float>(uDefC);
            float fCt = memo.ReadMemory<float>(uCT);
            float fExp = memo.ReadMemory<float>(uExp);
            float fOrb = memo.ReadMemory<float>(uOrb);
            //Tempo de duração dos Buffs
            float ftVida = memo.ReadMemory<float>(utVida);
            float ftKi = memo.ReadMemory<float>(utKi);
            float ftAtkC = memo.ReadMemory<float>(utAtkC);
            float ftAtkK = memo.ReadMemory<float>(utAtkK);
            float ftDefK = memo.ReadMemory<float>(utDefK);
            float ftCt = memo.ReadMemory<float>(utCT);
            float ftExp = memo.ReadMemory<float>(utExp);
            float ftOrb = memo.ReadMemory<float>(utOrb);
            //Exibir nas labels:
            //Valores dos buffs
            lblpVida.Text = fVida.ToString() + "%";
            lblpki.Text = fKi.ToString() + "%";
            lblpAtkF.Text = fAtkC.ToString() + "%";
            lblpAtkK.Text = fAtkK.ToString() + "%";
            lblpDefK.Text = fDefK.ToString() + "%";
            lblpCt.Text = fCt.ToString() + "%";
            lblpExp.Text = fExp.ToString() + "%";
            lblpOrb.Text = fOrb.ToString() + "%";
            //Tempo dos buffs
            lbtVida.Text = ftVida.ToString("F");
            lbtKi.Text = ftKi.ToString("F");
            lbtAtk.Text = ftAtkC.ToString("F");
            lbtAtkK.Text = ftAtkK.ToString("F");
            lbtDefK.Text = ftDefK.ToString("F");
            lbtCt.Text = ftCt.ToString("F");
            lbtExp.Text = ftExp.ToString("F");
            lbtOrb.Text = ftOrb.ToString("F");
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
                        int iOrb = 999999;
                        memo.WriteMemory<int>(OrbB, iOrb);
                        memo.WriteMemory<int>(OrbR, iOrb);
                        memo.WriteMemory<int>(OrbG, iOrb);
                        memo.WriteMemory<int>(OrbRain, iOrb);
                        memo.WriteMemory<int>(OrbSuper, iOrb);
                        memo.WriteMemory<int>(OrbSupre, iOrb);
                        Act.Play();
                    }
                    //NumPad2
                    if (nova == "98")
                    {
                        if (bBuff == false)
                        {
                            bBuff = true;
                        }
                        else
                        {
                            bBuff = false;
                            Dct.Play();
                        }
                        Act.Play();
                    }
                    //NumPad3
                    if (nova == "99")
                    {
                        if (bKi == false)
                        {
                            bKi = true;
                            Act.Play();
                        }    
                        else 
                        {
                            bKi = false;
                            Dct.Play();
                        }

                    }
                    //NumPad4
                    if (nova == "100")
                    {
                        if (bHP == false)
                        {
                            bHP = true;
                            Act.Play();
                        }
                        else
                        {
                            bHP = false;
                            Dct.Play();
                        }
                    }
                    //NumPad5
                    if (nova == "101")
                    {
                        if (bAwk == false)
                        {
                            bAwk = true;
                            Act.Play();
                        }
                        else
                        {
                            bAwk = false;
                            Dct.Play();
                        }
                    }
                    //NumPad6 - Zeni
                    if (nova == "102")
                    {
                        if (bHP == false)
                        {
                            bHP = true;
                            Act.Play();
                        }
                        else
                        {
                            bHP = false;
                            Dct.Play();
                        }
                    }
                    //NumPad7 - Medal
                    if (nova == "103")
                    {
                        if (bHP == false)
                        {
                            bHP = true;
                            Act.Play();
                        }
                        else
                        {
                            bHP = false;
                            Dct.Play();
                        }
                    }
                    //NumPad8 - Hit
                    if (nova == "104")
                    {
                        if (bHit == false)
                        {
                            bHit = true;
                            Act.Play();
                        }
                        else
                        {
                            bHit = false;
                            Dct.Play();
                        }
                    }
                }
            }
        }
        private void HabilitarBtn()
        {
            if (gamestatus)
            {
                cmbBuff.Enabled = true;
                cmbChar.Enabled = true;
                txtBuff.Enabled = true;
                btnBuff2.Enabled = true;


            }
            else
            {
                cmbBuff.Enabled = false;
                cmbChar.Enabled = false;
                txtBuff.Enabled = false;
                btnBuff2.Enabled = false;
                cmbBuff.Text = "";
            }
        }
        private void OpenBox(int i)
        {
            if (i == 0)
            {
                txtNv.Enabled = false;
                txtAtk.Enabled = false;
                txtAtkKi.Enabled = false;
                txtDef.Enabled = false;
                txtDefKi.Enabled = false;
                txtVida.Enabled = false;
                txtKi.Enabled = false;
                btnCanc.Enabled = false;
                btnSav.Enabled = false;
                btnMax.Enabled = false;
                cmbBuff.Enabled = true;
                btnEdtBuff.Enabled = true;
            }
            else
            {
                txtNv.Enabled = true;
                txtAtk.Enabled = true;
                txtAtkKi.Enabled = true;
                txtDef.Enabled = true;
                txtDefKi.Enabled = true;
                txtVida.Enabled = true;
                txtKi.Enabled = true;
                btnCanc.Enabled = true;
                btnSav.Enabled = true;
                btnMax.Enabled = true;
                cmbBuff.Enabled = false;
                btnEdtBuff.Enabled = false;
            }
        }
        private void MontarChar()
        {
            //Endereçamento Goku
            sgNv = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x2C8 });
            sgVida = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x2D8 });
            sgKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x2DC });
            sgAwk = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x2E0 });
            sgRefHp = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x2E8 });
            sgRefKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x2EC });
            sgAtk = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x2F0 });
            sgDef = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x2F4 });
            sgAtKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x2F8 });
            sgDeKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x2FC });
            //Endereçamento Gohan
            shNv = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x560 });
            shVida = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x570 });
            shKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x574 });
            shAwk = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x578 });
            shRefHp = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x580 });
            shRefKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x584 });
            shAtk = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x588 });
            shDef = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x58C });
            shAtKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x590 });
            shDeKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x594 });
            //Endereçamento Vegeta
            svNv = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x7F8 });
            svVida = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x808 });
            svKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x8C0 });
            svAwk = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x810 });
            svRefHp = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x818 });
            svRefKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x81C });
            svAtk = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x820 });
            svDef = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x824 });
            svAtKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x828 });
            svDeKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x82C });
            /*//Endereçamento Gohan
            shNv = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x560 });
            shVida = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x570 });
            shKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x574 });
            shAwk = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x578 });
            shRefHp = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x580 });
            shRefKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x584 });
            shAtk = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x588 });
            shDef = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x58C });
            shAtKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x590 });
            shDeKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x594 });
            //Endereçamento Gohan
            shNv = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x560 });
            shVida = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x570 });
            shKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x574 });
            shAwk = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x578 });
            shRefHp = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x580 });
            shRefKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x584 });
            shAtk = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x588 });
            shDef = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x58C });
            shAtKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x590 });
            shDeKi = MemoryUtils.OffsetCalculator(memo, BuffAddr, new int[] { 0x2C0, 0xE0, 0xF8, 0x190, 0x2A0, 0x100, 0x594 });

            */
        }
        private void SetChar()
        {
            switch (cmbChar.Text)
            {
                case "Goku":
                    txtNv.Text = memo.ReadMemory<int>(sgNv).ToString();
                    txtVida.Text = memo.ReadMemory<int>(sgRefHp).ToString();
                    txtKi.Text = memo.ReadMemory<int>(sgRefKi).ToString();
                    txtAtk.Text = memo.ReadMemory<int>(sgAtk).ToString();
                    txtDef.Text = memo.ReadMemory<int>(sgDef).ToString();
                    txtAtkKi.Text = memo.ReadMemory<int>(sgAtKi).ToString();
                    txtDefKi.Text = memo.ReadMemory<int>(sgDeKi).ToString();
                    break;
                case "Vegeta":
                    txtNv.Text = memo.ReadMemory<int>(svNv).ToString();
                    txtVida.Text = memo.ReadMemory<int>(svRefHp).ToString();
                    txtKi.Text = memo.ReadMemory<int>(svRefKi).ToString();
                    txtAtk.Text = memo.ReadMemory<int>(svAtk).ToString();
                    txtDef.Text = memo.ReadMemory<int>(svDef).ToString();
                    txtAtkKi.Text = memo.ReadMemory<int>(svAtKi).ToString();
                    txtDefKi.Text = memo.ReadMemory<int>(svDeKi).ToString();
                    break;
                case "Gohan":
                    txtNv.Text = memo.ReadMemory<int>(shNv).ToString();
                    txtVida.Text = memo.ReadMemory<int>(shRefHp).ToString();
                    txtKi.Text = memo.ReadMemory<int>(shRefKi).ToString();
                    txtAtk.Text = memo.ReadMemory<int>(shAtk).ToString();
                    txtDef.Text = memo.ReadMemory<int>(shDef).ToString();
                    txtAtkKi.Text = memo.ReadMemory<int>(shAtKi).ToString();
                    txtDefKi.Text = memo.ReadMemory<int>(shDeKi).ToString();
                    break;
                default:
                   // cmbChar.Text = "Goku";
                    break;
            }
        }
        private void UpdateChar(int Nv, int Vida, int Ki, int Atk, int Def, int AtkK, int DefKi)
        {
            switch (cmbChar.Text)
            {
                case "Goku":
                    memo.WriteMemory<int>(sgNv, Nv);
                    memo.WriteMemory<int>(sgRefHp, Vida);
                    memo.WriteMemory<int>(sgRefKi, Ki);
                    memo.WriteMemory<int>(sgAtk, Atk);
                    memo.WriteMemory<int>(sgDef, Def);
                    memo.WriteMemory<int>(sgAtKi, AtkK);
                    memo.WriteMemory<int>(sgDeKi, DefKi);
                    break;
                case "Gohan":
                    memo.WriteMemory<int>(shNv, Nv);
                    memo.WriteMemory<int>(shRefHp, Vida);
                    memo.WriteMemory<int>(shRefKi, Ki);
                    memo.WriteMemory<int>(shAtk, Atk);
                    memo.WriteMemory<int>(shDef, Def);
                    memo.WriteMemory<int>(shAtKi, AtkK);
                    memo.WriteMemory<int>(shDeKi, DefKi);
                    break;
                case "Vegeta":
                    memo.WriteMemory<int>(svNv, Nv);
                    memo.WriteMemory<int>(svRefHp, Vida);
                    memo.WriteMemory<int>(svRefKi, Ki);
                    memo.WriteMemory<int>(svAtk, Atk);
                    memo.WriteMemory<int>(svDef, Def);
                    memo.WriteMemory<int>(svAtKi, AtkK);
                    memo.WriteMemory<int>(svDeKi, DefKi);
                    break;
            }
        }
        private void btnMax_Click(object sender, EventArgs e)
        {

        }

        private void tChar_Tick(object sender, EventArgs e)
        {
            if(gamestatus == true)
            {
                SetChar();
            }
        }

        private void cmbChar_SelectedValueChanged(object sender, EventArgs e)
        {
            SetChar();
        }

        

        private void btnBuff2_Click(object sender, EventArgs e)
        {
            if(txtBuff.Text == "")
            {
                MessageBox.Show("Nenhum buff selecionado", "aviso");
                return;
            }
            fBuff = 999;
            memo.WriteMemory<float>(uVida, fBuff);
            memo.WriteMemory<float>(uKi, fBuff);
            memo.WriteMemory<float>(uAtkC, fBuff);
            memo.WriteMemory<float>(uAtkK, fBuff);
            memo.WriteMemory<float>(uDefC, fBuff);
            memo.WriteMemory<float>(uCT, 9999);
            memo.WriteMemory<float>(uExp, fBuff);
            memo.WriteMemory<float>(uOrb, fBuff);
        }

        private void btnEdtBuff_Click(object sender, EventArgs e)
        {
            if(gamestatus != false)
            {
                OpenBox(1);
                tChar.Stop();
            }
        }

        private void btnSav_Click(object sender, EventArgs e)
        {
            try
            {
                int Nv = Convert.ToInt32(txtNv.Text);
                int Hp = Convert.ToInt32(txtVida.Text);
                int Ki = Convert.ToInt32(txtKi.Text);
                int At = Convert.ToInt32(txtAtk.Text);
                int De = Convert.ToInt32(txtDef.Text);
                int Atk = Convert.ToInt32(txtAtkKi.Text);
                int Dek = Convert.ToInt32(txtDefKi.Text);
                UpdateChar(Nv, Hp, Ki, At, De, Atk, Dek);
            }
            catch
            {
                MessageBox.Show("Nenhum campo pode ficar vazio.\nUse somente números.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtNv.Focus();
                SetChar();
                return;
            }
            OpenBox(0);
            tChar.Start();
        }

        private void btnCanc_Click(object sender, EventArgs e)
        {
            OpenBox(0);
            tChar.Start();
        }

        private void txtBuff_Leave(object sender, EventArgs e)
        {
            if(txtBuff.Text == "")
            {
                txtBuff.Text = "000";
            }
        }

        private void txtBuff_Click(object sender, EventArgs e)
        {
            txtBuff.Clear();
        }

        private void bntBuff_Click(object sender, EventArgs e)
        {
            string sBuff = cmbBuff.Text;
            fBuff = float.Parse(txtBuff.Text);
            switch (sBuff)
            {
                case "Vida":
                    memo.WriteMemory<float>(uVida, fBuff);
                     break;
                case "Ki":
                    memo.WriteMemory<float>(uKi, fBuff);
                    break;
                case "Atk":
                    memo.WriteMemory<float>(uAtkC, fBuff);
                    break;
                case "Atk Ki":
                    memo.WriteMemory<float>(uAtkK, fBuff);
                    break;
                case "Def Ki":
                    memo.WriteMemory<float>(uDefC, fBuff);
                    break;
                case "Ct":
                    if (fBuff >= 100)
                        memo.WriteMemory<float>(uCT, 4900);
                    else
                        memo.WriteMemory<float>(uCT, fBuff);
                    break;
                case "Exp":
                    memo.WriteMemory<float>(uExp, fBuff);
                    break;
                case "Orbe":
                    memo.WriteMemory<float>(uOrb, fBuff);
                    break;
                    default:
                    MessageBox.Show("Nenhum buff selecionado", "Aviso");
                    break;
            }
        }

        private void txtBuff_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        //*** Ponteiros ***\\
        private void tSearch_Tick(object sender, EventArgs e)
        {
            try
            {
                p = Process.GetProcessesByName("AT-Win64-Shipping").FirstOrDefault();
                int en = Process.GetProcessesByName("AT-Win64-Shipping").FirstOrDefault().Id;
                memo = new MemoryHelper64(p);
                //Address 
                OrbAddr = memo.GetBaseAddress(0x04C46078);
                BuffAddr = memo.GetBaseAddress(0x04BE23D0);
                HitAddr = memo.GetBaseAddress(0x04E9A838);
                //Setar o nome e o ID
                lblStatus.Text = "AT-Win64-Shipping";
                lblID.Text = en.ToString();
                gamestatus = true;
                Itens();
                Buff();
                tControle.Start();
                tBtl.Start();
                tLed.Start();
                MontarChar();
            }
            catch
            {
                gamestatus = false;
                HabilitarBtn();
                OpenBox(0);
                lblID.Text = "- - - -";
                lblStatus.Text = "Jogo Inativo";
                tControle.Stop();
                tBtl.Stop();
                tLed.Stop();
            }

        }

        private void Kakarot_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Português";
            OpenBox(0);
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

        private void tBtl_Tick(object sender, EventArgs e)
        {
            Itens();
            Buff();
            InfHP();
            InfKi();
            InfAwk();
            MaxHit();
            TimeBuff();
            Exibir();
        }

        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            HotKeys();
            HabilitarBtn();
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
    }
}
