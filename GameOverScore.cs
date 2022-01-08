using System;
using System.Windows.Forms;

namespace Tetris
{
    public partial class GameOverScore : Form
    {
        public GameOverScore(int score, string userLevel)
        {
            InitializeComponent();

            totalScoreLabel.Text = score.ToString();
            levelLabel.Text = userLevel;
        }

        private void noSelectButton1_Click(object sender, EventArgs e)
        {
            string highScoreString = $"{totalScoreLabel.Text};";
            highScoreString += $"{levelLabel.Text};";
            highScoreString += $"{ShortenText(coolName.Text, 32)};";
            highScoreString += $"{ShortenText(coolQuote.Text, 128)}";
            System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\tetris.hsb", highScoreString);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private static string ShortenText(string text, int textLength)
        {
            text = text.Replace(";", "");

            if(text.Length > textLength)
            {
                return text.Substring(0, textLength);
            }
            return text;
        }
    }
}
