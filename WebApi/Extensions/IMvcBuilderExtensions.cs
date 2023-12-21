using WebApi.Utilities.Formatters;

namespace WebApi.Extensions
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomerCsvFormater(this IMvcBuilder builder) =>
            builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter())); 
    }
}
