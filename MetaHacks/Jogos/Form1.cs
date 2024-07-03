using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Memory.Win64;
using Memory.Utils;
using System.Diagnostics;

namespace MetaHacks
{
    public partial class Form1 : Form
    {
        //Ativação
        bool fix1, fix2, fix3;
        bool fixHp1, fixHp2, fixHp3;
        bool auto1, auto2, auto3;
        bool hpAuto1, hpAuto2, hpAuto3;
        bool hpau2, hpau3;
        //Controle de Memória
        ulong targetAddr;
        ulong AddrSp, AddrSp2, AddrSp3;
        ulong AddrHp1, AddrHp2, AddrHp3;
        ulong uHp1, uHp2, uHp3;
        ulong targetCt1, targetCt2, targetCt3;
        //Numéricas
        int lbl, lbl2;
        Int32 sAuto1, sAuto2, sAuto3;
        Int32 Arq, Arq2, Arq3;
        Int32 ArqHp1, ArqHp2, ArqHp3;
        Int32 iHpAuto1, iHpAuto2, iHpAuto3;
        //---------------------------------
        Process p;

        private void researchToolStripMenuItem_Click(object sender, EventArgs e)
        {   
                try
                {
                    //Definir qual o nome do processo que será buscado.
                    Process p = Process.GetProcessesByName("Digimon Story CS").FirstOrDefault();
                    // Declarar nossa variável de acesso da memória.
                    memo = new MemoryHelper64(p);
                    //Definir a base do addr, seguido pelos offsets que formam o ponteiro.
                    ulong baseAddr = memo.GetBaseAddress(0x00F205C0);
                    //Ponteiro do Dinheiro
                    targetAddr = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x30, 0x40 });
                    //Alterar segundo Led
                    timer1.Start();
                    timer3.Stop();
                    lblProc.Location = new Point(451, 73);
                    lblProc.Text = "Jogo Encontrado";
                    tControle.Start();

                }
                catch
                {
                    MessageBox.Show("Execute primeiro o jogo", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    timer3.Start();

                }
            
        }

