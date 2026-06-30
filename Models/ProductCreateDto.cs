namespace EcommerceApi.Models
{
    public class ProductCreateDto
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}