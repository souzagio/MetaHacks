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
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace MetaHacks
{
    public partial class Cadastro : Form
    {
        Login lgn = new Login();
        bool bErro = false;
        byte[] btImagem;
        string sSenha, sRandom, sLogin;
        public Cadastro()
        {
            InitializeComponent();
        }

        private void Cadastro_FormClosing(object sender, FormClosingEventArgs e)
        {
            Float.login = 2;
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            TestarBotoes();
            if(geraPontosSenha(sSenha) < 60)
            {
                MessageBox.Show("Senha muito fraca, insira uma mais forte", "Aviso");
                return;
            }
            if (bErro == true) return;
            string Mensagem;
            Intermed Med = new Intermed();
            string sEmail = txtemail.Text + comboBox1.Text;

            try
            {
                Random rdm = new Random();
                Int32 num = rdm.Next(10000, 99999);
                sRandom = num.ToString();
                Mensagem = Med.Cadastrar(txtLogin.Text, txtNome.Text, txtSenha.Text, DateTime.Now, sEmail, num);
                if (Mensagem == "Confirmar")
                {
                    string mail = Med.EnviarEmail(txtLogin.Text, sEmail, sRandom);
                    MessageBox.Show("Cadastro efeituado com sucesso\n\n" + mail, "E-mail de verificação enviado");
                    Float.login = 2;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Falha ao cadastrar\n" + Mensagem.ToString(), "Aviso");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public static int Contador(string word)
        {
            int count = 0;
            for (int i = 0; i < word.Length; i++)
            {
                count++;
            }
            return count;
        }
        public static int ContadorLg(string word)
        {
            int count = 0;
            for (int i = 0; i < word.Length; i++)
            {
                count++;
            }
            return count;
        }
        private void ForcaDaSenha(int senha)
        {
            mpSenha.Value = senha;
            if (senha == 100)
            {
                mpSenha.Style = MetroFramework.MetroColorStyle.Green;
                lblLvSenha.ForeColor = Color.Green;
                lblLvSenha.Text = "Segura";
            }
            else if(senha <100 && senha>=80)
            {
                mpSenha.Style = MetroFramework.MetroColorStyle.Yellow;
                lblLvSenha.ForeColor = Color.Yellow;
                lblLvSenha.Text = "Forte";
            }
            else if(senha<80 && senha>=60)
            {
                mpSenha.Style = MetroFramework.MetroColorStyle.Orange;
                lblLvSenha.ForeColor = Color.Orange;
                lblLvSenha.Text = "Não segura";
            }
            else if(senha<60 && senha >= 1)
            {
                mpSenha.Style = MetroFramework.MetroColorStyle.Red;
                lblLvSenha.ForeColor = Color.Red;
                lblLvSenha.Text = "Fraca";
            }
            else
            {
                mpSenha.Style = MetroFramework.MetroColorStyle.White;
                lblLvSenha.ForeColor = Color.Transparent;
                lblLvSenha.Text = "";
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            TestarLogin();
            if (comboBox1.Text.Contains("@") == false || comboBox1.Text.Contains(".com") == false)
            {
                comboBox1.Text = "@hotmail.com";
            }
            sSenha = txtSenha.Text;
            int ForcaSenha = geraPontosSenha(sSenha);
            ForcaDaSenha(ForcaSenha);
            if (txtConf.Text != txtSenha.Text) { errorProvider2.SetError(txtConf, "Senhas não coincidem"); }
            else { errorProvider2.Clear(); }
            if (txtLogin.Text != "")
            {
                string log = txtLogin.Text;
                int i = Contador(log);
                if (i < 4) errorProvider1.SetError(txtLogin, "Mínimo de quatro caracteres");
                else { errorProvider1.Clear(); }
            }
        }

        private void Cadastro_Load(object sender, EventArgs e)
        {
            //Delays e Sempre visível
            TP.InitialDelay = 500;
            TP.AutoPopDelay = 3000;
            TP.ReshowDelay = 1000;
            TP.ShowAlways = true;
            //Definir onde ele será exibido
            TP.SetToolTip(this.txtNome, "Insira seu nome completo");
            TP.SetToolTip(this.txtLogin, "Crie um login para acessar o hack");
            TP.SetToolTip(this.txtSenha, "Cria uma senha com 10 caracteres");
            TP.SetToolTip(this.txtConf, "Confirme a senha");
            TP.SetToolTip(this.txtemail, "Insira um e-mail válido");
        }
        private void TestarBotoes()
        {

            /*MemoryStream ms = new MemoryStream();
            Properties.Resources.devi.Save(ms, ImageFormat.Png);
            btImagem =  ms.ToArray();
            //BinaryReader Br = new BinaryReader(ms);
            //btImagem = Br.ReadBytes((int)ms.Length);*/
            if (txtNome.Text == "")
            {
                MessageBox.Show("Insira algum nome", "Campo Vazio");
                txtNome.Focus();
                bErro = true;
                return;
            }
            if (txtLogin.Text == "")
            {
                MessageBox.Show("Insira algum login", "Campo Vazio");
                txtLogin.Focus();
                bErro = true;
                return;
            }
            else
            {
                string log = txtLogin.Text;
                int i = Contador(log);
                if (i < 4)
                {
                    MessageBox.Show("Insira um login com 4 caracteres,\nno mínimo", "Login muito curto");
                    txtLogin.Focus();
                    bErro = true;
                    return;
                }
            }
            if (txtemail.Text == "")
            {
                MessageBox.Show("Insira algum e-mail válido", "Campo Vazio");
                txtemail.Focus();
                bErro = true;
                return;
            }
            if (txtSenha.Text == "")
            {
                MessageBox.Show("Insira alguma senha válida", "Campo Vazio");
                txtSenha.Focus();
                bErro = true;
                return;
            }
            else
            {
                string senha = txtSenha.Text;
                int i = Contador(senha);
                if (i < 10)
                {
                    MessageBox.Show("Insira uma senha com 10 dígitos", "Campo Vazio");
                    txtSenha.Focus();
                    bErro = true;
                    return;
                }
            }
            if (txtConf.Text == "")
            {
                MessageBox.Show("Confirme a senha", "Campo Vazio");
                txtConf.Focus();
                bErro = true;
                return;
            }
            if (txtSenha.Text != "")
            {
                string senha = txtSenha.Text;
                if (txtConf.Text != senha)
                {
                    MessageBox.Show("Senhas não compatíveis", "Verificar Senhas!!");
                    txtSenha.Clear();
                    txtConf.Clear();
                    bErro = true;
                    return;
                }
            }
            bErro = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public int geraPontosSenha(string senha)
        {
            if (senha == null) return 0;
            int pontosPorTamanho = GetPontoPorTamanho(senha);
            int pontosPorMinusculas = GetPontoPorMinusculas(senha);
            int pontosPorMaiusculas = GetPontoPorMaiusculas(senha);
            int pontosPorDigitos = GetPontoPorDigitos(senha);
            int pontosPorSimbolos = GetPontoPorSimbolos(senha);
            return pontosPorTamanho + pontosPorMinusculas + pontosPorMaiusculas + pontosPorDigitos + pontosPorSimbolos;
        }

        private int GetPontoPorTamanho(string senha)
        {
            return Math.Min(10, senha.Length) * 6;
        }

        private int GetPontoPorMinusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[a-z]", "").Length;
            return Math.Min(1, rawplacar) * 10;
        }

        private int GetPontoPorMaiusculas(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[A-Z]", "").Length;
            return Math.Min(1, rawplacar) * 10;
        }

        private int GetPontoPorDigitos(string senha)
        {
            int rawplacar = senha.Length - Regex.Replace(senha, "[0-9]", "").Length;
            return Math.Min(1, rawplacar) * 10;
        }

        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private int GetPontoPorSimbolos(string senha)
        {
            int rawplacar = Regex.Replace(senha, "[a-zA-Z0-9]", "").Length;
            return Math.Min(1, rawplacar) * 10;
        }
        private void TestarLogin()
        {
            Intermed mid = new Intermed();
            sLogin = mid.ConferirLogin(txtLogin.Text);
            if (txtLogin.Text != "")
            {
                if (txtLogin.Text == sLogin)
                {
                    lblConfLog.Text = "Indisponível";
                    lblConfLog.ForeColor = Color.Red;
                }
                else
                {
                    lblConfLog.Text = "Disponível";
                    lblConfLog.ForeColor = Color.Green;
                }                
            }
            else
            {
                lblConfLog.Text = "";
            }
        }

    }
}
