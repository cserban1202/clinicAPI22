using System.ComponentModel.DataAnnotations;

namespace clinicAPI.DTOs
{
    public class Category4CreationDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(140)]
        public string Name { get; set; }
        public DateTime wantedDate { get; set; }

    }
}
