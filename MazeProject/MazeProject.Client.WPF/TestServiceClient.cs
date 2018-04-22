using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeProject.MazeGeneral;
using MazeProject.MazeGeneral.Command;
using System.Threading;

namespace MazeProject.Client.WPF
{
    public class TestServiceClient
    {
        Socket client = new Socket(SocketType.Stream, ProtocolType.Tcp); // Создаём сокет
        Thread listenThread;
        bool IsWork = true;

        private Guid curUser;
        private Guid curGame;

        public delegate void LoginDelegate(Guid userID);
        public event LoginDelegate GetLoginEvent; // Событие при получении логина

        public delegate void CreateGameDelegate(Guid gameID);
        public event CreateGameDelegate CreateGameEvent; // Событие при создании игры

        public delegate void GetGameListDelegate(List<Guid> gameList);
        public event GetGameListDelegate GetGameListEvent; // Событие при получении игры

        public delegate void JoinGameDelegate(bool status);
        public event JoinGameDelegate JoinGameEvent; // Событие при получении сообщения о подключении к игре

        public delegate void SendMazeDelegate();
        public event SendMazeDelegate SendMazeEvent; // Событие о принятии лабиринта

        public delegate void SendPointDelegate();
        public event SendPointDelegate SendPointEvent; // Событие о принятии позиции

        public delegate void MoveObjectDelegate(bool status);
        public event MoveObjectDelegate MoveObjectEvent; // Событие о принятии позиции

        public delegate void YourStepDelegate();
        public event YourStepDelegate YourStepEvent; // Событие, сообщающее о ходе

        public delegate void GiveMazeDelegate();
        public event GiveMazeDelegate GiveMazeEvent; // Готовы принять лабиринт

        public delegate void GivePointDelegate();
        public event GivePointDelegate GivePointEvent; // Готовы принять начальную позицию

        public delegate void GameIsFinished(String message);
        public event GameIsFinished GameIsFinishedEvent; // Сообщение о конце игры

        public TestServiceClient()
        {
            client.Connect("25.67.51.48", 1337); // Подключаемся к серверу

            listenThread = new Thread(new ParameterizedThreadStart(HandleClient)); // Выделяем игроку свой поток
            listenThread.Start(client); // Запускаем прослушивание
        }

        public void Login(String login = "test123", String password = "123")
        {
            LoginRequest loginRequest = new LoginRequest(login, password); // Создаём пакет
            client.Send(LoginRequest.ToBytes(loginRequest)); // Посылаем заявку
        }
        public void CreateGame()
        {
            CreateGameRequest createGameRequest = new CreateGameRequest(curUser);
            client.Send(CreateGameRequest.ToBytes(createGameRequest));
        }
        public void GetGameList()
        {
            GameListRequest gameListRequest = new GameListRequest(curUser);
            client.Send(AbstractMessage.ToBytes(gameListRequest));
        }
        public void JoinGame(Guid id)
        {
            var request = new JoinGameRequest(curUser, id);
            curGame = id; // TODO: Если добавим в пакет ответа, то это убрать

            client.Send(AbstractRequest.ToBytes(request));
        }

        // Отправляем лабиринт
        public void SendMaze(MazeProject.MazeGeneral.Maze.Maze maze = null)
        {
            SendMazeRequest request = new SendMazeRequest(curUser, curGame, maze);
            client.Send(AbstractMessage.ToBytes(request));
        }

        //Отправляем начальную позицию
        public void SendPoint(MazeGeneral.Maze.MazePoint point)
        {
            SendStartPositionRequest request = new SendStartPositionRequest(curUser, curGame, point);
            client.Send(AbstractMessage.ToBytes(request));
        }

        // Передвинуть объект
        public void MoveObject(MoveDirection direction)
        {
            MoveObjectRequest moveObject = new MoveObjectRequest(curUser, curGame, direction);
            client.Send(AbstractMessage.ToBytes(moveObject));
        }

        /// <summary>
        /// Слушаем команды
        /// </summary>
        /// <param name="obj"></param>
        private void HandleClient(object obj)
        {
            Socket socket = obj as Socket;

            while (IsWork)
            {
                byte[] bytes = new byte[1024];
                socket.Receive(bytes);
                var response = AbstractMessage.ToObject(bytes); // Парсим ответ
                ResponseParse(response);
            }
        }

        private void ResponseParse(AbstractMessage response)
        {
            if (response is LoginResponse)
            {
                Guid userID = (response as LoginResponse).UserID;
                curUser = userID;

                GetLoginEvent.Invoke(userID);
            }
            else if (response is CreateGameResponse)
            {
                Guid gameID = (response as CreateGameResponse).GameID;
                curGame = gameID;

                CreateGameEvent?.Invoke(gameID);
            }
            else if (response is GameListResponse)
            {
                GetGameListEvent?.Invoke((response as GameListResponse).GameList);
            }
            else if (response is SendMazeResponse)
            {
                SendMazeEvent?.Invoke();
            }
            else if (response is YourStep)
            {
                YourStepEvent?.Invoke();
            }
            else if (response is SendStartPositionResponce)
            {
                SendPointEvent?.Invoke();
            }
            else if (response is MoveObjectResponse)
            {
                MoveObjectEvent?.Invoke((bool)(response as MoveObjectResponse).IsMoved);
            }
            else if (response is GiveMaze)
            {
                GiveMazeEvent?.Invoke();
            }
            else if (response is GivePoint)
            {
                GivePointEvent?.Invoke();
            }
            else if (response is CumulativeResponse)
            {
                foreach (var resp in (response as CumulativeResponse).ResponseList)
                {
                    ResponseParse(resp);
                }
            }
            else if (response is GameFinished)
            {
                if ((response as GameFinished).Winner.CompareTo(curUser)==0)
                {
                    GameIsFinishedEvent?.Invoke("Вы победили");
                }
                else
                {
                    GameIsFinishedEvent?.Invoke("Вы проиграли");
                }
            }
            else throw new Exception("Неизвестный пакет: " + response.GetType());
        }
    }
}
