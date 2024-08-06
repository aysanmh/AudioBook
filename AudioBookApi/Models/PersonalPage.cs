using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBookApi.Models
{
    [Table("Pages")]
    public class PersonalPage
    {
        public string UserId { get; set; }

        public int BookId { get; set; }

        public User User { get; set; }

        public Book Book { get; set; }
    }
}
