using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;

namespace Application.Resolvers
{
    public class RegionResolverForObligationSites : IValueResolver<ObligationSites, ObligationSitesDto, RegionDto>
    {
        private readonly IRegionRepository _regionRepository;

        public RegionResolverForObligationSites(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public RegionDto Resolve(ObligationSites source, ObligationSitesDto destination, RegionDto destMember, ResolutionContext context)
        {
            // Assuming GetByIdAsync returns a Task<Region> and you want to wait for it
            var region = _regionRepository.GetByIdAsync(source.region_id); 

            if (region != null)
            {
                return new RegionDto
                {
                    IdAsString = region._id,
                    name = region.name,
                };
            }

            return null; // Or return a default value if region is not found
        }
    }
}
