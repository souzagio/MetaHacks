using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace MetaHacks
{
    public partial class Email : MetroFramework.Forms.MetroForm
    {
        Int32 iCodUser = 0;
        public Email()
        {
            InitializeComponent();
        }
        MySqlConnection Conexao = new MySqlConnection("server=127.0.0.1; uid=root; pwr=; port=3306; database=giobd;");
        private void mpEmail_Click(object sender, EventArgs e)
        {
            Intermed mid = new Intermed();
            if (txtVal.Text != "")
            {
                int test = Convert.ToInt32(txtVal.Text);

                if (iCodUser != test)
                {
                    MessageBox.Show("Número de verificação errado, verifique\no seu código e tente novamente.", "Código Inválido", MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                }
                else
                {
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand("Update users set Validador=1 where ID = ?", Conexao);
                        Conexao.Open();
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@ID", MySqlDbType.Int16).Value = Games.ID;
                        cmd.Connection = Conexao;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Conta validada com sucesso!\nAproveite nossos hacks!", "Validação concluída", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Conexao.Close();
                        Float.valOk = true;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível efetuar a validação.\nErro indicado : " + ex.ToString());
                    }
                    finally { if (Conexao.State == ConnectionState.Open) Conexao.Close(); }
                }
            }
            else
            {
                MessageBox.Show("Campo não pode ficar vago", "Aviso");
                txtVal.Focus();
            }
        }
        private void Email_Load(object sender, EventArgs e)
        {
            
            try
            {
                Conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select codigo from users where id=?", Conexao);
                MySqlDataReader Dr;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@iID", MySqlDbType.Int16).Value = Games.ID;
                cmd.Connection = Conexao;
                cmd.CommandType = CommandType.Text;

                Dr = cmd.ExecuteReader();
                if (Dr.HasRows)
                {
                    Dr.Read();
                    iCodUser = Convert.ToInt32(Dr["Codigo"].ToString());
                    
                }
                else
                {
                    MessageBox.Show("Código não encontrado");
                }
                Dr.Close();
                Conexao.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (Conexao.State == ConnectionState.Open) Conexao.Close();
            }
        }

        private void txtVal_Enter(object sender, EventArgs e)
        {
            txtVal.Clear();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }   
}
