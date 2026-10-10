using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Form;
using Application.DTOs.User;
using Application.Entities;
using Application.Entities.Form;

namespace Application.IRepositories
{
    public interface IFormSubmission
    {
        Task<FormSubmissionsDto> CreateFormSubmissionAsync(FormSubmissionsDto requestData);
        Task<String?> GetFormByIdAsync(string id);
        Task<String?> GetSchemeFormByIdAsync(string id);
        Task<IEnumerable<FormSubmissionsDto>> GetFormSubmissionsAsync(string formId, string? submissionId, UserDto user);
    }
}
