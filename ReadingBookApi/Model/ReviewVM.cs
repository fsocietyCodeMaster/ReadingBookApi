using System.ComponentModel.DataAnnotations;

namespace ReadingBookApi.Model
{
    public class ReviewVM
    {
        [StringLength(200)]
        public string Comment { get; set; }
        public int Rating { get; set; }


    }
}
