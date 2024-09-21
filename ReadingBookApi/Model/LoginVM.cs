using System.ComponentModel.DataAnnotations;

namespace ReadingBookApi.Model
{
    public class LoginVM
    {
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
