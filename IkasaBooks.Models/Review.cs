namespace IkasaBooks.Models
{
    public class Review
    {
        public string Id { get; set; }
        public string Comments { get; set; }

        public string BookId { get; set; }
        public string UserId { get; set; }
        public Book Book { get; set; }
        public AppUser User { get; set; }
    }
}
