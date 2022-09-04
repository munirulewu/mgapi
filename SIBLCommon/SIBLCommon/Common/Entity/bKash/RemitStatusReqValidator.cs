using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.bKash
{
    public class RemitStatusReqValidator : AbstractValidator<RemitStatusCallBackReq>
    {
        public RemitStatusReqValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(5).WithErrorCode("1000");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).WithErrorCode("1001");
            RuleFor(x => x.conversionID).NotEmpty().NotNull();
            RuleFor(x => x.SIBLREFNO).NotNull();
            RuleFor(x => x.ResponseCode).NotEmpty().NotNull();
            RuleFor(x => x.ResponseDescription).NotEmpty();
        }
    }
}
