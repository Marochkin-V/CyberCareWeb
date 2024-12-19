using AutoMapper;
using CyberCareWeb.Application;
using CyberCareWeb.Web.Extensions;
using CyberCareWeb.Application.Requests.Queries;
using CyberCareWeb.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ��������� �����������
builder.Services.AddControllers();

// �������� CORS
builder.Services.ConfigureCors();

// ������������ DbContext
builder.Services.ConfigureDbContext(builder.Configuration);

// ������������ ������ �������
builder.Services.ConfigureServices();

// �������� AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);

// �������� MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetComponentTypesQuery).Assembly));

// ���������� Swagger ��� ������������ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ��� ������ � ������������� API � ������ ����������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ��� ������������ ����������� ������ �� ����� wwwroot
app.UseStaticFiles(); // ��� ������ ��������� ������� �������� ����������� ����� (HTML, CSS, JS � �.�.)

// ��������������� �� �������� index.html �� ���������
app.MapFallbackToFile("/index.html"); // �������������� �� index.html, ���� ��� ������ ���������.

app.UseHttpsRedirection(); // ��������������� �� HTTPS

// ���������� CORS ��������
app.UseCors("CorsPolicy");

app.UseRouting(); // ���������� �������������

app.MapControllers(); // ������ �����������

// ������������� ���� ������ ��� ������ ����������
DbInitializer.Initialize(app.Services, app.Logger);

app.Run(); // ��������� ����������
