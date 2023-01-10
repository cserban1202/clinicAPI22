using System.ComponentModel.DataAnnotations;

namespace clinicAPI.Entitites
{
    public class Category4
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(140)]
        public string Name { get; set; }
        public DateTime wantedDate { get; set; }

    }
}
