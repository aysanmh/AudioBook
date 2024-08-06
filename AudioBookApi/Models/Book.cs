using System.ComponentModel.DataAnnotations.Schema;

namespace AudioBookApi.Models
{
    [Table("Books")]
    public class Book
    {

        public int Id{ get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Narrator {  get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public List<PersonalPage> Pages { get; set; } = new List<PersonalPage>();
    }
}
