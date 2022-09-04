using FluentValidation;
using SIBLCommon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    public class CTansStatusValidator: AbstractValidator<CTransactionStatus>
    {
        CAppSettingConstant oAppSettingConstant = new CAppSettingConstant();
         public CTansStatusValidator()
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


           //TransactionStatus
            RuleFor(x => x.RefTxnId).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.RefTxnId).Length(6, 18).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.TxnIdSIBL).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);            
            RuleFor(x => x.TxnIdSIBL).Length(18).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            
        }
    }
}
