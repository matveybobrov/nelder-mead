// Этот файл является точкой входа в приложение

using System.Text.Json;

// Подключаем пространство имён из файла NelderMead.cs для доступа к его классам
using NELDER_MEAD;
using HELPERS;
using System.Numerics;
using Microsoft.Extensions.FileProviders;

// Создаём сервер
var builder = WebApplication.CreateBuilder();
builder.Services.AddCors();
var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "../client")),
    RequestPath = "/client"
});

// При запросе к http://localhost:7022/result вернётся результат работы метода
// На этот адрес делает запрос клиент при нажатии на кнопку calculate
app.MapPost("/result", (Point[] points) => {
    var initialPoints = new Simplex(points[0], points[1], points[2]);

    var result = new NelderMead(initialPoints).GetResult();

    // Клиент и сервер обмениваются строками, так что преобразуем объект к строке
    string jsonString = JsonSerializer.Serialize(result);
    return jsonString;
});

// Запускаем сервер
app.Run();