using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MazeProject.MazeGeneral.Command;
using MazeProject.Server.GameService;
using MazeProject.Server.MessageSender;

namespace MazeProject.Server.CommandBuilder.CommandAction
{
    public interface IActCommand
    {
        void Execute();
    }

    public abstract class ActAbstract : IActCommand
    {
        protected AbstractRequest request;
        protected AbstractResponse response;
        protected Sender sender;

        public ActAbstract(AbstractRequest req, Sender send)
        {
            request = req;
            sender = send;
        }

        public abstract void Execute();

        public virtual void SendMessage(Guid userID, AbstractResponse resp = null)
        {
            if (resp == null) resp = this.response;
            sender.SendMessage(userID, resp);
        }
        public virtual void SendMessage(List<Guid> userList, AbstractMessage resp = null)
        {
            if (resp == null) resp = this.response;
            sender.SendMessage(userList, resp);
        }
    }

    public abstract class ActGameAbstract : ActAbstract
    {
        protected IGameService gameService;

        public ActGameAbstract(AbstractRequest message, IGameService game, Sender sender) : base(message, sender)
        {
            gameService = game;
        }
    }
}
