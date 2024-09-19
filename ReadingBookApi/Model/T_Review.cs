using System.ComponentModel.DataAnnotations;

namespace ReadingBookApi.Model
{
    public class T_Review
    {
        [Key]
        public Guid ReviewId { get; set; } = Guid.NewGuid();

        [StringLength(200)]
        public string Comment { get; set; }

        public int Rating { get; set; }

        public Guid BookId { get; set; }
        public T_Book Book { get; set; }

        public string UserId { get; set; }

        public DateTime Created { get; set; }

    }
}
