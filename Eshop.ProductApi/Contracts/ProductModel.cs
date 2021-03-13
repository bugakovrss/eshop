namespace Eshop.ProductApi.Contracts
{
    public record ProductModel
    {
        public long Id { get; init; }
        public string Name { get; init; }

        public string Description { get; init; }

        public long AvailableCount { get; init; }

        public long TotalCount { get; init; }

        public long ProductGroupId { get; init; }
    }
}
