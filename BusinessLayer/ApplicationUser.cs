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
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName {  get; set; }
        [Required]
        public string LastName { get; set; }
        public  Role Role{ get; set; }

        //foreign key later
        public ReadingCard ReadingCard { get; set; }

        [Required]
        //[Range(3, 140, ErrorMessage = "The password must be at least 4 characters long, have at least one small and one large letter, and have a semantic !")]
        public string Password { get; set; }

        public ApplicationUser()
        {
        }

        public ApplicationUser(string username, string password, string firstName, string lastName, Role role)
        {
            this.UserName = username;
            this.NormalizedUserName = username.ToUpper();
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

        public ApplicationUser(string username, string password, Role role)
        {
            this.UserName = username;
            this.NormalizedUserName = username.ToUpper();
            Password = password;
            Role = role;
        }

        public ApplicationUser(string username, Role role)
        {
            this.UserName = username;
            this.NormalizedUserName = username.ToUpper();
            Role = role;
        }

        public ApplicationUser(string id, string username, string password, string firstName, string lastName, Role role)
            : this(username, password, firstName, lastName, role)
        {
            this.Id = id;
            //only changing the id
        }

        public override string ToString()
        {
            return string.Format($"{Id} {UserName} {Email}");
        }

    }
}
