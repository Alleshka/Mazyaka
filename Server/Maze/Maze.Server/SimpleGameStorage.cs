using Maze.MazeStructure;

namespace Maze.Server
{
    public class SimpleGameStorage : IGameStorage
    {
        private Dictionary<Guid, IMazeGame> _games;

        public SimpleGameStorage()
        {
            _games = new Dictionary<Guid, IMazeGame>();
        }

        public Guid AddGame(IMazeGame game)
        {
            var id = Guid.NewGuid();
            _games.Add(id, game);
            return id;
        }

        public IMazeGame GetGame(Guid id)
        {
            return _games[id];
        }
    }
}
