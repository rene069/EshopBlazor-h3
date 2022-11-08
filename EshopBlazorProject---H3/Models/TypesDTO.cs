namespace EshopBlazor.Models
{
    public class TypesDTO
    {
        public int TypesId { get; set; }
        public string TypeName { get; set; }

        public List<Produkt> Produkts { get; set; }
    }
}
