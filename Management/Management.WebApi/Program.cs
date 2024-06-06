using Autofac;
using Autofac.Extensions.DependencyInjection;
using Management.Repository.Common;
using Management.Service.Common;
using Management.Repository;
using Management.Service;


var builder = WebApplication.CreateBuilder(args);

// Register services in the IServiceCollection
builder.Services.AddControllers();

// Add Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Configure Autofac
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


    // Register your types
    containerBuilder.RegisterType<EmployeeRepository>()
                    .As<IEmployeeRepository>()
                    .WithParameter("connectionString", connectionString)
                    .InstancePerLifetimeScope();

    containerBuilder.RegisterType<EmployeeService>()
                    .As<IEmployeeService>()
                    .InstancePerLifetimeScope();
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
