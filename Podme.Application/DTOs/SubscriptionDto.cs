using Podme.Domain.Entities;
using System.Text.Json.Serialization;

namespace Podme.Application.DTOs
{
    public class SubscriptionDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SubscriptionStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? PausedDate { get; set; }
    }
}
