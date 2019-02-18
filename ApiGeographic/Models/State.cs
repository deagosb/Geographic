using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiGeographic.Models
{
    public class State
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Country 
        public int CountryId { get; set; }
      
    }
}