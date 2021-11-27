using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using Infra.Shared.Interfaces;

namespace Infra.Shared.Services
{
    public abstract class Notifiable : INotifiable
    {
        private List<Notification> _errorNotificationResult;

        private List<Notification> _successNotificationResult;

        [NotMapped]
        [JsonIgnore]
        public bool HasErrorNotification => ErrorNotificationResult.Any();

        [NotMapped]
        [JsonIgnore]
        public bool HasSuccessNotification => SuccessNotificationResult.Any();

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyList<Notification> ErrorNotificationResult => _errorNotificationResult;

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyList<Notification> SuccessNotificationResult => _successNotificationResult;

        public Notifiable()
        {
            _errorNotificationResult = new List<Notification>();
            _successNotificationResult = new List<Notification>();
        }

        protected void AddErrorNotification(Notification errorNotification) => _errorNotificationResult.Add(errorNotification);

        protected void AddSuccessNotification(Notification successNotification) => _successNotificationResult.Add(successNotification);
    }
}