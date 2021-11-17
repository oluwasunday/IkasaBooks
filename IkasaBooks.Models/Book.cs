using System.Collections.Generic;

namespace IkasaBooks.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string BookUrl { get; set; }
        public bool IsFeatured { get; set; }


        public string PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Author> Authors { get; set; }
        public string BookCategoryId { get; set; }
        public Category BookCategory { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
