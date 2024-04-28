// ���� ���� �������� ������ ����� � ����������

using System.Text.Json;

// ���������� ������������ ��� �� ����� NelderMead.cs ��� ������� � ��� �������
using NELDER_MEAD;
using HELPERS;
using System.Numerics;

// ������ ������
var builder = WebApplication.CreateBuilder();
builder.Services.AddCors();
var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());

// ����� ������� http://localhost:7022

// ��� ������� � http://localhost:7022/ ������� ����� Nelder-Mead
app.MapGet("/", () => "Nelder-Mead");

// ��� ������� � http://localhost:7022/result ������� ��������� ������ ������
// �� ���� ����� ������ ������ ������ ��� ������� �� ������ calculate
app.MapPost("/result", (Point[] points) => {
    var initialPoints = new Simplex(points[0], points[1], points[2]);

    var result = new NelderMead(initialPoints).GetResult();

    // ������ � ������ ������������ ��������, ��� ��� ����������� ������ � ������
    string jsonString = JsonSerializer.Serialize(result);
    return jsonString;
});

// ��������� ������
app.Run();