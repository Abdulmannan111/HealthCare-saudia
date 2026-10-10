using AutoMapper;
using Application.DTOs;
using Application.Entities;
using Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTOs.User;

namespace Application.Resolvers
{
    public class ModuleResolver : IValueResolver<Users, UserDto, List<ModuleDto>>
    {
        private readonly IModuleRepository _moduleRepository;

        // Constructor for dependency injection
        public ModuleResolver(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        // Resolve method to map User's ModuleIDs to a list of ModuleDto
        public List<ModuleDto> Resolve(Users source, UserDto destination, List<ModuleDto> destMember, ResolutionContext context)
        {
            // Check if ModuleIDs is null or empty
            if (source.module_ids == null || !source.module_ids.Any())
            {
                return new List<ModuleDto>(); // Return an empty list if there are no ModuleIDs
            }

            // Fetch modules based on the module IDs from the User
            var modules = _moduleRepository.GetModulesByIds(source.module_ids);

            // If no modules found, return an empty list
            if (modules == null || !modules.Any())
            {
                return new List<ModuleDto>();
            }

            // Map the fetched modules to ModuleDto
            return modules.Select(m => new ModuleDto
            {
                Name = m.name,  // Ensure that 'name' exists in Module entity
                _id = m.ID.ToString(),  // Assuming _id is a string representation of Module ID
                reference_name = m.reference_name,  // Ensure 'reference_name' exists in Module entity
                active = m.active,  // Ensure 'active' exists in Module entity
                UserIds = m.user_ids  // Assuming user_ids is a list of users associated with the module
            }).ToList();
        }
    }
}
