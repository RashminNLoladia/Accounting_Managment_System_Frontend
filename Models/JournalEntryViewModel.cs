using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accounting_Managment_System_Frontend.Models
{
    public class JournalEntryLineViewModel
    {
        public int? JournalEntryLineId { get; set; }

        [Required]
        public int AccountId { get; set; }

        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Debit { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Credit { get; set; }
    }

    public class JournalEntryViewModel
    {
        public int? JournalEntryId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string JournalNumber { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; } = DateTime.Today;

        public string? Narration { get; set; }

        public string Status { get; set; } = "Draft"; // Draft, Posted

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? PostedAt { get; set; }

        public List<JournalEntryLineViewModel> Lines { get; set; } = new();
    }
}
