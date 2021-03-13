namespace Eshop.ProductApi.Contracts
{
    public record StatusResponse
    {
        public string Status { get;}

        public StatusResponse(string status)
        {
            Status = status;
        }
    }
}
