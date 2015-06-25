using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {

        struct changeListType
        {
            public int col;
            public int row;
        }
        const int numOfCells = 400; // 20x20
        Color deadColor = Color.DarkGray, aliveColor = Color.Yellow;
        Color[,] grid = new Color[(int)Math.Sqrt(numOfCells), (int)Math.Sqrt(numOfCells)];
        changeListType[] changeList = new changeListType[numOfCells];
        int amountOfChanges;
        int lastArray;

        public Form1()
        {
            InitializeComponent();
            lastArray = (int)Math.Sqrt(numOfCells);
            for (int row = 0; row < lastArray; row++)
            {
                for (int col = 0; col < lastArray; col++)
                {
                    Label lblLife = new Label();
                    lblLife.BackColor = deadColor;
                    lblLife.Size = new Size(22, 22);
                    lblLife.AutoSize = false;
                    lblLife.Name = "lbllife-" + row + "-" + col;
                    lblLife.Margin = new Padding(2);
                    lblLife.Font = new Font("Arial", 5);
                    lblLife.Text = row + "-" + col;
                    lblLife.Click += lblLife_Click;
                    flowLayoutPanel1.Controls.Add(lblLife);
                    grid[row, col] = deadColor;
                }
            }
        }

        void lblLife_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            int col, row;
            string[] splitStr = (sender as Label).Name.Split('-');
            row = int.Parse(splitStr[1]);
            col = int.Parse(splitStr[2]);
            int rcRow = row;
            int rcCol = col;
            if (me.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //MessageBox.Show(col.ToString() + row.ToString()    );
                if (grid[row, col] == deadColor)
                {
                    grid[row, col] = aliveColor;
                    (sender as Label).BackColor = aliveColor;
                }
                else
                {
                    grid[row, col] = deadColor;
                    (sender as Label).BackColor = deadColor;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGo_Click(object sender, EventArgs e, int row, int col)
        {
            for (row = 0; row < 19; row++)
            {
                for (col = 0; col < 19; col++)
                {
                    if (grid[row, col] == deadColor)
                    {
                        if (CheckBaby(row, col) == true)
                        {
                            grid[row, col] = aliveColor;
                            this.Controls["lbl" + row + "0"].BackColor = aliveColor;
                        }
                    }
                    else
                    {
                        if (CheckLonely(row, col) == true || CheckCrowded(row, col) == true)
                        {
                            grid[row, col] = deadColor;
                            this.Controls["lbl" + row + "0"].BackColor = deadColor;
                        }
                        if (CheckLives(row, col) == true)
                        {
                            grid[row, col] = aliveColor;
                            this.Controls["lbl" + row + "0"].BackColor = aliveColor;
                        }
                    }
                }
            }
        }//loop to be added to make game run automatically, rather than 1 click per step
            
        bool CheckLonely(int row, int col)
        {
            bool Lonely = false;
            int neighbourCount = 0;
            if (ValidCell(row, col + 1) )
            {
                if (grid[row, col + 1] == aliveColor)//Checks cell to the right
                {
                    neighbourCount++;
                }
            }
            if (ValidCell(row + 1, col + 1) )
            {   
                if (grid[row + 1, col + 1] == aliveColor)//Checks cell below and to the right
                {
                        neighbourCount++;
                }
                if (ValidCell(row + 1, col) )
                {
                    if (grid[row + 1, col] == aliveColor)//Checks cell below
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row + 1, col - 1) )
                {
                    if (grid[row + 1, col - 1] == aliveColor)//Checks cell below and to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row, col - 1) )
                {
                    if (grid[row, col - 1] == aliveColor)//Checks cell to the left
                    {   
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col - 1))
                {
                    if (grid[row - 1, col - 1] == aliveColor)//Checks cell above and to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col) )
                {
                    if (grid[row - 1, col] == aliveColor)//Checks cell above 
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col + 1) )
                {
                    if (grid[row - 1, col + 1] == aliveColor)//Checks cell above and to the right
                    {
                        neighbourCount++;
                    }
                }
            if (neighbourCount <= 1)
            {
                Lonely = true;
            }
        }
            return Lonely;
    }//Checks if cell has 0-1 neighbours

        bool CheckCrowded(int row, int col)
        {
            bool Crowded = false;
            int neighbourCount = 0;
            if (ValidCell(row, col + 1))
            {
                if (grid[row, col + 1] == aliveColor)//Checks cell to the right
                {
                    neighbourCount++;
                }
            }
            if (ValidCell(row + 1, col + 1))
            {
                if (grid[row + 1, col + 1] == aliveColor)//Checks cell below and to the right
                {
                    neighbourCount++;
                }
                if (ValidCell(row + 1, col))
                {
                    if (grid[row + 1, col] == aliveColor)//Checks cell below
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row + 1, col - 1))
                {
                    if (grid[row + 1, col - 1] == aliveColor)//Checks cell below and to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row, col - 1))
                {
                    if (grid[row, col - 1] == aliveColor)//Checks cell to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col - 1))
                {
                    if (grid[row - 1, col - 1] == aliveColor)//Checks cell above and to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col))
                {
                    if (grid[row - 1, col] == aliveColor)//Checks cell above 
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col + 1))
                {
                    if (grid[row - 1, col + 1] == aliveColor)//Checks cell above and to the right
                    {
                        neighbourCount++;
                    }
                }
                if (neighbourCount >= 4 && neighbourCount <= 8)
                {
                    Crowded = true;
                }
            }
            return Crowded;
        }//Checks if cell has 4-8 neighbours

        bool CheckLives(int row, int col)
        {
            bool Lives = false;
            int neighbourCount = 0;
            if (ValidCell(row, col + 1))
            {
                if (grid[row, col + 1] == aliveColor)//Checks cell to the right
                {
                    neighbourCount++;
                }
            }
            if (ValidCell(row + 1, col + 1))
            {
                if (grid[row + 1, col + 1] == aliveColor)//Checks cell below and to the right
                {
                    neighbourCount++;
                }
                if (ValidCell(row + 1, col))
                {
                    if (grid[row + 1, col] == aliveColor)//Checks cell below
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row + 1, col - 1))
                {
                    if (grid[row + 1, col - 1] == aliveColor)//Checks cell below and to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row, col - 1))
                {
                    if (grid[row, col - 1] == aliveColor)//Checks cell to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col - 1))
                {
                    if (grid[row - 1, col - 1] == aliveColor)//Checks cell above and to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col))
                {
                    if (grid[row - 1, col] == aliveColor)//Checks cell above 
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col + 1))
                {
                    if (grid[row - 1, col + 1] == aliveColor)//Checks cell above and to the right
                    {
                        neighbourCount++;
                    }
                }
                if (neighbourCount == 2 || neighbourCount == 3)
                {
                    Lives = true;
                }
            }
            return Lives;
        }//Checks if cell has 2-3 neighbours

        bool CheckBaby(int row, int col)
        {
            bool Baby = false;
            int neighbourCount = 0;
            if (ValidCell(row, col + 1))
            {
                if (grid[row, col + 1] == aliveColor)//Checks cell to the right
                {
                    neighbourCount++;
                }
            }
            if (ValidCell(row + 1, col + 1))
            {
                if (grid[row + 1, col + 1] == aliveColor)//Checks cell below and to the right
                {
                    neighbourCount++;
                }
                if (ValidCell(row + 1, col))
                {
                    if (grid[row + 1, col] == aliveColor)//Checks cell below
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row + 1, col - 1))
                {
                    if (grid[row + 1, col - 1] == aliveColor)//Checks cell below and to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row, col - 1))
                {
                    if (grid[row, col - 1] == aliveColor)//Checks cell to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col - 1))
                {
                    if (grid[row - 1, col - 1] == aliveColor)//Checks cell above and to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col))
                {
                    if (grid[row - 1, col] == aliveColor)//Checks cell above 
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col + 1))
                {
                    if (grid[row - 1, col + 1] == aliveColor)//Checks cell above and to the right
                    {
                        neighbourCount++;
                    }
                }
                if (neighbourCount == 3)
                {
                    Baby = true;
                }
            }
            return Baby;
        }//Checks if cell has 3 neighbours

        bool CheckBarren(int row, int col)
        {
            bool Barren = false;
            int neighbourCount = 0;
            if (ValidCell(row, col + 1))
            {
                if (grid[row, col + 1] == aliveColor)//Checks cell to the right
                {
                    neighbourCount++;
                }
            }
            if (ValidCell(row + 1, col + 1))
            {
                if (grid[row + 1, col + 1] == aliveColor)//Checks cell below and to the right
                {
                    neighbourCount++;
                }
                if (ValidCell(row + 1, col))
                {
                    if (grid[row + 1, col] == aliveColor)//Checks cell below
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row + 1, col - 1))
                {
                    if (grid[row + 1, col - 1] == aliveColor)//Checks cell below and to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row, col - 1))
                {
                    if (grid[row, col - 1] == aliveColor)//Checks cell to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col - 1))
                {
                    if (grid[row - 1, col - 1] == aliveColor)//Checks cell above and to the left
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col))
                {
                    if (grid[row - 1, col] == aliveColor)//Checks cell above 
                    {
                        neighbourCount++;
                    }
                }
                if (ValidCell(row - 1, col + 1))
                {
                    if (grid[row - 1, col + 1] == aliveColor)//Checks cell above and to the right
                    {
                        neighbourCount++;
                    }
                }
                if (neighbourCount == 0)
                {
                    Barren = true;
                }
            }
            return Barren;
        }//checks if cell has no neighbours,I don't think this is needed

        bool ValidCell(int row, int col)
        {
            if (row >= 0 && row <= 19 && col >= 0 && col <= 19)
            {
                return true;
            }
            else
            {
                return false;
            }
        }//Makes sure all cells checked are within the bounds of the array

    }
}