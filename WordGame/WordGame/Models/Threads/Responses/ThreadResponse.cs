namespace WordGame.Models.Threads.Responses
{
    public class ThreadResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Word { get; set; }

        public int WordCount { get; set; }

        public string Author { get; set; }
    }
}