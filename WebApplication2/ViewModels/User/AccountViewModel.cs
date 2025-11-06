using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$",
    ErrorMessage = "رمز عبور باید حداقل شامل یک حرف کوچک، یک حرف بزرگ، یک عدد و یک کاراکتر خاص باشد و طول آن حداقل باید ۶ کاراکتر باشد.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }

    public class PaymentViewModel
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string CustomerName { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "{0} باید با '09' شروع شده و 11 رقم باشد.")]
        public string Phone { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string PaymentUrl { get; set; }


    }
}
