using System.Collections.Generic;

namespace IkasaBooks.Models
{
    public class BookType
    {
        public string Id { get; set; }
        public string TypeName { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
