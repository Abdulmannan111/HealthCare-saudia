using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using OfficeOpenXml;
using Quartz;
using Infrastructure.Mongo;
using Infrastructure.Jobs;
using API.Middlewares;
using Al_Munazam.ApplicationMappers;
using Application.IRepositories;
using Domain.Repositories;
using Domain.Services;
using Application.Resolvers;
using Infrastructure.Configurations;
using Application.Entities.ApiSettings;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration;
using MongoDB.Driver;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Hangfire.Mongo.Migration.Strategies;

namespace Al_Munazam
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var mongoConnection = Configuration["MongoSettings:ConnectionString"];
            var databaseName = Configuration["MongoSettings:DatabaseName"];

            var mongoUrlBuilder = new MongoUrlBuilder(mongoConnection);
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

            services.AddHangfire(configuration => configuration
                 .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                 .UseSimpleAssemblyNameTypeSerializer()
                 .UseRecommendedSerializerSettings()
                 .UseMongoStorage(mongoClient, databaseName, new MongoStorageOptions
                 {
                     MigrationOptions = new MongoMigrationOptions
                     {
                         MigrationStrategy = new MigrateMongoMigrationStrategy(),
                         BackupStrategy = new NoneMongoBackupStrategy()
                     },
                     Prefix = "hangfire.mongo",
                     CheckConnection = false // Temporarily disable connection check
                 })
             );

            // Start Hangfire server
            services.AddHangfireServer();


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            services.AddMemoryCache();
            services.AddControllers();
            ConfigureCors(services);
            ConfigureMongoDB(services);
            ConfigureEmailSettings(services);
            ConfigureQuartz(services);
            RegisterRepositories(services);
            ConfigureAuthentication(services);
            ConfigureSwagger(services);
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(ApplicationMapper));
            MongoConventions.Register();
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IBackgroundJobClient backgroundJobClient)
        {
            // Enable Hangfire Dashboard (optional, for monitoring jobs)
            app.UseHangfireDashboard();
            // Ensure Hangfire server is started
            app.UseHangfireServer();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAllOrigins");
            app.UseMiddleware<JwtClaimsMiddleware>();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
        #region private methods
        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });
        }

        private void ConfigureMongoDB(IServiceCollection services)
        {
            services.Configure<MongoDBContext>(Configuration);
            services.AddSingleton<MongoDBContext>();
            var mongoSettings = Configuration.GetSection("MongoSettings").Get<MongoSettings>();
            services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoSettings.ConnectionString));
            services.AddSingleton<IMongoDatabase>(provider =>
                provider.GetRequiredService<IMongoClient>().GetDatabase(mongoSettings.DatabaseName));
        }
        private void ConfigureEmailSettings(IServiceCollection services)
        {
            // Load EmailSettings from appsettings.json
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            // Ensure that the EmailSettings Paths are correctly populated
            var emailSettings = Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            services.AddSingleton(emailSettings);
        }

        private void ConfigureQuartz(IServiceCollection services)
        {
            var jobKey = new JobKey("RegionCleanupJob");
            services.AddQuartz(q =>
            {
                q.AddJob<RegionCleanupJob>(opts => opts.WithIdentity(jobKey));
                q.AddTrigger(opts => opts.ForJob(jobKey)
                    .WithIdentity("RegionCleanupTrigger")
                    .WithCronSchedule("0 0 * * * ?")); // Runs every hour
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            services.AddTransient<RegionCleanupJob>();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISiteRepository, SiteRepository>();
            services.AddScoped<IPlatformRepository, PlatformRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddSingleton<IRegionRepository, RegionRepository>();
            services.AddScoped<IClusterRepository, ClusterRepository>();
            services.AddScoped<IFacilitySiteRepository, FacilitySiteRepository>();
            services.AddScoped<ISiteOfficeRepository, SiteOfficeRepository>();
            services.AddScoped<IFormSubmission, FormSubmissionRepository>();
            services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
            services.AddScoped<ICustomNotificationRepository, CustomNotificationRepository>();
            services.AddScoped<IFacilityTypeRepository, FacilityTypeRepository>();
            services.AddScoped<IRiskComplianceItem, RiskComplianceItemRepository>();
            services.AddScoped<ITicketSlasRepository, TicketSlasRepository>();
            services.AddSingleton<IAccessTokens, AccessTokensRepository>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddScoped<RegionResolver>();
            services.AddScoped<RegionResolverforUser>();
            services.AddScoped<ModuleResolver>();
            services.AddScoped<RegionResolverForObligationSites>();
            services.AddScoped<ClusterResolver>();
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            services.AddAuthorization();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, Array.Empty<string>() }
                });
            });
        }

        #endregion
    }
}
