using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Required]
        [StringLength(75)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }

        public Genre()
        {
            Books = new List<Book>();
        }

        public Genre(string name)
        {
            Name = name;
            Books = new List<Book>();
        }
    }
}
