using System.Windows.Forms;

namespace Tetris
{
    public class TetrisBlock : PictureBox
    {
        public TetrisBlock()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // TetrisBlock
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TetrisBlock_Paint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void TetrisBlock_Paint(object sender, PaintEventArgs e)
        {
            if(this.BackColor != TetrisColors.backgroundColor &&
                this.BackColor != TetrisColors.ghostColor &&
                this.BackColor != TetrisColors.invisibleGhostColor &&
                this.BackColor != TetrisColors.previewBackColor)
            {
                TetrisColors.DrawCustomBorder(e, this, this.BackColor);
            }
        }
    }
}
