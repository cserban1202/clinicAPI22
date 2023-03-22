using System.ComponentModel.DataAnnotations;

namespace clinicAPI.Entitites
{
    public class Examination
    {
        public int Id { get; set; }

        public string Description { get; set; }


        [Required]
        [StringLength(140)]
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime wantedDate { get; set; }
        public string Time { get; set; }
    }
}
