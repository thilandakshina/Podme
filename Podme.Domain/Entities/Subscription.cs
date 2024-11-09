namespace Podme.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public SubscriptionStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? PausedDate { get; set; }
        public User User { get; set; }
    }
}
