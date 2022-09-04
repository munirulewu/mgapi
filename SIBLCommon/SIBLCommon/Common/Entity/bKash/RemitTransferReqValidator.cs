using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.bKash
{
    public class CRemitTransferReqValidator : AbstractValidator<RemitCallBack>
    {

        public CRemitTransferReqValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(5).WithErrorCode("1000");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).WithErrorCode("1001");

            RuleFor(x => x.conversionID).NotEmpty().NotNull();
            //RuleFor(x => x.msisdn).NotNull().MinimumLength(11).MaximumLength(14);
            RuleFor(x => x.SIBLREFNO).NotNull();
            //RuleFor(x => x.firstName).NotEmpty();
            //RuleFor(x => x.lastName).NotEmpty();
            //RuleFor(x => x.fullName).NotEmpty();

            RuleFor(x => x.ResponseCode).NotEmpty().NotNull();
            RuleFor(x => x.ResponseDescription).NotEmpty();
        }
    }
}
