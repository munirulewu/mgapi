using FluentValidation;
using SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    public class CTransactionValidation : AbstractValidator<CTransaction>
    {
        CAppSettingConstant oAppSettingConstant = new CAppSettingConstant();
        public CTransactionValidation()
        {
            //transaction
            RuleFor(x => x.transaction.mgiTransactionId).Length(20, 32).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            RuleFor(x => x.accountCode).Length(5, 20).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            RuleFor(x => x.accountNumber).Length(2, 20).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            RuleFor(x => x.transaction.receiveCountryCode).Length(2,3).WithMessage(oAppSettingConstant.messageforCode1003).WithErrorCode(oAppSettingConstant.responseCode1003);
            RuleFor(x => x.transaction.sendCountryCode).Length(2,3).WithMessage(oAppSettingConstant.messageforCode1003).WithErrorCode(oAppSettingConstant.responseCode1003);
          
            //sender

            RuleFor(x => x.transaction.sender.person.firstName).Length(1, 80).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            RuleFor(x => x.transaction.sender.person.middleName).MaximumLength(80).WithMessage(oAppSettingConstant.messageforCode1005).WithErrorCode(oAppSettingConstant.responseCode1005);
            RuleFor(x => x.transaction.sender.person.lastName).Length(1, 80).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            RuleFor(x => x.transaction.sender.person.SecondLastName).MaximumLength(80).WithMessage(oAppSettingConstant.messageforCode1005).WithErrorCode(oAppSettingConstant.responseCode1005);
            RuleFor(x => x.transaction.sendCountryCode).MinimumLength(3).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            //receiver

            RuleFor(x => x.transaction.receiver.person.firstName).Length(1,80).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.transaction.receiver.person.middleName).MaximumLength(80).WithMessage(oAppSettingConstant.messageforCode1005).WithErrorCode(oAppSettingConstant.responseCode1005);
            RuleFor(x => x.transaction.receiver.person.lastName).Length(1,80).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.transaction.receiver.person.SecondLastName).MaximumLength(80).WithMessage(oAppSettingConstant.messageforCode1005).WithErrorCode(oAppSettingConstant.responseCode1005);
            RuleFor(x => x.transaction.receiveCountryCode).MinimumLength(3).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);

            //receiver amount
            RuleFor(x => x.transaction.receiveAmount.value).Length(1,20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
           // RuleFor(x => Convert.ToDecimal(x.transaction.receiveAmount.value)).LessThanOrEqualTo(0).WithMessage(oAppSettingConstant.messageforCode1008).WithErrorCode(oAppSettingConstant.responseCode1008);
            RuleFor(x => x.transaction.receiveAmount.currencyCode).Length(3).WithMessage(oAppSettingConstant.messageforCode1003).WithErrorCode(oAppSettingConstant.responseCode1003);

            //sender amount
            //RuleFor(x => x.sendAmount.value).MinimumLength(1).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            //RuleFor(x => x.sendAmount.currencyCode).Length(3).WithMessage(oAppSettingConstant.messageforCode1003).WithErrorCode(oAppSettingConstant.responseCode1003);
            
            
            
            
        }
    }
}
