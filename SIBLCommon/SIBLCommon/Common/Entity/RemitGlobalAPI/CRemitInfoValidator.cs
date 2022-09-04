using FluentValidation;
using SIBLCommon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIBLCommon.SIBLCommon.Common.Entity.RemitGlobalAPI
{
    public class CRemitInfoValidator : AbstractValidator<CRemitInfo>
    {
        CAppSettingConstant oAppSettingConstant = new CAppSettingConstant();

        public CRemitInfoValidator()
        {
            //systeminfo
            RuleFor(x => x.SystemInfo).SetValidator(x=> new CSystemInfoValidator {});

            //systeminfo
            //RuleFor(x => x.SystemInfo.UserName).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            //RuleFor(x => x.SystemInfo.UserName).Length(5, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            //RuleFor(x => x.SystemInfo.Password).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            //RuleFor(x => x.SystemInfo.Password).Length(5, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            //RuleFor(x => x.SystemInfo.RemitCompanyCode).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            //RuleFor(x => x.SystemInfo.RemitCompanyCode).Length(4).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);

            //TransactionInfo            
            RuleFor(x => x.TransactionInfo.Amount).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.TransactionInfo.Amount).Length(1,10).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);            
            RuleFor(x => x.TransactionInfo.Amount).Matches(oAppSettingConstant.regExpNumber).WithMessage(oAppSettingConstant.messageforCode1006).WithErrorCode(oAppSettingConstant.responseCode1006);            
            RuleFor(x => x.TransactionInfo.AccountNo).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.TransactionInfo.AccountNo).Length(10,17).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.TransactionInfo.Country).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001); ;
            RuleFor(x => x.TransactionInfo.Country).Length(4, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.TransactionInfo.Currency).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001); ;
            RuleFor(x => x.TransactionInfo.Currency).Length(2, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.TransactionInfo.RefTxnId).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001); ;
            RuleFor(x => x.TransactionInfo.RefTxnId).Length(6, 18).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.TransactionInfo.TransactionType).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.TransactionInfo.TransactionType).IsInEnum().WithMessage(oAppSettingConstant.messageforCode1007).WithErrorCode(oAppSettingConstant.responseCode1007);
            RuleFor(x => x.TransactionInfo.BankName).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.TransactionInfo.BranchName).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.TransactionInfo.District).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            //RuleFor(x => x.TransactionInfo.BankName).NotEmpty().When(x => x.TransactionInfo.TransactionType == CurrentTransactionType.BEFTN).WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            //RuleFor(x => x.TransactionInfo.BranchName).NotEmpty().When(x => x.TransactionInfo.TransactionType == CurrentTransactionType.BEFTN).WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            //RuleFor(x => x.TransactionInfo.District).NotEmpty().When(x => x.TransactionInfo.TransactionType == CurrentTransactionType.BEFTN).WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.TransactionInfo.BankName).NotEmpty().When(x => x.TransactionInfo.TransactionType == CurrentTransactionType.BEFTN).WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.TransactionInfo.BranchName).NotEmpty().When(x => x.TransactionInfo.TransactionType == CurrentTransactionType.BEFTN).WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.TransactionInfo.District).NotEmpty().When(x => x.TransactionInfo.TransactionType == CurrentTransactionType.BEFTN).WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            When(x => !string.IsNullOrEmpty(x.TransactionInfo.BankName),
            () =>
            {
                RuleFor(x => x.TransactionInfo.BankName).Length(4, 70).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            });
            When(x => !string.IsNullOrEmpty(x.TransactionInfo.BranchName),
            () =>
            {
                RuleFor(x => x.TransactionInfo.BranchName).Length(4, 100).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            });
            When(x => !string.IsNullOrEmpty(x.TransactionInfo.District),
            () =>
            {
                RuleFor(x => x.TransactionInfo.District).Length(4, 50).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            });
            When(x => !string.IsNullOrEmpty(x.TransactionInfo.BankCode),
            () =>
            {
                RuleFor(x => x.TransactionInfo.BankCode).Length(1, 6).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            });
            When(x => !string.IsNullOrEmpty(x.TransactionInfo.BranchCode),
            () =>
            {
                RuleFor(x => x.TransactionInfo.BranchCode).Length(1, 6).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            });
            When(x => !string.IsNullOrEmpty(x.TransactionInfo.DistrictCode),
            () =>
            {
                RuleFor(x => x.TransactionInfo.DistrictCode).Length(1, 6).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            });
            RuleFor(x => x.TransactionInfo.RoutingNumber).Length(5, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
                                  
            //SenderInfo
            RuleFor(x => x.SenderInfo.Nationality).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.SenderInfo.FirstName).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.SenderInfo.FirstName).Length(4,40).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);            
            RuleFor(x => x.SenderInfo.LastName).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.SenderInfo.LastName).Length(4, 40).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.SenderInfo.ContactNo).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.SenderInfo.ContactNo).Length(13, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.SenderInfo.Location).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.SenderInfo.Location).Length(4, 150).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.SenderInfo.Address).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.SenderInfo.Address).Length(4, 150).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.SenderInfo.Nationality).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.SenderInfo.Nationality).Length(4, 50).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.SenderInfo.DocumentType).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.SenderInfo.DocumentType).IsInEnum().WithMessage(oAppSettingConstant.messageforCode1007).WithErrorCode(oAppSettingConstant.responseCode1007);
            RuleFor(x => x.SenderInfo.DocumentNumber).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.SenderInfo.DocumentNumber).Length(5, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            When(x => (x.SenderInfo.KycSourceOfFund!=null),
            () =>
            {
                RuleFor(x => x.SenderInfo.KycSourceOfFund).IsInEnum().WithMessage(oAppSettingConstant.messageforCode1007).WithErrorCode(oAppSettingConstant.responseCode1007);
            });
            When(x => (x.SenderInfo.KycPurpose != null),
            () =>
            {
                RuleFor(x => x.SenderInfo.KycPurpose).IsInEnum().WithMessage(oAppSettingConstant.messageforCode1007).WithErrorCode(oAppSettingConstant.responseCode1007);
            });            
            When(x => !string.IsNullOrEmpty(x.SenderInfo.Dob),
            () =>
            {
                RuleFor(x => x.SenderInfo.Dob).Matches(oAppSettingConstant.regExpDateFormat).WithMessage(oAppSettingConstant.messageforCode1009).WithErrorCode(oAppSettingConstant.responseCode1009);
                RuleFor(x => x.SenderInfo.Dob).Length(10).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            });
            When(x => !string.IsNullOrEmpty(x.SenderInfo.Pob),
            () =>
            {
                RuleFor(x => x.SenderInfo.Pob).Length(4, 50).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            });            
            When(x => !string.IsNullOrEmpty(x.SenderInfo.IdIssueDate),
            () =>
            {
                RuleFor(x => x.SenderInfo.IdIssueDate).Matches(oAppSettingConstant.regExpDateFormat).WithMessage(oAppSettingConstant.messageforCode1009).WithErrorCode(oAppSettingConstant.responseCode1009);
                RuleFor(x => x.SenderInfo.IdIssueDate).Length(10).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            });            
            When(x => !string.IsNullOrEmpty(x.SenderInfo.IdExpiryDate),
            () =>
            {
                RuleFor(x => x.SenderInfo.IdExpiryDate).Matches(oAppSettingConstant.regExpDateFormat).WithMessage(oAppSettingConstant.messageforCode1009).WithErrorCode(oAppSettingConstant.responseCode1009);
                RuleFor(x => x.SenderInfo.IdExpiryDate).Length(10).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            });

            
            // Receiver Information Validator
            RuleFor(x => x.RecipientInfo.ContactNo).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.RecipientInfo.ContactNo).Length(13, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.RecipientInfo.FirstName).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.RecipientInfo.FirstName).Length(4, 40).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.RecipientInfo.LastName).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.RecipientInfo.LastName).Length(4, 40).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.RecipientInfo.FullName).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.RecipientInfo.FullName).Length(4, 80).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.RecipientInfo.Location).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.RecipientInfo.Location).Length(4, 150).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            RuleFor(x => x.RecipientInfo.CountryCode).NotEmpty().WithMessage(oAppSettingConstant.messageforCode1001).WithErrorCode(oAppSettingConstant.responseCode1001);
            RuleFor(x => x.RecipientInfo.CountryCode).Length(2).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            When(x => !string.IsNullOrEmpty(x.RecipientInfo.IdNo),
            () =>
            {
                RuleFor(x => x.RecipientInfo.IdNo).Length(4, 20).WithMessage(oAppSettingConstant.messageforCode1002).WithErrorCode(oAppSettingConstant.responseCode1002);
            });
            When(x => (x.RecipientInfo.IdType!=null),
            () =>
            {
                RuleFor(x => x.RecipientInfo.IdType).IsInEnum().WithMessage(oAppSettingConstant.messageforCode1007).WithErrorCode(oAppSettingConstant.responseCode1007);
            });            
            When(x => !string.IsNullOrEmpty(x.RecipientInfo.IdIssueDate),
            () =>
            {
                RuleFor(x => x.RecipientInfo.IdIssueDate).Matches(oAppSettingConstant.regExpDateFormat).WithMessage(oAppSettingConstant.messageforCode1009).WithErrorCode(oAppSettingConstant.responseCode1009);
                RuleFor(x => x.RecipientInfo.IdIssueDate).Length(10).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            });

            When(x => !string.IsNullOrEmpty(x.RecipientInfo.IdExpiryDate),
            () =>
            {
                RuleFor(x => x.RecipientInfo.IdExpiryDate).Matches(oAppSettingConstant.regExpDateFormat).WithMessage(oAppSettingConstant.messageforCode1009).WithErrorCode(oAppSettingConstant.responseCode1009);
                RuleFor(x => x.RecipientInfo.IdExpiryDate).Length(10).WithMessage(oAppSettingConstant.messageforCode1004).WithErrorCode(oAppSettingConstant.responseCode1004);
            });

        }
    }
}

