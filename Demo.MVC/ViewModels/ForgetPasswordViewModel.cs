using System.ComponentModel.DataAnnotations;

namespace Demo.MVC.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
