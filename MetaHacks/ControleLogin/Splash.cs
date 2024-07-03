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
    public partial class Splash : MetroFramework.Forms.MetroForm
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            metroProgressBar1.Value = metroProgressBar1.Value + 1;

            if (metroProgressBar1.Value >= 100)
            {
                Perfil perf = new Perfil();
                int x = this.Left + (this.Width / 2) - (perf.Width / 2);
                int y = this.Top + (this.Height / 2) - (perf.Height / 2);
                perf.Show();
                perf.Location = new Point(x, y);
                this.Close();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {

        }
    }
}
