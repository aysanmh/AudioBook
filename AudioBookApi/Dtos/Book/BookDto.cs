namespace AudioBookApi.Dtos.Book
{
    public class BookDto
    {

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Narrator { get; set; } = string.Empty;

        public string Genre {  get; set; } = string.Empty;
    }
}
