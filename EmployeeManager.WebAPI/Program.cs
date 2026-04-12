using EmployeeManager.WebAPI.DependencyInjection;

var builder = new DependencyInjection().InitBuilder(args);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // This creates /openapi/v1.json

    app.UseSwaggerUI(options =>
    {
        // The name 'v1' here must match the default document name
        options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
