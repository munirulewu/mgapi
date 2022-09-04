using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace SIBLCommon.SIBLCommon.Common.Entity.bKash
{
    public class CRequestValidator:AbstractValidator<CallBackRequest>
    {
        public CRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(5).WithErrorCode("1000");
            //RuleFor(x => x.UserName).Equal("UserName");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).WithErrorCode("1001");
            
            RuleFor(x => x.conversionID).NotEmpty().NotNull();
            RuleFor(x => x.countryCode).NotEmpty().NotNull();
            RuleFor(x => x.msisdn).NotNull().MinimumLength(11).MaximumLength(14);
            RuleFor(x => x.firstName).NotEmpty().When(x=>x.ResponseCode.Equals("6000"));
            RuleFor(x => x.lastName).NotEmpty().When(x => x.ResponseCode.Equals("6000"));
            RuleFor(x => x.fullName).NotEmpty().When(x => x.ResponseCode.Equals("6000"));
            
            RuleFor(x => x.ResponseCode).NotEmpty().NotNull();
            RuleFor(x => x.ResponseDescription).NotEmpty();
            
           
        }
    }
}
