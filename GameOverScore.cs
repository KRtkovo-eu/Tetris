using System;
using System.Windows.Forms;

namespace Tetris
{
    public partial class GameOverScore : Form
    {
        private bool _isCheater;
        MainWindow.GameDifficulty _gameDifficulty;

        public GameOverScore(int score, string userLevel, MainWindow.GameDifficulty gameDifficulty, bool isCheater = false)
        {
            InitializeComponent();

            totalScoreLabel.Text = score.ToString();
            levelLabel.Text = userLevel;
            _isCheater = isCheater;
            _gameDifficulty = gameDifficulty;
        }

        private void noSelectButton1_Click(object sender, EventArgs e)
        {
            SaveAndClose();
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

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            else if (Form.ModifierKeys == Keys.None && keyData == Keys.Enter)
            {
                SaveAndClose();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void SaveAndClose()
        {
            string highScoreString = $"{totalScoreLabel.Text};";
            highScoreString += _isCheater ? $"CHEATER {ShortenText(coolName.Text, 32)};" : $"{ShortenText(coolName.Text, 32)};";
            highScoreString += $"{levelLabel.Text};";
            highScoreString += $"{_gameDifficulty};";
            highScoreString += _isCheater ? "I was cheating all the time, I'm such a bad player!" : $"{ShortenText(coolQuote.Text, 128)}";
            highScoreString += "\n";

            Properties.Settings.Default.HighScoreSave += highScoreString;
            Properties.Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
