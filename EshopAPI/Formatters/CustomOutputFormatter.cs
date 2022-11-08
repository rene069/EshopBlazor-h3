using Datalayer.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Swashbuckle.Swagger;
using System.Text;

namespace EshopAPI.Formatters
{
    public class CustomOutputFormatter : TextOutputFormatter
    {
        public CustomOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/text"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
           => typeof(Produkt).IsAssignableFrom(type)
            || typeof(IEnumerable<Produkt>).IsAssignableFrom(type);


        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<CustomOutputFormatter>>();
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Produkt> produkts)
            {
                foreach (var produkt in produkts)
                {
                    FormatVcard(buffer, produkt, logger);
                }
            }
            else
            {
                FormatVcard(buffer,(Produkt)context.Object,logger);
            }

            await httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
        }


        private static void FormatVcard(
        StringBuilder buffer, Produkt produkt, ILogger logger)
        {
            buffer.AppendLine("BEGIN:ProduktText");

            buffer.AppendLine($"ProduktName:{produkt.ProduktName} Price:{produkt.Price}");
            buffer.AppendLine($"Description:{produkt.Description}");
            buffer.AppendLine($"UID:{produkt.ProduktId}");
            buffer.AppendLine("END:ProduktText");

            logger.LogInformation("Writing {ProduktName} {Price}",
                produkt.ProduktName, produkt.Price);
        }
    }
}
