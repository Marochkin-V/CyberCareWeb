using AutoMapper;
using CyberCareWeb.Application;
using CyberCareWeb.Web.Extensions;
using CyberCareWeb.Application;
using CyberCareWeb.Web.Extensions;
using CyberCareWeb.Application.Requests.Queries;

var builder = WebApplication.CreateBuilder(args);

// Добавление контроллеров (основная логика приложения)
builder.Services.AddControllers();

// Настройка CORS (междоменный доступ)
builder.Services.ConfigureCors();

// Конфигурация подключения к базе данных
builder.Services.ConfigureDbContext(builder.Configuration);

// Регистрация сервисов, необходимых для работы приложения
builder.Services.ConfigureServices();

// Конфигурация AutoMapper (инструмент для преобразования объектов)
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);

// Настройка MediatR (посредник для работы с запросами и командами)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetComponentTypesQuery).Assembly));

// Настройка Swagger для документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Подключение Razor Pages и настройка путей для отображения представлений
builder.Services.AddRazorPages().AddRazorOptions(options =>
{
    options.PageViewLocationFormats.Add("/Pages/Shared/{0}.cshtml"); // Путь к общим представлениям
});

var app = builder.Build();

// Подключение middleware для обработки статических файлов (например, CSS, JS, изображения)
app.UseStaticFiles();

// Конфигурация обработки HTTP-запросов
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Включение Swagger в режиме разработки
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Перенаправление HTTP-запросов на HTTPS

// Разрешение CORS для кросс-доменных запросов
app.UseCors("CorsPolicy");

// Настройка маршрутизации
app.UseRouting();

// Настройка маршрутов для API
app.MapControllers();

// Настройка маршрутов для Razor Pages
app.MapRazorPages();

// Установка маршрута по умолчанию, который перенаправляет на главную страницу
app.MapGet("/", () => Results.Redirect("/Home/Index"));
app.MapFallbackToPage("/Home/Index"); // Если маршрут не найден, открывается Home/Index

// Настройка конечных точек маршрутизации
app.UseEndpoints(endpoints =>
{
    // Маршрут для API
    endpoints.MapControllerRoute(
        name: "api",
        pattern: "api/{controller}/{action}/{id?}");

    // Маршрут по умолчанию для остальных страниц
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

// Запуск приложения
app.Run();
