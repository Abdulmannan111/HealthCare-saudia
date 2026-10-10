using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Form
{
    public class FormModel : AuditInfoComplete
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        public string NameEn { get; set; } = string.Empty;

        [Required]
        public string NameAr { get; set; } = string.Empty;

        [Required]
        public string SideMenu { get; set; } = string.Empty;

        [Required]
        public string Logo { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string Status { get; set; } = string.Empty;
    }

}
