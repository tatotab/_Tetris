using System;

namespace Tetris
{
    public class Boxes
    {
        public int x; //Row
        public int y; //Column
        public int[,] matrix; //My Boxes
        public int[,] next;
        public int sizing;
        public int sizingNext;

        public int[,] Box1 = new int[4, 4] //First Form
        {
            {0,1,0,0},
            {0,1,0,0},
            {0,1,0,0},
            {0,1,0,0},
        };

        public int[,] Box2 = new int[3, 3] //Second Form
        {
            {0,2,0},
            {0,2,2},
            {0,0,2},
        };

        public int[,] Box3 = new int[3, 3] //Third Form
        {
            {0,0,0},
            {3,3,3},
            {0,3,0},
        };

        public int[,] Box4 = new int[3, 3] //Forth Form
        {
            {4,0,0},
            {4,0,0},
            {4,4,0},
        };
        public int[,] Box5 = new int[2, 2] //Fifth Form
        {
            {5,5},
            {5,5},
        };
        public int[,] Box6 = new int[3, 3] //Second Form
        {
            {0,6,0},
            {6,6,0},
            {6,0,0},
        };
        public int[,] Box7 = new int[3, 3] //Forth Form
        {
            {0,0,7},
            {0,0,7},
            {0,7,7},
        };



        public Boxes(int _x,int _y) //Creating Forms
        {
            x = _x;
            y = _y;
            matrix = GenerateMatrix();
            sizing = (int)Math.Sqrt(matrix.Length);
            next = GenerateMatrix();
            sizingNext = (int)Math.Sqrt(next.Length);
        }

        public void ResetBoxes(int _x, int _y) //Reseting Box Forms
        {
            x = _x;
            y = _y;
            matrix = next;
            sizing = (int)Math.Sqrt(matrix.Length);
            next = GenerateMatrix();
            sizingNext = (int)Math.Sqrt(next.Length);
        }

        public int[,] GenerateMatrix()
        {
            int[,] _matrix = Box1;
            Random rand = new Random();
            switch (rand.Next(1, 100) % 7 + 1) //i think it is more random this way than just next(1, 8)
            {
                case 1:
                    _matrix = Box1;
                    break;
                case 2:
                    _matrix = Box2;
                    break;
                case 3:
                    _matrix = Box3;
                    break;
                case 4:
                    _matrix = Box4;
                    break;
                case 5:
                    _matrix = Box5;
                    break;
                case 6:
                    _matrix = Box6;
                    break;
                case 7:
                    _matrix = Box7;
                    break;
            }
            return _matrix;
        }

        public void Rotate() //Rotate Function
        {
            //Creating Temporary Matrix
            int[,] temporary = new int[sizing, sizing];
            for(int i = 0; i < sizing; i++)
            {
                for (int j = 0; j < sizing; j++)
                {
                    temporary[i, j] = matrix[j, (sizing - 1) - i];
                }
            }
            matrix = temporary;
            int firstOff = (10 - (x + sizing)); 

            if (firstOff < 0)
            {
                for (int i = 0; i < Math.Abs(firstOff); i++)
                    Left();
            }
            if (x < 0)
            {
                for (int i = 0; i < Math.Abs(x)+1; i++)
                    Right();
            }

        }

        public void Down() //Moving Down
        {
            y++;
        }
        public void Right() //Moving Right
        {
            x++;
        }
        public void Left() //Moving Left
        {
            x--;
        }
    }
}
