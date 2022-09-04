using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    public class CDASValidator : AbstractValidator<CDAS>
    {
        CAppSettingConstant oAppSettingConstant = new CAppSettingConstant();
        public CDASValidator()
        {
            //systeminfo
            RuleFor(x => x.SystemInfo).SetValidator(x => new CSystemInfoValidator { });

            ////systeminfo
            //RuleFor(x => x.SystemInfo.UserName).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            //RuleFor(x => x.SystemInfo.UserName).Length(5, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            //RuleFor(x => x.SystemInfo.Password).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            //RuleFor(x => x.SystemInfo.Password).Length(5, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            //RuleFor(x => x.SystemInfo.RemitCompanyCode).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            //RuleFor(x => x.SystemInfo.RemitCompanyCode).Length(4).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);


            //other
            RuleFor(x => x.LastRecordId).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.LastRecordId).Length(1,5).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);                        
        }
    }
}
