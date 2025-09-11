namespace MyWebApp.ApplicationLayer.Common
{
    public static class MemoryProductStore
    {
        public static readonly List<ProductDTO> Products = new List<ProductDTO>();
        public static int IdCounter = 1;
    }
}
