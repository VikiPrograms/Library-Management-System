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
    public class ReadingCard
    {
        [Key]
        public int ReadingCardId {  get; set; }
        [Range(0, 3, ErrorMessage ="You cannot borrow more than 3 books at the same time!")]
        public int BorrowedBooks { get; set; }//with a method, i will count how many books there are borrowed by one user in any given time
        public DateOnly DateCreated { get; set; }

        [ForeignKey("User")]
        [DisplayName("User")]
        public string Name {  get; set; }
        [Required]
        public User User { get; set; }
        public ICollection<Book> Books { get; set; }

        public ReadingCard()
        {
            BorrowedBooks = 0;
            DateCreated = DateOnly.FromDateTime(DateTime.Now);
            Books = new List<Book>();
        }
       
        public ReadingCard( User user, int borrowedBooks = 0)
        {
            Name = user.Name;
            User = user;
            BorrowedBooks = borrowedBooks;
            DateCreated = DateOnly.FromDateTime(DateTime.Now);
            Books = new List<Book>();
        }
    }
}
