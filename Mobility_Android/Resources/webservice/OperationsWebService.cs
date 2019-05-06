using System;
using System.Collections.Generic;
using System.Net;

using Mobility_Android.WebService.Operations;
using Mobility_Android.WebService.Security;
using Mobility_Android.Resources.global;
using Acr.UserDialogs;

namespace Mobility_Android.Resources.webservice
{
    /**
     * Classe qui gére les méthodes du web service Operation 
     * Méthodes :
     *      connectToWebServiceOperations
     *      getListReceptions
     *      getReceptionByNRI
     *      pickLicenseReception
     *      updateSSCCListForProduct
     *      getReceptionProductDetails
     *      completeReception
     *      relocateLicense
     *      getListWorkOrdersByStation
     *      putLicenseInWorkOrder
     *      getListInterWarehouse
     *      getInterWarehouse
     *      createNewInterWarehouse
     *      pickLicenseInterWarehouse
     *      getInterWarehouseDetails
     *      completeInterWarehouse
     * 
     **/
    class OperationsWebService
    {
        private const string urlExtension = "Services/Inventory/WMSOperations.asmx";
        private static WMSOperations webServiceOperation;


        /*
         * Méthode de connexion au web service Operations pour pouvoir accèder aux méthodes
         * 
         */
        private static bool connectToWebServiceOperations()
        {
            bool isSuccess = false;
            try
            {
                webServiceOperation = new WMSOperations();
                webServiceOperation.Url = Configuration.webServiceURL + urlExtension;
                isSuccess = true;
            }
            catch (UriFormatException ex)
            {
                if (Configuration.currentLanguage == CR_TTLangue.French_France)
                {
                    //MessageBox.Show(My.Resources.RessourceFR.errWebServiceUrlIncorrect);
                }
                else
                {
                    //MessageBox.Show(My.Resources.RessourceEN.errWebServiceUrlIncorrect);
                }

                isSuccess = false;
            }
            catch (Exception ex)
            {
                if (Configuration.currentLanguage == CR_TTLangue.French_France)
                {
                    //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                }
                else
                {
                    //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                }

                isSuccess = false;
            }
            return isSuccess;
        }

#region Reception 


        /*
         * Méthode qui permet de récupérer la liste des receptions
         * 
         */
        public static List<ReceptionWS> getListReceptions(string userToken)
        {
            List<ReceptionWS> receptions = new List<ReceptionWS>();
            try
            {
                CR_ResultActionOfListOfReceptionWS result;

                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.GetListReceptions(userToken, (int)Configuration.currentLanguage, Configuration.userInfos.warehouseNRI);

                    if (result.Success)
                        receptions = new List<ReceptionWS>(result.ReturnValue);
                    else
                    {

                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                        receptions.Clear();
                    }
                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
            }

            return receptions;
        }


        /*
         * Méthode qui permet de récupérer une reception par son code NRI
         * 
         */
        public static ReceptionWS getReceptionByNRI(string userToken, int receptionNRI, int lang, int UdP_NRI, string UdP_Label)
        {
            ReceptionWS reception = new ReceptionWS();
            try
            {
                CR_ResultActionOfReceptionWS result;

                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.GetReceptionByNRI(userToken, receptionNRI, lang);

                    if (result.Success)
                        reception = result.ReturnValue;
                    else
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                        reception = null/* TODO Change to default(_) if this is not a reference type */;
                    }
                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case object _ when Ceritar.Common.CR_TTLangue.French:
                        {
                            MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
            }

            return reception;
        }


        /*
         * Méthode qui permet de récupérer le détail d'un produit grace à une licence
         * 
         */
        public static ProductDetailsWS pickLicenseReception(string userToken, LicenseWS license, int lang, int UdP_NRI, string UdP_Label)
        {
            ProductDetailsWS currentProduct = new ProductDetailsWS();
            try
            {
                CR_ResultActionOfProductDetailsWS result;

                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.PickLicenseReception(userToken, license, lang);

                    if (result.Success)
                        currentProduct = result.ReturnValue;
                    else
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                        currentProduct = null/* TODO Change to default(_) if this is not a reference type */;
                    }
                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case object _ when Ceritar.Common.CR_TTLangue.French:
                        {
                            MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
            }

            return currentProduct;
        }


        /*
         * Méthode qui permet de mettre à jour le SSCC d'un produit
         * 
         */
        public static bool updateSSCCListForProduct(string userToken, int proNRI, string sscc, int receptionNRI)
        {
            bool blnreturn = false;
            try
            {
                WebService.Operations.CR_ResultActionOfBoolean result;

                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.UpdateSSCCListForProduct(userToken, proNRI, sscc, receptionNRI);
                    blnreturn = result.Success;
                    if (!result.Success)
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                    }
                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case object _ when Ceritar.Common.CR_TTLangue.French:
                        {
                            MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
            }
            return blnreturn;
        }


        /*
         * Méthode qui permet de récupérer une liste de produit via une reception
         * 
         */
        public static ProductDetailsWS[] getReceptionProductDetails(string userToken, int receptionNRI, int lang, int UdP_NRI, string UdP_Label)
        {
            ProductDetailsWS[] productDetails = null;
            try
            {
                CR_ResultActionOfListOfProductDetailsWS result;

                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.GetReceptionProductDetails(userToken, receptionNRI, lang);

                    if (result.Success)
                        productDetails = result.ReturnValue;
                    else
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                        productDetails = null;
                    }
                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case object _ when Ceritar.Common.CR_TTLangue.French:
                        {
                            MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
            }
            return productDetails;
        }


        /*
         * Méthode qui permet de completer une reception
         * 
         */
        public static bool completeReception(string userToken, int receptionNRI)
        {
            bool blnReturn = false;
            try
            {
                WebService.Operations.CR_ResultActionOfBoolean result;

                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.CompleteReception(userToken, receptionNRI);

                    if (result.Success)
                        blnReturn = result.Success;
                    else
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                    }

                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case object _ when Ceritar.Common.CR_TTLangue.French:
                        {
                            MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
            }

            return blnReturn;
        }

#endregion

#region Relocation


            /*
             * Méthode qui permet de déplacer une licence dans un autre entrepôt
             * 
             */
            public static bool relocateLicense(string userToken, string sourceCode, string destinationCode, RELOCATION_DESTINATION destinationType, int warehouseNRI)
        {
            bool blnReturn = false;
            try
            {
                WebService.Operations.CR_ResultActionOfBoolean result;

                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.RelocaliseLicense(userToken, sourceCode, destinationCode, destinationType, warehouseNRI);

                    if (result.Success)
                        blnReturn = result.Success;                 
                    else
                    {
                        blnReturn = false;
                    }
                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
            }
            return blnReturn;
        }

#endregion

#region Production


        /*
         * Méthode qui permet de récupérer une liste de commande selon la station
         * 
         */
        public static List<WorkOrderNewWS> getListWorkOrdersByStation(string userToken, string stationCode)
        {
            List<WorkOrderNewWS> items = new List<WorkOrderNewWS>();
            try
            {
                CR_ResultActionOfListOfWorkOrderNewWS result;

                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.GetListWorkOrdersByStation(userToken, stationCode);

                    if (result.Success)
                        items = new List<WorkOrderNewWS>(result.ReturnValue);
                    else
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                        items.Clear();
                    }
                }
            }
            /*catch (WebException ex)
            {
                switch (gcCurrentConfiguration.currentLanguage)
                {
                    case object _ when Ceritar.Common.CR_TTLangue.French:
                        {
                            MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
            }

            return items;
        }


        /*
         * Méthode qui permet d'ajouter une licence dans une commande
         * 
         */
        public static bool putLicenseInWorkOrder(string usertoken, string licenseCode, int workOrderNRI)
        {
            WebService.Operations.CR_ResultActionOfBoolean result = new WebService.Operations.CR_ResultActionOfBoolean();
            try
            {
                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.PutLicenseInWorkOrder(usertoken, licenseCode, workOrderNRI, Configuration.userInfos.warehouseNRI);

                    if (!result.Success)
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                    }

                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
                result.Success = false;
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
                result.Success = false;
            }

            return result.Success;
        }

#endregion

#region WareHouse


