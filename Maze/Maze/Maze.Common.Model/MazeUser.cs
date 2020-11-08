namespace Maze.Common.Model
{
    public class MazeUser : BaseMazeObject
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public MazeUserRole Role { get; set; }
    }
}
