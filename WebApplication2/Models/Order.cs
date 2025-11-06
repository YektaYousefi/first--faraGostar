using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Order
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
