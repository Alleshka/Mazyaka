using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MazeProject.MazeGeneral;
using MazeProject.MazeGeneral.Maze;

namespace MazeProject.Client.WPF
{
    /// <summary>
    /// Логика взаимодействия для MazeArea.xaml
    /// </summary>
    public partial class MazeArea : UserControl
    {
        private Random T = new Random();

        private TestServiceClient client = new TestServiceClient();

        private int CELLWIDTH = 50;
        private int CELLHEIGHT = 50;

        private MoveDirection direction;
        private MazePoint curPoint;

        public MazeArea()
        {
            InitializeComponent();

            client.CreateGameEvent += Client_CreateGameEvent;
            client.GetGameListEvent += Client_GetGameListEvent;
            client.GetLoginEvent += Client_GetLoginEvent;
            client.GiveMazeEvent += Client_GiveMazeEvent;
            client.GivePointEvent += Client_GivePointEvent;
            client.JoinGameEvent += Client_JoinGameEvent;
            client.MoveObjectEvent += Client_MoveObjectEvent;
            client.SendMazeEvent += Client_SendMazeEvent;
            client.SendPointEvent += Client_SendPointEvent;
            client.YourStepEvent += Client_YourStepEvent;
            client.GameIsFinishedEvent += Client_GameIsFinishedEvent;

            client.Login();
            StatusInterface(false);

            maze.Children.Add(new Line());
            ReplaseHuman(1, 1);
        }

        private void Client_GameIsFinishedEvent(string message)
        {
            MessageBox.Show(message);
        }

