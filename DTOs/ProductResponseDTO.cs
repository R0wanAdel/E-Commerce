namespace ErasmusProject.DTOs
{
    public class ProductResponseDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; } // Asegúrate que sea double
        public int Stock { get; set; }
    }
}
