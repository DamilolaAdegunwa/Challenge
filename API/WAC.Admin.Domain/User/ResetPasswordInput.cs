using WAC.Admin.Domain.Emailing;
using WAC.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FWK.Domain.bus;
using FWK.Domain.Extensions;
using FWK.Domain.Mail;

namespace WAC.Admin.Domain
{
    public class ResetPasswordInput
    {
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        public string ResetCode { get; set; }

        [Required]        
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string SingleSignIn { get; set; }
    }
    public class ResetPasswordOutput
    {
        public bool CanLogin { get; set; }

        public string UserName { get; set; }
    }
}
