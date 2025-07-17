 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModel
{
    public class BillingViewModel
    {
        public bool IsVisible { get; set; } = false;
        public string Product { get; set; } = string.Empty;
        public string BatchNo { get; set; } = string.Empty;
        public string Pac { get; set; } = string.Empty;
        public string Qty { get; set; } = string.Empty;
        public string Fr { get; set; } = string.Empty;
        public string Exp { get; set; } = string.Empty;
        public string Mrp { get; set; } = string.Empty;
        public string Ptr { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        // Implement INotifyPropertyChanged...
    }
}
