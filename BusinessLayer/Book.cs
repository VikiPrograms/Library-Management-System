using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateOnly? PublicationDate { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [ForeignKey("Genre")]
        public int GenreId {  get; set; }
        public Genre Genre { get; set; }

        public DateOnly? PickUpDate {  get; set; }   
        public DateOnly? ReturnDate { get; set; }
        public bool IsPickedUp {  get; set; }

        [ForeignKey("ReadingCard")]
        [DisplayName("ReadingCard")]
        public int? ReadingCardId {  get; set; }
        public ReadingCard? ReadingCard { get; set; }

        public Book()
        {
            Description = string.Empty;
            PublicationDate = null;
            PickUpDate = DateOnly.MinValue;
            ReturnDate = DateOnly.MinValue;
            IsPickedUp = false;
        }

        public Book(string iSBN, string title, int pages, DateOnly? publicationDate, string description, Author author, Genre genre)
        {
            ISBN = iSBN;
            Title = title;
            Pages = pages;
            PublicationDate = publicationDate;
            Description = description;
            Author = author;
            AuthorId = author.AuthorId;
            Genre = genre; 
            GenreId = genre.GenreId;
            PickUpDate = DateOnly.MinValue;
            ReturnDate = DateOnly.MinValue;
            IsPickedUp = false;
        }

        public Book(string iSBN, string title, int pages, DateOnly? publicationDate, Author author, Genre genre)//additional constructor if there isn't a written description
        {
            ISBN = iSBN;
            Title = title;
            Pages = pages;
            PublicationDate = publicationDate;
            Author = author;
            AuthorId = author.AuthorId;
            Genre = genre;
            GenreId = genre.GenreId;
            PickUpDate = DateOnly.MinValue;
            ReturnDate = DateOnly.MinValue;
            IsPickedUp = false;
        }

        public Book(string iSBN)
        {
            ISBN = iSBN;
        }
    }
}
