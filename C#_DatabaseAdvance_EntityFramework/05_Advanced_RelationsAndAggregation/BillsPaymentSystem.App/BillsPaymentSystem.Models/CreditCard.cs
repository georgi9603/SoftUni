namespace BillsPaymentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using BillsPaymentSystem.Data.CustomAttributes;

    public class CreditCard
    {
        [Key]
        public int CreditCardId { get; set; }

        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Limit { get; set; }

        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal MoneyOwed { get; set; }

        [Required]
        public decimal LimitLeft =>
            this.Limit - this.MoneyOwed;

        [Required]
        [ExpirationDate]
        public DateTime ExpirationDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}