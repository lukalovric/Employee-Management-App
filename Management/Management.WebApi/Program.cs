using Autofac;
using Autofac.Extensions.DependencyInjection;
using Management.Repository.Common;
using Management.Service.Common;
using Management.Repository;
using Management.Service;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");



    containerBuilder.RegisterType<EmployeeRepository>()
                    .As<IEmployeeRepository>()
                    .WithParameter("connectionString", connectionString)
                    .InstancePerLifetimeScope();

    containerBuilder.RegisterType<EmployeeService>()
                    .As<IEmployeeService>()
                    .InstancePerLifetimeScope();
});


builder.Services.AddControllers();
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
