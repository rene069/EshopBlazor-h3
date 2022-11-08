using Datalayer.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace Eshop.TagHelpers
{
   
    
    public class ShoppingCartTotal : TagHelper
    {
        public List<UserProdukt> UserProdukts { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "label";
            decimal total = 0;
            var sb = new StringBuilder();

            foreach (var item in UserProdukts)
            {
                total += (item.Produkt.Price * item.Quantity);
            }

            sb.AppendFormat($"{total} DKK");
            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }
}
