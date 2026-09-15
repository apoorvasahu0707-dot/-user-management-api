using UserApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI (helpful for testing endpoints without Postman)
app.UseSwagger();
app.UseSwaggerUI();

// ---- Custom middleware pipeline ----
app.UseRequestLogging();   // logs every request: method, path, status, duration
app.UseApiKeyAuth();       // requires header X-Api-Key: mysecretkey123

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
