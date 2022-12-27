namespace SantaShreds.Models
{
    public class Completion
    {
        public bool IsCompleted { get; set;}
        public string Completed { get { if (IsCompleted) { return "true"; } return "false"; } }
    }
}
