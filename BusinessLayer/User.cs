
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [Range(6,140, ErrorMessage = "Age must be between 6 and 140!")]
        public int Age {  get; set; }
        public Role Role { get; set; }
        public ReadingCard ReadingCard { get; set; }//should it be initialized during the use of the constructor?

        public User()
        {
            
        }

        public User(string name, int age, Role role, ReadingCard readingCard)
        {
            Name = name;
            Age = age;
            Role = role;
            ReadingCard = readingCard;//should it be initialized during the use of the constructor?
        }
    }
}
