using System.ComponentModel.DataAnnotations;
using cafeMvc.Models;


namespace cafeMvc.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }  
    }
}