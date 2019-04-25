using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mobility_Android.WebService.WorkOrder;

namespace Mobility_Android.Resources.global
{
    public class Product
    {
        public string codeSSCC;
        public DateTime? packingDate;
        public decimal weightKG;
        public string palletCode;


        public int NRI;
        public string code;
        public string descFR;
        public string descEN;

        public Product()
        {
            codeSSCC = string.Empty;
            packingDate = default(DateTime?);
            weightKG = 0;
            palletCode = string.Empty;
            NRI = 0;
            code = string.Empty;
            descFR = string.Empty;
            descEN = string.Empty;
        }

        public Product(WorkOrderProductWS item)
        {
            codeSSCC = item.SSCC;
            packingDate = default(DateTime?);
            weightKG = item.Weight;
            palletCode = string.Empty;
            NRI = item.ProductId;
            code = item.ProductCode;
            descFR = item.productDescFr;
            descEN = item.ProductDescEn;
        }
    }
}