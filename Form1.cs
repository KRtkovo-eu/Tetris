﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        Control[] activePiece = { null, null, null, null };
        Control[] activePiece2 = { null, null, null, null };
        Control[] nextPiece = { null, null, null, null };
        int timeElapsed = 0;
        int currentPiece;
        int nextPieceInt;
        int rotations = 0;
        Color pieceColor = Color.White;
        Color nextPieceColor = Color.White;
        int combo = 0;
        int score = 0;
        int clears = 0;
        int level = 0;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            timer2.Start();

            System.Random random = new System.Random();
            nextPieceInt = random.Next(7);

            dropNewPiece();
        }

        public void dropNewPiece()
        {
            rotations = 0;
            System.Random random = new System.Random();
           
            
            currentPiece = nextPieceInt;
            nextPieceInt = random.Next(7);
            //currentPiece = 2;

            Control[] nextPiecePanel =
            {
                pictureBox201,
                pictureBox202,
                pictureBox203,
                pictureBox204,
                pictureBox205,
                pictureBox206,
                pictureBox207,
                pictureBox208,
                pictureBox209,
                pictureBox210,
                pictureBox211,
                pictureBox212,
                pictureBox213,
                pictureBox214,
                pictureBox215,
                pictureBox216,
            };

            foreach (Control x in nextPiecePanel)
            {
                x.BackColor = Color.White;
            }

            if (nextPieceInt == 0)
            {
                nextPiece[0] = pictureBox203;
                nextPiece[1] = pictureBox207;
                nextPiece[2] = pictureBox211;
                nextPiece[3] = pictureBox215;
                nextPieceColor = Color.Cyan;
            }
            else if (nextPieceInt == 1)
            {
                nextPiece[0] = pictureBox202;
                nextPiece[1] = pictureBox206;
                nextPiece[2] = pictureBox210;
                nextPiece[3] = pictureBox211;
                nextPieceColor = Color.Orange;
            }
            else if (nextPieceInt == 2)
            {
                nextPiece[0] = pictureBox203;
                nextPiece[1] = pictureBox207;
                nextPiece[2] = pictureBox211;
                nextPiece[3] = pictureBox210;
                nextPieceColor = Color.Blue;
            }
            else if (nextPieceInt == 3)
            {
                nextPiece[0] = pictureBox206;
                nextPiece[1] = pictureBox207;
                nextPiece[2] = pictureBox203;
                nextPiece[3] = pictureBox204;
                nextPieceColor = Color.Green;
            }
            else if (nextPieceInt == 4)
            {
                nextPiece[0] = pictureBox202;
                nextPiece[1] = pictureBox203;
                nextPiece[2] = pictureBox207;
                nextPiece[3] = pictureBox208;
                nextPieceColor = Color.Red;
            }
            else if (nextPieceInt == 5)
            {
                nextPiece[0] = pictureBox206;
                nextPiece[1] = pictureBox207;
                nextPiece[2] = pictureBox210;
                nextPiece[3] = pictureBox211;
                nextPieceColor = Color.Yellow;
            }
            else if (nextPieceInt == 6)
            {
                nextPiece[0] = pictureBox207;
                nextPiece[1] = pictureBox210;
                nextPiece[2] = pictureBox211;
                nextPiece[3] = pictureBox212;
                nextPieceColor = Color.Purple;
            }
            
            if (currentPiece == 0)
            {
                activePiece[0] = pictureBox6;
                activePiece[1] = pictureBox16;
                activePiece[2] = pictureBox26;
                activePiece[3] = pictureBox36;
                pieceColor = Color.Cyan;
            }
            else if (currentPiece == 1)
            {
                activePiece[0] = pictureBox4;
                activePiece[1] = pictureBox14;
                activePiece[2] = pictureBox24;
                activePiece[3] = pictureBox25;
                pieceColor = Color.Orange;
            }
            else if (currentPiece == 2)
            {
                activePiece[0] = pictureBox5;
                activePiece[1] = pictureBox15;
                activePiece[2] = pictureBox25;
                activePiece[3] = pictureBox24;
                pieceColor = Color.Blue;
            }
            else if (currentPiece == 3)
            {
                activePiece[0] = pictureBox14;
                activePiece[1] = pictureBox15;
                activePiece[2] = pictureBox5;
                activePiece[3] = pictureBox6;
                pieceColor = Color.Green;
            }
            else if (currentPiece == 4)
            {
                activePiece[0] = pictureBox5;
                activePiece[1] = pictureBox6;
                activePiece[2] = pictureBox16;
                activePiece[3] = pictureBox17;
                pieceColor = Color.Red;
            }
            else if (currentPiece == 5)
            {
                activePiece[0] = pictureBox5;
                activePiece[1] = pictureBox6;
                activePiece[2] = pictureBox15;
                activePiece[3] = pictureBox16;
                pieceColor = Color.Yellow;
            }
            else if (currentPiece == 6)
            {
                activePiece[0] = pictureBox6;
                activePiece[1] = pictureBox15;
                activePiece[2] = pictureBox16;
                activePiece[3] = pictureBox17;
                pieceColor = Color.Purple;
            }

            foreach (Control square in nextPiece)
            {
                square.BackColor = nextPieceColor;
            }

            foreach (Control square in activePiece)
            {
                square.BackColor = pieceColor;
            }
        }
            
        public bool testMove(string direction)
        {
            int currentHighRow = 19;
            int currentLowRow = 0;
            int currentLeftCol = 9;
            int currentRightCol = 0;

            int nextSquare = 0;

            Control newSquare = new Control();

            foreach (Control square in activePiece)
            {
                if (grid.GetRow(square) < currentHighRow)
                {
                    currentHighRow = grid.GetRow(square);
                }
                if (grid.GetRow(square) > currentLowRow)
                {
                    currentLowRow = grid.GetRow(square);
                }
                if (grid.GetColumn(square) < currentLeftCol)
                {
                    currentLeftCol = grid.GetColumn(square);
                }
                if (grid.GetColumn(square) > currentRightCol)
                {
                    currentRightCol = grid.GetColumn(square);
                }
            }

            foreach (Control square in activePiece)
            {
                int squareRow = grid.GetRow(square);
                int squareCol = grid.GetColumn(square);

                //Left
                if (direction == "left" & squareCol > 0)
                {
                    newSquare = grid.GetControlFromPosition(squareCol - 1, squareRow);
                    nextSquare = currentLeftCol;
                }
                else if (direction == "left" & squareCol == 0)
                {
                    return false;
                }

                //Right
                else if (direction == "right" & squareCol < 9)
                {
                    newSquare = grid.GetControlFromPosition(squareCol + 1, squareRow);
                    nextSquare = currentRightCol;
                }
                else if (direction == "right" & squareCol == 9)
                {
                    return false;
                }

                //Down
                else if (direction == "down" & squareRow < 19)
                {
                    newSquare = grid.GetControlFromPosition(squareCol, squareRow + 1);
                    nextSquare = currentLowRow;
                }
                else if (direction == "down" & squareRow == 19)
                {
                    return false;
                }

                if (newSquare.BackColor != Color.White & activePiece.Contains(newSquare) == false & nextSquare > 0)
                {
                    //MessageBox.Show("test2");
                    return false;
                }

            }

            return true;
        }

        public void movePiece(string direction)
        {
            int x = 0;
            foreach (PictureBox square in activePiece)
            {
                square.BackColor = Color.White;
                int squareRow = grid.GetRow(square);
                int squareCol = grid.GetColumn(square);
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

                activePiece2[x] = grid.GetControlFromPosition(newSquareCol, newSquareRow);
                x++;
            }
            x = 0;
            foreach (PictureBox square in activePiece2)
            {
                square.BackColor = pieceColor;
                activePiece[x] = square;
                x++;
            }
        }

        private bool testOverlap()
        {
            foreach (PictureBox square in activePiece2)
            {
                if (square.BackColor != Color.White & activePiece.Contains(square) == false)
                {
                    return false;
                }
            }
            return true;
        }
                    
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {   
            if ((e.KeyCode == Keys.Left | e.KeyCode == Keys.A) & testMove("left") == true)
            {
                movePiece("left");
            }
            else if ((e.KeyCode == Keys.Right | e.KeyCode == Keys.D) & testMove("right") == true)
            {
                movePiece("right");
            }
            else if ((e.KeyCode == Keys.Down | e.KeyCode == Keys.S) & testMove("down") == true)
            {
                movePiece("down");
            }
            else if (e.KeyCode == Keys.Up | e.KeyCode == Keys.W)
            {
                //Rotate

                int square1Col = grid.GetColumn(activePiece[0]);
                int square1Row = grid.GetRow(activePiece[0]);

                int square2Col = grid.GetColumn(activePiece[1]);
                int square2Row = grid.GetRow(activePiece[1]);

                int square3Col = grid.GetColumn(activePiece[2]);
                int square3Row = grid.GetRow(activePiece[2]);

                int square4Col = grid.GetColumn(activePiece[3]);
                int square4Row = grid.GetRow(activePiece[3]);

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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col - 2, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col - 1, square2Row - 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row - 2);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row - 3);
                        
                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 2, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col + 1, square2Row + 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row + 2);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col - 1, square4Row + 3);
                        
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row + 2);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col + 1, square2Row + 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col + 2, square3Row);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row - 1);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 1, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row - 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row - 2);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col - 2, square4Row - 1);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 1, square1Row - 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row + 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row + 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col - 2, square1Row - 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col - 1, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row + 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col - 2, square1Row + 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col - 1, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row - 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 1, square1Row + 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row - 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row - 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 1, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row + 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row + 2);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col - 2, square4Row + 1);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row - 2);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col + 1, square2Row - 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col + 2, square3Row);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row + 1);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col + 1, square1Row - 2);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row - 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col + 1, square3Row);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row + 1);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col - 1, square1Row + 2);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row + 1);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row - 1);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                    if (rotations == 0 & square1Row == 0)
                    {
                        return;
                    }
                    else if (rotations == 1 & (square1Row == 0 | square1Col == 8))
                    {
                        return;
                    }

                    //If test passes, rotate piece
                    if (rotations == 0)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row + 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col - 1, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row - 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col - 1, square4Row - 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row - 1);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col + 1, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col, square3Row + 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 1, square4Row + 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                    if (rotations == 0 & square1Row == 0)
                    {
                        return;
                    }
                    else if (rotations == 1 & (square1Row == 0 | square1Col == 9))
                    {
                        return;
                    }
                    else if (rotations == 2 & square1Row == 0)
                    {
                        return;
                    }
                    else if (rotations == 3 & (square1Row == 0 | square1Col == 0))
                    {
                        return;
                    }

                    //If test passes, rotate piece
                    if (rotations == 0)
                    {
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row - 2);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row - 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col - 2, square4Row);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col + 2, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col + 1, square3Row - 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row - 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col, square2Row + 2);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col + 1, square3Row + 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col + 2, square4Row);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
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
                        activePiece2[0] = grid.GetControlFromPosition(square1Col, square1Row);
                        activePiece2[1] = grid.GetControlFromPosition(square2Col - 2, square2Row);
                        activePiece2[2] = grid.GetControlFromPosition(square3Col - 1, square3Row + 1);
                        activePiece2[3] = grid.GetControlFromPosition(square4Col, square4Row + 2);

                        //Test if new position overlaps another piece. If it does, cancel rotation.
                        if (testOverlap() == true)
                        {
                            rotations = 0;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                //Set old position of piece to white
                foreach (PictureBox square in activePiece)
                {
                    square.BackColor = Color.White;
                }

                //Set new position of piece to that piece's color
                int x = 0;
                foreach (PictureBox square in activePiece2)
                {
                    square.BackColor = pieceColor;
                    activePiece[x] = square;
                    x++;
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Move piece down, or drop new piece if it can't move
            if (testMove("down") == true)
            {
                movePiece("down");
            }
            else
            {
                if (checkForCompleteRows() > -1)
                {
                    ClearFullRow();
                }
                dropNewPiece();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //Update timer
            timeElapsed++;
            label2.Text = "Time: " + timeElapsed.ToString();
        }

        private void ClearFullRow()
        {
            int completedRow = checkForCompleteRows();

            //Turn that row white
            for (int x = 0; x <= 9; x++)
            {
                Control z = grid.GetControlFromPosition(x, completedRow);
                z.BackColor = Color.White;
            }

            //Move all other squares down
            //For each above cleared row
            for (int x = completedRow - 1; x >= 0; x--)
            {
                //For each square in row
                for (int y = 0; y <= 9; y++)
                {
                    //the square
                    Control z = grid.GetControlFromPosition(y, x);

                    //the square below it
                    Control zz = grid.GetControlFromPosition(y, x + 1);

                    zz.BackColor = z.BackColor;
                    z.BackColor = Color.White;
                }
            }

            //Update score
            if (checkForCompleteRows() == -1)
            {
                if (combo % 3 != 0)
                {
                    combo = 0;
                }
            }

            if (combo < 3)
            {
                score = score + 100;
            }
            else if (combo == 3)
            {
                score = score + 500;
            }
            else if (combo > 3 & combo < 6)
            {
                score = score + 100;
            }
            else if (combo >= 6)
            {
                score = score + 900;
            }

            combo++;
            label3.Text = "Score: " + score.ToString();

            clears++;
            label4.Text = "Lines cleared: " + clears;

            if (clears % 10 == 0)
            {
                levelUp();
            }

            if (checkForCompleteRows() > -1)
            {
                //combo++;
                ClearFullRow();
            }
            else
            {
                if (combo % 4 != 0)
                {
                    combo = 0;
                }
            }
        }  

        private int checkForCompleteRows()
        {
            //For each row
            for (int x = 19; x >= 0; x--)
            {


                //For each square in row
                for (int y = 0; y <= 9; y++)
                {
                    Control z = grid.GetControlFromPosition(y /* col */, x /* row */);
                    if (z.BackColor == Color.White)
                    {
                        break;
                    }
                    if (y == 9)
                    {
                        //Return the row that is full
                        return x;
                    }
                }
            }
            return -1; //"null"
        }

        private void levelUp()
        {
            level++;
            label5.Text = "Level " + level.ToString();

            int[] levelSpeed =
            {
                800, 716, 633, 555, 466, 383, 300, 216, 133, 100, 083, 083, 083, 066, 066,
                066, 050, 050, 050, 033, 033, 033, 033, 033, 033, 033, 033, 033, 033, 016
            };

            timer1.Interval = levelSpeed[level];
        }
    }   
}