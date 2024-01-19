using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Book
    {
        [Key]
        public string ISBN {  get; set; }
        [Required] 
        [MaxLength(75, ErrorMessage ="Max length is 75!")]
        public string Title {  get; set; }
        [MaxLength(5000)]
        public string? Description { get; set; }
        [Range(10,1600, ErrorMessage = "The range of pages is between 10 and 1600!")]
        [Required]
        public int Pages { get; set; }
        public Author Author { get; set; }
        public DateOnly? PublicationDate { get; set; }
        public Genre Genre { get; set; }

        public Book()
        {
            PublicationDate = null;
        }

        public Book(string title, string description, Author author, Genre genre, DateOnly? publicationDate = null)
        {
            Title = title;
            Description = description;
            Author = author;
            Genre = genre;
            PublicationDate = publicationDate;
        }

        public Book(string title, Author author, Genre genre, DateOnly? publicationDate = null)//additional constructor if there isn't a written description
        {
            Title = title;
            Author = author;
            Genre = genre;
            PublicationDate= publicationDate;
        }
    }
}
