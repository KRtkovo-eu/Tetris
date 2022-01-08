using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MainWindow : Form
    {

        // Handle inputs - triggered on any keypress
        // Cleanup needed
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameRunning)
            {
                PauseGame(false);

                if (!CheckGameOver() & ((e.KeyCode == Keys.Left | e.KeyCode == Keys.A) & TestMove("left") == true))
                {
                    SoundEffects.PlaySound(Properties.Resources.selection);
                    MovePiece("left");
                }
                else if (!CheckGameOver() & ((e.KeyCode == Keys.Right | e.KeyCode == Keys.D) & TestMove("right") == true))
                {
                    SoundEffects.PlaySound(Properties.Resources.selection);
                    MovePiece("right");
                }
                else if ((e.KeyCode == Keys.Down | e.KeyCode == Keys.S) & TestMove("down") == true)
                {
                    SoundEffects.PlaySound(Properties.Resources.selection);
                    MovePiece("down");
                }
                else if (e.KeyCode == Keys.Up | e.KeyCode == Keys.W)
                {
                    SoundEffects.PlaySound(Properties.Resources.selection);
                    //Rotate

                    int square1Col = gridPanel.GetColumn(activePiece[0]);
                    int square1Row = gridPanel.GetRow(activePiece[0]);

                    int square2Col = gridPanel.GetColumn(activePiece[1]);
                    int square2Row = gridPanel.GetRow(activePiece[1]);

                    int square3Col = gridPanel.GetColumn(activePiece[2]);
                    int square3Row = gridPanel.GetRow(activePiece[2]);

                    int square4Col = gridPanel.GetColumn(activePiece[3]);
                    int square4Row = gridPanel.GetRow(activePiece[3]);

                    if (currentPiece == 0) //The line piece
                    {
                        //Test if piece is too close to edge of board
                        if (rotations == 0 & (square1Col == 0 | square1Col == 1 | square1Col == 9))
                        {
                            return;
                        }
                        else if (rotations == 1 & (square3Col == 0 | square3Col == 1 | square3Col == 9))
                        {
                            return;
                        }

                        //If test passes, rotate piece
                        if (rotations == 0)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col - 2, square1Row);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col - 1, square2Row - 1);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col, square3Row - 2);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col + 1, square4Row - 3);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 1)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col + 2, square1Row);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col + 1, square2Row + 1);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col, square3Row + 2);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col - 1, square4Row + 3);

                            if (TestOverlap() == true)
                            {
                                rotations = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (currentPiece == 1) //The normal L
                    {
                        //Test if piece is too close to edge of board
                        if (rotations == 0 & (square1Col == 8 | square1Col == 9))
                        {
                            return;
                        }
                        else if (rotations == 2 & (square1Col == 9))
                        {
                            return;
                        }

                        //If test passes, rotate piece
                        if (rotations == 0)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col, square1Row + 2);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col + 1, square2Row + 1);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col + 2, square3Row);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col + 1, square4Row - 1);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 1)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col + 1, square1Row);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col, square2Row - 1);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col - 1, square3Row - 2);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col - 2, square4Row - 1);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 2)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col + 1, square1Row - 1);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col, square2Row);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col - 1, square3Row + 1);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col, square4Row + 2);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 3)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col - 2, square1Row - 1);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col - 1, square2Row);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col, square3Row + 1);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col + 1, square4Row);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (currentPiece == 2) //The backwards L
                    {
                        //Test if piece is too close to edge of board
                        if (rotations == 0 & (square1Col == 0 | square1Col == 1))
                        {
                            return;
                        }
                        else if (rotations == 2 & square1Col == 0)
                        {
                            return;
                        }

                        //If test passes, rotate piece
                        if (rotations == 0)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col - 2, square1Row + 1);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col - 1, square2Row);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col, square3Row - 1);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col + 1, square4Row);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 1)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col + 1, square1Row + 1);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col, square2Row);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col - 1, square3Row - 1);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col, square4Row - 2);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 2)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col + 1, square1Row);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col, square2Row + 1);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col - 1, square3Row + 2);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col - 2, square4Row + 1);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 3)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col, square1Row - 2);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col + 1, square2Row - 1);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col + 2, square3Row);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col + 1, square4Row + 1);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (currentPiece == 3) //The normal S
                    {
                        //Test if piece is too close to edge of board
                        if (rotations == 0 & (square1Row == 1 | square1Col == 9))
                        {
                            return;
                        }
                        else if (rotations == 1 & square1Col == 0)
                        {
                            return;
                        }

                        //If test passes, rotate piece
                        if (rotations == 0)
                        {

                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col + 1, square1Row - 2);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col, square2Row - 1);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col + 1, square3Row);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col, square4Row + 1);


                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 1)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col - 1, square1Row + 2);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col, square2Row + 1);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col - 1, square3Row);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col, square4Row - 1);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (currentPiece == 4) //The backwards S
                    {
                        //Test if piece is too close to edge of board
                        if (rotations == 1 & square1Col == 8)
                        {
                            return;
                        }

                        //If test passes, rotate piece
                        if (rotations == 0)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col, square1Row + 1);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col - 1, square2Row);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col, square3Row - 1);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col - 1, square4Row - 2);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 1)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col, square1Row - 1);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col + 1, square2Row);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col, square3Row + 1);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col + 1, square4Row + 2);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else if (currentPiece == 5) //The square
                    {
                        //The square cannot rotate
                        return;
                    }
                    else if (currentPiece == 6) //The pyramid
                    {
                        //Test if piece is too close to edge of board
                        if (rotations == 1 & square1Col == 9)
                        {
                            return;
                        }
                        else if (rotations == 3 & square1Col == 0)
                        {
                            return;
                        }

                        //If test passes, rotate piece
                        if (rotations == 0)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col, square1Row);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col, square2Row - 2);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col - 1, square3Row - 1);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col - 2, square4Row);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 1)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col, square1Row);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col + 2, square2Row);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col + 1, square3Row - 1);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col, square4Row - 2);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 2)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col, square1Row);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col, square2Row + 2);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col + 1, square3Row + 1);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col + 2, square4Row);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (rotations == 3)
                        {
                            activePiece2[0] = gridPanel.GetControlFromPosition(square1Col, square1Row);
                            activePiece2[1] = gridPanel.GetControlFromPosition(square2Col - 2, square2Row);
                            activePiece2[2] = gridPanel.GetControlFromPosition(square3Col - 1, square3Row + 1);
                            activePiece2[3] = gridPanel.GetControlFromPosition(square4Col, square4Row + 2);

                            //Test if new position overlaps another piece. If it does, cancel rotation.
                            if (TestOverlap() == true)
                            {
                                rotations = 0;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }

                    //Set old position of piece to black
                    foreach (PictureBox square in activePiece)
                    {
                        square.BackColor = TetrisColors.backgroundColor;
                    }

                    DrawGhost();

                    //Set new position of piece to that piece's color
                    int x = 0;
                    foreach (PictureBox square in activePiece2)
                    {
                        square.BackColor = colorList[currentPiece];
                        activePiece[x] = square;
                        x++;
                    }
                }
                else if (!CheckGameOver() & (e.KeyCode == Keys.Space || e.KeyCode == Keys.End || e.KeyCode == Keys.PageDown))
                {
                    // Hard drop
                    SoundEffects.PlaySound(Properties.Resources.fall);

                    // Calculate score addition based on dropped height
                    var droppedPieceScoreBonus = (Ghost[0].Location.Y - activePiece[0].Location.Y) / 20;
                    score += droppedPieceScoreBonus;
                    TotalScoreLabel.Text = score.ToString();
                    gainedScoreDebugVar = droppedPieceScoreBonus;
                    gainedPoints.Text = $"+{gainedScoreDebugVar}";

                    for (int x = 0; x < 4; x++)
                    {
                        Ghost[x].BackColor = colorList[currentPiece];
                        activePiece[x].BackColor = TetrisColors.backgroundColor;
                    }
                    if (CheckForCompleteRows() > -1)
                    {
                        ClearFullRow(true);
                    }

                    DropNewPiece();
                }
            }

            // Cheat menu
            if (e.Control && e.Alt && e.KeyCode == Keys.W)
            {
                cheatsToolStripMenuItem.Visible = !cheatsToolStripMenuItem.Visible;

                if (cheatsToolStripMenuItem.Visible)
                {
                    cheatUsed = true;
                }
            }
        }
    }
}