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
using System.Net;
using System.Net.Mail;

namespace MetaHacks
{
    public partial class RecSenha : Form
    {
        public RecSenha()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            string senha, sLogin, Mensagem;
            try
            {
                Intermed mid = new Intermed();
                Mensagem = mid.RecSenha(textBox1.Text);

                if (Mensagem != "Senha Recuperada!")
                {
                    MessageBox.Show("Não foi possível recuperar a senha.\nVerifique" +
                        " se o email está\ncorreto e tente novamente.", "Aviso, falha na recuperação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.Enabled = true;
                }
                else
                {
                    sLogin = mid.varRecLogin;
                    senha = mid.varRecSenha;
                    /*string strEmail = "Este é um e-mail automático, não responda a este e-mail : \nSegue abaixo as informações de login e senha para a " +
                        "recuperação de sua conta.\n        Login :  " + sLogin + "\n      Senha : " + senha + "\nLembre-se de colocar uma senha que irá se lembrar " +
                        "dessa vez.";
                    MailMessage mensagem = new MailMessage("metahacksgiovany@gmail.com", textBox1.Text);
                    mensagem.Subject = "E-mail para recuperação de conta";
                    mensagem.Body = strEmail;
                    mensagem.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                    mensagem.BodyEncoding = Encoding.GetEncoding("UTF-8");

                    SmtpClient smClient = new SmtpClient("smtp.gmail.com", 587);
                    smClient.UseDefaultCredentials = false;
                    smClient.Credentials = new NetworkCredential("metahacksgiovany@gmail.com", "Kg1068660#");
                    smClient.EnableSsl = true;
                    smClient.Send(mensagem);
                    MessageBox.Show("E-mail enviado para sua caixa de entrada.\nVerifique-a para recuperar sua conta.\nCaso o e-mail não apareça aguarde alguns minutos.");*/
                    MessageBox.Show("Login: " + sLogin + "\nSenha: " + senha, "Senha Recuperada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
            }
            catch { MessageBox.Show("Uma falha occoreu ao tentar se conectar com o servidor", "Falha no sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }
    }
}
