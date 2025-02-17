namespace ChefLocation.WebApi.Contracts.Product
{
    public class CreateProductRequest
    {

        public string ProcductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
