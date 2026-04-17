namespace Maze.GameWorld
{
    public class Blocker
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ActionResult
    {
        public int RoomId { get; set; }
        public bool Win { get; set; }
        public bool SuccessMove { get; set; }
        public Blocker? BlockedBy { get; set; }
    }
}
