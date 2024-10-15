using BusinessManagementReporting.Core.Interfaces;
using BusinessManagementReporting.Infrastructure.Data;
using BusinessManagementReporting.Infrastructure.Repositories;
using BusinessManagementReporting.Infrastructure.UnitOfWork;
using BusinessManagementReporting.Services.Implementations;
using BusinessManagementReporting.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using BusinessManagementReporting.Core.Entities;
using AutoMapper;
using BusinessManagementReporting.Core.Mappings;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        tableName: "Logs",
        autoCreateSqlTable: true)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddScoped<IReportValidationService, ReportValidationService>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IBookingService, BusinessManagementReporting.Services.Implementations.BookingService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IBookingServiceService, BookingServiceService>();

var mapperConfig = AutoMapperConfig.Configure();
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();
