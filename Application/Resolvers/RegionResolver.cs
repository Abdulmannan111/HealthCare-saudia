using AutoMapper;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Resolvers
{
    public class RegionResolver : IValueResolver<Clusters, ClusterDto, RegionDto>
    {
        private readonly IRegionRepository _regionService;

        public RegionResolver(IRegionRepository regionService)
        {
            _regionService = regionService;
        }

        public RegionDto Resolve(Clusters source, ClusterDto destination, RegionDto destMember, ResolutionContext context)
        {
            // Fetch the Region asynchronously using the RegionId
            var region =  _regionService.GetByIdAsync(source.region_id);

            if (region != null)
            {
                return new RegionDto
                {
                    IdAsString = region._id,
                    name = region.name,
                    selfAssessmentScheduleIds = region.selfAssessmentScheduleIds,
                    phcRegion=region.phcRegion,
                    created_at = region.created_at,
                    updated_at = region.updated_at,
                    Version = region.Version,
                    userIDs = region.userIDs,
                };
            }

            return null; // Or return a default value if region is not found
        }
    }

}