        private void led2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (led2ToolStripMenuItem.Checked != true) { lblProc.Visible = false; }
            else { lblProc.Visible = true; }
        }

        private void ligarLedsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ligarLedsToolStripMenuItem.Checked != true) { timer2.Stop(); lblNamee.ForeColor = Color.Red; lblProc.ForeColor = Color.Red; }
            else { timer2.Start(); }
        }




        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iRemove = Convert.ToInt32(textBox1.Text);
                Int32 iSave = Convert.ToInt32(memo.ReadMemory<int>(targetAddr).ToString());
                iCount = iSave - iRemove;
                if (iCount < 0)
                {
                    MessageBox.Show("O resultado não pode\nser um número negativo", "Aviso - Erro de Cálculo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    textBox1.Clear();
                    return;
                }
                textBox1.Clear();
                memo.WriteMemory<int>(targetAddr, iCount);
            }
            catch { MessageBox.Show("Insira somente números", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); return; }
        }

        private void IncHP1_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtHP1a.Text);
                Int32 iSave = Convert.ToInt32(mem.ReadMemory<int>(AddrHp1).ToString());
                iCount = iSave + iAdd;
                mem.WriteMemory<int>(AddrHp1, iCount);
                txtHP1a.Clear();
            }
            catch { MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); return; }
        }

        private void DecHP2_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtHP1a.Text);
                Int32 iSave = Convert.ToInt32(mem.ReadMemory<int>(AddrHp1).ToString());
                iCount = iSave - iAdd;
                mem.WriteMemory<int>(AddrHp1, iCount);
                txtHP1a.Clear();
            }
            catch { MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); return; }
        }

        private void FixHP1_Click(object sender, EventArgs e)
        {
            if (hpAuto1 == false)
            {
                hpAuto1 = true;
                Float.HPAuto1 = true;
                timerBtl.Start();
            }
            else
            {
                hpAuto1 = false;
                Float.HPAuto1 = false;
                lblAuHp1.Text = "Desligado";
                lblAuHp1.ForeColor = Color.DarkViolet;
                txtHP1b.Clear();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(textBox1.Text);
                Int32 iSave = Convert.ToInt32(memo.ReadMemory<int>(targetAddr).ToString());
                if (Float.b1 == 0) { iCount = iSave * iAdd; }
                else { iCount = iSave + iAdd; }
                memo.WriteMemory<int>(targetAddr, iCount);
                textBox1.Clear();
            }
            catch { MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); return; }
        }

        private void IncHP2_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtHP2a.Text);
                Int32 iSave = Convert.ToInt32(mem.ReadMemory<int>(AddrHp2).ToString());
                iCount = iSave + iAdd;
                mem.WriteMemory<int>(AddrHp2, iCount);
                txtHP2a.Clear();
            }
            catch { MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); return; }
        }

        private void IncHP3_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtHP3a.Text);
                Int32 iSave = Convert.ToInt32(mem.ReadMemory<int>(AddrHp3).ToString());
                iCount = iSave + iAdd;
                mem.WriteMemory<int>(AddrHp3, iCount);
                txtHP3a.Clear();
            }
            catch { MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); return; }
        }

        private void DecHP22_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtHP2a.Text);
                Int32 iSave = Convert.ToInt32(mem.ReadMemory<int>(AddrHp2).ToString());
                iCount = iSave - iAdd;
                mem.WriteMemory<int>(AddrHp2, iCount);
                txtHP2a.Clear();
            }
            catch { MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); return; }
        }

        private void DecHP3_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtHP3a.Text);
                Int32 iSave = Convert.ToInt32(mem.ReadMemory<int>(AddrHp3).ToString());
                iCount = iSave - iAdd;
                mem.WriteMemory<int>(AddrHp3, iCount);
                txtHP3a.Clear();
            }
            catch { MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); return; }
        }

        private void FixHP2_Click(object sender, EventArgs e)
        {
            if (hpau2 == false)
            {
                hpau2 = true;
                Float.HPAuto2 = true;
                timerBtl.Start();
            }
            else
            {
                hpau2 = false;
                Float.HPAuto2 = false;
                lblAuHp2.Text = "Desligado";
                lblAuHp2.ForeColor = Color.DarkViolet;
                txtHP2b.Clear();
            }
        }

        private void FixHP3_Click(object sender, EventArgs e)
        {
            {
                if (hpau3 == false)
                {
                    hpau3 = true;
                    Float.HPAuto3 = true;
                    timerBtl.Start();
                }
                else
                {
                    hpau3 = false;
                    Float.HPAuto3 = false;
                    lblAuHp3.Text = "Desligado";
                    lblAuHp3.ForeColor = Color.DarkViolet;
                    txtHP3b.Clear();
                }
            }
        }

        private void btnAddBlt_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtSp1.Text);
                Int32 iSave = Convert.ToInt32(memo.ReadMemory<int>(AddrSp).ToString());
                iCount = iSave + iAdd;
                if (iCount < 0)
                {
                    MessageBox.Show("O valor final não pode ser negativo", "Alerta");
                    txtFixBtl1.Clear(); txtFixBtl1.Focus();
                    return;
                }
                memo.WriteMemory<int>(AddrSp, iCount);
                txtSp1.Clear();
            }
            catch
            {
                MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtSp1.Clear();
                return;
            }
        }

        private void btnAddBlt2_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtSp2.Text);
                Int32 iSave = Convert.ToInt32(memo.ReadMemory<int>(AddrSp2).ToString());
                iCount = iSave + iAdd;
                if (iCount < 0)
                {
                    MessageBox.Show("O valor final não pode ser negativo", "Alerta");
                    txtFixBtl2.Clear(); txtFixBtl2.Focus();
                    return;
                }
                memo.WriteMemory<int>(AddrSp2, iCount);
                txtSp2.Clear();
            }
            catch
            {
                MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtSp2.Clear();
                return;
            }
        }

        private void btnAddBlt3_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtSp3.Text);
                Int32 iSave = Convert.ToInt32(memo.ReadMemory<int>(AddrSp3).ToString());
                iCount = iSave + iAdd;
                if (iCount < 0)
                {
                    MessageBox.Show("O valor final não pode ser negativo", "Alerta");
                    txtFixBtl3.Clear(); txtFixBtl3.Focus();
                    return;
                }
                memo.WriteMemory<int>(AddrSp3, iCount);
                txtSp3.Clear();
            }
            catch
            {
                MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtSp3.Clear();
                return;
            }
        }

        private void btnAddBltHp1_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtHp1.Text);
                Int32 iSave = Convert.ToInt32(memo.ReadMemory<int>(uHp1).ToString());
                iCount = iSave + iAdd;
                if (iCount < 0)
                {
                    MessageBox.Show("O valor final não pode ser negativo", "Alerta");
                    txtHp1.Clear(); txtHp1.Focus();
                    return;
                }
                mem.WriteMemory<int>(uHp1, iCount);
                txtHp1.Clear();
            }
            catch
            {
                MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtHp1.Clear();
                return;
            }
        }

        private void btnAddBltHp2_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtHp2.Text);
                Int32 iSave = Convert.ToInt32(memo.ReadMemory<int>(uHp2).ToString());
                iCount = iSave + iAdd;
                if (iCount < 0)
                {
                    MessageBox.Show("O valor final não pode ser negativo", "Alerta");
                    txtHp2.Clear(); txtHp2.Focus();
                    return;
                }
                mem.WriteMemory<int>(uHp2, iCount);
                txtHp2.Clear();
            }
            catch
            {
                MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtHp2.Clear();
                txtHp2.Focus();
                return;
            }
        }

        private void btnAddBltHp3_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(txtHp3.Text);
                Int32 iSave = Convert.ToInt32(memo.ReadMemory<int>(uHp3).ToString());
                iCount = iSave + iAdd;
                if (iCount < 0)
                {
                    MessageBox.Show("O valor final não pode ser negativo", "Alerta");
                    txtHp3.Clear(); txtHp3.Focus();
                    return;
                }
                mem.WriteMemory<int>(uHp3, iCount);
                txtHp3.Clear();
            }
            catch
            {
                MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtHp3.Clear();
                txtHp3.Focus();
                return;
            }
        }

        private void btnFixBtl_Click(object sender, EventArgs e)
        {
            if (fix1 == false)
            {
                btnAddBlt.Enabled = false;
                btnAutoBtl.Enabled = false;
                txtFixBtl1.Enabled = false;
                txtFixBtl1.Text = null;
                fix1 = true;
                Float.f1 = true;
                Arq = Convert.ToInt32(memo.ReadMemory<int>(AddrSp).ToString());
                timerBtl.Start();
            }
            else
            {
                btnAddBlt.Enabled = true;
                btnAutoBtl.Enabled = true;
                txtFixBtl1.Enabled = true;
                fix1 = false;
                Float.f1 = false;
            }
        }

        private void btnFixBtl2_Click(object sender, EventArgs e)
        {
            if (fix2 == false)
            {
                btnAddBlt2.Enabled = false;
                btnAutoBtl2.Enabled = false;
                txtFixBtl2.Enabled = false;
                txtFixBtl2.Text = null;
                fix2 = true;
                Float.f2 = true;
                Arq2 = Convert.ToInt32(memo.ReadMemory<int>(AddrSp2).ToString());
                timerBtl.Start();
            }
            else
            {
                btnAddBlt2.Enabled = true;
                btnAutoBtl2.Enabled = true;
                txtFixBtl2.Enabled = true;
                fix2 = false;
                Float.f2 = false;
            }
        }

        private void btnFixBtl3_Click(object sender, EventArgs e)
        {
            if (fix3 == false)
            {
                btnAddBlt3.Enabled = false;
                btnAutoBtl3.Enabled = false;
                txtFixBtl3.Enabled = false;
                txtFixBtl3.Text = null;
                fix3 = true;
                Float.f3 = true;
                Arq3 = Convert.ToInt32(memo.ReadMemory<int>(AddrSp3).ToString());
                timerBtl.Start();
            }
            else
            {
                btnAddBlt3.Enabled = true;
                btnAutoBtl3.Enabled = true;
                txtFixBtl3.Enabled = true;
                fix3 = false;
                Float.f3 = false;
            }
        }

        private void btnFixBtlHp1_Click(object sender, EventArgs e)
        {
            if (fixHp1 == false)
            {
                btnAddBltHp1.Enabled = false;
                btnAutoBtlHp1.Enabled = false;
                txtFixBtlHp1.Enabled = false;
                txtFixBtlHp1.Text = null;
                fixHp1 = true;
                Float.bHp1 = true;
                ArqHp1 = Convert.ToInt32(mem.ReadMemory<int>(uHp1).ToString());
                timerBtl.Start();
            }
            else
            {
                btnAddBltHp1.Enabled = true;
                btnAutoBtlHp1.Enabled = true;
                txtFixBtlHp1.Enabled = true;
                fixHp1 = false;
                Float.bHp1 = false;
            }
        }

        private void btnFixBtlHp2_Click(object sender, EventArgs e)
        {
            if (fixHp2 == false)
            {
                btnAddBltHp2.Enabled = false;
                btnAutoBtlHp2.Enabled = false;
                txtFixBtlHp2.Enabled = false;
                txtFixBtlHp2.Text = null;
                fixHp2 = true;
                Float.bHp2 = true;
                ArqHp2 = Convert.ToInt32(mem.ReadMemory<int>(uHp2).ToString());
                timerBtl.Start();
            }
            else
            {
                btnAddBltHp2.Enabled = true;
                btnAutoBtlHp2.Enabled = true;
                txtFixBtlHp2.Enabled = true;
                fixHp2 = false;
                Float.bHp2 = false;
            }
        }

        private void btnFixBtlHp3_Click(object sender, EventArgs e)
        {
            if (fixHp3 == false)
            {
                btnAddBltHp3.Enabled = false;
                btnAutoBtlHp3.Enabled = false;
                txtFixBtlHp3.Enabled = false;
                txtFixBtlHp3.Text = null;
                fixHp3 = true;
                Float.bHp3 = true;
                ArqHp3 = Convert.ToInt32(mem.ReadMemory<int>(uHp3).ToString());
                timerBtl.Start();
            }
            else
            {
                btnAddBltHp3.Enabled = true;
                btnAutoBtlHp3.Enabled = true;
                txtFixBtlHp3.Enabled = true;
                fixHp3 = false;
                Float.bHp3 = false;
            }
        }

        private void btnAutoBtl_Click(object sender, EventArgs e)
        {

            if (auto1 == false)
            {
                btnFixBtl.Enabled = false;
                auto1 = true;
                Float.auto1 = true;
                timerBtl.Start();
            }
            else
            {
                btnFixBtl.Enabled = true;
                auto1 = false;
                Float.auto1 = false;
                txtFixBtl1.Clear();
            }
        }

        private void btnAutoBtl2_Click(object sender, EventArgs e)
        {
            if (auto2 == false)
            {
                btnFixBtl2.Enabled = false;
                auto2 = true;
                Float.auto2 = true;
                timerBtl.Start();
            }
            else
            {
                btnFixBtl2.Enabled = true;
                auto2 = false;
                Float.auto2 = false;
                txtFixBtl2.Clear();
            }
        }

        private void btnAutoBtl3_Click(object sender, EventArgs e)
        {
            if (auto3 == false)
            {
                btnFixBtl3.Enabled = false;
                auto3 = true;
                Float.auto3 = true;
                timerBtl.Start();
            }
            else
            {
                btnFixBtl3.Enabled = true;
                auto3 = false;
                Float.auto3 = false;
                txtFixBtl3.Clear();
            }
        }

        private void btnAutoBtlHp1_Click(object sender, EventArgs e)
        {

            if (hpAuto1 == false)
            {
                btnFixBtlHp1.Enabled = false;
                hpAuto1 = true;
                Float.Hpauto1 = true;
                timerBtl.Start();
            }
            else
            {
                btnFixBtlHp1.Enabled = true;
                hpAuto1 = false;
                Float.Hpauto1 = false;
                txtFixBtlHp1.Clear();
            }
        }

        private void btnAutoBtlHp2_Click(object sender, EventArgs e)
        {
            if (hpAuto2 == false)
            {
                btnFixBtlHp2.Enabled = false;
                hpAuto2 = true;
                Float.Hpauto2 = true;
                timerBtl.Start();
            }
            else
            {
                btnFixBtlHp2.Enabled = true;
                hpAuto2 = false;
                Float.Hpauto2 = false;
                txtFixBtlHp2.Clear();
            }
        }

        private void txtHP1b_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void txtHP2b_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }

        private void txtHP3b_TextChanged(object sender, EventArgs e)
        {

        }

        private void opçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpDS OptDS = new FormOpDS();
            if (Application.OpenForms.OfType<FormOpDS>().Count() == 0)
            {
                OptDS.Show();
            }
            else { Application.OpenForms.OfType<FormOpDS>().First().BringToFront(); }
        }
        private void btnAutoBtlHp3_Click(object sender, EventArgs e)
        {
            if (hpAuto3 == false)
            {
                btnFixBtlHp3.Enabled = false;
                hpAuto3 = true;
                Float.Hpauto3 = true;
                timerBtl.Start();
            }
            else
            {
                btnFixBtlHp3.Enabled = true;
                hpAuto3 = false;
                Float.Hpauto3 = false;
                txtFixBtlHp3.Clear();
            }
        }

        MemoryHelper64 memo, mem;
        public Form1()
        {
            InitializeComponent();
            this.Cursor = new Cursor(Properties.Resources.favicon__7_.Handle);
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Deseja sair?";
            string title = "Encerrar aplicação";
            MessageBoxButtons bnt = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, bnt, MessageBoxIcon.Stop);
            if (result == DialogResult.Yes) { this.Close(); }
            else { return; }
        }
        private void IniciarTip()
        {
            //Delays e Sempre visível
            TP.InitialDelay = 600;
            TP.AutoPopDelay = 3000;
            TP.ReshowDelay = 1000;
            TP.ShowAlways = true;
            //Definir onde ele será exibido
            TP.SetToolTip(this.FixHP1, "Insira um valor\npara habilitar");
            TP.SetToolTip(this.FixHP2, "Insira um valor\npara habilitar");
            TP.SetToolTip(this.FixHP3, "Insira um valor\npara habilitar");
            TP.SetToolTip(this.txtHP1b, "Insira um valor\npara habilitar");
            TP.SetToolTip(this.txtHP2b, "Insira um valor\npara habilitar");
            TP.SetToolTip(this.txtHP3b, "Insira um valor\npara habilitar");
            TP.SetToolTip(this.txtFixBtl1, "Insira um valor\npara habilitar");
            TP.SetToolTip(this.txtFixBtl2, "Insira um valor\npara habilitar");
            TP.SetToolTip(this.txtFixBtl3, "Insira um valor\npara habilitar");
            TP.SetToolTip(this.btnAddBlt, "Valor negativo subtrai");
            TP.SetToolTip(this.btnAddBlt2, "Para subtrair insira\nvalores negativos");
            TP.SetToolTip(this.btnAddBlt3, "Para subtrair insira\nvalores negativos");
            TP.SetToolTip(this.txtFixBtlHp1, "Para subtrair insira\nvalores negativos");
            TP.SetToolTip(this.txtFixBtlHp2, "Para subtrair insira\nvalores negativos");
            TP.SetToolTip(this.txtFixBtlHp3, "Para subtrair insira\nvalores negativos");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                memo.WriteMemory<int>(targetAddr, int.Parse(textBox1.Text));
            }
            catch { MessageBox.Show("Insira somente números", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
            textBox1.Clear();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            MakeAddr();
        }
        //Métodos
        private void MakeAddr()
        {
            try
            {
                //Controle
                Process prc = Process.GetProcessesByName("Digimon Story CS").FirstOrDefault();
                mem = new MemoryHelper64(prc);
                //Endereços 00F20598
                ulong AdHP = mem.GetBaseAddress(0x00F205C0);
                ulong AdBSp = mem.GetBaseAddress(0x00F20598);
                ulong AdSp2 = mem.GetBaseAddress(0x00F206C0);
                ulong uBHp1 = mem.GetBaseAddress(0x00F20598);
                ulong baseCt1 = mem.GetBaseAddress(0x00F205A8);
                //Offsets
                AddrHp1 = MemoryUtils.OffsetCalculator(mem, AdHP, new int[] { 0x10, 0xB0, 0x0, 0x10, 0x7C });
                AddrHp2 = MemoryUtils.OffsetCalculator(mem, AdHP, new int[] { 0x10, 0xB0, 0x8, 0x10, 0x7C });
                AddrHp3 = MemoryUtils.OffsetCalculator(mem, AdHP, new int[] { 0x10, 0xB0, 0x10, 0x10, 0x7C });
                AddrSp = MemoryUtils.OffsetCalculator(mem, AdBSp, new int[] { 0x2D0 });
                AddrSp2 = MemoryUtils.OffsetCalculator(mem, AdSp2, new int[] { 0xE8, 0x2B8, 0x800 });
                AddrSp3 = MemoryUtils.OffsetCalculator(mem, AdSp2, new int[] { 0xE8, 0x2B8, 0xD38 });
                uHp1 = MemoryUtils.OffsetCalculator(mem, uBHp1, new int[] { 0x2C0 });
                uHp2 = MemoryUtils.OffsetCalculator(mem, AdSp2, new int[] { 0xE8, 0x2B8, 0x7F0 });
                uHp3 = MemoryUtils.OffsetCalculator(mem, AdSp2, new int[] { 0xE8, 0x2B8, 0xD28 });
                targetCt1 = MemoryUtils.OffsetCalculator(mem, baseCt1, new int[] { 0x38, 0x310 }); // Taxa do Combo em batalha
                targetCt2 = MemoryUtils.OffsetCalculator(mem, baseCt1, new int[] { 0x38, 0x848 });
                targetCt3 = MemoryUtils.OffsetCalculator(mem, uBHp1, new int[] { 0xD88 });
                //Exibição
                label1.Text = memo.ReadMemory<int>(targetAddr).ToString();
                lblhp1.Text = mem.ReadMemory<int>(AddrHp1).ToString();
                lblhp2.Text = mem.ReadMemory<int>(AddrHp2).ToString();
                lblhp3.Text = mem.ReadMemory<int>(AddrHp3).ToString();
                //Batalha Sp
                lblBtSp1.Text = mem.ReadMemory<int>(AddrSp).ToString();
                lblBtSp2.Text = mem.ReadMemory<int>(AddrSp2).ToString();
                lblBtSp3.Text = mem.ReadMemory<int>(AddrSp3).ToString();
                //Batalha HP
                lblBtHp1.Text = mem.ReadMemory<int>(uHp1).ToString();
                lblBtHp2.Text = mem.ReadMemory<int>(uHp2).ToString();
                lblBtHp3.Text = mem.ReadMemory<int>(uHp3).ToString();
                //Taxa de Combo da Batalha
                lblCt1.Text = mem.ReadMemory<int>(targetCt1).ToString();
                lblCt2.Text = mem.ReadMemory<int>(targetCt2).ToString();
                lblCt3.Text = mem.ReadMemory<int>(targetCt3).ToString();
            }
            catch
            {
                timer3.Start();
                timer1.Stop();
            }
        }
        private void ControlarBtnAuto()
        {
            //Auto1
            if (txtFixBtl1.Text == "") { btnAutoBtl.Enabled = false; }
            else { btnAutoBtl.Enabled = true; }
            //Auto2
            if (txtFixBtl2.Text == "") { btnAutoBtl2.Enabled = false; }
            else { btnAutoBtl2.Enabled = true; }
            //Auto2
            if (txtFixBtl3.Text == "") { btnAutoBtl3.Enabled = false; }
            else { btnAutoBtl3.Enabled = true; }
            //Auto Hp1
            if (txtHP1b.Text == "") { FixHP1.Enabled = false; }
            else { FixHP1.Enabled = true; }
            //Auto Hp2
            if (txtHP2b.Text == "") { FixHP2.Enabled = false; }
            else { FixHP2.Enabled = true; }
            //Auto Hp3
            if (txtHP3b.Text == "") { FixHP3.Enabled = false; }
            else { FixHP3.Enabled = true; }
            //Auto Btl Hp1
            if (txtFixBtlHp1.Text == "") { btnAutoBtlHp1.Enabled = false; }
            else { btnAutoBtlHp1.Enabled = true; }
            //Auto Btl Hp2
            if (txtFixBtlHp2.Text == "") { btnAutoBtlHp2.Enabled = false; }
            else { btnAutoBtlHp2.Enabled = true; }
            //Auto Btl Hp3
            if (txtFixBtlHp3.Text == "") { btnAutoBtlHp3.Enabled = false; }
            else { btnAutoBtlHp3.Enabled = true; }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lbl++;
            if (lbl > 2)
            {
                lbl = 0;

            }
            if (lbl == 2)
            {
                lblNamee.ForeColor = Color.Green;
                lblProc.ForeColor = Color.Green;

            }
            if (lbl == 1)
            {
                lblNamee.ForeColor = Color.Red;
                lblProc.ForeColor = Color.Red;

            }
            if (lbl == 0)
            {
                lblNamee.ForeColor = Color.Black;
                lblProc.ForeColor = Color.Black;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
             try
            {
                //Definir qual o nome do processo que será buscado.
                p = Process.GetProcessesByName("Digimon Story CS").FirstOrDefault();

                // Declarar nossa variável de acesso da memória.
                memo = new MemoryHelper64(p);
                //Definir a base do addr, seguido pelos offsets que formam o ponteiro.
                ulong baseAddr = memo.GetBaseAddress(0x00F205C0);
                //Definir nosso ponteiro.
                targetAddr = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x30, 0x40 });
                //Alterar segundo Led
                timer1.Start();
                timerBtl.Start();
                lblProc.Location = new Point(451, 73);
                lblProc.Text = "Jogo Encontrado";
                timer3.Stop();
            }
            catch
            {
                lblProc.Text = "Jogo Não Encontrado";
                lblProc.Location = new Point(405, 73);
            }
        }

        private void tControle_Tick(object sender, EventArgs e)
        {
            //Controle de nome dos CheckBox (direito)
            if (Float.Ct1 != 0) { chCt1a.Text = "Digi 1 - " + Float.Ct1.ToString() + "%"; }
            else { chCt1a.Name = "Digi 1 -  100%"; }
            if (Float.Ct2 != 0) { chCt2a.Text = "Digi 2 - " + Float.Ct2.ToString() + "%"; }
            else { chCt2a.Name = "Digi 2 -  100%"; }
            if (Float.Ct3 != 0) { chCt3a.Text = "Digi 3 - " + Float.Ct3.ToString() + "%"; }
            else { chCt3a.Name = "Digi 3 -  100%"; }

            ControlarBtnAuto();
            if (led2ToolStripMenuItem.Checked == true) { lblProc.Visible = true; }
            else { lblProc.Visible = false; }
            if (ligarLedsToolStripMenuItem.Checked == true) { timer2.Start(); }
            else { timer2.Stop(); lblNamee.ForeColor = Color.Red; lblProc.ForeColor = Color.Red; }

            //Combo Box
            if (cbbHack.Text == "Controle de batalha")
            {
                AtivarBlt();
            }
            ChBoxCt();
        }

        private void tOption_Tick(object sender, EventArgs e)
        {
            //Opções
            if (Float.p1 == 0) { groupBox2.BackColor = Color.Transparent; }
            else { groupBox2.BackColor = Color.Snow; }
            if (Float.p2 == 0) { groupBox1.BackColor = Color.Transparent; }
            else { groupBox1.BackColor = Color.Snow; }
            if (Float.b1 == 0) { button1.Text = "Multiplicar"; } else { button1.Text = "Adicionar"; }
        }

        private void timerBtl_Tick(object sender, EventArgs e)
        {
            try
            {
                AutoSp();
                AutoHP();
                FixarSP();
                FixarHP();
                HpWorld();
                CheckTaxa();
            }
            catch
            {
                DesligarTimers();
            }

        }
        private void ChBoxCt()
        {
            if (chCt1a.Checked == true) { chCt1b.Enabled = false; } else { chCt1b.Enabled = true; }
            if (chCt2a.Checked == true) { chCt2b.Enabled = false; } else { chCt2b.Enabled = true; }
            if (chCt3a.Checked == true) { chCt3b.Enabled = false; } else { chCt3b.Enabled = true; }
            if (chCt1b.Checked == true) { chCt1a.Enabled = false; } else { chCt1a.Enabled = true; }
            if (chCt2b.Checked == true) { chCt2a.Enabled = false; } else { chCt2a.Enabled = true; }
            if (chCt3b.Checked == true) { chCt3a.Enabled = false; } else { chCt3a.Enabled = true; }
        }
        private void HpWorld()
        {
            //Auto de HP 1
            if (Float.HPAuto1 == true)
            {

                try
                {
                    lblAuHp1.Text = "Ligado";
                    lblAuHp1.ForeColor = Color.Crimson;
                    Int32 iAuto = Convert.ToInt32(txtHP1b.Text);
                    iHpAuto1 = Convert.ToInt32(memo.ReadMemory<int>(AddrHp1).ToString());
                    if (iHpAuto1 < iAuto)
                    {
                        memo.WriteMemory<int>(AddrHp1, iAuto);
                    }

                }
                catch { txtHP1b.Text = "5"; MessageBox.Show("Não pode deixar o campo vazio", "Aviso", MessageBoxButtons.OK); }

            }
            else { lblAuHp1.Text = "Desligado"; lblBtlOn1.ForeColor = Color.DarkViolet; }
            //Auto de HP 2
            if (Float.HPAuto2 == true)
            {

                try
                {
                    lblAuHp2.Text = "Ligado";
                    lblAuHp2.ForeColor = Color.Crimson;
                    Int32 iAuto = Convert.ToInt32(txtHP2b.Text);
                    iHpAuto2 = Convert.ToInt32(memo.ReadMemory<int>(AddrHp2).ToString());
                    if (iHpAuto2 < iAuto)
                    {
                        memo.WriteMemory<int>(AddrHp2, iAuto);
                    }

                }
                catch { txtHP2b.Text = "5"; MessageBox.Show("Não pode deixar o campo vazio", "Aviso", MessageBoxButtons.OK); }

            }
            else { lblAuHp2.Text = "Desligado"; lblBtlOn2.ForeColor = Color.DarkViolet; }
            //Auto de HP 3
            if (Float.HPAuto3 == true)
            {

                try
                {
                    lblAuHp3.Text = "Ligado";
                    lblAuHp3.ForeColor = Color.Crimson;
                    Int32 iAuto = Convert.ToInt32(txtHP3b.Text);
                    iHpAuto3 = Convert.ToInt32(memo.ReadMemory<int>(AddrHp3).ToString());
                    if (iHpAuto3 < iAuto)
                    {
                        memo.WriteMemory<int>(AddrHp3, iAuto);
                    }

                }
                catch { txtHP3b.Text = "5"; MessageBox.Show("Não pode deixar o campo vazio", "Aviso", MessageBoxButtons.OK); }

            }
            else { lblAuHp3.Text = "Desligado"; lblBtlOn2.ForeColor = Color.DarkViolet; }
        }
        private void AutoSp()
        {
            //Auto de Batalha SP 1
            if (Float.auto1 == true)
            {

                try
                {
                    lblBtlOn1.Text = "Ligado";
                    lblBtlOn1.ForeColor = Color.Crimson;
                    Int32 iAuto = Convert.ToInt32(txtFixBtl1.Text);
                    sAuto1 = Convert.ToInt32(memo.ReadMemory<int>(AddrSp).ToString());
                    if (sAuto1 < iAuto)
                    {
                        memo.WriteMemory<int>(AddrSp, iAuto);
                    }

                }
                catch { txtFixBtl1.Text = "5"; MessageBox.Show("Não pode deixar o campo vazio", "Aviso", MessageBoxButtons.OK); }

            }
            else { lblBtlOn1.Text = "Desligado"; lblBtlOn1.ForeColor = Color.DarkViolet; }
            //Auto de Batalha SP 2
            if (Float.auto2 == true)
            {

                try
                {
                    lblBtlOn2.Text = "Ligado";
                    lblBtlOn2.ForeColor = Color.Crimson;
                    Int32 iAuto = Convert.ToInt32(txtFixBtl2.Text);
                    sAuto2 = Convert.ToInt32(memo.ReadMemory<int>(AddrSp2).ToString());
                    if (sAuto2 < iAuto)
                    {
                        memo.WriteMemory<int>(AddrSp2, iAuto);
                    }

                }
                catch { txtFixBtl2.Text = "5"; MessageBox.Show("Não pode deixar o campo vazio", "Aviso", MessageBoxButtons.OK); }

            }
            else { lblBtlOn2.Text = "Desligado"; lblBtlOn2.ForeColor = Color.DarkViolet; }
            //Auto de Batalha SP 3
            if (Float.auto3 == true)
            {

                try
                {
                    lblBtlOn3.Text = "Ligado";
                    lblBtlOn3.ForeColor = Color.Crimson;
                    Int32 iAuto = Convert.ToInt32(txtFixBtl3.Text);
                    sAuto3 = Convert.ToInt32(memo.ReadMemory<int>(AddrSp3).ToString());
                    if (sAuto3 < iAuto)
                    {
                        memo.WriteMemory<int>(AddrSp3, iAuto);
                    }

                }
                catch { txtFixBtl3.Text = "5"; MessageBox.Show("Não pode deixar o campo vazio", "Aviso", MessageBoxButtons.OK); }

            }
            else { lblBtlOn3.Text = "Desligado"; lblBtlOn3.ForeColor = Color.DarkViolet; }
        }
        private void AutoHP()
        {
            //Auto de Batalha HP 1
            if (Float.Hpauto1 == true)
            {

                try
                {
                    lblBtlOnHp1.Text = "Ligado";
                    lblBtlOnHp1.ForeColor = Color.Crimson;
                    Int32 iAuto = Convert.ToInt32(txtFixBtlHp1.Text);
                    sAuto1 = Convert.ToInt32(memo.ReadMemory<int>(uHp1).ToString());
                    if (sAuto1 < iAuto)
                    {
                        memo.WriteMemory<int>(uHp1, iAuto);
                    }

                }
                catch { txtFixBtlHp1.Text = "5"; MessageBox.Show("Não pode deixar o campo vazio", "Aviso", MessageBoxButtons.OK); }

            }
            else { lblBtlOnHp1.Text = "Desligado"; lblBtlOnHp1.ForeColor = Color.DarkViolet; }
            //Auto de Batalha HP 3
            if (Float.Hpauto3 == true)
            {

                try
                {
                    lblBtlOnHp3.Text = "Ligado";
                    lblBtlOnHp3.ForeColor = Color.Crimson;
                    Int32 iAuto = Convert.ToInt32(txtFixBtlHp3.Text);
                    sAuto1 = Convert.ToInt32(memo.ReadMemory<int>(uHp3).ToString());
                    if (sAuto1 < iAuto)
                    {
                        memo.WriteMemory<int>(uHp3, iAuto);
                    }

                }
                catch { txtFixBtlHp3.Text = "5"; MessageBox.Show("Não pode deixar o campo vazio", "Aviso", MessageBoxButtons.OK); }

            }
            else { lblBtlOnHp3.Text = "Desligado"; lblBtlOnHp3.ForeColor = Color.DarkViolet; }
            //Auto de Batalha HP 2
            if (Float.Hpauto2 == true)
            {

                try
                {
                    lblBtlOnHp2.Text = "Ligado";
                    lblBtlOnHp2.ForeColor = Color.Crimson;
                    Int32 iAuto = Convert.ToInt32(txtFixBtlHp2.Text);
                    sAuto1 = Convert.ToInt32(memo.ReadMemory<int>(uHp2).ToString());
                    if (sAuto1 < iAuto)
                    {
                        memo.WriteMemory<int>(uHp2, iAuto);
                    }

                }
                catch { txtFixBtlHp2.Text = "5"; MessageBox.Show("Não pode deixar o campo vazio", "Aviso", MessageBoxButtons.OK); }

            }
            else { lblBtlOnHp2.Text = "Desligado"; lblBtlOnHp2.ForeColor = Color.DarkViolet; }
        }

        private void FixarSP()
        {
            //Fix 1
            if (Float.f1 == true)
            {
                lblFixBtn1.Text = "On";
                lblFixBtn1.ForeColor = Color.DarkGreen;
                memo.WriteMemory<int>(AddrSp, Arq);
            }
            else { lblFixBtn1.Text = "Off"; lblFixBtn1.ForeColor = Color.DarkRed; }
            //Fix 2
            if (Float.f2 == true)
            {
                lblFixBtn2.Text = "On";
                lblFixBtn2.ForeColor = Color.DarkGreen;
                memo.WriteMemory<int>(AddrSp2, Arq2);
            }
            else { lblFixBtn2.Text = "Off"; lblFixBtn2.ForeColor = Color.DarkRed; }
            //Fix 3
            if (Float.f3 == true)
            {
                lblFixBtn3.Text = "On";
                lblFixBtn3.ForeColor = Color.DarkGreen;
                memo.WriteMemory<int>(AddrSp3, Arq3);
            }
            else { lblFixBtn3.Text = "Off"; lblFixBtn3.ForeColor = Color.DarkRed; }
        }
        private void FixarHP()
        {
            //Fix1
            if (Float.bHp1 == true)
            {
                lblFixBtnHp1.Text = "On";
                lblFixBtnHp1.ForeColor = Color.DarkGreen;
                memo.WriteMemory<int>(uHp1, ArqHp1);
            }
            else { lblFixBtnHp1.Text = "Off"; lblFixBtnHp1.ForeColor = Color.DarkRed; }
            //Fix2
            if (Float.bHp2 == true)
            {
                lblFixBtnHp2.Text = "On";
                lblFixBtnHp2.ForeColor = Color.DarkGreen;
                memo.WriteMemory<int>(uHp2, ArqHp2);
            }
            else { lblFixBtnHp2.Text = "Off"; lblFixBtnHp2.ForeColor = Color.DarkRed; }
            //Fix3
            if (Float.bHp3 == true)
            {
                lblFixBtnHp3.Text = "On";
                lblFixBtnHp3.ForeColor = Color.DarkGreen;
                memo.WriteMemory<int>(uHp3, ArqHp3);
            }
            else { lblFixBtnHp3.Text = "Off"; lblFixBtnHp3.ForeColor = Color.DarkRed; }
        }
        private void CheckTaxa()
        {
            int ii = 0;
            int total = 100;

            //Taxa do Combo 1
            if (chCt1b.Checked == true)
            {
                memo.WriteMemory<int>(targetCt1, ii);
            }
            else if (chCt1a.Checked == true)
            {

                if (Float.Ct1 == 0) { memo.WriteMemory<int>(targetCt1, total); }
                else { memo.WriteMemory<int>(targetCt1, Float.Ct1); }
            }
            //Taxa do Combo 2
            if (chCt2b.Checked == true)
            {
                memo.WriteMemory<int>(targetCt2, ii);
            }
            else if (chCt2a.Checked == true)
            {
                if (Float.Ct2 == 0) { memo.WriteMemory<int>(targetCt2, total); }
                else { memo.WriteMemory<int>(targetCt2, Float.Ct2); }
            }
            //Taxa do Combo 3
            if (chCt3b.Checked == true)
            {
                memo.WriteMemory<int>(targetCt3, ii);
            }
            else if (chCt3a.Checked == true)
            {
                if (Float.Ct3 == 0) { memo.WriteMemory<int>(targetCt3, total); }
                else { memo.WriteMemory<int>(targetCt3, Float.Ct3); }
            }
        }
        private void DesligarTimers()
        {
            timerBtl.Stop();
            timer1.Stop();
            timer3.Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 iCount;
                Int32 iAdd = Convert.ToInt32(textBox1.Text);
                Int32 iSave = Convert.ToInt32(memo.ReadMemory<int>(targetAddr).ToString());
                if (Float.b1 == 0) { iCount = iSave * iAdd; }
                else { iCount = iSave + iAdd; }
                memo.WriteMemory<int>(targetAddr, iCount);
                textBox1.Clear();
            }
            catch { MessageBox.Show("Insira somente números inteiros", "Erro!Valores Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); return; }
        }
       /* private void button1_Click(object sender, EventArgs e)
        {
            /*string email = maskedTextBox1.Text;
            IsValidEmail(email);
            bool IsValidEmail(string em)
            {
                try
                {
                    var mail = new System.Net.Mail.MailAddress(email);
                    MessageBox.Show("Ok");
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {
            // Criando ComboBox
            cbbHack.Items.Clear();
            cbbHack.Items.Add("Controle de batalha");
            cbbHack.Items.Add("Controle de Itens");
            cbbHack.Items.Add("Default");
            //Iniciar
            IniciarTip();
            groupBttBtn.Visible = false;
            groupLbBtt.Visible = false;
            ligarLedsToolStripMenuItem.Checked = true;
            led2ToolStripMenuItem.Checked = true;
            tOption.Start();
            tControle.Start();
            //Encontrar Jogo
            try
            {
                //Definir qual o nome do processo que será buscado.
                p = Process.GetProcessesByName("Digimon Story CS").FirstOrDefault();

                // Declarar nossa variável de acesso da memória.
                memo = new MemoryHelper64(p);
                //Definir a base do addr, seguido pelos offsets que formam o ponteiro.
                ulong baseAddr = memo.GetBaseAddress(0x00F205C0);
                //Definir nosso ponteiro.
                targetAddr = MemoryUtils.OffsetCalculator(memo, baseAddr, new int[] { 0x30, 0x40 });
                //Alterar segundo Led
                timer1.Start();
                lblProc.Location = new Point(451, 73);
                lblProc.Text = "Jogo Encontrado";
                tControle.Start();
                timer2.Start();
                timerBtl.Start();
            }
            catch
            {
                MessageBox.Show("Execute primeiro o jogo", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                timer3.Start();
            }
        }
        void AtivarBlt()
        {
            groupBttBtn.Visible = true;
            groupLbBtt.Visible = true;

        }
        void Desl()
        {
            groupBttBtn.Visible = false;
            groupLbBtt.Visible = false;
        }
    }
}
