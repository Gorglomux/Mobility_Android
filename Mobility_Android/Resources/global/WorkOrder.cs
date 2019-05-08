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

    public class WorkOrder
    {
        public int NRI;

        public string templateCode;

        public string templateDescFR;

        public string templateDescEN;

        public DateTime? dateStart;

        public DateTime? dateEnd;

        public int productionLineNRI;

        public int companyNRI;

        public string companyCode;

        public string companyName;

        public bool isMyCompany;

        public List<Product> products;
        public WorkOrder()
        {
            NRI = 0;
            templateCode = string.Empty;
            templateDescFR = string.Empty;
            templateDescEN = string.Empty;
            dateStart = default(DateTime?);
            dateEnd = default(DateTime?);
            productionLineNRI = 0;
            companyNRI = 0;
            companyCode = string.Empty;
            companyName = string.Empty;
            isMyCompany = false;
            products = new List<Product>();
        }

        public WorkOrder(WorkOrderWS item)
        {
            NRI = item.id;
            templateCode = item.TemplateCode;
            templateDescFR = item.TemplateDescFr;
            templateDescEN = item.TemplateDescEn;
            dateStart = item.dateDebut;
            dateEnd = item.dateFin;
            productionLineNRI = item.ProductionLine;
            companyNRI = item.cieNri;
            companyCode = item.cieCode;
            companyName = item.cieNom;
            isMyCompany = item.cieMoi;
            products = new List<Product>();
            foreach (WorkOrderProductWS pro in item.Products)
                products.Add(new Product(pro));
        }
    }
}