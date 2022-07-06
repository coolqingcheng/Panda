﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Admin.Models.Accounts
{
    public class CreateAccountModel
    {
        public Guid? Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    //public class CreateAccountModelValidator : AbstractValidator<CreateAccountModel>
    //{
    //    public CreateAccountModelValidator()
    //    {
    //        When(a => a.Id.HasValue == false, () =>
    //        {
    //            RuleFor(a => a.Password).IsNullThrow("密码必填");
    //        });
    //    }
    //}
}
