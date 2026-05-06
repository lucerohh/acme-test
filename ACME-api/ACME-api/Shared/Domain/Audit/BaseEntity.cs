namespace ACME_api.Shared.Domain.Aggregates
{
    public abstract class BaseEtity
    {
        public int Id { get; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
