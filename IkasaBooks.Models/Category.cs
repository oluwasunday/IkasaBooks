using System.Collections.Generic;

namespace IkasaBooks.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsPdf { get; set; }

        public ICollection<BookType> BookTypes { get; set; }
    }
}
