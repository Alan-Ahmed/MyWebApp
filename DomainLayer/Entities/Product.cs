namespace MyWebApp.DomainLayer.Entities
{
    public class Product
    {
        public int Id { get; set; }       // Primärnyckel
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
