using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application.Entities;
using MongoDB.Driver;
using System.Data;
using Application.Entities.Category;
using Application.Entities.Domain;
using Application.Entities.Form;
using Application.Entities.Facility;
using Application.Entities.RiskCompliance;
using Application.Entities.SubSector;
using Application.Entities.SubRegion;

namespace Infrastructure.Mongo
{

    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetSection("MongoSettings:ConnectionString").Value);
            _database = client.GetDatabase(config.GetSection("MongoSettings:DatabaseName").Value);
        }

        public IMongoCollection<Users> Users => _database.GetCollection<Users>("users");
        public IMongoCollection<Sector> Sector => _database.GetCollection<Sector>("sectors");
        public IMongoCollection<Sub_sector> Sub_sector => _database.GetCollection<Sub_sector>("sub_sectors");
        public IMongoCollection<Sub_region> Sub_region => _database.GetCollection<Sub_region>("sub_regions");
        public IMongoCollection<Platform> _Platforms => _database.GetCollection<Platform>("platforms");
        public IMongoCollection<Role> Roles => _database.GetCollection<Role>("roles");
        public IMongoCollection<Permission> Permission => _database.GetCollection<Permission>("permission");
        public IMongoCollection<UserRole> _userRoles => _database.GetCollection <UserRole>("userRole");
        public IMongoCollection<ReportNumber> _reportNumbers => _database.GetCollection <ReportNumber>("report_numbers");
        public IMongoCollection<CorrectionPlan> _correction_plans => _database.GetCollection <CorrectionPlan>("correction_plans");
        public IMongoCollection<FormSubmissions> _form_submissions => _database.GetCollection <FormSubmissions>("form_submissions");

        public IMongoCollection<UserToken> _userTokens => _database.GetCollection<UserToken>("userTokens");
        public IMongoCollection<TicketSlas> _ticketSlasCollection => _database.GetCollection<TicketSlas>("ticket_slas");
        public IMongoCollection<Modules> _modules => _database.GetCollection<Modules>("modules");
        public IMongoCollection<Regions> _regions => _database.GetCollection<Regions>("regions");
        public IMongoCollection<Clusters> _clusters => _database.GetCollection<Clusters>("clusters");
        public IMongoCollection<Sites> sites => _database.GetCollection<Sites>("sites");
        public IMongoCollection<FacilityType> _facilityTypes => _database.GetCollection<FacilityType>("facilitytype");
        public IMongoCollection<ObligationSites> _ObligationSites => _database.GetCollection<ObligationSites>("obligation_sites");
        public IMongoCollection<SiteOffice> _SiteOffice => _database.GetCollection<SiteOffice>("site_offices");
        public IMongoCollection<Facilities> _Facilities => _database.GetCollection<Facilities>("obligation_sites");
        public IMongoCollection<ActivityLog> _ActivityLog => _database.GetCollection<ActivityLog>("ActivityLog");
        public IMongoCollection<activity_log> _activity_log => _database.GetCollection<activity_log>("activity_log");

        public IMongoCollection<oauth_access_tokens> _oauth_access_tokens => _database.GetCollection<oauth_access_tokens>("oauth_access_tokens");
        public IMongoCollection<RiskComplianceItem> _riskCompliance => _database.GetCollection<RiskComplianceItem>("risk_compliances");
        public IMongoCollection<Domains> _domains => _database.GetCollection<Domains>("domains");
        public IMongoCollection<SubCategories> _subCategories => _database.GetCollection<SubCategories>("sub_categories");
        public IMongoCollection<Category> _category => _database.GetCollection<Category>("categories");
        public IMongoCollection<RiskComplianceNumber> risk_compliance_numbers => _database.GetCollection<RiskComplianceNumber>("risk_compliance_numbers");
        public IMongoCollection<CustomNotification> CustomNotifications => _database.GetCollection<CustomNotification>("custom_notifications");
        public IMongoCollection<SubSector> _subSector => _database.GetCollection<SubSector>("sub_sectors");
        public IMongoCollection<SubRegions> _subRegions => _database.GetCollection<SubRegions>("sub_regions");

        public IMongoCollection<ObligationSites>? ObligationSites { get; set; }
    }
}
