using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DTOs.UserDTOs
{
    [NotMapped]
    public class UserLoginDTO
    {
       
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }
      
        public bool rememberMe {  get; set; } =false;
  
    }
}
