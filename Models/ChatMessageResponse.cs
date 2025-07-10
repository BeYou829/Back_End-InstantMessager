namespace server.Models
{
    public class ChatMessageResponse
    {
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime DateTimeMessage => DateTime.UtcNow;
    }
}
