using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class User
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
