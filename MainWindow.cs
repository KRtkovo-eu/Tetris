using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MainWindow : Form
    {
        // Initialize global variables
        Control[] activePiece = { null, null, null, null };
        Control[] activePiece2 = { null, null, null, null };
        Control[] nextPiece = { null, null, null, null };
        Control[] savedPiece = { null, null, null, null };
        Control[] Ghost = { null, null, null, null };
        List<int> PieceSequence = new List<int>();
        int timeElapsed = 0;
        int currentPiece;
        int nextPieceInt;
        int rotations = 0;
        Color pieceColor = TetrisColors.backgroundColor;
        Color savedPieceColor = TetrisColors.backgroundColor;
        int combo = 0;
        int score = 0;
        int clears = 0;
        int level = 0;
        bool gameOver = false;
        bool gameRunning = false;
        int PieceSequenceIteration = 0;

        readonly Color[] colorList = 
        {  
            TetrisColors.pieceI, // I piece
            TetrisColors.pieceL, // L piece
            TetrisColors.pieceJ, // J piece
            TetrisColors.pieceS, // S piece
            TetrisColors.pieceZ, // Z piece
            TetrisColors.pieceO, // O piece
            TetrisColors.pieceT  // T piece
        };

        // Load main window
        public MainWindow()      
        {
            InitializeComponent();

            ResizeRedraw = true;
            DoubleBuffered = true;

            ChangeTetrisBackground(level);
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            if (new AboutWindow().ShowDialog() == DialogResult.OK)
            {
                //StartNewGame(level);
                startingLevelTextBox.Text = "0";
            }
        }

        public void StartNewGame(int startingLevel)
        {
            TerminateGame(forceHideDialog: true);

            // Reset game area
            foreach(TetrisBlock tetrisBlock in gridPanel.Controls)
            {
                tetrisBlock.BackColor = TetrisColors.backgroundColor;
            }

            // Reset block preview area
            foreach (TetrisBlock tetrisBlock in nextTetrisBlockPanel.Controls)
            {
                tetrisBlock.BackColor = TetrisColors.backgroundColor;
            }

            // Reset checking variables
            SpeedTimer.Interval = 800;
            gameRunning = true;
            gameOver = false;
            level = 0;
            for(int i=0; i < startingLevel; i++)
            {
                LevelUp(isLoopingBeforeStart: true); ;
            }

            combo = 0;
            score = 0;
            clears = 0;

            timeElapsed = 0;
            currentPiece = 0;
            nextPieceInt = 0;
            rotations = 0;

            ScoreLabel.Text = score.ToString();
            LevelLabel.Text = level.ToString();
            ScoreUpdateLabel.Text = score.ToString();
            ClearsLabel.Text = clears.ToString();
            TimeLabel.Text = timeElapsed.ToString();

            ChangeTetrisBackground(level);

            //ScoreUpdateLabel.Text = "";
            SpeedTimer.Start();
            GameTimer.Start();

            SoundEffects.PlayMusic();

            // Initialize/reset ghost piece
            // box1 through box4 are invisible
            activePiece2[0] = box1;
            activePiece2[1] = box2;
            activePiece2[2] = box3;
            activePiece2[3] = box4;

            // Generate piece sequence
            System.Random random = new System.Random();
            while (PieceSequence.Count < 7)
            {
                int x = random.Next(7);
                if (!PieceSequence.Contains(x))
                {
                    PieceSequence.Add(x);
                }
            }

            // Select first piece
            nextPieceInt = PieceSequence[0];
            PieceSequenceIteration++;

            DropNewPiece();
        }

        public void DropNewPiece()
        {
            // Reset number of times current piece has been rotated
            rotations = 0;

            // Move next piece to current piece
            currentPiece = nextPieceInt;

            // If last piece of PieceSequence, generate new PieceSequence
            if (PieceSequenceIteration == 7)
            {
                PieceSequenceIteration = 0;

                // Scramble PieceSequence
                PieceSequence.Clear();
                System.Random random = new System.Random();
                while (PieceSequence.Count < 7)
                {
                    int x = random.Next(7);
                    if (!PieceSequence.Contains(x))
                    {
                        PieceSequence.Add(x);
                    }
                }
            }

            // Select next piece from PieceSequence
            nextPieceInt = PieceSequence[PieceSequenceIteration];
            PieceSequenceIteration++;

            // If not first move, clear next piece panel
            if (nextPiece.Contains(null) == false)
            {
                foreach (Control x in nextPiece)
                {
                    x.BackColor = TetrisColors.backgroundColor;
                }
            }

            // Layout options for next piece
            Control[,] nextPieceArray = 
            {
                { box203, box207, box211, box215 }, // I piece
                { box202, box206, box210, box211 }, // L piece
                { box203, box207, box211, box210 }, // J piece
                { box206, box207, box203, box204 }, // S piece
                { box202, box203, box207, box208 }, // Z piece
                { box206, box207, box210, box211 }, // O piece
                { box207, box210, box211, box212 }  // T piece
            };

            // Retrieve layout for next piece
            for (int x = 0; x < 4; x++)
            {
                nextPiece[x] = nextPieceArray[nextPieceInt,x];
            }

            // Populate next piece panel with correct color
            foreach (Control square in nextPiece)
            {
                square.BackColor = colorList[nextPieceInt];
            }

            // Layout options for falling piece
            Control[,] activePieceArray =
            {
                { box6, box16, box26, box36 }, // I piece
                { box4, box14, box24, box25 }, // L piece
                { box5, box15, box25, box24 }, // J piece
                { box14, box15, box5, box6 },  // S piece
                { box5, box6, box16, box17 },  // Z piece
                { box5, box6, box15, box16 },  // O piece
                { box6, box15, box16, box17 }  // T piece
            };

            // Select falling piece
            for (int x = 0; x < 4; x++)
            {
                activePiece[x] = activePieceArray[currentPiece, x];
            }

            // This is needed for DrawGhost()
            for (int x = 0; x < 4; x++)
            {
                activePiece2[x] = activePieceArray[currentPiece, x];
            }

            // Check for game over
            foreach (Control box in activePiece)
            {
                if (box.BackColor != TetrisColors.backgroundColor & box.BackColor != (pieceGhostPositionToolStripMenuItem.Checked ? TetrisColors.ghostColor : TetrisColors.invisibleGhostColor))
                {
                    //Game over!
                    TerminateGame();
                    return;
                }
            }

            // Draw ghost piece
            DrawGhost();

            // Populate falling piece squares with correct color
            foreach (Control square in activePiece)
            {
                square.BackColor = colorList[currentPiece];
            }
        }

        // Test if a potential move (left/right/down) would be outside the grid or overlap another piece
        public bool TestMove(string direction)
        {
            int currentHighRow = 21;
            int currentLowRow = 0;
            int currentLeftCol = 9;
            int currentRightCol = 0;

            int nextSquare = 0;

            Control newSquare = new Control();

            // Determine highest, lowest, left, and right rows of potential move
            foreach (Control square in activePiece)
            {
                if (gridPanel.GetRow(square) < currentHighRow)
                {
                    currentHighRow = gridPanel.GetRow(square);
                }
                if (gridPanel.GetRow(square) > currentLowRow)
                {
                    currentLowRow = gridPanel.GetRow(square);
                }
                if (gridPanel.GetColumn(square) < currentLeftCol)
                {
                    currentLeftCol = gridPanel.GetColumn(square);
                }
                if (gridPanel.GetColumn(square) > currentRightCol)
                {
                    currentRightCol = gridPanel.GetColumn(square);
                }
            }

            // Test if any squares would be outside of grid
            foreach (Control square in activePiece)
            {
                int squareRow = gridPanel.GetRow(square);
                int squareCol = gridPanel.GetColumn(square);

                // Left
                if (direction == "left" & squareCol > 0)
                {
                    newSquare = gridPanel.GetControlFromPosition(squareCol - 1, squareRow);
                    nextSquare = currentLeftCol;
                }
                else if (direction == "left" & squareCol == 0)
                {
                    // Move would be outside of grid, left
                    return false;
                }

                // Right
                else if (direction == "right" & squareCol < 9)
                {
                    newSquare = gridPanel.GetControlFromPosition(squareCol + 1, squareRow);
                    nextSquare = currentRightCol;
                }
                else if (direction == "right" & squareCol == 9)
                {
                    // Move would be outside of grid, right
                    return false;
                }

                // Down
                else if (direction == "down" & squareRow < 21)
                {
                    newSquare = gridPanel.GetControlFromPosition(squareCol, squareRow + 1);
                    nextSquare = currentLowRow;
                }
                else if (direction == "down" & squareRow == 21)
                {
                    return false;
                    // Move would be below grid
                }

                // Test if potential move would overlap another piece
                if ((newSquare.BackColor != TetrisColors.backgroundColor & newSquare.BackColor != (pieceGhostPositionToolStripMenuItem.Checked ? TetrisColors.ghostColor : TetrisColors.invisibleGhostColor) & activePiece.Contains(newSquare) == false & nextSquare > 0))
                {
                    return false;
                }

            }

            // All tests passed
            return true;
        }

        public void MovePiece(string direction)
        {
            // Erase old position of piece
            // and determine new position based on input direction
            int x = 0;
            foreach (PictureBox square in activePiece)
            {
                square.BackColor = TetrisColors.backgroundColor;
                int squareRow = gridPanel.GetRow(square);
                int squareCol = gridPanel.GetColumn(square);
                int newSquareRow = 0;
                int newSquareCol = 0;
                if (direction == "left")
                {
                    newSquareCol = squareCol - 1;
                    newSquareRow = squareRow;
                }
                else if (direction == "right")
                {
                    newSquareCol = squareCol + 1;
                    newSquareRow = squareRow;
                }
                else if (direction == "down")
                {
                    newSquareCol = squareCol;
                    newSquareRow = squareRow + 1;
                }

                activePiece2[x] = gridPanel.GetControlFromPosition(newSquareCol, newSquareRow);
                x++;
            }

            // Copy activePiece2 to activePiece
            x = 0;
            foreach (PictureBox square in activePiece2)
            {

                activePiece[x] = square;
                x++;
            }

            // Draw ghost piece (must be between erasing old position and drawing new position)
            DrawGhost();

            // Draw piece in new position
            x = 0;
            foreach (PictureBox square in activePiece2)
            {
                square.BackColor = colorList[currentPiece];
                x++;
            }
        }

        // Test if a potential rotation would overlap another piece
        private bool TestOverlap()
        {
            foreach (PictureBox square in activePiece2)
            {
                if ((square.BackColor != TetrisColors.backgroundColor & square.BackColor != (pieceGhostPositionToolStripMenuItem.Checked ? TetrisColors.ghostColor : TetrisColors.invisibleGhostColor) & activePiece.Contains(square) == false))
                {
                    return false;
                }
            }
            return true;
        }
        
        // Timer for piece movement speed - increases with game level
        // Speed is controlled by LevelUp() method
        private void SpeedTimer_Tick(object sender, EventArgs e)
        {
            if (CheckGameOver() == true)
            {
                TerminateGame();
            }

            else
            {
                //Move piece down, or drop new piece if it can't move
                if (TestMove("down") == true)
                {
                    MovePiece("down");
                }
                else
                {
                    if (CheckGameOver() == true)
                    {
                        TerminateGame();
                    }
                    if (CheckForCompleteRows() > -1)
                    {
                        ClearFullRow();
                    }
                    DropNewPiece();
                }
            }
        }

        // Game time (seconds elapsed)
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            TimeLabel.Text = timeElapsed.ToString();
        }

        // Clear lowest full row
        private void ClearFullRow()
        {
            SoundEffects.PlaySound(Properties.Resources.line);

            int completedRow = CheckForCompleteRows();

            //Turn that row white
            for (int x = 0; x <= 9; x++)
            {
                Control z = gridPanel.GetControlFromPosition(x, completedRow);
                z.BackColor = TetrisColors.backgroundColor;
            }

            //Move all other squares down
            for (int x = completedRow - 1; x >= 0; x--) //For each row above cleared row
            {
                //For each square in row
                for (int y = 0; y <= 9; y++)
                {
                    //the square
                    Control z = gridPanel.GetControlFromPosition(y, x);

                    //the square below it
                    Control zz = gridPanel.GetControlFromPosition(y, x + 1);

                    zz.BackColor = z.BackColor;
                    z.BackColor = TetrisColors.backgroundColor;
                }
            }

            UpdateScore();

            clears++;
            ClearsLabel.Text = clears.ToString();

            if (clears % 10 == 0)
            {
                LevelUp();
            }

            if (CheckForCompleteRows() > -1)
            {
                ClearFullRow();
            }
        }

        private void UpdateScore()
        {
            // 1-3 line clear is worth 100 per line
            // Quad line clear (no combo) is worth 800
            // 2 or more quad line clears in a row is worth 1200 

            bool skipComboReset = false;

            // Single clear
            if (combo == 0)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+100";
            }

            // Double clear
            else if (combo == 1)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+200";
            }

            // Triple clear
            else if (combo == 2)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+300";
            }

            // Quad clear, start combo
            else if (combo == 3)
            {
                score += 500;
                ScoreUpdateLabel.Text = "+800";
                skipComboReset = true;
            }

            // Single clear, broken combo
            else if (combo > 3 && combo % 4 == 0)
            {
                score += 100;
                ScoreUpdateLabel.Text = "+100";
            }

            // Double clear, broken combo
            else if (combo > 3 && ((combo - 1) % 4 == 0))
            {
                score += 100;
                ScoreUpdateLabel.Text = "+200";
            }

            // Triple clear, broken combo
            else if (combo > 3 && ((combo - 2) % 4 == 0))
            {
                score += 100;
                ScoreUpdateLabel.Text = "+300";
            }

            // Quad clear, continue combo
            else if (combo > 3 && ((combo - 3) % 4 == 0))
            {
                score += 900;
                ScoreUpdateLabel.Text = "+1200";
                skipComboReset = true;
            }

            if (CheckForCompleteRows() == -1 && skipComboReset == false)
            {
                // 1-3 line clear
                combo = 0;
            }
            else
            {
                // Quad clear
                combo++;
            }

            ScoreLabel.Text = score.ToString();
            //ScoreUpdateTimer.Start();
        }

        // Return row number of lowest full row
        // If no full rows, return -1
        private int CheckForCompleteRows()
        {
            // For each row
            for (int x = 21; x >= 2; x--)
            {
                // For each square in row
                for (int y = 0; y <= 9; y++)
                {
                    Control z = gridPanel.GetControlFromPosition(y, x);
                    if (z.BackColor == TetrisColors.backgroundColor)
                    {
                        break;
                    }
                    if (y == 9)
                    {
                        // Return full row number
                        return x;
                    }
                }
            }
            return -1; // "null"
        }

        // Increase fall speed
        private void LevelUp(bool isLoopingBeforeStart = false)
        {
            if (!isLoopingBeforeStart)
            {
                SoundEffects.PlaySound(Properties.Resources.clear);
            }

            level++;
            LevelLabel.Text = level.ToString();

            // Milliseconds per square fall
            // Level 1 = 800 ms per square, level 2 = 716 ms per square, etc.
            int[] levelSpeed =
            {
                800, 716, 633, 555, 466, 383, 300, 216, 133, 100, 083, 083, 083, 066, 066,
                066, 050, 050, 050, 033, 033, 033, 033, 033, 033, 033, 033, 033, 033, 016
            };

            // Speed does not change after level 29
            if (level <= 29)
            {
                SpeedTimer.Interval = levelSpeed[level];
            }

            ChangeTetrisBackground(level);
        }


        // Game ends if a piece is in the top row when the next piece is dropped
        private bool CheckGameOver()
        {
            Control[] topRow = { box1, box2, box3, box4, box5, box6, box7, box8, box9, box10 };

            foreach (Control box in topRow)
            {
                if ((box.BackColor != TetrisColors.backgroundColor & box.BackColor != (pieceGhostPositionToolStripMenuItem.Checked ? TetrisColors.ghostColor : TetrisColors.invisibleGhostColor) & !activePiece.Contains(box)))
                {
                    //Game over!
                    return true;
                }
            }

            if (gameOver == true)
            {
                return true;
            }

            return false;
        }

        // Clear score update notification every 2 seconds
        private void ScoreUpdateTimer_Tick(object sender, EventArgs e)
        {
                ScoreUpdateLabel.Text = "";
                ScoreUpdateTimer.Stop();
        }

        // Change background based on level
        private void ChangeTetrisBackground(int level)
        {
            switch (level % 10)
            {
                case 0:
                    tetrisBackground.BackgroundImage = Properties.Resources.level0;
                    break;
                case 1:
                    tetrisBackground.BackgroundImage = Properties.Resources.level1;
                    break;
                case 2:
                    tetrisBackground.BackgroundImage = Properties.Resources.level2;
                    break;
                case 3:
                    tetrisBackground.BackgroundImage = Properties.Resources.level3;
                    break;
                case 4:
                    tetrisBackground.BackgroundImage = Properties.Resources.level4;
                    break;
                case 5:
                    tetrisBackground.BackgroundImage = Properties.Resources.level5;
                    break;
                case 6:
                    tetrisBackground.BackgroundImage = Properties.Resources.level6;
                    break;
                case 7:
                    tetrisBackground.BackgroundImage = Properties.Resources.level7;
                    break;
                case 8:
                    tetrisBackground.BackgroundImage = Properties.Resources.level8;
                    break;
                case 9:
                    tetrisBackground.BackgroundImage = Properties.Resources.level9;
                    break;
            }
        }

    private void tetrisContainerPanel_Paint(object sender, PaintEventArgs e)
        {
            TetrisColors.DrawCustomBorder(e, tetrisContainerPanel, true, 4);
        }

        private void nextTetrisBlockPanel_Paint(object sender, PaintEventArgs e)
        {
            TetrisColors.DrawCustomBorder(e, nextTetrisBlockPanel);
        }

        private void statusPanel_Paint(object sender, PaintEventArgs e)
        {
            TetrisColors.DrawCustomBorder(e, statusPanel);
        }

        private void statusContainerPanel_Paint(object sender, PaintEventArgs e)
        {
            TetrisColors.DrawCustomBorder(e, statusContainerPanel, true, 4);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            this.Refresh();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutTetrisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Pause game
            if (!pauseGameToolStripMenuItem.Checked)
            {
                PauseGame(true);
            }

            if (new AboutWindow().ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void pauseGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PauseGame(!pauseGameToolStripMenuItem.Checked);
        }

        public void PauseGame(bool pauseGame)
        {
            if (gameRunning)
            {
                if (pauseGame)
                {
                    SoundEffects.PlayMusic(SoundEffects.MusicState.Pause);
                    GameTimer.Stop();
                    SpeedTimer.Stop();
                    pauseGameToolStripMenuItem.Checked = true;
                }
                else
                {
                    SoundEffects.PlayMusic(SoundEffects.MusicState.Play);
                    GameTimer.Start();
                    SpeedTimer.Start();
                    pauseGameToolStripMenuItem.Checked = false;
                }
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartNewGame(Convert.ToInt32(startingLevelTextBox.Text));
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(!e.ClickedItem.Text.Contains("Pause"))
                PauseGame(true);
        }

        public void TerminateGame(bool forceHideDialog = false, bool dialogResetGame = false)
        {
            SpeedTimer.Stop();
            GameTimer.Stop();
            gameOver = true;

            if (dialogResetGame && gameRunning)
            {
                MessageBox.Show("Game restarted to apply new settings.");
            }

            gameRunning = false;

            if (!forceHideDialog)
            {
                SoundEffects.PlaySound(Properties.Resources.gameover);
                SoundEffects.PlayMusic(SoundEffects.MusicState.Stop);
                MessageBox.Show("Game over!");
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            PauseGame(true);

            DialogResult dg;
            using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
            {
                dg = MessageBox.Show(this, "End TETRIS?", "TETRIS for Windows", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            e.Cancel = (dg == DialogResult.No);
        }
        private void startingLevelTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyPressed = e.KeyChar.ToString();

            // Check for a naughty character in the KeyDown event.
            if (Regex.IsMatch(keyPressed, @"[^0-9^\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }
        }

        private void playmusicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PlayMusic = playmusicToolStripMenuItem.Checked;
        }

        private void soundEffectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PlaySounds = soundEffectsToolStripMenuItem.Checked;
        }

        private void pieceGhostPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminateGame(forceHideDialog: true, dialogResetGame: true);
        }
    }   
}
