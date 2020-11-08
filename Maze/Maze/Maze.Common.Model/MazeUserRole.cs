namespace Maze.Common.Model
{
    public class MazeUserRole : BaseMazeObject
    {
        public string RoleName { get; }

        public  MazeUserRole()
        {

        }

        public MazeUserRole(string roleName)
        {
            RoleName = roleName;
        }
    }
}
