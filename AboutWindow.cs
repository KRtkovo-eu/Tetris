using System;
using System.Windows.Forms;

namespace Tetris
{
    public partial class AboutWindow : Form
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AboutWindow_Paint(object sender, PaintEventArgs e)
        {
            TetrisColors.DrawCustomBorder(e, this, true, 2);
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            TetrisColors.DrawCustomBorder(e, this, true, 2);
        }

        private void AboutWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
