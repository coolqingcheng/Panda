using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Tools.Attributes;

namespace Panda.Admin.Controllers
{
    public class TestController : AdminController
    {
        private readonly IValidator<TestModel> _validator;

        public TestController()
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public string Test([FromQuery] TestModel model)
        {
            return model.Id;
        }
    }


    public class TestModel
    {
        public string Id { get; set; }
    }

    public class TestModelValidator : AbstractValidator<TestModel>
    {
        public TestModelValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("Id不能为空字符串")
                .NotNull().WithMessage("Id不能等于空");
            RuleFor(a => a.Id).MinimumLength(20).WithMessage("长度不能小于20");
        }
    }
}
