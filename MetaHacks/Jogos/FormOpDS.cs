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
    public partial class FormOpDS : Form
    {
        public FormOpDS()
        {
            InitializeComponent();
        }

        private void FormOpDS_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = true;
            radioButton5.Checked = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) { Float.p1 = 1; }
            else { Float.p1 = 0; }
            if (radioButton2.Checked == true) { Float.p2 = 1; }
            else { Float.p2 = 0; }
            if (radioButton5.Checked == true) { Float.b1 = 1; }
            else { Float.b1 = 0; }
            TestCheck();
        }
        private void TestCheck()
        {
            //Valor da Taxa de Combo 1
            if (chV1.Checked == true)
            {
                if (txtValue1.Text != "")
                {
                    int iVerifica = int.Parse(txtValue1.Text);
                    if (iVerifica > 100)
                    {
                        Float.Ct1 = 100;
                    }
                    else { Float.Ct1 = int.Parse(txtValue1.Text); }

                }
                else
                {
                    Float.Ct1 = 0;
                    MessageBox.Show("O campo não pode ficar vazio", "Primeiro Valor da Taxa");
                    chV1.Checked = false;
                }
            }
            else { Float.Ct1 = 0; }
            //Valor da Taxa de Combo 2
            if (chV2.Checked == true)
            {

                if (txtValue2.Text != "")
                {
                    int iVerifica = int.Parse(txtValue2.Text);
                    if (iVerifica > 100)
                    {
                        Float.Ct2 = 100;
                    }
                    else { Float.Ct2 = int.Parse(txtValue2.Text); }
                }
                else
                {
                    Float.Ct2 = 0;
                    MessageBox.Show("O campo não pode ficar vazio", "Segundo valor da Taxa");
                    chV2.Checked = false;
                }
            }
            else { Float.Ct2 = 0; }
            //Valor da Taxa de Combo 3
            if (chV3.Checked == true)
            {

                if (txtValue3.Text != "")
                {
                    int iVerifica = int.Parse(txtValue3.Text);
                    if (iVerifica > 100)
                    {
                        Float.Ct3 = 100;
                    }
                    else { Float.Ct3 = int.Parse(txtValue3.Text); }
                }
                else
                {
                    Float.Ct3 = 0;
                    MessageBox.Show("O campo não pode ficar vazio", "Terceiro valor da Taxa");
                    chV3.Checked = false;
                }
            }
            else { Float.Ct3 = 0; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) { Float.p1 = 1; }
            else { Float.p1 = 0; }
            if (radioButton2.Checked == true) { Float.p2 = 1; }
            else { Float.p2 = 0; }
            if (radioButton5.Checked == true) { Float.b1 = 1; }
            else { Float.b1 = 0; }
            //Testar se os campos estão vazios 1
            if (chV1.Checked == true && txtValue1.Text == "")
            {
                MessageBox.Show("Campo não pode ficar vazio", "Sem valor no primeiro campo");
                txtValue1.Focus();
                return;
            }
            //Testar se os campos estão vazios 2
            if (chV2.Checked == true && txtValue2.Text == "")
            {
                MessageBox.Show("Campo não pode ficar vazio", "Sem valor no segundo campo");
                txtValue2.Focus();
                return;
            }
            //Testar se os campos estão vazios 3
            if (chV3.Checked == true && txtValue3.Text == "")
            {
                MessageBox.Show("Campo não pode ficar vazio", "Sem valor no terceiro campo");
                txtValue3.Focus();
                return;
            }
            TestCheck();
            this.Close();
        }

        private void txtValue1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }
    }
}
