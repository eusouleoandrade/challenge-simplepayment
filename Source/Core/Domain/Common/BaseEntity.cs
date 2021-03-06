using Infra.Notification;

namespace Domain.Common
{
    public abstract class BaseEntity<TId> : Notifiable where TId : struct
    {
        public TId Id { get; set; }
    }
}