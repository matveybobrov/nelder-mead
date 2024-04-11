// Этот файл является точкой входа вприложение

using System.Text.Json;

// Подключаем пространство имён из файла NelderMead.cs для доступа к его классам
using NELDER_MEAD;

// Создаём сервер
var builder = WebApplication.CreateBuilder();
builder.Services.AddCors();
var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin());

// Адрес сервера http://localhost:7022

// При запросе к http://localhost:7022/ вернётся текст Nelder-Mead
app.MapGet("/", () => "Nelder-Mead");

// При запросе к http://localhost:7022/result вернётся результат работы метода
// На этот адрес делает запрос клиент при нажатии на кнопку calculate
app.MapGet("/result", () => {
    var result = NelderMead.GetResult();
                
    // Клиент и сервер обмениваются строками, так что преобразуем объект к строке
    string jsonString = JsonSerializer.Serialize(result);
    return jsonString;
});

// Запускаем сервер
app.Run();