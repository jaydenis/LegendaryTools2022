using System;
using System.Collections.Generic;
using System.Text;

namespace LegendaryMastermindCore.Models
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }
        public string CardName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
