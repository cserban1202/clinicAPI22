using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace clinicAPI.DTOs
{
    public class Category2CreationDTO
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        public DateTime ConsultationDate { get; set; }
        public IFormFile Picture { get; set; }
    }
}
