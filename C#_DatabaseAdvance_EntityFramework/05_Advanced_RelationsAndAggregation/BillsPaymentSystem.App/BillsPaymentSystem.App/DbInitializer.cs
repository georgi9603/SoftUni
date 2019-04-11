namespace BillsPaymentSystem.App
{
    using BillsPaymentSystem.Data;
    using BillsPaymentSystem.Models;
    using BillsPaymentSystem.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class DbInitializer
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            SeedUsers(context);

            SeedCreditCards(context);

            SeedBankAccounts(context);

            SeedPaymentMethods(context);
        }

        private static void SeedPaymentMethods(BillsPaymentSystemContext context)
        {
            var paymentMethodsList = new List<PaymentMethod>();

            for (int i = 0; i < 3; i++)
            {
                var paymentMethod = new PaymentMethod
                {
                    UserId = new Random().Next(1, context.Users.Count()),
                    PaymentType = (PaymentType)new Random().Next(1, 3)
                };

                if (i % 3 == 0)
                {
                    paymentMethod.CreditCardId = 1;
                    paymentMethod.BankAccountId = 1;
                }
                else if (i % 2 == 0)
                {
                    paymentMethod.CreditCardId = 1;
                }
                else
                {
                    paymentMethod.BankAccountId = 2;
                }

                if (!IsValid(paymentMethod))
                {
                    continue;
                }

                paymentMethodsList.Add(paymentMethod);
            }

            context.PaymentMethods.AddRange(paymentMethodsList);
            context.SaveChanges();
        }

        private static void SeedCreditCards(BillsPaymentSystemContext context)
        {
            var creditCardsList = new List<CreditCard>();

            for (int i = 0; i < 20; i++)
            {
                var credtiCard = new CreditCard
                {
                    Limit = new Random().Next(-5000, 5000),
                    MoneyOwed = new Random().Next(-5000, 5000),
                    ExpirationDate = DateTime.Now.AddDays(new Random().Next(-100, 100)),
                };

                if (!IsValid(credtiCard))
                {
                    continue;
                }

                creditCardsList.Add(credtiCard);
            }

            context.CreditCards.AddRange(creditCardsList);
            context.SaveChanges();
        }

        private static void SeedBankAccounts(BillsPaymentSystemContext context)
        {
            var bankAccountsList = new List<BankAccount>();

            for (int i = 0; i < 6; i++)
            {
                var bankAccount = new BankAccount
                {
                    Balance = new Random().Next(-5000, 5000),
                    BankName = "Bank" + i,
                    SWIFT = "Switft" + i + i
                };

                if (!IsValid(bankAccount))
                {
                    continue;
                }

                bankAccountsList.Add(bankAccount);
            }

            context.BankAccounts.AddRange(bankAccountsList);
            context.SaveChanges();
        }

        private static void SeedUsers(BillsPaymentSystemContext context)
        {
            string[] firstNames = { "Gosho", "Pesho", "Ivan", null, "", "Error" };
            string[] lastNames = { "Goshov", "Peshov", "Ivanov", null, "", "Error" };
            string[] emails = { "gosho@gmail.com", "pesho@32323.com", "van@abv.333", null, "", "Error" };
            string[] passwords = { "123456", "-213412", "pesho@gmail.com", null, "", "Error" };

            List<User> usersList = new List<User>();

            for (int i = 0; i < firstNames.Length; i++)
            {
                var user = new User
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    Email = emails[i],
                    Password = passwords[i]
                };

                if (!IsValid(user))
                {
                    continue;
                }

                usersList.Add(user);
            }

            context.Users.AddRange(usersList);
            context.SaveChanges();
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            return isValid;
        }
    }
}