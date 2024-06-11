using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.UserDTOs
{
    [NotMapped]
    public class UserRegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        public string? Name { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
      
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string Country { get; set; }
        [Required]
        public string password { get; set; }
        public IFormFile? userImage { get; set; }
    }
}
