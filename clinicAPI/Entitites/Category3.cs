using clinicAPI.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace clinicAPI.Entitites
{
    public class Category3

    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
    }

}
