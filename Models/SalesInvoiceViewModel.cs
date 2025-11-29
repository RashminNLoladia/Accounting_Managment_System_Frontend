using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Accounting_Managment_System_Frontend.Models
{
    public class SalesInvoiceViewModel
    {
        [JsonPropertyName("invoiceId")]
        public int? InvoiceId { get; set; }

        [Required]
        public int CompanyId { get; set; } = 1;

        [Required]
        public int CurrencyId { get; set; } = 1;

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        public string? InvoiceNumber { get; set; }

        public decimal ExchangeRate { get; set; } = 1;

        public decimal SubTotal { get; set; }
        public decimal TaxTotal { get; set; } = 0;
        public decimal Total { get; set; }

        public string Status { get; set; } = "Draft";

        public string? Reference { get; set; }
        public DateTime? DueDate { get; set; }

        [JsonPropertyName("items")]
        public List<SalesInvoiceItemViewModel> Items { get; set; } = new();
    }

    public class SalesInvoiceItemViewModel
    {
        [JsonPropertyName("invoiceItemId")]
        public int? InvoiceItemId { get; set; }

        [JsonPropertyName("invoiceId")]
        public int? InvoiceId { get; set; }

        public int? ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }

        public int? TaxRateId { get; set; }

        public string? Description { get; set; }
    }
}
