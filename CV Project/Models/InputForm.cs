using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_Project.Models
{
    public class InputForm
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public IFormFile? ProfilePicture { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string Experiences { get; set; }
        public string Tech_Skills { get; set; }
        public DateTime Created_on { get; set; }

    }
}
