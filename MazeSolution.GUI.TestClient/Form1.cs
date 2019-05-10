using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MazeSolution.Core;
using MazeSolution.Core.MazeStructrure;
using MazeSolution.Core.Generators;
using MazeSolution.Core.MazeStructrure.Cells;
using MazeSolution.Core.GameObjects.LiveGameObject;

namespace MazeSolution.GUI.TestClient
{
    public partial class Form1 : Form
    { 
        private IMazeGenerator _generator = new RecursiveSquareMazeGenerator();
        private Maze<SquareCell> _maze;

        private Human human;

        private int size = 10;

        private bool _visible = false;

        private DefaultActionStorage _formActionStorage;

        public Form1()
        {
            InitializeComponent();
            _formActionStorage = new DefaultActionStorage()
            {
                EndGameAction = (gameObj) =>
                {
                    this._maze.SetCellsVisible(true, null, 0);
                    this.pictureBox1.Refresh();
                    MessageBox.Show($"Победил {gameObj.ObjectID}");
                    GenerateMaze();
                },
                LobbyFormed = () =>
                {
                    throw new NotImplementedException();
                }
            };
        }

        private void GenerateMaze()
        {
            this._visible = false;
            size = Convert.ToInt32(this.textBox1.Text);
            _maze = new Maze<SquareCell>(_generator.GenerateMaze(size, size), _formActionStorage);
            human = new Human();
            _maze.AddLiveGameObject(human, 5, 5);
            this.pictureBox1.Refresh();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GenerateMaze();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (_maze != null)
            {

                int lineSize = (pictureBox1.Height - 50) / (size + 1) + 5;
                int columnSize = (pictureBox1.Width - 50) / (size + 1) + 5;
                Pen pen = new Pen(Color.Black, 2);

                //e.Graphics.DrawLine(pen, 0 * lineSize, 0 * columnSize, 0 * lineSize + lineSize, 0 * columnSize);
                //e.Graphics.DrawLine(pen, 1 * lineSize, 0 * columnSize, 1 * lineSize + lineSize, 0 * columnSize);
                //e.Graphics.DrawLine(pen, 2 * lineSize, 0 * columnSize, 2 * lineSize + lineSize, 0 * columnSize);
                //e.Graphics.DrawLine(pen, 3 * lineSize, 0 * columnSize, 3 * lineSize + lineSize, 0 * columnSize);


                for (int line = 0; line < size; line++)
                {
                    for (int column = 0; column < size; column++)
                    {
                        var cell = _maze[line, column];

                        if (!cell[Direction.Up].CanMove && cell[Direction.Up].Visible) e.Graphics.DrawLine(pen, column * columnSize, line * lineSize, column * columnSize + columnSize, line * lineSize);

                        if (!cell[Direction.Down].CanMove && cell[Direction.Down].Visible) e.Graphics.DrawLine(pen, column * columnSize, line * lineSize + lineSize, column * columnSize + columnSize, line * lineSize + lineSize);

                        if (!cell[Direction.Left].CanMove && cell[Direction.Left].Visible) e.Graphics.DrawLine(pen, column * columnSize, line * lineSize, column * columnSize, line * lineSize + lineSize);

                        if (!cell[Direction.Right].CanMove && cell[Direction.Right].Visible) e.Graphics.DrawLine(pen, column * columnSize + columnSize, line * lineSize, column * columnSize + columnSize, line * lineSize + lineSize);

                        var _human = cell.GetGameObject<Human>() as Human;
                        if (_human != null) e.Graphics.DrawEllipse(pen, column * columnSize + columnSize / 10, line * lineSize + lineSize / 10, Convert.ToSingle(columnSize * 0.8), Convert.ToSingle(lineSize * 0.8));
                        //var types = cell.TypesInCell();
                        //if (types.Contains(typeof(HumanGameObject))) 
                    }
                }

            }
        }



        private void Button2_Click(object sender, EventArgs e)
        {
            _maze.MoveLiveGameObject(human.ObjectID, Direction.Up);
            this.pictureBox1.Refresh();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            _maze.MoveLiveGameObject(human.ObjectID, Direction.Right);
            this.pictureBox1.Refresh();
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            _maze.MoveLiveGameObject(human.ObjectID, Direction.Down);
            this.pictureBox1.Refresh();
        }


        private void Button5_Click(object sender, EventArgs e)
        {
            _maze.MoveLiveGameObject(human.ObjectID, Direction.Left);
            this.pictureBox1.Refresh();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    {
                        _maze.MoveLiveGameObject(human.ObjectID, Direction.Up);
                        this.pictureBox1.Refresh();
                        break;
                    }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.pictureBox1.Refresh();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (_maze != null)
            {
                this._visible = !this._visible;
                this._maze.SetCellsVisible(this._visible, human, 2);
                this.pictureBox1.Refresh();
            }
        }
    }
}
