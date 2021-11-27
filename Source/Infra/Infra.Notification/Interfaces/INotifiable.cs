using System.Collections.Generic;
using Infra.Notification.Models;

namespace Infra.Notification.Interfaces
{
    public interface INotifiable
    {
        bool HasErrorNotification { get; }

        bool HasSuccessNotification { get; }

        IReadOnlyList<NotificationMessage> ErrorNotificationResult { get; }

        IReadOnlyList<NotificationMessage> SuccessNotificationResult { get; }
    }
}