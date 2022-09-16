using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PcMan
{
    public partial class Form1 : Form
    {
        bool goup, godown, goleft, goright, isGameOver;

        int score, playerSpeed, redGhostSpeed, yellowGhostSpeed, pinkGhostX, pinkGhostY;

        public Form1()
        {
            InitializeComponent();
            resetGame();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            { 
                goup = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true) 
            { 
                resetGame();
            }

        }

        private void mainGameTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            if (goleft == true)
            {
                msPacman.Left -= playerSpeed;
                msPacman.Image = Properties.Resources.left;
            }
            if (goright == true)
            {
                msPacman.Left += playerSpeed;
                msPacman.Image = Properties.Resources.right;
            }
            if (goup == true)
            {
                msPacman.Top -= playerSpeed;
                msPacman.Image = Properties.Resources.Up;
            }
            if (godown == true)
            {
                msPacman.Top += playerSpeed;
                msPacman.Image = Properties.Resources.down;
            }

            if (msPacman.Left < -10) {
                msPacman.Left = 680;
            }
            if (msPacman.Left > 680)
            {
                msPacman.Left = -10;
            }

            if (msPacman.Top < -10)
            {
                msPacman.Top = 550;
            }
            if (msPacman.Top > 550)
            {
                msPacman.Top = 0;
            }

            
            foreach (Control x in this.Controls) 
            {
                if (x is PictureBox) 
                {
                    /*Keeps track of coins - counts coins as score if visible*/
                    if ((string)x.Tag == "coin" && x.Visible == true) 
                    {
                        if (msPacman.Bounds.IntersectsWith(x.Bounds)) 
                        {
                            score += 1;
                            x.Visible = false;
                        }
                    }


                    if ((string)x.Tag == "wall")
                    {
                        if (msPacman.Bounds.IntersectsWith(x.Bounds)) 
                        {
                            gameOver("You Lose!");
                        }

                        if (pinkGhost.Bounds.IntersectsWith(x.Bounds)) 
                        {
                            pinkGhostX = -pinkGhostX;
                        }

                    }

                    if ((string)x.Tag == "ghost")
                    {
                        if (msPacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("You Lose!");
                        }
                    }
                }
            }

            //moving ghosts

            redGhost.Left += redGhostSpeed;

            if (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds) || redGhost.Bounds.IntersectsWith(pictureBox2.Bounds)) 
            {
                redGhostSpeed = -redGhostSpeed;
            }

            yellowGhost.Left += yellowGhostSpeed;

            if (yellowGhost.Bounds.IntersectsWith(pictureBox3.Bounds) || yellowGhost.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                yellowGhostSpeed = -yellowGhostSpeed;
            }

            pinkGhost.Left -= pinkGhostX;
            pinkGhost.Top -= pinkGhostY;

            if (pinkGhost.Left < 0 || pinkGhost.Left > 620)
            {
                pinkGhostX = -pinkGhostX;//reverse velocity 
            }

            if (pinkGhost.Top < 0 || pinkGhost.Top > 520) 
            {
                pinkGhostY = -pinkGhostY;//reverse velocity 
            }

            if (score == 46) 
            {
                gameOver("You Win!");
            }

        }

        private void resetGame() 
        {
            txtScore.Text = "Score: 0";
            score = 0;

            redGhostSpeed = 5;
            yellowGhostSpeed = 5;
            pinkGhostX = 5;
            pinkGhostY = 5;
            playerSpeed = 8;

            isGameOver = false;

            msPacman.Left = 31;
            msPacman.Top = 46;

            redGhost.Left = 200;
            redGhost.Top = 55;

            yellowGhost.Left = 448;
            yellowGhost.Top = 445;

            pinkGhost.Left = 525;
            pinkGhost.Top = 235;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }

            gameTimer.Start();
        }

        private void gameOver(string message)
        {
            isGameOver = true;

            gameTimer.Stop();

            txtScore.Text += "Score: " + score + Environment.NewLine + message;

        }
    }
}
