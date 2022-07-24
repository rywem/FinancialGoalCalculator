﻿using FinancialGoalCalculator.Web.Entities.Accounts;
using FinancialGoalCalculator.Web.Enum;
using FinancialGoalCalculator.Web.Helpers;
using FinancialGoalCalculator.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FinancialGoalCalculator.Web.Pages.Accounts
{
    public partial class Upsert
    {
        [Parameter] public int? Id { get; set; }
        [Inject] private AccountService AccountService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private Account _Account;
        private List<string> _errors = new List<string>();
        bool isUpdate = false;
        protected override void OnInitialized()
        {
            _errors = new List<string>();
            if (Id == null)
            {
                isUpdate = false;
                _Account = new Account();
            }
            else
            {
                var acct = AccountService.GetAccountById((int)Id);

                if (acct != null)
                {
                    isUpdate = true;
                    _Account = acct;
                }
                else
                {
                    _errors.Add("Account not found.");
                }
            }
        }

        private void HandleSubmit()
        {
            _errors = new List<string>();
            if (_Account != null)
            {
                if (_Account.OpenedDate > DateTime.Now)
                {
                    _errors.Add("Opened Date must be today or in the past.");
                    return;
                }
                if (isUpdate == true)
                {
                    try
                    {
                        AccountService.UpdateAccount(_Account);
                    }
                    catch (Exception ex)
                    {
                        _errors = ErrorHelper.GetErrors(ex);
                    }
                }
                else
                {
                    try
                    {
                        AccountService.AddAccount(_Account);
                    }
                    catch (Exception ex)
                    {
                        _errors = ErrorHelper.GetErrors(ex);
                    }
                }
            }
            NavigationManager.NavigateTo("accounts/list");
        }

        private void OnAccountTypeValueChanged(AccountType value)
        {
            _Account.AccountType = value;
            //StateHasChanged();
        }
    }
}
