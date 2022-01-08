using System.Windows.Forms;

namespace Tetris
{
    public class NoSelectButton : Button
    {
        public NoSelectButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NoSelectButton
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.NoSelectButton_Paint);
            this.ResumeLayout(false);

        }

        private void NoSelectButton_Paint(object sender, PaintEventArgs e)
        {
            TetrisColors.DrawCustomBorder(e, this, true, 2);
        }
    }
}
