using System;
using System.Collections.Generic;
using System.Linq;

namespace PdfGenerator.ViewModels
{
    public class InvoiceViewModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime Due { get; set; }
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string CompanyName { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount => Items.Sum(i => i.Amount);
        public ICollection<InvoiceItemViewModel> Items { get; set; }
    }

    public class InvoiceItemViewModel
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }

        public InvoiceItemViewModel(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}