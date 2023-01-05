using System.ComponentModel.DataAnnotations;

namespace clinicAPI.Entitites
{
    public class Category2
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        public DateTime ConsultationDate { get; set; }
        public string Picture { get; set; }
    }
}
