using AutoMapper;
using Application.DTOs;
using Application.Entities;
using Application.Entities.Category;
using Application.Resolvers;
using Application.DTOs.User;
using Application.DTOs.RiskCompliance;
using Application.DTOs.Form;
using Application.DTOs.Category;
using Application.Entities.Domain;
using Application.Entities.Form;
using Application.Entities.Facility;
using Application.Entities.RiskCompliance;
using Application.Entities.SubRegion;
using Application.Entities.SubSector;

namespace Al_Munazam.ApplicationMappers
{
    public class ApplicationMapper : Profile
    {

        public ApplicationMapper()
        {
            CreateMap<Regions, RegionDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString)).ReverseMap();
            CreateMap<SubRegions, SubRegionsDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString)).ReverseMap();
            CreateMap<SubSector, SubSectorDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString)).ReverseMap();
            CreateMap<Clusters, ClusterDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString))
                                            .ForMember(dest => dest.region, opt => opt.MapFrom<RegionResolver>()).ReverseMap();
            CreateMap<Modules, ModuleDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString))
                                         .ForMember(dest => dest.form_options, opt => opt.MapFrom(src => src.FormOptions)).ReverseMap();
            CreateMap<SiteOffice, SiteOfficeDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.ID.ToString())).ReverseMap();
            CreateMap<TicketSlas, TicketSlasDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.ID.ToString())).ReverseMap();
            CreateMap<region, SiteOfficeDto.Region_Dto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src._id))
                .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.name)).ReverseMap();
            CreateMap<Clusters, SiteOfficeDto.Cluster_Dto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.name)).ReverseMap();
            CreateMap<ParentSiteDto, ObligationSites>().ForMember(dest => dest.ID, opt => opt.MapFrom(src => src._id)).ReverseMap();
            CreateMap<Users, UserDto>()
            .ForMember(dest => dest.IdAsString, opt => opt.MapFrom(src => src.ID))
            .ReverseMap();
            CreateMap<Users, UserDto>()
            .ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString))
            .ReverseMap();
            try
            {
                CreateMap<Platform, PlatformDto>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString))
                .ReverseMap();
            }
            catch(Exception ex)
            {

            }
            CreateMap<FormOptions, FormOptionsDto>().ReverseMap();
            CreateMap<FormSubmissions, FormSubmissionsDto>()
            .ForMember(dest => dest.data, opt => opt.MapFrom(src => src.data))
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.form_id))
            .ReverseMap();

            CreateMap<ObligationSites, ObligationSitesDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.ID)).ReverseMap();

            CreateMap<ClusterDto, Clusters>().ReverseMap();
            CreateMap<SmeRegions, SmeRegionsDto>().ForMember(dest => dest.start_date, opt => opt.MapFrom(src => src._start_date)).ReverseMap();
            CreateMap<Facilities, FacilitiesDto>().ReverseMap();
            CreateMap<oauth_access_tokens, AccessTokensDTO>().ReverseMap();
            CreateMap<Users, AllUsersDto>().ReverseMap();
            CreateMap<RiskComplianceItem, RiskComplianceItemDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString)).ReverseMap();
            CreateMap<Category, CategoryDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString)).ReverseMap();
            CreateMap<Domains, DomainDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString)).ReverseMap();
            CreateMap<SubCategories, SubCategoryDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString)).ReverseMap();
            CreateMap<CategoriesDomains, CategoriesDomainsDto>().ReverseMap();
            CreateMap<activity_log, activity_logDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString)).ReverseMap();
            CreateMap<Properties, PropertiesDto>().ReverseMap();
            CreateMap<Attributes, AttributesDto>().ReverseMap();
            CreateMap<FormSubmissionsDto, FormSubmissions>().ReverseMap();
            CreateMap<CustomNotification, CustomNotificationDto>().ForMember(dest => dest._id, opt => opt.MapFrom(src => src.IdAsString)).ReverseMap();

        }

    }
}
