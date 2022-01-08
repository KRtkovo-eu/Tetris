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
            try
            {
                string highScoreString = $"{totalScoreLabel.Text};";
                highScoreString += $"{levelLabel.Text};";
                highScoreString += $"{ShortenText(coolName.Text, 32)};";
                highScoreString += $"{ShortenText(coolQuote.Text, 128)}";
                highScoreString += "\n";

                var highScoreFile = AppDomain.CurrentDomain.BaseDirectory + "\\tetris.hsb";

                if (!System.IO.File.Exists(highScoreFile))
                {
                    System.IO.File.Create(highScoreFile);
                }
                System.IO.File.AppendAllText(highScoreFile, highScoreString);
            }
            catch
            {

            }

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
