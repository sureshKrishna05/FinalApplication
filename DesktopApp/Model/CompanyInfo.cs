using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
    public class CompanyInfo
    {

            public int Id { get; set; }
            public string CompanyName { get; set; } = string.Empty;
            public string ContactNumber { get; set; } = string.Empty;
            public string GSTIN { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PAN { get; set; } = string.Empty;
            public string CompanyType { get; set; } = string.Empty;

            public string AddressLine1 { get; set; } = string.Empty;
            public string AddressLine2 { get; set; } = string.Empty;
            public string AddressLine3 { get; set; } = string.Empty;
            public string ZipCode { get; set; } = string.Empty;

            public string NickName { get; set; } = string.Empty;
            public string Website { get; set; } = string.Empty;
            public double Discount { get; set; }
        
    }
}
