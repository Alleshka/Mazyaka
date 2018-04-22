using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MazeProject.MazeGeneral.Command
{
    /// <summary>
    /// От данного класса наследуются все передаваемые сообщения
    /// </summary>
    [DataContract]
    public abstract class AbstractMessage
    {
        protected static Type[] types = new Type[]
        {
            typeof(AbstractMessage),
            typeof(AbstractRequest),
            typeof(AbstractResponse),
            typeof(CreateGameRequest),
            typeof(CreateGameResponse),
            typeof(GameListRequest),
            typeof(GameListResponse),
            typeof(JoinGameRequest),
            //typeof(JoinGameResponse),
            typeof(LoginRequest),
            typeof(LoginResponse),
            typeof(MoveObjectRequest),
            typeof(MoveObjectResponse),
            typeof(SendStartPositionRequest),
            typeof(SendStartPositionResponce),
            typeof(SendMazeRequest),
            typeof(SendMazeResponse),
            typeof(UserCountRequest),
            typeof(UserCountResponse),
            typeof(GiveMaze),
            typeof(GivePoint),
            typeof(YourStep),
            typeof(CumulativeResponse),
            typeof(GameFinished),
        };

        public static byte[] ToBytes(AbstractMessage obj) => Serializer<AbstractMessage>.ToBytes(obj, types);
        public static String ToXml(AbstractMessage obj) => Serializer<AbstractMessage>.ToXml(obj, types);
        public static AbstractMessage ToObject(byte[] bytes) => Serializer<AbstractMessage>.ToObject(bytes, types);
        public static AbstractMessage ToObject(String xml) => Serializer<AbstractMessage>.ToObject(xml, types);
    }

    [DataContract]
    public abstract class AbstractRequest : AbstractMessage
    {
        public AbstractRequest() : base ()
        {

        }
    }

    [DataContract]
    public abstract class AbstractResponse : AbstractMessage
    {
        public AbstractResponse() : base ()
        {

        }
    }
}
