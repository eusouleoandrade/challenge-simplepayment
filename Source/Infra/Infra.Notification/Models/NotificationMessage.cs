namespace Infra.Notification
{
    public class NotificationMessage
    {
        public string Message { get; set; }

        public NotificationMessage(string message)
        {
            Message = message;
        }
    }
}