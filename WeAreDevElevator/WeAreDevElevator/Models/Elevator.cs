namespace WeAreDevElevator.Models
{
    public class Elevator
    {
        public int CurrentFloor { get; set; } = 0;
        public List<int> Requests { get; set; } = new List<int>();
        public bool IsMoving { get; set; } = false;
        public List<string> Messages { get; set; } = new List<string>();
        public List<int> DistinctRequests { get; set; } = new List<int>();
    }
}
