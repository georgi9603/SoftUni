namespace BillsPaymentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }

        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Balance { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string BankName { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string SWIFT { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}