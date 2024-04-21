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

        [ForeignKey("User")]
        [DisplayName("User")]
        public string UserName {  get; set; }
        public ApplicationUser User { get; set; }
        public List<Book> Books { get; set; }

        [Range(0, 3, ErrorMessage = "You cannot borrow more than 3 books at the same time!")]
        public int BorrowedBooks { get; set; }

        [Range(0, 3, ErrorMessage = "You cannot overwrite more than 3 times in 6 months!")]
        public int NumberOfOverwrites { get; set; }
        public DateOnly DateCreated { get; set; }

        public ReadingCard()
        {   
            BorrowedBooks = 0;
            NumberOfOverwrites = 0;
            DateCreated = DateOnly.FromDateTime(DateTime.Now);
            Books = new List<Book>();
        }
       
        public ReadingCard(ApplicationUser user)
        {
            User = user;
            UserName = user.Id;
            BorrowedBooks = 0;
            NumberOfOverwrites = 0;
            DateCreated = DateOnly.FromDateTime(DateTime.Now);
            Books = new List<Book>();
        }
    }
}
