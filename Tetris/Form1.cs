using System;
using System.Windows.Forms;
using Tetris.Control;

namespace Tetris
{
    public partial class Form1 : Form
    {
        
        string name;
        public Form1()
        {
            InitComponent();
            //Initializing the Player's Information Box
            name = Microsoft.VisualBasic.Interaction.InputBox("Enter the Name of a Player: ",
                "Player's Information", "New Player");
            if(name == "")
            {
                name = "New Player";
            }
            this.KeyUp += new KeyEventHandler(KeyboardKeys);
            Initialize();
        }

        public void Initialize()
        {   
            this.Text = "Tetris Game :: Player - " + name;
            
            Drawings.totScore = 0;
            Drawings.shape = new Boxes(3, 0);
            Drawings.RemovedLine = 0;
            Drawings.interval = 500;
            Drawings.sizeOfBox = 25;

            label1.Text = "Score \n " + Drawings.totScore;
            label2.Text = "Lines \n" + Drawings.RemovedLine;
            label3.Text = "Level \n" + Drawings.level;
            timer1.Interval = Drawings.interval;
            timer1.Tick += new EventHandler(Update);
            timer1.Start();
            
            Invalidate();
        }
        public void InitializeLoaded()
        {
            this.Text = "Tetris Game :: Player - " + name;

            Drawings.sizeOfBox = 25;
      
            Drawings.shape = new Boxes(3, 0);
           
            
            label1.Text = "Score \n " + Drawings.totScore;
            label2.Text = "Lines \n" + Drawings.RemovedLine;
            label3.Text = "Level \n" + Drawings.level;
            timer1.Interval = Drawings.interval;
            timer1.Tick += new EventHandler(Update);
            MessageBox.Show($"User Name: {name}\n" +
                            $"Score: {Drawings.totScore} \n" +
                            $"Level: {Drawings.level}\n" +
                            $"Removed Lines {Drawings.RemovedLine}\n" +
                            $"Moving speed in miliseconds: {timer1.Interval}");
            
            timer1.Start();

            Invalidate();
        }

        private void KeyboardKeys(object sender, KeyEventArgs graph)
        {
            switch (graph.KeyCode)
            {
                case Keys.Up:

                    if (!Drawings.Intersection())
                    {
                        Drawings.Reset();
                        Drawings.shape.Rotate();
                        Drawings.Share();
                        Invalidate();
                    }
                    break;
                case Keys.Space:
                    timer1.Interval = 10;
                    break;
                case Keys.Right:
                    if (!Drawings.Collide2(1))
                    {
                        Drawings.Reset();
                        Drawings.shape.Right();
                        Drawings.Share();
                        Invalidate();
                    }
                    break;
                case Keys.Left:
                    if (!Drawings.Collide2(-1))
                    {
                        Drawings.Reset();
                        Drawings.shape.Left();
                        Drawings.Share();
                        Invalidate();
                    }
                    break;
            }
        }

        
        private void Update(object sender, EventArgs graph)
        {
            Drawings.Reset();
            if (!Drawings.Collide1())
            {
                Drawings.shape.Down();
            }
            else
            {
                Drawings.Share();
                Drawings.Cut(label1,label2);
                timer1.Interval = Drawings.interval;
                Drawings.shape.ResetBoxes(3,0);
                if (Drawings.Collide1())
                {
                    Drawings.Clear();
                    timer1.Tick -= new EventHandler(Update);
                    timer1.Stop();
                    DialogResult result = MessageBox.Show($"Game Over \n Score: {Drawings.totScore} \n Do you want to play Again?"
                                                            ,"Game Over", MessageBoxButtons.YesNo);
                    if(result == DialogResult.No) { Application.Exit(); }
                    Records.SaveForHighScores(name);
                    
                    Initialize();
                }
            }
            Drawings.Share();
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs graph)
        {
            Drawings.Grid(graph.Graphics);
            Drawings.Map(graph.Graphics);
            Drawings.Next(graph.Graphics);
        }

        private void OnPauseButtonClick(object sender, EventArgs graph)
        {
            var pressedButton = sender as ToolStripMenuItem;
            if (timer1.Enabled)
            {
                pressedButton.Text = "Resume";
                timer1.Stop();
                saveGameToolStripMenuItem.Enabled = true;
                loadGameToolStripMenuItem.Enabled = true;
            }
            else
            {
                pressedButton.Text = "Pause";
                timer1.Start();
                saveGameToolStripMenuItem.Enabled = false;
                loadGameToolStripMenuItem.Enabled = false;

            }
        }

        private void OnAgainButtonClick(object sender, EventArgs graph)
        {
            timer1.Tick -= new EventHandler(Update);
            timer1.Stop();
            Drawings.Clear();
            Initialize();
        }


    

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string highScores = "";
            highScores = Records.Show();
            MessageBox.Show(highScores, "High Scores");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Records.SaveForUser(name); //call for function which will save progress in txt
            MessageBox.Show("Progress Saved!");
        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            name = Records.LoadForUser();
            timer1.Tick -= new EventHandler(Update);
            Drawings.Clear();
            InitializeLoaded();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }


        private void authorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string infoString = "";
            infoString = "Mariam Gugushvili\n";
            infoString += "Dimitri Tabagari\n";
            infoString += "Tetris for Comp E361 Final Project\n";
            infoString += "December 2020\n";
            MessageBox.Show(infoString, "Credits");
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            string infoString = "";
            infoString = "Figures are Moved by Left/Right Arrows\n";
            infoString += "To Fasten the Game use Space Bar\n";
            infoString += "To Rotate the Box Use 'A'\n";
            infoString += "Pause - Ctrl+P\n";
            infoString += "Resume - Ctrl+G\n";
            infoString += "New Game - Ctrl+N\n";
            MessageBox.Show(infoString, "Rules");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            timer1.Stop();
            DialogResult dialogResult = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo);
            PauseTools.Text = "Resume";
           
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                timer1.Start();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                PauseTools.Text = "Pause";
                timer1.Start();
                saveGameToolStripMenuItem.Enabled = false;
                loadGameToolStripMenuItem.Enabled = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Font = new System.Drawing.Font("Poor Richard", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
