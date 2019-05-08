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
using Mobility_Android.WebService.Operations;
using static CodeParser;

namespace Mobility_Android.Resources.global
{
    public class Converts
    {
        public static LicenseWS ParsedLicToLicenceWS(ParsedLicence parsedLic)
        {
            LicenseWS result = new LicenseWS();
            result.productSSCC = parsedLic.codeSSCC;
            result.weightKG = parsedLic.weightKG;
            result.weightLb = parsedLic.weightLb;
            result.packingDate = parsedLic.packingDate;
            result.productionDate = parsedLic.productionDate;
            result.expirationDate = parsedLic.ExpirationDate;
            result.saleDate = parsedLic.SaleDate;
            result.licenseCode = parsedLic.palletCode;

            return result;
        }
    }
}