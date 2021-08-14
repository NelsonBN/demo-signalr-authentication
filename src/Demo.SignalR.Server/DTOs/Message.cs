namespace Demo.SignalR.Server.DTOs
{
    public class Message
    {
        public string Text { get; init; }
        public Message(string text)
        {
            this.Text = text;
        }
    }
}