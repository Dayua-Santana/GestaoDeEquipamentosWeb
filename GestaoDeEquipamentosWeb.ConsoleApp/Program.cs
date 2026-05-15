//Montar um Servidor Web


// Builder de um servidor web
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//MVC
builder.Services.AddControllersWithViews();

// Criação da instância do Servidor Web
WebApplication app = builder.Build();

//Middlewares - Funções que executam em cada chamada que o nosso servidor vai receber
app.UseRouting();

//Todo controlador vai ter uma rota bem especifica padrão
app.MapDefaultControllerRoute();

//Inicia o loop da Aplicação
app.Run();


