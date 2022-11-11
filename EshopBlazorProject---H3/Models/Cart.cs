namespace EshopBlazor.Models
{
    public class Cart
    {
        public List<ProduktDTO> Produkts { get; set; } = new();
        public Decimal Total
        {
            get
            {
                decimal total = (decimal)0.0;
                foreach (var item in Produkts)
                {
                    total += item.Price;
                }
                return total;
            }
        }

    }
}

