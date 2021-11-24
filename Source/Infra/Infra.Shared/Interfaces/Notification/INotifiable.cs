using System.Collections.Generic;
using Infra.Shared.Services;

namespace Infra.Shared.Interfaces
{
    public interface INotifiable
    {
        bool HasErrorNotification { get; }

        bool HasSuccessNotification { get; }

        IReadOnlyList<Notification> ErrorNotificationResult { get; }

        IReadOnlyList<Notification> SuccessNotificationResult { get; }
    }
}