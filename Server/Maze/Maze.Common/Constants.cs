using Maze.Common.Types;
using System.Text.Json;

namespace Maze.Common
{
    public static class Constants
    {
        public static class ConnectionNames
        {
            public const string Wall = "Wall";
            public const string Exit = "Exit";
            public const string Boundary = "Boundary";
            public const string DestroyedWall = "DestroyedWall";
        }

        public static class HubMethods
        {
            public const string CreateGame = "CreateGame";
            public const string SetUser = "SetUser";
            public const string Move = "Move";
            public const string DestroyWall = "DestroyWall";
        }

        public readonly static JsonSerializerOptions JsonSerializerOptions = CreateDefault();

        private static JsonSerializerOptions CreateDefault()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                IncludeFields = true,
            };
            options.Converters.Add(new EntityIdJsonConverter());

            return options;
        }
    }
}
