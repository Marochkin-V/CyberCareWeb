using AutoMapper;
using CyberCareWeb.Application;
using CyberCareWeb.Web.Extensions;
using CyberCareWeb.Application.Requests.Queries;
using CyberCareWeb.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры
builder.Services.AddControllers();

// Настроим CORS
builder.Services.ConfigureCors();

// Конфигурация DbContext
builder.Services.ConfigureDbContext(builder.Configuration);

// Регистрируем другие сервисы
builder.Services.ConfigureServices();

// Настроим AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);

// Настроим MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetComponentTypesQuery).Assembly));

// Добавление Swagger для документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Для работы с документацией API в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Для обслуживания статических файлов из папки wwwroot
app.UseStaticFiles(); // Эта строка позволяет серверу отдавать статические файлы (HTML, CSS, JS и т.д.)

// Перенаправление на страницу index.html по умолчанию
app.MapFallbackToFile("/index.html"); // Перенаправляем на index.html, если нет других маршрутов.

app.UseHttpsRedirection(); // Перенаправление на HTTPS

// Используем CORS политику
app.UseCors("CorsPolicy");

app.UseRouting(); // Используем маршрутизацию

app.MapControllers(); // Маппим контроллеры

// Инициализация базы данных при старте приложения
DbInitializer.Initialize(app.Services, app.Logger);

app.Run(); // Запускаем приложение
