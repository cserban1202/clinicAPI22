using System.ComponentModel.DataAnnotations;

namespace clinicAPI.DTOs
{
    public class Category2DTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime ConsultationDate { get; set; }
        public string Picture { get; set; }
    }
}
