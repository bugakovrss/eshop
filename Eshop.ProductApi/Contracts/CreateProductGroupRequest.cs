namespace Eshop.ProductApi.Contracts
{
    /// <summary>
    /// Request creation product group
    /// </summary>
    public record CreateProductGroupRequest
    {
        /// <summary>
        /// Name product group
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Image identifier 
        /// </summary>
        public long? ImageId { get; init; }
    }
}
