// ���� ���� �������� ������ ����� � ����������

using System.Text.Json;

// ���������� ������������ ��� �� ����� NelderMead.cs ��� ������� � ��� �������
using NELDER_MEAD;
using HELPERS;
using System.Numerics;
using Microsoft.Extensions.FileProviders;

// ������ ������
var builder = WebApplication.CreateBuilder();
builder.Services.AddCors();
var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());
app.UseDefaultFiles(new DefaultFilesOptions{
    DefaultFileNames = new List<string> { "index.html" },
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "../client"))
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "../client"))
});

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