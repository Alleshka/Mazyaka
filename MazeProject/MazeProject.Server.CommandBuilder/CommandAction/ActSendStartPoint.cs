﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazeProject.MazeGeneral.Maze;
using MazeProject.MazeGeneral.Command;
using MazeProject.Server.GameService;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public class ActSendStartPoint : ActGameAbstract
    {
        public ActSendStartPoint(SendStartPositionRequest gameRequest, IGameService game) : base(gameRequest, game)
        {

        }

        public override void Execute()
        {
            Guid userID = (request as SendStartPositionRequest).UserID;
            Guid gameID = (request as SendStartPositionRequest).GameID;
            var point = (request as SendStartPositionRequest).Point;

            gameService.AddLive(gameID, userID, point);

            response = new SendStartPositionResponce();

            var sender = MessageSender.Sender.GetInstanse();
            sender.SendMessage(userID, response); // Говорим, что приняли

            GameRoom room = gameService.FindGameByID(gameID);
            if(room.Status==StatusGame.STARTED)
            {
                System.Threading.Thread.Sleep(1000);
                AbstractMessage response = new YourStep();
                sender.SendMessage(room.GetPlayerByIndex(room.StepByUser).PlayerID, response); // Сообщаем человеку про его ход
            }
        }
    }
}