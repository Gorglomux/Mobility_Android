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
using Mobility_Android.WebService.Security;
using Mobility_Android.Resources.global;

namespace Mobility_Android.Resources.webservice
{
    /**
     * Classe qui gére les méthodes des web service concernant la partie reception 
     * Méthodes :
     *      connectToWebServiceOperations
     *      getListReceptions
     *      getReceptionByNRI
     *      PickLicenseReception
     *      UpdateSSCCListForProduct
     *      GetReceptionProductDetails
     *      CompleteReception
     * 
     **/
    class ReceivingWebService
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
                        //MessageBox.Show(result.Errors(0).Message.ToString);
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
                        //MessageBox.Show(result.Errors(0).Message.ToString);
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
        public static ProductDetailsWS PickLicenseReception(string userToken, LicenseWS license, int lang, int UdP_NRI, string UdP_Label)
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
                        //MessageBox.Show(result.Errors(0).Message.ToString);
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
        public static bool UpdateSSCCListForProduct(string userToken, int proNRI, string sscc, int receptionNRI)
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
                        //MessageBox.Show(result.Errors(0).Message.ToString);
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
        public static ProductDetailsWS[] GetReceptionProductDetails(string userToken, int receptionNRI, int lang, int UdP_NRI, string UdP_Label)
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
                        //MessageBox.Show(result.Errors(0).Message.ToString);
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
        public static bool CompleteReception(string userToken, int receptionNRI)
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
                        //MessageBox.Show(result.Errors(0).Message.ToString);
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
    }
}