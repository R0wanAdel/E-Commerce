namespace ErasmusProject.DTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Stock { get; set; }
        public int AdminId { get; set; }
    }
}