        /*
         * Méthode qui permet de récupérer une liste d'inter entrepot
         * 
         */
        public static List<InterWarehouseObject> getListInterWarehouse(string usertoken, int warehouseSourceNRI)
        {
            List<InterWarehouseObject> items = new List<InterWarehouseObject>();
            CR_ResultActionOfListOfInterWarehouseObject result = new CR_ResultActionOfListOfInterWarehouseObject();
            try
            {
                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.GetListInterWarehouse(usertoken, warehouseSourceNRI);

                    if (result.Success)
                    {
                        foreach (InterWarehouseObject item in result.ReturnValue)
                            items.Add(item);
                    }
                    else
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                    }

                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
                result.Success = false;
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
                result.Success = false;
            }

            return items;
        }


        /*
         * Méthode qui permet de récupérer un entrepot
         * 
         */
        public static InterWarehouseObject getInterWarehouse(string usertoken, int interWarehouseNRI)
        {
            CR_ResultActionOfInterWarehouseObject result = new CR_ResultActionOfInterWarehouseObject();
            try
            {
                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.GetInterWarehouse(usertoken, interWarehouseNRI);

                    if (!result.Success)
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                    }

                }
            }
           /* catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
                result.Success = false;
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
                result.Success = false;
            }

            return result.ReturnValue;
        }


        /*
         * Méthode qui permet de créer un nouvel entrepot
         * 
         */
        public static int createNewInterWarehouse(string usertoken, int destinationWarehouseNRI, Mobility_Android.WebService.Security.UserInfosWS userInfos)
        {
            CR_ResultActionOfInt32 result = new CR_ResultActionOfInt32();
            try
            {
                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.CreateNewInterWarehouse(destinationWarehouseNRI, usertoken, userInfos.NRI, userInfos.warehouseNRI);

                    if (!result.Success)
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                    }

                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case object _ when Ceritar.Common.CR_TTLangue.French:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
                result.Success = false;
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
                result.Success = false;
            }

            return result.ReturnValue;
        }


        /*
         * Méthode qui permet de cueillir une licence
         * 
         */
        public static ProductDetailsWS pickLicenseInterWarehouse(string usertoken, LicenseWS lic)
        {
            CR_ResultActionOfProductDetailsWS result = new CR_ResultActionOfProductDetailsWS();
            try
            {
                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.PickLicenseInterWarehouse(usertoken, lic);

                    if (!result.Success)
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                    }

                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
                result.Success = false;
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
                result.Success = false;
            }

            return result.ReturnValue;
        }


        /*
         * Méthode qui permet de récupérer une liste concernant les détails des objets d'un entrepot
         * 
         */
        public static InterWarehouseDetailsObject[] getInterWarehouseDetails(string usertoken, int interWarehousenNRI)
        {
            CR_ResultActionOfListOfInterWarehouseDetailsObject result = new CR_ResultActionOfListOfInterWarehouseDetailsObject();
            try
            {
                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.GetInterWarehouseDetails(usertoken, interWarehousenNRI);

                    if (!result.Success)
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                    }

                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
                result.Success = false;
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
                result.Success = false;
            }

            return result.ReturnValue;
        }


        /*
         * Méthode qui de completer un entrepot
         * 
         */
        public static bool completeInterWarehouse(string usertoken, int interWarehousenNRI)
        {
            WebService.Operations.CR_ResultActionOfBoolean result = new WebService.Operations.CR_ResultActionOfBoolean();
            try
            {
                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.CompleteInterWarehouse(usertoken, interWarehousenNRI);

                    if (!result.Success)
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                    }
                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
                result.Success = false;
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
                result.Success = false;
            }

            return result.ReturnValue;
        }

#endregion

#region Vente

        public static CR_ResultActionOfListOfSaleWS getListPickingSale(string securedToken, int lang, int warehouseNRI)
        {
            CR_ResultActionOfListOfSaleWS productDetails = null;
            try
            {
                CR_ResultActionOfListOfSaleWS result;

                if (connectToWebServiceOperations())
                {
                    result = webServiceOperation.GetListSale(securedToken, lang, warehouseNRI);

                    if (result.Success)
                        productDetails = result;
                    else
                    {
                        var toastConfig = new ToastConfig(result.Errors[0].Message.ToString());
                        toastConfig.SetDuration(500);
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                        UserDialogs.Instance.Toast(toastConfig);
                        productDetails = null;
                    }
                }
            }
            /*catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case object _ when Ceritar.Common.CR_TTLangue.French:
                        {
                            MessageBox.Show(My.Resources.RessourceFR.errCannotReachWebservice);
                            break;
                        }

                    default:
                        {
                            MessageBox.Show(My.Resources.RessourceEN.errCannotReachWebservice);
                            break;
                        }
                }
            }*/
            catch (Exception ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
                            break;
                        }

                    default:
                        {
                            //MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
                            break;
                        }
                }
            }
            return productDetails;
        }

#endregion

    }
}