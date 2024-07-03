using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MetaHacks
{
    public partial class Perfil : Form
    {
        bool controles = false;
        bool bErro = false;
        string sfoto;
        byte[] btImagem = null;
        MySqlConnection Conexao = new MySqlConnection("server=localhost; uid=root; pwr=; port=3306; database=giobd;");
        Intermed mid = new Intermed();
        
        public Perfil()
        {
            InitializeComponent();
            this.Cursor = new Cursor(Properties.Resources.favicon__13_.Handle);
        }

        private void Perfil_Load(object sender, EventArgs e)
        {
            Int16 i = Games.ID;
            mid.Conectar(i);
            i = mid.iValidador;
            if(mid.iValidador == 0)
            {
                Float.valOk = false;
            }
            else
            {
                Float.valOk = true;
            }
            HabilitarControles(false);
            if (Float.login == 2)
            {
                int iOk = ValidarPin();
                
                if (iOk > 0)
                {
                    Float.login = 3;
                    this.Close();
                }
                btnImg.Visible = false;
                btnEditar.Enabled = false;
                textBox1.Focus();
            }
            txtExibir.Text = mid.varNome;
            lblID.Text = i.ToString();
            lblEmail.Text = mid.varEmail;
            lblData.Text = mid.dData.ToString("dd/MM/yyyy");
            lblModify.Text = mid.dAlter.ToString("dd/MM/yyyy");
            ValidarPin();
            if (mid.varImagem != null)
            {
                MemoryStream MemoS = new MemoryStream(mid.varImagem);
                boximg.Image = Image.FromStream(MemoS);
            }
            textBox1.Focus();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if(controles == false) 
            {
                HabilitarControles(true); 
                controles = true;
                btnEditar.Text = "Cancelar";
            }
            else { HabilitarControles(false); controles = false; btnEditar.Text = "Editar Perfil"; }
        }
        
        private void HabilitarControles(bool status)
        {
            if (status == false)
            {
                txtNome.Visible = false;
                txtEmail.Visible = false;
                txtSenha.Visible = false;
                txtConf.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
                btnSalvar.Enabled = false;
                btnImg.Enabled = false;
                txtNome.Clear();
                txtEmail.Clear();
                txtSenha.Clear();
                txtConf.Clear();
                checkBox1.Checked = false;
                checkBox1.Visible = false;
            }
            else
            {
                txtNome.Visible = true;
                txtEmail.Visible = true;
                txtSenha.Visible = true;
                txtConf.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                btnSalvar.Enabled = true;
                btnImg.Enabled = true;
                checkBox1.Visible = true;
                txtNome.Text = mid.varNome;
                txtEmail.Text = mid.varEmail;

            }
        }
        private int ValidarPin()
        {
            DateTime dToday = DateTime.Now;
            //if (mid.varImagem == null) mid.varImagem = [1, 2, 3];
            int iValidade = DateTime.Compare(mid.dValidade, dToday);
            //MessageBox.Show(mid.dValidade.ToString());
            if (iValidade <= 0)
            {
                lblStatus.Text = "Invalidado";
                lblStatus.ForeColor = Color.FromArgb(192, 0, 0);
                lblCodigo.Text = "Expirado";
                lblCodigo.ForeColor = Color.FromArgb(192, 0, 0);
            }
            else
            {
                lblStatus.Text = "Validado";
                lblStatus.ForeColor = Color.FromArgb(0, 192, 0);
                lblCodigo.Text = mid.dValidade.ToString("dd/MM/yyyy");
                lblCodigo.ForeColor = Color.FromArgb(0, 192, 0);
            }
            return iValidade;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(Float.valOk == false)
            {
                textBox1.Enabled = false;
                btnValidar.Enabled = false;
                btnAtivar.Visible = true;
            }else
            {
                textBox1.Enabled = true;
                btnValidar.Enabled = true;
                btnAtivar.Visible = false;
            }
            if (txtSenha.Text != "")
            {
                string senha = txtSenha.Text;
                if (txtConf.Text != senha) { errorProvider1.SetError(txtConf, "Senhas não coincidem"); }
                else { errorProvider1.Clear(); }
            }
            if (checkBox1.Checked == true)
            {
                txtSenha.PasswordChar = '\u0000';
                txtConf.PasswordChar = '\u0000';
            }
            else
            {
                txtSenha.PasswordChar = '♦';
                txtConf.PasswordChar = '♦';
            }
            
        }

        private void Perfil_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Float.login == 2)
            {
                Application.Exit();
            }
            else
            {
                Login lgn = new Login();
                lgn.Close();
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            DateTime dNow = DateTime.Now;
           
            if (Float.login == 2)
            {
                try
                {
                    Int16 i = 0;
                    Conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("select val from Pincode where codigo = ?", Conexao);
                    MySqlDataReader Dr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@Codigo", MySqlDbType.VarChar).Value = textBox1.Text;
                    cmd.Connection = Conexao;
                    cmd.CommandType = CommandType.Text;


                    Dr = cmd.ExecuteReader();
                    if (Dr.HasRows)
                    {
                        Dr.Read();
                        i = Convert.ToInt16(Dr["val"].ToString());
                        if (i != 0)
                        {
                            string Mensagem = mid.Validar(Games.ID, dNow, i);
                            MessageBox.Show(Mensagem, "Validação concluída", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Float.login = 3;
                            Dr.Close();
                            Conexao.Close();
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Pin inexistente ou \nFoi digitado errado.\n\nVerifique o seu pin\nE digite um válido.", "Código Não Validado");
                        textBox1.Clear();
                        textBox1.Focus();
                        Dr.Close();
                        Conexao.Close();
                        return;
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro na validação :" + ex);
                }
                finally
                {
                    if (Conexao.State == ConnectionState.Open) Conexao.Close();
                }
                
            }
            else
            {
                try
                {
                    Int16 i = 0;
                    Conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("select val from Pincode where codigo = ?", Conexao);
                    MySqlDataReader Dr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@Codigo", MySqlDbType.VarChar).Value = textBox1.Text;
                    cmd.Connection = Conexao;
                    cmd.CommandType = CommandType.Text;


                    Dr = cmd.ExecuteReader();
                    if (Dr.HasRows)
                    {
                        Dr.Read();
                        i = Convert.ToInt16(Dr["val"].ToString());
                        if (i != 0)
                        {
                            if (i == 1)
                            {
                                int result = DateTime.Compare(mid.dValidade, DateTime.Now);
                                if (result > 0)
                                {
                                    MessageBox.Show("Você já possui um pin válido!\nPor favor, espere até que ele expire.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                    return;
                                }
                                
                            }
                            string Mensagem = mid.Validar(Games.ID, dNow, i);
                            MessageBox.Show(Mensagem, "Validação concluída", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            ValidarPin();
                            Dr.Close();
                            Conexao.Close();
                            textBox1.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Insira um Pin válido", "Atenção");
                            Dr.Close();
                            Conexao.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Pin inexistente ou \nFoi digitado errado.\n\nVerifique o seu pin\nE digite um válido.", "Código Não Validado");
                        textBox1.Clear();
                        textBox1.Focus();
                        Dr.Close();
                        Conexao.Close();
                        return;
                    }

                    Conexao.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro na validação :" + ex);
                }
                finally
                {
                    if (Conexao.State == ConnectionState.Open) Conexao.Close();
                }
            }
        }

        private void Perfil_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                this.SelectNextControl(this.ActiveControl, true, true, true, true);

            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Tem certeza que deseja\nexcluir esse perfil?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                string Mensagem;
                try
                {
                    Mensagem = mid.Excluir(Games.ID);
                    MessageBox.Show(Mensagem, "Concluído!");
                    Application.Exit();
                }
                catch
                {
                    MessageBox.Show("Falha na Exclusão do usuário", "Aviso de erro");
                }
            }
        }

        private void btnImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfile = new OpenFileDialog();
            opfile.Filter = "JPG Files(*.jpg)|*jpg|PNG Files(*.png)|*.png|Alls Files(*.*)|*.*";
            if(opfile.ShowDialog() == DialogResult.OK)
            {
                sfoto = opfile.FileName.ToString();
                boximg.ImageLocation = sfoto;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            TestarBotoes();
            if (bErro == true) return;
            string Mensagem;
            try
            {
                Mensagem = mid.AlterarUsuario(btImagem, txtNome.Text, txtSenha.Text, DateTime.Now, txtEmail.Text, Games.ID);
                MessageBox.Show(Mensagem, "Informação");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            HabilitarControles(false);
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
        private void TestarBotoes()
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Insira algum nome", "Campo Vazio");
                txtNome.Focus();
                bErro = true;
                return;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Insira algum e-mail válido", "Campo Vazio");
                txtEmail.Focus();
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
            if (sfoto != null)
            {
                FileStream fstream = new FileStream(this.sfoto, FileMode.Open, FileAccess.Read);
                BinaryReader Br = new BinaryReader(fstream);
                btImagem = Br.ReadBytes((int)fstream.Length);
            }
            else btImagem = mid.varImagem;
            bErro = false;
        }

        private void btnAtivar_Click(object sender, EventArgs e)
        {
            Email frmMail = new Email();
            Float.valOk = false;
            frmMail.Show();
        }
    }
}
