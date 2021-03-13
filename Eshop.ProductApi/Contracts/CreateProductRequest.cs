namespace Eshop.ProductApi.Contracts
{
    /// <summary>
    /// Create product request
    /// </summary>
    public record CreateProductRequest
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public long AvailableCount { get; init; }

        public long TotalCount { get; init; }

        public long ProductGroupId { get; init; }
    }
}
