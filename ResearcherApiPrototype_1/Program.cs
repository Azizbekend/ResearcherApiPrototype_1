using ComandSenderManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ResearcherApiPrototype_1;
using ResearcherApiPrototype_1.Repos.CharacteristicRepo;
using ResearcherApiPrototype_1.Repos.ControlBlockRepo;
using ResearcherApiPrototype_1.Repos.DocumentRepo;
using ResearcherApiPrototype_1.Repos.FileStorageRepo;
using ResearcherApiPrototype_1.Repos.HardwareRepo;
using ResearcherApiPrototype_1.Repos.IncidentRepo;
using ResearcherApiPrototype_1.Repos.MaintenanceRepo;
using ResearcherApiPrototype_1.Repos.NodeIndicatesRepo;
using ResearcherApiPrototype_1.Repos.NodeRepo;
using ResearcherApiPrototype_1.Repos.ObjectPassportRepo;
using ResearcherApiPrototype_1.Repos.ServiceRequestRepo;
using ResearcherApiPrototype_1.Repos.ShemaRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("ResearcherBd")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<INodeRepo, NodeRepo>();
builder.Services.AddScoped<IHardwareRepo, HardwareRepo>();
builder.Services.AddScoped<IControlBlockRepo, ControlBlockRepo>();
builder.Services.AddScoped<IFileStorageRepo, FileStorageRepo>();
builder.Services.AddScoped<IDocumentRepo, DocumentRepo>();
builder.Services.AddScoped<IComandSender, ComandSender>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });     
});
//services.AddScoped<IEnsureseedData, EnsureSeedData>();
builder.Services.AddScoped<INodeIndicatesRepo, NodeIndicatesRepo>();
builder.Services.AddScoped<IIncidentRepo, IncidentRepo>();
builder.Services.AddScoped<ICharRepo,  CharRepo>();
builder.Services.AddScoped<IObjectPassportRepo, ObjectPassportRepo>();
builder.Services.AddScoped<IMaintenanceRepo, MaintenanceRepo>();
builder.Services.AddScoped<ISchemaRepo, SchemaRepo>();
builder.Services.AddScoped<IServiceRepo,  ServiceRepo>  ();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
