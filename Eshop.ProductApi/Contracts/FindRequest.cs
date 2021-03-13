namespace Eshop.ProductApi.Contracts
{
    /// <summary>
    /// Find query
    /// </summary>
    public record FindRequest
    {
        /// <summary>
        /// Elements to skip
        /// </summary>
        public int Skip { get; init; }

        /// <summary>
        /// Elements to take
        /// </summary>
        public int Take { get; init; }

        /// <summary>
        /// Search param
        /// </summary>
        public string Search { get; init; }
    }
}
