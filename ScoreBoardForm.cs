using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Tetris
{
    public partial class ScoreBoardForm : Form
    {
        public ScoreBoardForm()
        {
            InitializeComponent();

            LoadHighScores();
        }

        private void LoadHighScores()
        {
            highScoreList.Items.Clear();
            highScoreList.Refresh();

            var scoreFile = Properties.Settings.Default.HighScoreSave;
            var scoreLines = scoreFile.Split(new char[] { '\n' });

            foreach (var score in scoreLines)
            {
                if (score.Contains(";") && score != "" && score != "\r" && score != "\n")
                {
                    var scoreTextEntry = score.Split(new char[] { ';' });

                    ListViewItem scoreEntry = new ListViewItem();
                    scoreEntry.Text = scoreTextEntry[0];

                    ListViewItem.ListViewSubItem scoreEntryLevel = new ListViewItem.ListViewSubItem();
                    scoreEntryLevel.Text = scoreTextEntry[1];
                    scoreEntry.SubItems.Add(scoreEntryLevel);

                    ListViewItem.ListViewSubItem scoreEntryName = new ListViewItem.ListViewSubItem();
                    scoreEntryName.Text = scoreTextEntry[2];
                    scoreEntry.SubItems.Add(scoreEntryName);

                    ListViewItem.ListViewSubItem scoreEntryQuote = new ListViewItem.ListViewSubItem();
                    scoreEntryQuote.Text = scoreTextEntry[3];
                    scoreEntry.SubItems.Add(scoreEntryQuote);

                    highScoreList.Items.Add(scoreEntry);
                }
            }
        }

        private void noSelectButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void noSelectButton2_Click(object sender, EventArgs e)
        {
            DialogResult dg;
            using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
            {
                dg = MessageBox.Show(this, "Do you really want to erase all high score records from the board?", "Erase High Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dg == DialogResult.Yes)
                {
                    Properties.Settings.Default.HighScoreSave = "";
                    Properties.Settings.Default.Save();

                    LoadHighScores();
                }
            }
        }
    }
}
