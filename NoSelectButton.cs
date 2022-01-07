using System.Windows.Forms;

namespace Tetris
{
    public class NoSelectButton : Button
    {
        public NoSelectButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
