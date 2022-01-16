using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using Infra.Notification.Interfaces;
using Infra.Notification.Models;

namespace Infra.Notification.Abstrations
{
    public abstract class Notifiable : INotifiable
    {
        private List<NotificationMessage> _errorNotificationResult;

        private List<NotificationMessage> _successNotificationResult;

        [NotMapped]
        [JsonIgnore]
        public bool HasErrorNotification => ErrorNotificationResult.Any();

        [NotMapped]
        [JsonIgnore]
        public bool HasSuccessNotification => SuccessNotificationResult.Any();

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyList<NotificationMessage> ErrorNotificationResult => _errorNotificationResult;

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyList<NotificationMessage> SuccessNotificationResult => _successNotificationResult;

        public Notifiable()
        {
            _errorNotificationResult = new List<NotificationMessage>();
            _successNotificationResult = new List<NotificationMessage>();
        }

        protected void AddErrorNotification(NotificationMessage errorNotificationMessage) => _errorNotificationResult.Add(errorNotificationMessage);

        protected void AddErrorNotification(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                _errorNotificationResult.Add(new NotificationMessage(message));
        }

        protected void AddSuccessNotification(NotificationMessage successNotificationMessage) => _successNotificationResult.Add(successNotificationMessage);
    }
}