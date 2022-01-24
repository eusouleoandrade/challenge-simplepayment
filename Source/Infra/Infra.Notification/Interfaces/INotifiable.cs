using System.Collections.Generic;

namespace Infra.Notification
{
    public interface INotifiable
    {
        bool HasErrorNotification { get; }

        bool HasSuccessNotification { get; }

        IReadOnlyList<NotificationMessage> ErrorNotificationResult { get; }

        IReadOnlyList<NotificationMessage> SuccessNotificationResult { get; }
    }
}