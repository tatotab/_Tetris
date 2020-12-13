using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris.Control
{ 
    public static class Drawings
    {
        public static Boxes shape; //Object for Class Boxes
        public static int sizeOfBox; //Size of a Single Box
        public static int[,] cell = new int[18, 10]; //Whole Playing Cell
        public static uint RemovedLine; 
        public static uint totScore;
        public static uint currScore;
        public static int interval;
        public static uint level;
        public static void Next(Graphics graph) //Shows Next Box
        {
            //Changing Colors and Shapes of Boxes
            for (int i = 0; i < shape.sizingNext; i++)
            {
                for (int j = 0; j < shape.sizingNext; j++)
                {
                    if (shape.next[i, j] == 1) 
                    {
                        graph.FillRectangle(Brushes.Indigo, new Rectangle(370 + j * (sizeOfBox) + 1, 
                            100 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (shape.next[i, j] == 2)
                    {
                        graph.FillRectangle(Brushes.ForestGreen, new Rectangle(370 + j * (sizeOfBox) + 1, 
                            100 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (shape.next[i, j] == 3)
                    {
                        graph.FillRectangle(Brushes.Maroon, new Rectangle(370 + j * (sizeOfBox) + 1, 
                            100 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (shape.next[i, j] == 4)
                    {
                        graph.FillRectangle(Brushes.DeepPink, new Rectangle(370 + j * (sizeOfBox) + 1, 
                            100 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (shape.next[i, j] == 5)
                    {
                        graph.FillRectangle(Brushes.Violet, new Rectangle(370 + j * (sizeOfBox) + 1, 
                            100 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (shape.next[i, j] == 6)
                    {
                        graph.FillRectangle(Brushes.Gold, new Rectangle(370 + j * (sizeOfBox) + 1,
                            100 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (shape.next[i, j] == 7)
                    {
                        graph.FillRectangle(Brushes.DarkTurquoise, new Rectangle(370 + j * (sizeOfBox) + 1,
                            100 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                }
            }
        }

        public static void Clear() //Clear Whole Playing Cell
        {
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    cell[i, j] = 0;
                }
            }
        }

        public static void Map(Graphics graph) //Drawing Map for Playing Cell
        {
            //Changing Colors and Shapes of Boxes
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (cell[i, j] == 1)
                    {
                        graph.FillRectangle(Brushes.Indigo, new Rectangle(50 + j * (sizeOfBox) + 1, 
                            50 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (cell[i, j] == 2)
                    {
                        graph.FillRectangle(Brushes.ForestGreen, new Rectangle(50 + j * (sizeOfBox) + 1, 
                            50 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (cell[i, j] == 3)
                    {
                        graph.FillRectangle(Brushes.Maroon, new Rectangle(50 + j * (sizeOfBox) + 1, 
                            50 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (cell[i, j] == 4)
                    {
                        graph.FillRectangle(Brushes.DeepPink, new Rectangle(50 + j * (sizeOfBox) + 1, 
                            50 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (cell[i, j] == 5)
                    {
                        graph.FillRectangle(Brushes.Violet, new Rectangle(50 + j * (sizeOfBox) + 1, 
                            50 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (cell[i, j] == 6)
                    {
                        graph.FillRectangle(Brushes.Gold, new Rectangle(50 + j * (sizeOfBox) + 1,
                            50 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                    if (cell[i, j] == 7)
                    {
                        graph.FillRectangle(Brushes.DarkTurquoise, new Rectangle(50 + j * (sizeOfBox) + 1,
                            50 + i * (sizeOfBox) + 1, sizeOfBox - 1, sizeOfBox - 1));
                    }
                }
            }
        }

        public static void Grid(Graphics graph) //Making Grids for Playing Cell
        { 
            //Columns
            for (int i = 0; i <= 18; i++)
            {
                graph.DrawLine(Pens.White, new Point(50, 50 + i * sizeOfBox), 
                    new Point(50 + 10 * sizeOfBox, 50 + i * sizeOfBox));
            }
            //Rows
            for (int j = 0; j <= 10; j++)
            {
                graph.DrawLine(Pens.White, new Point(50 + j * sizeOfBox, 50), 
                    new Point(50 + j * sizeOfBox, 50 + 18 * sizeOfBox));
            }
        }

        public static void Cut(Label first,Label second) //Slicing Cell
        {
            int count;
            uint cutLines = 0;
            for (int i = 0; i < 18; i++)
            {
                count = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (cell[i, j] != 0)
                        count++;
                }
                if (count == 10)
                {
                    cutLines++;
                    for (int k = i; k >= 1; k--)
                    {
                        for (int o = 0; o < 10; o++)
                        {
                            cell[k, o] = cell[k - 1, o];
                        }
                    }
                }
            }
            if (RemovedLine % 10 == 0 && RemovedLine != 0) //level passed checker
            {
                level++; // new level on every 10 cleared lines and speed 25% faster
                interval = interval * 5 / 4; // 25% fster
            }
            for (uint i = 0; i < cutLines; i++)
            {
                currScore += 100 * (i + 1);
                

            }
            if (cutLines > 1) //if more than one row complete at the same time 
            {
                currScore += 50 * (cutLines - 1); //a bonus of 50 points for each additional row 
            }
            totScore  = (level > 0) ? (currScore * level) : currScore; //The points earned for completing 
                                                                       //rows grow proportional to the level number

            RemovedLine += cutLines;
            first.Text = "Score: " + totScore; 
            second.Text = "Lines: " + RemovedLine;
        }

        public static bool Intersection() //If intersects --- Checks If Shapes Intersect
        {
            for (int i = shape.y; i < shape.y + shape.sizing; i++)
            {
                for (int j = shape.x; j < shape.x + shape.sizing; j++)
                {
                    if (j >= 0 && j <= 9)
                    {
                        if (cell[i, j] != 0 && shape.matrix[i - shape.y, j - shape.x] == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void Share() //Merges Boxes and Cell
        {
            for (int i = shape.y; i < shape.y + shape.sizing; i++)
            {
                for (int j = shape.x; j < shape.x + shape.sizing; j++)
                {
                    if (shape.matrix[i - shape.y, j - shape.x] != 0)
                    {
                        cell[i, j] = shape.matrix[i - shape.y, j - shape.x];
                    }
                }
            }
        }

        public static bool Collide1() //Checks if Boxes Collide with Each Other
        {
            for (int i = shape.y + shape.sizing - 1; i >= shape.y; i--)
            {
                for (int j = shape.x; j < shape.x + shape.sizing; j++)
                {
                    if (shape.matrix[i - shape.y, j - shape.x] != 0)
                    {
                        if (i + 1 == 18)
                        {
                            return true;
                        }   
                        if (cell[i + 1, j] != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool Collide2(int direction) //Checks if Boxes Collide with Cell Borders
        {
            for (int i = shape.y; i < shape.y + shape.sizing; i++)
            {
                for (int j = shape.x; j < shape.x + shape.sizing; j++)
                {
                    if (shape.matrix[i - shape.y, j - shape.x] != 0)
                    {
                        if (j + 1 * direction > 9 || j + 1 * direction < 0)
                        {
                            return true;
                        }

                        if (cell[i, j + 1 * direction] != 0)
                        {
                            if (j - shape.x + 1 * direction >= shape.sizing || j - shape.x + 1 * direction < 0)
                            {
                                return true;
                            }
                            if (shape.matrix[i - shape.y, j - shape.x + 1 * direction] == 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static void Reset() //Resets Whole Playing Cell
        {
            for (int i = shape.y; i < shape.y + shape.sizing; i++)
            {
                for (int j = shape.x; j < shape.x + shape.sizing; j++)
                {
                    if (i >= 0 && j >= 0 && i < 18 && j < 10)
                    {
                        if (shape.matrix[i - shape.y, j - shape.x] != 0)
                        {
                            cell[i, j] = 0;
                        }
                    }
                }
            }
        }

    }
}
