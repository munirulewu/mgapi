using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.MGAPI
{
    public class CbKashTransValidation: AbstractValidator<CbKashValidationRequest>
    {
  
         CAppSettingConstant oAppSettingConstant = new CAppSettingConstant();
         public CbKashTransValidation()
        {
            //transaction
            
            RuleFor(x => x.accountCode).Length(5,20).WithMessage(oAppSettingConstant.messageforCode1003).WithErrorCode(oAppSettingConstant.responseCode1003);
            RuleFor(x => x.accountNumber).Length(2, 20).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            RuleFor(x => x.receiveCountryCode).Length(2,3).WithMessage(oAppSettingConstant.messageforCode1003).WithErrorCode(oAppSettingConstant.responseCode1003);
            
          
            //sender

            RuleFor(x => x.sender.person.firstName).MinimumLength(1).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            //RuleFor(x => x.transaction.sender.person.middleName).MinimumLength(1).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.sender.person.lastName).MinimumLength(1).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
           // RuleFor(x => x.transaction.sender.person.SecondLastName).MinimumLength(1).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
           // RuleFor(x => x.sendCountryCode).MinimumLength(3).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            //receiver

            RuleFor(x => x.receiver.person.firstName).MinimumLength(1).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
           // RuleFor(x => x.transaction.receiver.person.middleName).MinimumLength(1).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.receiver.person.lastName).MinimumLength(1).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            //RuleFor(x => x.transaction.receiver.person.SecondLastName).MinimumLength(1).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.receiveCountryCode).MinimumLength(3).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);

            //receiver amount
            RuleFor(x => x.receiveAmount.value).MinimumLength(1).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.receiveAmount.currencyCode).Length(3).WithMessage(oAppSettingConstant.messageforCode1003).WithErrorCode(oAppSettingConstant.responseCode1003);

           
            
            
            
        }
    }
}
