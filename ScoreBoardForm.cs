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

            try
            {
                var scoreFile = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\tetris.hsb");
                var scoreLines = scoreFile.Split(new char[] { '\n' });
                //var scoreSorted = scoreLines.OrderByDescending(x => Convert.ToInt32(x.Split(new char[] { ';' })[0]));

                foreach (var score in scoreLines)
                {
                    if(score.Contains(";") && score != "" && score != "\r" && score != "\n")
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
            catch
            {

            }
        }

        private void noSelectButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
