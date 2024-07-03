using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace MetaHacks
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        
        Menu frmMenu = new Menu();
        Intermed med = new Intermed();
        public Int16 i = 0;
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                this.SelectNextControl(this.ActiveControl, true, false, true, true);

            }
        }

        private void llCad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            Cadastro frmCad = new Cadastro();
            frmCad.Show();
            Controles(false);
        }
        private void Controles(bool crtl)
        {
            textBox1.Enabled = crtl;
            textBox2.Enabled = crtl;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Cadastro>().Count() == 0 && Application.OpenForms.OfType<RecSenha>().Count() == 0)
            {
                Controles(true);
            }
            
                try
                {
                    i = 0;
                    med.Logar(textBox1.Text, textBox2.Text);
                    i = med.ID;

                    if (i != 0)
                    {
                        Games.ID = i;
                        timer1.Stop();
                        timer2.Start();
                        Float.login = 2;
                        this.Enabled = false;
                        Splash spl = new Splash();
                        int x = this.Left + (this.Width / 2) - (spl.Width / 2);
                        int y = this.Top + (this.Height / 2) - (spl.Height / 2);
                        spl.Show();
                        spl.Location = new Point(x, y);
                    }

                }
                catch
                {
                    timer1.Stop();
                    MessageBox.Show("O servidor se encontra offline no momento,\ntente novamente mais tarde", "Server Offline");
                    button1.Visible = true;
                    btnAdm.Visible = true;
                }

                if (checkBox2.Checked == true)
                {
                    textBox2.PasswordChar = '\u0000';
                }
                else
                {
                    textBox2.PasswordChar = '♦';
                }
            
            /*try
            {
                nome = textBox1.Text.ToLower();
                pass = textBox2.Text;
                if (nome == "giovany" && pass == "Kg1068660")
                {
                    timer1.Stop();
                    Float.login = 1;
                    this.Close();
                }
            }
            catch
            {

            }*/
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Float.login == 1 || Float.login == 2)
            {
                Application.Exit();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecSenha frmRec = new RecSenha();
            frmRec.Show();
            Controles(false);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Float.login == 3) this.Close();
            
        }

        private void Login_Load(object sender, EventArgs e)
        {

            timer1.Start();
            button1.Visible = false;
            btnAdm.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection Conexao = new MySqlConnection("server=localhost; uid=root; pwr=; port=3306; database=giobd;");
                Conexao.Open();

                if (Conexao.State == ConnectionState.Open) Conexao.Close();
                timer1.Start();
                button1.Visible = false;
                MessageBox.Show("Conectado","Servidores online");
            }
            catch { MessageBox.Show("Servidores offilne, tente novamente mais tarde", "Aviso"); }
        }

        private void btnAdm_Click(object sender, EventArgs e)
        {
            Float.login = 3;
           
            this.Close();
        }
    }
}
