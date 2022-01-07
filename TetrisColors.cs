using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public static class TetrisColors
    {
        public static Color backgroundColor = Color.Black;
        public static Color ghostColor = Color.FromArgb(255, 34, 34, 34);
        public static Color invisibleGhostColor = Color.FromArgb(0, 34, 34, 34);
        public static Color pieceI = Color.FromArgb(255, 191, 0, 0); //RED
        public static Color pieceL = Color.FromArgb(255, 191, 191, 0); //YELLOW
        public static Color pieceJ = Color.FromArgb(255, 191, 0, 191); //PURPLE
        public static Color pieceS = Color.FromArgb(255, 0, 0, 191); //BLUE
        public static Color pieceZ = Color.FromArgb(255, 0, 191, 0); //GREEN
        public static Color pieceO = Color.FromArgb(255, 0, 191, 191); //CYAN
        public static Color pieceT = Color.FromArgb(255, 115, 115, 115); //GRAY

        public static void DrawCustomBorder(PaintEventArgs e, Control panel, bool outset = false, int borderSize = 2)
        {
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
            outset ? SystemColors.ControlLightLight : SystemColors.Control, borderSize, outset ? ButtonBorderStyle.Outset : ButtonBorderStyle.Inset,
            outset ? SystemColors.ControlLightLight : SystemColors.Control, borderSize, outset ? ButtonBorderStyle.Outset : ButtonBorderStyle.Inset,
            SystemColors.ControlLightLight, borderSize, outset ? ButtonBorderStyle.Outset : ButtonBorderStyle.Inset,
            SystemColors.ControlLightLight, borderSize, outset ? ButtonBorderStyle.Outset : ButtonBorderStyle.Inset);
        }

        public static void DrawCustomBorder(PaintEventArgs e, Control panel, Color color)
        {
            int borderSize = 4;
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
            color, borderSize, ButtonBorderStyle.Outset,
            color, borderSize, ButtonBorderStyle.Outset,
            color, borderSize, ButtonBorderStyle.Outset,
            color, borderSize, ButtonBorderStyle.Outset);
        }
    }
}
