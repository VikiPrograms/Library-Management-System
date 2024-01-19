using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Author
    {
        [Key]
        public int AuthorId {  get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }

        public Author(string name)
        {
            Name = name;
            Books = new List<Book>();
        }
    }
}
