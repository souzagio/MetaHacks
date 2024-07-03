using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaHacks
{
    public partial class Menu : Form
    {
        bool OldVar = false;
        bool bIco = false;
        bool MousePress = false;
        Point lastPos;
        public Menu()
        {
            InitializeComponent();
            
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            
            if (Float.login == 0)
            {
                using (Login Form = new Login())
                {
                    Float.login = 1;
                    Form.ShowDialog();
                    // Aqui considerei que se o login for efetuado com sucesso, o DialogResult será OK
                    this.Hide();
                    
                }
            }
            modoLivreToolStripMenuItem.Checked = true;
            Float.radio = 1;
            IniciarTp();
        }

        private void Exibir_App(object sender, EventArgs e)
        {
            this.Show();
            this.Cursor = new Cursor(Properties.Resources.favicon__12_.Handle);
            notIco.Visible = false;
        }
        private void Fechar_Click(object sender, EventArgs e)
        {
            OldVar = true;
            string message = "Deseja sair?";
            string title = "Encerrar aplicação";
            MessageBoxButtons bnt = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, bnt, MessageBoxIcon.Stop);
            if (result == DialogResult.Yes) { Application.Exit(); }
            else { return; }
        }
        private void Digi_Click(object sender, EventArgs e)
        {
            if (Float.radio == 1)
            {
                Form1 frm = new Form1();


                if (Application.OpenForms.OfType<Form1>().Count() == 0)
                {
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<Form1>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<Form1>().First().BringToFront();
                }



            }
            else
            {
                Form1 frm = new Form1();

                if (Application.OpenForms.OfType<Form1>().Count() == 0)
                {
                    if (bIco == false)
                    {
                        notIco.ShowBalloonTip(30, "Janela em Modo MDI", "A janela foi aberta,\nabrir aplicação para visualizar.", ToolTipIcon.Info);
                        frm.MdiParent = this;
                        frm.Show(); notIco.Visible = true;
                        bIco = true;
                    }
                    else { notIco.ShowBalloonTip(50, "", "Hack já está aberto\nabra a aplicação para visualizar", ToolTipIcon.Error); }

                }
                else
                {
                    Application.OpenForms.OfType<Form1>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<Form1>().First().BringToFront();
                }


            }
        }
        private void CDZ_Click(object sender, EventArgs e)
        {

            if (Float.radio == 1)
            {
                SSSS frm = new SSSS();


                if (Application.OpenForms.OfType<SSSS>().Count() == 0)
                {
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<Form1>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<Form1>().First().BringToFront();
                }



            }
            else
            {
                SSSS frm = new SSSS();

                if (Application.OpenForms.OfType<SSSS>().Count() == 0)
                {
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<Form1>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<Form1>().First().BringToFront();
                }


            }
        }

        //--- Montando Opções no Iniciar
        private void IniciarTp()
        {
            var ContextMenu = new ContextMenu();
            ContextMenu.MenuItems.Add(new MenuItem("Abrir Mega Hacks", Exibir_App));
            ContextMenu.MenuItems.Add(new MenuItem("Fechar Mega Hacks", Fechar_Click));
            ContextMenu.MenuItems.Add(new MenuItem("Digimon Story CS", Digi_Click));
            ContextMenu.MenuItems.Add(new MenuItem("Saint Seiya Soldier Soul", CDZ_Click));
            notIco.ContextMenu = ContextMenu;
        }
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login FormMenu = new Login();
            //DialogResult
            string message = "Deseja encerrar o programa?\n\nVocê pode deixar o programa ocultado";
            string title = "Encerrar aplicação";
            MessageBoxButtons bnt = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, bnt, MessageBoxIcon.Stop);
            if (result == DialogResult.Yes)
            {

                this.Close();
                
            }
            
            
        }

        private void esconderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            notIco.Visible = true;
            notIco.ShowBalloonTip(50, "Aviso - Programa rodando", "O programa foi ocultado,\n mas continua ativo", ToolTipIcon.Error);
        }

        private void digimonStoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Float.radio == 1)
            {
                Form1 frm = new Form1();


                if (Application.OpenForms.OfType<Form1>().Count() == 0)
                {
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<Form1>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<Form1>().First().BringToFront();
                }



            }
            else
            {
                Form1 frm = new Form1();

                if (Application.OpenForms.OfType<Form1>().Count() == 0)
                {
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<Form1>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<Form1>().First().BringToFront();
                }


            }
        }

        private void modoMDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Float.radio = 0;
            if (modoLivreToolStripMenuItem.Checked == true)
            { modoLivreToolStripMenuItem.Checked = false; }
            else { modoLivreToolStripMenuItem.Checked = true; Float.radio = 1; }
        }

        private void modoLivreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Float.radio = 1;
            if (modoMDIToolStripMenuItem.Checked == true)
            { modoMDIToolStripMenuItem.Checked = false; }
            else { modoMDIToolStripMenuItem.Checked = true; Float.radio = 0; }
        }

        private void sSSSToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Float.radio == 1)
            {
                SSSS frm = new SSSS();


                if (Application.OpenForms.OfType<SSSS>().Count() == 0)
                {
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<SSSS>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<SSSS>().First().BringToFront();
                }



            }
            else
            {
                SSSS frm = new SSSS();

                if (Application.OpenForms.OfType<SSSS>().Count() == 0)
                {
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<SSSS>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<SSSS>().First().BringToFront();
                }
            }
        }

        private void perfilDoUsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Perfil frm = new Perfil();

            if (Application.OpenForms.OfType<Perfil>().Count() == 0)
            {
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                Application.OpenForms.OfType<Perfil>().First().WindowState = FormWindowState.Normal;
                Application.OpenForms.OfType<Perfil>().First().BringToFront();
            }
        }

        private void codeVeinToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Float.radio == 1)
            {
                CodeVein frm = new CodeVein();


                if (Application.OpenForms.OfType<CodeVein>().Count() == 0)
                {
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<CodeVein>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<CodeVein>().First().BringToFront();
                }



            }
            else
            {
                CodeVein frm = new CodeVein();

                if (Application.OpenForms.OfType<CodeVein>().Count() == 0)
                {
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<CodeVein>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<CodeVein>().First().BringToFront();
                }
            }
        }

        private void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            MousePress = true;
            lastPos = e.Location;
        }

        private void Menu_MouseUp(object sender, MouseEventArgs e)
        {
            MousePress = false;
        }

        private void Menu_MouseMove(object sender, MouseEventArgs e)
        {
            if (MousePress)
            {
                this.Location = new Point(this.Location.X - lastPos.X + e.X, this.Location.Y - lastPos.Y + e.Y);
            }
        }

        private void JumpForce_Click(object sender, EventArgs e)
        {

        }

        private void dragonBallKakarotToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Float.radio == 1)
            {
                Kakarot frm = new Kakarot();


                if (Application.OpenForms.OfType<Kakarot>().Count() == 0)
                {
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<Kakarot>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<Kakarot>().First().BringToFront();
                }



            }
            else
            {
                Kakarot frm = new Kakarot();

                if (Application.OpenForms.OfType<Kakarot>().Count() == 0)
                {
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<Kakarot>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<Kakarot>().First().BringToFront();
                }
            }
        }

        private void jumpForceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Float.radio == 1)
            {
                JumpForce frm = new JumpForce();


                if (Application.OpenForms.OfType<JumpForce>().Count() == 0)
                {
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<JumpForce>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<JumpForce>().First().BringToFront();
                }



            }
            else
            {
                JumpForce frm = new JumpForce();

                if (Application.OpenForms.OfType<JumpForce>().Count() == 0)
                {
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    Application.OpenForms.OfType<JumpForce>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<JumpForce>().First().BringToFront();
                }
            }
        }
    }
}
