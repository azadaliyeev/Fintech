using Fintech.Api.BackgroundServices;
using Fintech.Api.Filters;
using Fintech.Application.Extensions;
using Fintech.Repository.Extensions;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<FluentValidationFilter>();
    opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
    options.SerializerSettings.DateFormatString = "dd/MM/yyyy";
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy(),
        allowIntegerValues: false));
});

builder.Services.AddHostedService<CurrencyBackgroundService>();





// .AddJsonOptions(opt =>
//       opt.JsonSerializerOptions.Converters.Add(
//           new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false)));


builder.Services.AddRepository(builder.Configuration).AddServices();
var app = builder.Build();

app.UseExceptionHandler(x => { });
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapControllers();
}

app.UseHttpsRedirection();


app.Run();