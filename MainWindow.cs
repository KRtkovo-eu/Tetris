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
        int combo = 0;
        int score = 0;
        int gainedScoreDebugVar = 0;
        int totalClears, currentClears = 0;
        int level = 0;
        bool gameOver = false;
        bool gameRunning = false;
        int PieceSequenceIteration = 0;
        bool cheatUsed = false;
        GameDifficulty currentGameDifficulty;

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

            startingLevelTextBox.Text = "1";

            soundEffectsToolStripMenuItem.Checked = Properties.Settings.Default.PlaySounds;
            playmusicToolStripMenuItem.Checked = Properties.Settings.Default.PlayMusic;
            pieceGhostPositionToolStripMenuItem.Checked = Properties.Settings.Default.ShowGhost;
            trackHighscoreToolStripMenuItem.Checked = Properties.Settings.Default.TrackHighScores;
            startingLevelTextBox.Text = Properties.Settings.Default.StartingLevel;
            currentGameDifficulty = (GameDifficulty)Properties.Settings.Default.Difficulty;
            SetGameDifficulty(currentGameDifficulty);

            if(startingLevelTextBox.Text != "")
            {
                ChangeTetrisBackground(Convert.ToInt32(startingLevelTextBox.Text) - 1);
            }

            this.Size = Properties.Settings.Default.WindowSize;
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            if (new AboutWindow().ShowDialog() == DialogResult.OK)
            {

            }
        }

        public void StartNewGame(int startingLevel)
        {
            // Ensure that no game is running
            TerminateGame(forceHideDialog: true);

            // Save current startingLevel to application settings
            currentGameDifficulty = (GameDifficulty)Properties.Settings.Default.Difficulty;
            Properties.Settings.Default.StartingLevel = startingLevelTextBox.Text;
            Properties.Settings.Default.Save();

            // Reset game area
            foreach (TetrisBlock tetrisBlock in gridPanel.Controls)
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

            // Set cheat check to cheat menu visibility
            cheatUsed = cheatsToolStripMenuItem.Visible;

            combo = 0;
            score = 0;
            currentClears = 0;
            totalClears = 0;

            timeElapsed = 0;
            currentPiece = 0;
            nextPieceInt = 0;
            rotations = 0;
            PieceSequenceIteration = 0;

            TotalScoreLabel.Text = score.ToString();
            LevelLabel.Text = (level + 1).ToString();
            gainedScoreDebugVar = score;
            gainedPoints.Text = $"{gainedScoreDebugVar}";
            gainedPoints.ForeColor = Color.Green;
            ClearsLabel.Text = totalClears.ToString();
            TimeLabel.Text = $"{timeElapsed} sec.";

            ChangeTetrisBackground(level);

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
            // Force sleep between dropping new piece, trying to solve disappearing dropped pieces from game
            System.Threading.Thread.Sleep(16 / (level + 1) * 20);

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
                    x.BackColor = TetrisColors.previewBackColor;
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
            nextLbl.Visible = true;

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
                    if (CheckGameOver() == true)
                    {
                        TerminateGame();
                    }
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
            TimeLabel.Text = $"{timeElapsed} sec.";
        }

        // Clear lowest full row
        private void ClearFullRow(bool clearedFromDrop = false)
        {
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

            UpdateScore(clearedFromDrop);

            currentClears++;
            totalClears ++;
            ClearsLabel.Text = totalClears.ToString();

            // You're leveling by clearing lines, not by scoring points.
            // You level up by clearing[current level] * 10 lines.
            int clearsToPassLevel;
            switch (currentGameDifficulty)
            {
                case GameDifficulty.Begginer:
                    clearsToPassLevel = (level + 1) * 5;
                    break;
                default:
                    clearsToPassLevel = (level + 1) * 10;
                    break;
            }


            if (currentClears >= clearsToPassLevel)
            {
                LevelUp();
                currentClears = 0;
            }

            if (CheckForCompleteRows() > -1)
            {
                ClearFullRow(clearedFromDrop);
            }
        }

        private void UpdateScore(bool clearedFromDrop = false)
        {
            if (CheckForCompleteRows() == -1)
            {
                int[] tetrisScore = { 40, 100, 300, 1200 };
                int tetrisScoringSystem = (level * tetrisScore[combo]) + tetrisScore[combo];
                score += tetrisScoringSystem;

                if(clearedFromDrop)
                {
                    gainedScoreDebugVar += tetrisScoringSystem;
                }
                else
                {
                    gainedScoreDebugVar = tetrisScoringSystem;
                }

                gainedPoints.Text = $"+{gainedScoreDebugVar}";
                gainedPoints.ForeColor = Color.Green;

                TotalScoreLabel.Text = score.ToString();

                // If 4 lines cleared = TETRIS, play TETRIS sound
                if (combo == 3)
                {
                    SoundEffects.PlaySound(Properties.Resources.tetrisClear);
                }
                else
                {
                    SoundEffects.PlaySound(Properties.Resources.line);
                }

                combo = 0;
            }
            else
            {
                combo++;
            }
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
            level++;

            // User is normally playing (level 0-15)
            if (level <= 15)
            {
                if (!isLoopingBeforeStart)
                {
                    SoundEffects.PlaySound(Properties.Resources.clear);
                }
                
                LevelLabel.Text = (level + 1).ToString();

                // Milliseconds per square fall
                // Level 1 = 800 ms per square, level 2 = 716 ms per square, etc.
                int[] levelSpeed;
                switch (currentGameDifficulty)
                {
                    case GameDifficulty.Begginer:
                        levelSpeed = new int[]
                        {
                            // LEVEL
                            // 0    1    2    3    4    5    6    7    8    9    A    B    C    D    E    F
                            800, 750, 700, 650, 600, 550, 500, 450, 400, 350, 300, 250, 200, 180, 160, 140
                        };
                        break;
                    case GameDifficulty.Legend:
                        levelSpeed = new int[]
                        {
                            // LEVEL
                            // 0    1    2    3    4    5    6    7    8    9    A    B    C    D    E    F
                            800, 716, 633, 555, 466, 383, 300, 216, 133, 115, 100, 090, 085, 080, 075, 070
                        };
                        break;
                    default:
                        levelSpeed = new int[]
                        {
                            // LEVEL
                            // 0    1    2    3    4    5    6    7    8    9    A    B    C    D    E    F
                            800, 730, 660, 590, 520, 450, 380, 310, 240, 205, 170, 145, 125, 115, 105, 100
                        };
                        break;
                }
                SpeedTimer.Interval = levelSpeed[level];

                ChangeTetrisBackground(level);
            }
            // User won the level 15, so he won the game
            else
            {
                ChangeTetrisBackground(16);
                TerminateGame(playerInLevel16: true);
            }
        }


        // Game ends if a piece is in the top row when the next piece is dropped
        private bool CheckGameOver()
        {
            Control[] topRow = { box1, box2, box3, box4, box5, box6, box7, box8, box9, box10,
             box11, box12, box13, box14, box15, box16, box17, box18, box19, box20 };

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

        // Change background based on level
        private void ChangeTetrisBackground(int level)
        {
            switch (level)
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
                case 10:
                    tetrisBackground.BackgroundImage = Properties.Resources.levelA;
                    break;
                case 11:
                    tetrisBackground.BackgroundImage = Properties.Resources.levelB;
                    break;
                case 12:
                    tetrisBackground.BackgroundImage = Properties.Resources.levelC;
                    break;
                case 13:
                    tetrisBackground.BackgroundImage = Properties.Resources.levelD;
                    break;
                case 14:
                    tetrisBackground.BackgroundImage = Properties.Resources.levelE;
                    break;
                case 15:
                    tetrisBackground.BackgroundImage = Properties.Resources.levelF;
                    break;
                // Background for player who won the game
                case 16:
                    tetrisBackground.BackgroundImage = Properties.Resources.levelXVI;
                    break;
            }
        }

        private void tetrisContainerPanel_Paint(object sender, PaintEventArgs e)
        {
            TetrisColors.DrawCustomBorder(e, tetrisContainerPanel, true, 4);
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
            Properties.Settings.Default.WindowSize = this.Size;
            Properties.Settings.Default.Save();
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
            // User set starting level 1, internally its level 0, etc.
            int userSelectedLevel = -1;
            if(startingLevelTextBox.Text != "")
            {
                userSelectedLevel = Convert.ToInt32(startingLevelTextBox.Text) - 1;
            }

            if (userSelectedLevel < 0)
            {
                startingLevelTextBox.Text = "1";
                userSelectedLevel = 0;
            }
            else if (userSelectedLevel >= 15)
            {
                startingLevelTextBox.Text = "16";
                userSelectedLevel = 15;
            }

            StartNewGame(userSelectedLevel);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(!e.ClickedItem.Text.Contains("Pause"))
                PauseGame(true);
        }

        public void TerminateGame(bool forceHideDialog = false, bool dialogResetGame = false, bool playerInLevel16 = false)
        {
            SpeedTimer.Stop();
            GameTimer.Stop();
            gameOver = true;

            // Reset block preview area
            nextLbl.Visible = false;
            foreach (TetrisBlock tetrisBlock in nextTetrisBlockPanel.Controls)
            {
                tetrisBlock.BackColor = TetrisColors.previewBackColor;
            }


            if (dialogResetGame && gameRunning)
            {
                DialogResult dg;
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    dg = MessageBox.Show("Game restarted to apply new settings.");
                }
            }

            gameRunning = false;

            if (!forceHideDialog)
            {
                if(playerInLevel16)
                {
                    SoundEffects.PlaySound(Properties.Resources.jingleLevelXVI);
                }
                else
                {
                    SoundEffects.PlaySound(Properties.Resources.gameover);
                }
                SoundEffects.PlayMusic(SoundEffects.MusicState.Stop);

                if(trackHighscoreToolStripMenuItem.Checked && score > 0)
                {
                    // If user played with cheats, allow him to write his score with "Cheated" flag
                    new GameOverScore(score, $"{(level + 1)}", currentGameDifficulty, cheatUsed).ShowDialog();
                    
                }
                else
                {
                    DialogResult dg;
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        dg = MessageBox.Show(playerInLevel16 ? "Congratulations! You just won at Tetris!" : "Game over!");
                    }
                }
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            PauseGame(true);

            Properties.Settings.Default.WindowSize = this.Size;
            Properties.Settings.Default.Save();

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
            if (Regex.IsMatch(keyPressed, @"[^0-9^\b^\n]"))
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }
        }

        private void playmusicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PlayMusic = playmusicToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void soundEffectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PlaySounds = soundEffectsToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void pieceGhostPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowGhost = pieceGhostPositionToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            TerminateGame(forceHideDialog: true, dialogResetGame: true);
        }

        private void trackHighscoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.TrackHighScores = trackHighscoreToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void startingLevelTextBox_TextChanged(object sender, EventArgs e)
        {
            if (startingLevelTextBox.Text != "")
            {
                // User set starting level 1, internally its level 0, etc.
                int userSelectedLevel = Convert.ToInt32(startingLevelTextBox.Text) - 1;

                if (userSelectedLevel < 0)
                {
                    startingLevelTextBox.Text = "1";
                    userSelectedLevel = 0;
                }
                else if (userSelectedLevel >= 15)
                {
                    startingLevelTextBox.Text = "16";
                    userSelectedLevel = 15;
                }

                if (!gameRunning)
                {
                    ChangeTetrisBackground(userSelectedLevel);
                }
            }
        }

        private void previewPanelContainer_Paint(object sender, PaintEventArgs e)
        {
            TetrisColors.DrawCustomBorder(e, previewPanelContainer);
        }

        private void highScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ScoreBoardForm().ShowDialog();
        }

        private void startingLevelTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            Properties.Settings.Default.StartingLevel = startingLevelTextBox.Text;
            Properties.Settings.Default.Save();
        }

        // Cheat Level UP (e.g. for debugging)
        // CTRL+SHIFT+TAB
        private void levelUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cheat enabled only if cheat menu is visible
            if(cheatsToolStripMenuItem.Visible && gameRunning)
            {
                cheatUsed = true;
                LevelUp();
            }
        }

        // Cheat 1000 to Score
        // CTRL+SHIFT+Q
        private void score1000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cheat enabled only if cheat menu is visible
            if (cheatsToolStripMenuItem.Visible && gameRunning)
            {
                cheatUsed = true;
                score += 1000;
                TotalScoreLabel.Text = $"{score}";
                gainedPoints.Text = "+1000";
                gainedPoints.ForeColor = Color.Magenta;
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/KRtkovo-eu/Tetris/wiki");
        }

        public void SetGameDifficulty(GameDifficulty gameDifficulty)
        {
            begginerToolStripMenuItem.Checked = false;
            standardToolStripMenuItem.Checked = false;
            legendToolStripMenuItem.Checked = false;

            switch(gameDifficulty)
            {
                case GameDifficulty.Begginer:
                    begginerToolStripMenuItem.Checked = true;
                    break;
                case GameDifficulty.Standard:
                    standardToolStripMenuItem.Checked = true;
                    break;
                case GameDifficulty.Legend:
                    legendToolStripMenuItem.Checked = true;
                    break;
            }

            Properties.Settings.Default.Difficulty = (int)gameDifficulty;
            Properties.Settings.Default.Save();
        }

        private void begginerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetGameDifficulty(GameDifficulty.Begginer);
        }

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetGameDifficulty(GameDifficulty.Standard);
        }

        private void legendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetGameDifficulty(GameDifficulty.Legend);
        }

        public enum GameDifficulty
        {
            Begginer = 0,
            Standard = 1,
            Legend = 2
        }
    }   
}
