using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DapperLearning.Swagger
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach(var desc in context.ApiDescriptions)
            {
                if (desc.ParameterDescriptions.Any((x => x.Name == "api-version" && x.Source.Id == "Query")))
                    swaggerDoc.Paths.Remove($"/{desc.RelativePath?.TrimEnd('/')}");
            }
        }
    }
}
