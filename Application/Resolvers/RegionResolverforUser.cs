using AutoMapper;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.User;

namespace Application.Resolvers
{
    public class RegionResolverforUser : IValueResolver<Users, UserDto, List<RegionDto>>
    {
        private readonly IRegionRepository _regionRepository;

        public RegionResolverforUser(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public List<RegionDto> Resolve(Users source, UserDto destination, List<RegionDto> destMember, ResolutionContext context)
        {
            // Fetch the regions based on region IDs
            var regions = _regionRepository.GetRegionsByIds(source.region_ids);

            // Map the regions to RegionDto
            return regions.Select(r => new RegionDto
            {
                IdAsString = r.ID.ToString(),
                name = r.name,
                phcRegion = r.phcRegion,
                selfAssessmentScheduleIds = r.selfAssessmentScheduleIds,
                Version = r.Version
            }).ToList();
        }
    }
}