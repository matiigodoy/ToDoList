using ToDoList.Data.EF;
using ToDoList.Servicio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<AzureListDbContext>();
builder.Services.AddScoped<IPrioridadLogica, PrioridadLogica>();
builder.Services.AddScoped<ITareaLogica, TareaLogica>();
builder.Services.AddScoped<ITableroLogica, TableroLogica>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tablero}/{action=MisTableros}/{id?}");

app.Run();