        private void Client_GetLoginEvent(Guid userID)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate { myID.Content = "ID = " + userID.ToString(); }));
        }

        private void Client_CreateGameEvent(Guid gameID)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate { myID.Content += Environment.NewLine + "GameID = " + gameID.ToString(); }));
        }

        private void Client_GivePointEvent()
        {
            curPoint = new MazePoint(5, 5);
            ReplaseHuman(curPoint.Line, curPoint.Column);
            client.SendPoint(curPoint);
        }

      

        private void Client_GiveMazeEvent()
        {
            client.SendMaze(null);
        }

        private void Client_YourStepEvent()
        {
            StatusInterface(true); // Разблокируем интерфейс
        }

        private void Client_SendPointEvent()
        {
            // Когда прислали ответ точку, ничо не делаем
        }

        private void Client_SendMazeEvent()
        {
            // Ответ на лабиринт
        }

        private void Client_MoveObjectEvent(bool status)
        {
            StatusInterface(false);
            
            if(status) // Если удалось перейти
            {
                // Двигаем объект
                GetNewPoint(direction);
                ReplaseHuman(curPoint.Line, curPoint.Column);
            }
            else
            {
                // Иначе ставим стенку
                AddNewWall(direction);
            }
        }

        private void Client_JoinGameEvent(bool status)
        {
            String message = null;

            if (status)
            {
                message = "Успешно";
            }
            else
            {
                message = "Не удалось";
            }

            Dispatcher.BeginInvoke(new ThreadStart(delegate { myID.Content += Environment.NewLine + message; }));
        }

        private void Client_GetGameListEvent(List<Guid> gameList)
        {
            foreach (var game in gameList)
            {
                Dispatcher.BeginInvoke(new ThreadStart(delegate { gamesID.Items.Add(game); }));
            }
        }

        private void GetNewPoint(MoveDirection direction)
        {
            switch(direction)
            {
                case MoveDirection.UP:
                    {
                        curPoint.Line -= 1;
                        break;
                    }
                case MoveDirection.RIGHT:
                    {
                        curPoint.Column += 1;
                        break;
                    }
                case MoveDirection.LEFT:
                    {
                        curPoint.Column -= 1;
                        break;
                    }
                case MoveDirection.DOWN:
                    {
                        curPoint.Line += 1;
                        break;
                    }
            }
        }
        private void AddNewWall(MoveDirection direction)
        {
            switch(direction)
            {
                case MoveDirection.DOWN:
                    {
                        AddDownWall(curPoint.Line, curPoint.Column);
                        break;
                    }
                case MoveDirection.LEFT:
                    {
                        AddLeftWall(curPoint.Line, curPoint.Column);
                        break;
                    }
                case MoveDirection.RIGHT:
                    {
                        AddRightWall(curPoint.Line, curPoint.Column);
                        break;
                    }
                case MoveDirection.UP:
                    {
                        AddUpWall(curPoint.Line, curPoint.Column);
                        break;
                    }
            }
        }


        private void ReplaseHuman(int line, int column)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate {

                var figure = maze.Children[0] as Line;

                figure.X1 = column * CELLWIDTH + CELLWIDTH/3;
                figure.X2 = (column + 1) * CELLWIDTH - CELLWIDTH/3;

                figure.Y1 = (line) * CELLHEIGHT + CELLHEIGHT / 2;
                figure.Y2 = (line) * CELLHEIGHT + CELLHEIGHT / 2;

                figure.Stroke = Brushes.Green;

            }));

        }

        /// <summary>
        /// Добавить левую стенку для указанной ячейки
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void AddLeftWall(int line, int column)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate {
                Line temp = new Line
                {
                    X1 = column * CELLWIDTH,
                    X2 = column * CELLWIDTH,
                    Y1 = line * CELLHEIGHT,
                    Y2 = (line + 1) * CELLHEIGHT,
                    Stroke = Brushes.Red,
                };

                maze.Children.Add(temp);
            }));
        }
        
        public void AddRightWall(int line, int column)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate {
                maze.Children.Add(new Line()
                {
                    X1 = CELLWIDTH * (column + 1),
                    X2 = CELLWIDTH * (column + 1),
                    Y1 = CELLHEIGHT * line,
                    Y2 = CELLHEIGHT * (line + 1),
                    Stroke = Brushes.Red
                });
            }));
        }

        public void AddUpWall(int line, int column)
        {

            Dispatcher.BeginInvoke(new ThreadStart(delegate {
                maze.Children.Add(new Line()
                {
                    X1 = CELLWIDTH * column,
                    X2 = CELLWIDTH * (column + 1),
                    Y1 = CELLHEIGHT * line,
                    Y2 = CELLHEIGHT * line,
                    Stroke = Brushes.Red
                });
            }));
        }

        public void AddDownWall(int line, int column)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate {
                maze.Children.Add(new Line()
                {
                    X1 = CELLWIDTH * column,
                    X2 = CELLWIDTH * (column + 1),
                    Y1 = CELLHEIGHT * (line + 1),
                    Y2 = CELLHEIGHT * (line + 1),
                    Stroke = Brushes.Red
                });
            }));
        }

        private void CreateGame_Click(object sender, RoutedEventArgs e)
        {
            client.CreateGame();
        }

        private void GetGameList_Click(object sender, RoutedEventArgs e)
        {
            client.GetGameList();
        }

        private void JoinGame_Click(object sender, RoutedEventArgs e)
        {
            Guid idGame = (Guid)gamesID.SelectedItem;
            Dispatcher.BeginInvoke(new ThreadStart(delegate { myID.Content += Environment.NewLine + "Попытка подключения к игре " + idGame.ToString(); }));
            client.JoinGame(idGame);
        }

        private void StatusInterface(bool status)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate {
                BtnUP.IsEnabled = status;
                BtnR.IsEnabled = status;
                BtnL.IsEnabled = status;
                BtnD.IsEnabled = status;
            }));

        }

        private void BtnUP_Click(object sender, RoutedEventArgs e)
        {
            Move(MoveDirection.UP);
        }

        private void BtnL_Click(object sender, RoutedEventArgs e)
        {
            Move(MoveDirection.LEFT);
        }

        private void BtnR_Click(object sender, RoutedEventArgs e)
        {
            Move(MoveDirection.RIGHT);
        }

        private void BtnD_Click(object sender, RoutedEventArgs e)
        {
            Move(MoveDirection.DOWN);
        }

        private void Move(MoveDirection dir)
        {
            direction = dir;
            StatusInterface(false);
            client.MoveObject(dir);
        }
    }
}
