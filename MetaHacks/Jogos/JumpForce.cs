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
    public partial class JumpForce : Form
    {
        bool MouseDwn;
        Point lastPt;
        public JumpForce()
        {
            InitializeComponent();
        }

        private void JumpForce_Load(object sender, EventArgs e)
        {

        }

        private void JumpForce_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDwn = true;
            lastPt = e.Location;
        }

        private void JumpForce_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDwn)
            {
                this.Location = new Point(this.Location.X - lastPt.X + e.X, this.Location.Y - lastPt.Y + e.Y);
            }
        }

        private void JumpForce_MouseUp(object sender, MouseEventArgs e)
        {
            MouseDwn = false;
        }
    }
}
