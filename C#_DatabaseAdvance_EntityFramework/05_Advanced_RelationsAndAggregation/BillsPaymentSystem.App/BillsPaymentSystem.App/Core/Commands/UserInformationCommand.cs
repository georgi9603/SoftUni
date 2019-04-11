namespace BillsPaymentSystem.App.Core.Commands
{
    using BillsPaymentSystem.App.Core.Commands.Contracts;
    using BillsPaymentSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Text;

    public class UserInformationCommand : ICommand
    {
        private readonly BillsPaymentSystemContext context;

        public UserInformationCommand(BillsPaymentSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            int userId = int.Parse(args[0]);

            var users = context.Users
                .Include(u => u.PaymentMethods)
                    .ThenInclude(ba => ba.BankAccount)
                .Include(u => u.PaymentMethods)
                    .ThenInclude(c => c.CreditCard)
                .ToList();

            var user = users.FirstOrDefault(x => x.UserId == userId);

            if (user == null)
            {
                throw new ArgumentNullException($"User with id {userId} not found!");
            }

            var paymentMethods = context.PaymentMethods.FirstOrDefault(x => x.Id == user.UserId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"User: {user.FirstName}" + " " + $"{user.LastName}");
            sb.AppendLine($"Bank Accounts:");

            foreach (var paymentMethod in user.PaymentMethods)
            {
                if (paymentMethod.BankAccount == null)
                {
                    sb.AppendLine("User does not have bank account!");
                    break;
                }
                sb.AppendLine($"-- ID: {paymentMethod.BankAccount.BankAccountId} ");
                sb.AppendLine($"--- Balance {paymentMethod.BankAccount.Balance:F2}");
                sb.AppendLine($"--- Bank: {paymentMethod.BankAccount.BankName}");
                sb.AppendLine($"--- SWIFT: {paymentMethod.BankAccount.SWIFT}");
            }

            sb.AppendLine("Credit Cards:");

            foreach (var paymentMethod in user.PaymentMethods)
            {
                if (paymentMethod.CreditCard == null)
                {
                    sb.AppendLine("User does not have credit card!");
                    break;
                }
                sb.AppendLine($"-- ID: {paymentMethod.CreditCard.CreditCardId}");
                sb.AppendLine($"--- Limit: {paymentMethod.CreditCard.Limit:F2}");
                sb.AppendLine($"--- Money Owed: {paymentMethod.CreditCard.MoneyOwed:F2}");
                sb.AppendLine($"--- Limit Left:: {paymentMethod.CreditCard.LimitLeft:F2}");
                sb.AppendLine($"--- Expiration Date: {paymentMethod.CreditCard.ExpirationDate}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}