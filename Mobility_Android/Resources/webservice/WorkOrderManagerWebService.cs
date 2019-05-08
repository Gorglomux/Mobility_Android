using System;
using System.Collections.Generic;
using System.Net;

using Mobility_Android.WebService.Security;
using Mobility_Android.WebService.WorkOrder;
using Mobility_Android.Resources.global;

namespace Mobility_Android.Resources.webservice
{
    /**
     * Classe qui gére les méthodes du web service concernant la partie utilisateur 
     *  Méthodes :
     *      connectToWebServiceWorkOrder
     *      getListWorkOrders
     *      createLicense
     * 
     **/
    class WorkOrderManagerWebService
    {
        private const string urlExtension = "services/inventory/workorder.asmx";

        private static WebService.WorkOrder.WorkOrder webServiceWorkOrder;


        /*
         * Méthode de connexion au web service WorkOrder pour pouvoir accèder aux méthodes
         * 
         */
        private static bool connectToWebServiceWorkOrder()
        {
            bool isSuccess = false;
            try
            {
                webServiceWorkOrder = new WebService.WorkOrder.WorkOrder();
                webServiceWorkOrder.Url = Configuration.webServiceURL + urlExtension;
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


        /*
         * Méthode pour pouvoir récuperer une liste de commande
         * 
         */
        public static List<global.WorkOrder> getListWorkOrders(string userToken, int stationNRI)
        {
            List<global.WorkOrder> workOrders = new List<global.WorkOrder>();
            try
            {
                CR_ResultActionOfListOfWorkOrderWS result;

                if (connectToWebServiceWorkOrder())
                {
                    result = webServiceWorkOrder.GetWorkOrdersRF(userToken, stationNRI);

                    if (result.Success)
                    {
                        foreach (WorkOrderWS item in result.ReturnValue)

                            workOrders.Add(new global.WorkOrder(item));
                    }
                    else
                    {
                        //MessageBox.Show(result.Errors(0).Message.ToString);
                        workOrders.Clear();
                    }
                }
            }
            catch (WebException ex)
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
            }
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

            return workOrders;
        }


        /*
         * Méthode pour créer une licence
         * 
         */
        public static bool createLicense(LisenseExtrant[] licenses, bool isFullPallet)
        {
            bool blnReturn = false;
            try
            {
                WebService.WorkOrder.CR_ResultActionOfBoolean result;

                if (connectToWebServiceWorkOrder())
                {
                    result = webServiceWorkOrder.CreateBtExtrantLic(licenses, isFullPallet);

                    if (!result.Success)
                    {
                        //MessageBox.Show(result.Errors(0).Message.ToString);
                        blnReturn = false;
                    }
                    else
                    {
                        switch (Configuration.currentLanguage)
                        {
                            case CR_TTLangue.French_France:
                                {
                                    //MessageBox.Show(My.Resources.RessourceFR.palletConfirmed);
                                    break;
                                }

                            default:
                                {
                                    //MessageBox.Show(My.Resources.RessourceEN.palletConfirmed);
                                    break;
                                }
                        }
                        blnReturn = true;
                    }
                }
                else
                    blnReturn = false;
            }
            catch (WebException ex)
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
                blnReturn = false;
            }
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
                blnReturn = false;
            }
            return blnReturn;
        }
    }
}