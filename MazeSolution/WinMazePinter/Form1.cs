using MazeSolution.MazeStruct.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MazeSolution.MazeStruct.Core.MazeStructure.Directions;
using MazeSolution.MazeStruct.Core.GameObjects.LiveGameObjects;

namespace WinMazePinter
{
    public partial class Form1 : Form
    {
        TestMaze maze;
        private int size = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            int startLine = Convert.ToInt32(lineTextBox.Text);
            int startColumn = Convert.ToInt32(columnTextBox.Text);

            maze = new TestMaze(size, startLine, startColumn);
            maze.GenerateMaze();

            pictureBox1.Refresh();
        }
        

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (maze != null)
            {
                int lineSize = (pictureBox1.Height - 50) / (size + 1) + 5;
                int columnSize = (pictureBox1.Width - 50) / (size + 1) + 5;
                Pen pen = new Pen(Color.Black, 2);

                //e.Graphics.DrawLine(pen, 0 * lineSize, 0 * columnSize, 0 * lineSize + lineSize, 0 * columnSize);
                //e.Graphics.DrawLine(pen, 1 * lineSize, 0 * columnSize, 1 * lineSize + lineSize, 0 * columnSize);
                //e.Graphics.DrawLine(pen, 2 * lineSize, 0 * columnSize, 2 * lineSize + lineSize, 0 * columnSize);
                //e.Graphics.DrawLine(pen, 3 * lineSize, 0 * columnSize, 3 * lineSize + lineSize, 0 * columnSize);


                for(int line = 0; line < size; line ++)
                {
                    for(int column = 0; column < size; column++)
                    {
                        var cell = maze[line, column];

                        if (!cell[DirectionEnum.Up].CanMove) e.Graphics.DrawLine(pen, column * columnSize, line * lineSize, column * columnSize + columnSize, line * lineSize);

                        if (!cell[DirectionEnum.Down].CanMove) e.Graphics.DrawLine(pen, column * columnSize, line * lineSize + lineSize, column * columnSize + columnSize, line * lineSize + lineSize);

                        if (!cell[DirectionEnum.Left].CanMove) e.Graphics.DrawLine(pen, column * columnSize, line * lineSize, column * columnSize, line * lineSize + lineSize);

                        if (!cell[DirectionEnum.Right].CanMove) e.Graphics.DrawLine(pen, column * columnSize + columnSize, line * lineSize, column * columnSize + columnSize, line * lineSize + lineSize);

                        var types = cell.TypesInCell();

                        if (types.Contains(typeof(HumanGameObject))) e.Graphics.DrawEllipse(pen, column * columnSize + columnSize / 10, line * lineSize + lineSize / 10, Convert.ToSingle(columnSize * 0.8), Convert.ToSingle(lineSize * 0.8));
                    }
                }

            }
        }
    }
}
