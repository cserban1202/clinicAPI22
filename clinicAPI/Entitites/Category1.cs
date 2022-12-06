using clinicAPI.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace clinicAPI.Entitites
{
    public class Category1
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "the field with the name {0} is required")]
        [StringLength(50)]
        [FirstLetterUpperCase]
        public string Name { get; set; }
        

     
    }
}
