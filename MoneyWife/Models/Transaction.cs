using System;
using System.Collections.Generic;

namespace MoneyWife.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal MoneyNum { get; set; }
        public string CashOrBank { get; set; } = null!;
        public string? MoneyContent { get; set; }
        public int MoneyType { get; set; }
        public DateTime DateUse { get; set; }

        public virtual Type MoneyTypeNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
