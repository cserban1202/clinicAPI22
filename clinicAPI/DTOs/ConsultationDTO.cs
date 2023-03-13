using System.ComponentModel.DataAnnotations;

namespace clinicAPI.DTOs
{
    public class ConsultationDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }
        [Required]
        [StringLength(140)]
        public string Name { get; set; }
        public DateTime wantedDate { get; set; }
        public string Time { get; set; }
    }
}
