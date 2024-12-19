using AutoMapper;
using CyberCareWeb.Application;
using CyberCareWeb.Web.Extensions;
using CyberCareWeb.Application;
using CyberCareWeb.Web.Extensions;
using CyberCareWeb.Application.Requests.Queries;

var builder = WebApplication.CreateBuilder(args);

// ���������� ������������ (�������� ������ ����������)
builder.Services.AddControllers();

// ��������� CORS (����������� ������)
builder.Services.ConfigureCors();

// ������������ ����������� � ���� ������
builder.Services.ConfigureDbContext(builder.Configuration);

// ����������� ��������, ����������� ��� ������ ����������
builder.Services.ConfigureServices();

// ������������ AutoMapper (���������� ��� �������������� ��������)
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);

// ��������� MediatR (��������� ��� ������ � ��������� � ���������)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetComponentTypesQuery).Assembly));

// ��������� Swagger ��� ������������ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ����������� Razor Pages � ��������� ����� ��� ����������� �������������
builder.Services.AddRazorPages().AddRazorOptions(options =>
{
    options.PageViewLocationFormats.Add("/Pages/Shared/{0}.cshtml"); // ���� � ����� ��������������
});

var app = builder.Build();

// ����������� middleware ��� ��������� ����������� ������ (��������, CSS, JS, �����������)
app.UseStaticFiles();

// ������������ ��������� HTTP-��������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // ��������� Swagger � ������ ����������
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // ��������������� HTTP-�������� �� HTTPS

// ���������� CORS ��� �����-�������� ��������
app.UseCors("CorsPolicy");

// ��������� �������������
app.UseRouting();

// ��������� ��������� ��� API
app.MapControllers();

// ��������� ��������� ��� Razor Pages
app.MapRazorPages();

// ��������� �������� �� ���������, ������� �������������� �� ������� ��������
app.MapGet("/", () => Results.Redirect("/Home/Index"));
app.MapFallbackToPage("/Home/Index"); // ���� ������� �� ������, ����������� Home/Index

// ��������� �������� ����� �������������
app.UseEndpoints(endpoints =>
{
    // ������� ��� API
    endpoints.MapControllerRoute(
        name: "api",
        pattern: "api/{controller}/{action}/{id?}");

    // ������� �� ��������� ��� ��������� �������
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

// ������ ����������
app.Run();
