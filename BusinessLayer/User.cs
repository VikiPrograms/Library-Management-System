
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
        public string UserName { get; set; }
        [Required]
        [Range(6,140, ErrorMessage = "Age must be between 6 and 140!")]
        public int Age {  get; set; }
        public ReadingCard ReadingCard { get; set; }//should it be initialized during the use of the constructor?

        public User()
        {
            //basic constructor
        }

        public User(string username, int age, ReadingCard readingCard)
        {
            this.UserName = username;
            this.NormalizedUserName = username.ToUpper();
            Age = age;
            ReadingCard = readingCard;
            //basic second construtor
            //do i need to add a password?
        }

        public User(string username, int age)
        {
            this.UserName = username;
            this.NormalizedUserName = username.ToUpper();
            Age = age;
            ReadingCard = new ReadingCard();
            //constructor where the reading card is initialized parallel to the reading card
            //do i need to add a password?
        }

        public User(string id, string username, int age, ReadingCard readingCard)
            : this(username, age, readingCard)
        {
            this.Id = id;
            //only changing the id
        }

        public override string ToString()
        {
            return string.Format($"{Id} {UserName} {Age}");
        }

    }
}
