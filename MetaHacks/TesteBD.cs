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
    public partial class TesteBD : Form
    {
        public TesteBD()
        {
            InitializeComponent();
        }

        private void TesteBD_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Intermed Med = new Intermed();
            if(txtLogin.Text == "" || txtMail.Text == "" || txtNome.Text == "" || txtSenha.Text == "")
            {
                MessageBox.Show("Por favor, não deixe nenhum campo vazio", "Alerta");
            }
            else
            {
               string Mensagem = Med.NeoCad(txtLogin.Text, txtSenha.Text, txtNome.Text, txtMail.Text);
                if(Mensagem == "Confirmado")
                {
                    MessageBox.Show("Cadastro efetuado\nverificar no Banco de dados", "Sucesso, vaquinha!!");
                }
                else
                {
                    MessageBox.Show("Falha ao cadastrar\n" + Mensagem.ToString(), "Aviso");
                }
                
            }
        }
    }
}
