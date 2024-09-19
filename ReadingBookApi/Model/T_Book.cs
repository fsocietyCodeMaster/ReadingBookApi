using System.ComponentModel.DataAnnotations;

namespace ReadingBookApi.Model
{
    public class T_Book
    {
        [Key]
        public Guid BookId { get; set; } = Guid.NewGuid();

        [StringLength(30)]
        public string? Title { get; set; }

        [StringLength(150)]
        public string? Description { get; set; }

        [StringLength(30)]
        public string? Author { get; set; }

        [StringLength(50)]
        public string? Genre { get; set; }
 

        public ICollection<T_Review> review_Ratings { get; set; }
        
    }
}
