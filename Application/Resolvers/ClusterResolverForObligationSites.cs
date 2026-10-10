using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;

namespace Application.Resolvers
{
    public class ClusterResolver : IValueResolver<ObligationSites, ObligationSitesDto, ClusterDto>
    {
        private readonly IClusterRepository _clusterRepository;

        public ClusterResolver(IClusterRepository clusterRepository)
        {
            _clusterRepository = clusterRepository;
        }

        public ClusterDto Resolve(ObligationSites source, ObligationSitesDto destination, ClusterDto destMember, ResolutionContext context)
        {
            try
            {
                // Assuming GetByIdAsync returns a Task<Cluster> and you want to wait for it
                var cluster = _clusterRepository.GetById(source.sector_id);

                if (cluster != null)
                {
                    return new ClusterDto
                    {
                        IdAsString = cluster._id,
                        name = cluster.name,
                        region_id = cluster.region_id,
                        
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                // Log the exception
                return null;
            }
        }
    }
}