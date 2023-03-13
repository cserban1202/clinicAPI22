using clinicAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace clinicAPI.DTOs
{
    public class Category1CreationDTO
    {
        //[Required(ErrorMessage = "the field with the name {0} is required")]
        [StringLength(50)]
        [FirstLetterUpperCase]
        public string Name { get; set; }
    }
}
