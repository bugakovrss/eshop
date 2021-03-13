namespace Eshop.ProductApi.Contracts
{
    public record ProductGroupModel
    {
        public long Id { get; init; }

        public string Name { get; init; }

        public long? ImageId { get; init; }
    }
}
