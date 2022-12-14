using FluentValidation;
using SIBLCommon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    public class CSystemInfoValidator : AbstractValidator<CSystemInfo>
    {
        CAppSettingConstant oAppSettingConstant = new CAppSettingConstant();
        public CSystemInfoValidator()
        {

            //systeminfo
            RuleFor(x => x.UserName).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.UserName).Length(5, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.Password).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.Password).Length(5, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
           // RuleFor(x => x.RemitCompanyCode).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
           // RuleFor(x => x.RemitCompanyCode).Length(4).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            
        }
    }
}
