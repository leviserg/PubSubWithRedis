namespace MessageContract
{
    public class TopicMessage
    {
        public string Content { get; set; }
        public string Topic { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public TopicMessage(string topic)
        {
            Topic = topic;
            Id = new Guid();
            Content = GenerateRandomString(topic.Length);
            CreatedAtUtc = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"\t{Topic} : \t{Content},\t {Id.ToString()}\t : {CreatedAtUtc.ToString("HH:mm:ss")}";
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
