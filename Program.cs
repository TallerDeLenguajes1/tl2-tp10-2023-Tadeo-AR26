using espacioRepositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

var cadenaConexion = builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();//el signo de exclamacion es para decir que no es null
builder.Services.AddSingleton<string>(cadenaConexion);

builder.Services.AddScoped<ITareaRepository, TareaRepository>(); // Añade las interfaces al scope para poder ser inyectadas en otra clase
builder.Services.AddScoped<ITableroRepository, TableroRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddSession(options =>//se agrega para login***********************************
{
    options.IdleTimeout = TimeSpan.FromSeconds(500000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
