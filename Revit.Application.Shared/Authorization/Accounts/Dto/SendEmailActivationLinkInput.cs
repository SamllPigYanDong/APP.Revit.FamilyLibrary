﻿using System.ComponentModel.DataAnnotations;

namespace Revit.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}