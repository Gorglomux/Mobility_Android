using System;
using System.Collections.Generic;
using System.Net;

using Mobility_Android.WebService.Security;
using Mobility_Android.Resources.global;


namespace Mobility_Android.Resources.webservice
{
    /**
     * Classe qui gére les méthodes du web service concernant la partie utilisateur 
     *  Méthodes :
     *      connectToWebServiceSecurity
     *      doLogin
     *      GetUserInfos
     *      SaveUserInfos
     *      GetListWarehouses
     *      GetListCompanies
     *      UpdateUserWarehouse
     * 
     **/
    class SecurityWebService
    {
        private const string urlExtension = "services/security.asmx";

        private static Security webServiceSecurity;


        /*
         * Méthode de connexion au web service Security pour pouvoir accèder aux méthodes
         * 
         */
        private static bool connectToWebServiceSecurity()
        {
            bool isSuccess = false;
            try
            {
                webServiceSecurity = new Security();
                webServiceSecurity.Url = Configuration.webServiceURL + urlExtension;
                isSuccess = true;
            }
            catch (UriFormatException ex)
            {
                if (Configuration.currentLanguage == CR_TTLangue.French_France)
                    Console.WriteLine("francais");
                else
                    Console.WriteLine("anglais");
                isSuccess = false;
            }
            catch (Exception ex)
            {
                if (Configuration.currentLanguage == CR_TTLangue.French_France)
                    Console.WriteLine("francais");
                else
                    Console.WriteLine("anglais");
                isSuccess = false;
            }
            return isSuccess;
        }


        /*
         * Méthode pour se connecter à un user 
         * 
         */
        public static bool doLogin(string username, string password)
        {
            bool isSuccess = false;
            try
            {
                CR_ResultActionOfCR_WebAuthentification result;

                if (connectToWebServiceSecurity())
                {
                    result = webServiceSecurity.authentificateUser(username, password, Configuration.currentLanguage);

                    if (result.Success)
                    {
                        Configuration.securityToken = result.ReturnValue.Token;
                        getUserInfos(Configuration.securityToken);
                        isSuccess = true;
                    }
                    else
                    {
                        Console.WriteLine("erreur");
                        isSuccess = false;
                    }
                }
            }
            catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            Console.WriteLine("francais");
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("anglais");
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
                            Console.WriteLine("francais");
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("anglais");
                            break;
                        }
                }
            }

            return isSuccess;
        }


        /*
         * Méthode pour récupérer les informations d'un user 
         * 
         */
        public static bool getUserInfos(string userToken)
        {
            bool isSuccess = false;
            try
            {
                CR_ResultActionOfUserInfosWS result;

                if (connectToWebServiceSecurity())
                {
                    result = webServiceSecurity.GetUserInfos(userToken);

                    if (result.Success)
                    {
                        Configuration.userInfos = result.ReturnValue;
                        isSuccess = true;
                    }
                    else
                    {
                        //
                        isSuccess = true;
                    }
                }
            }
            catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //
                            break;
                        }

                    default:
                        {
                            //
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
                            //
                            break;
                        }

                    default:
                        {
                            //
                            break;
                        }
                }
            }

            return isSuccess;
        }


        /*
         * Méthode pour sauvegarder les informations d'un user
         * 
         */
        public static bool saveUserInfos(string userToken, UserInfosWS userInfo)
        {
            bool isSuccess = false;
            try
            {
                CR_ResultActionOfBoolean result;

                if (connectToWebServiceSecurity())
                {
                    result = webServiceSecurity.SaveUserInfos(userToken, userInfo);

                    if (result.Success)
                        isSuccess = true;
                    else
                    {
                        //
                        isSuccess = false;
                    }
                }
            }
            catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //
                            break;
                        }

                    default:
                        {
                            //
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
                            //
                            break;
                        }

                    default:
                        {
                            //
                            break;
                        }
                }
            }

            return isSuccess;
        }


        /*
         * Méthode qui permet de récupérer la liste des entrepots d'un user
         * 
         */
        public static Dictionary<int, string> getListWarehouses(string userToken, int[] warehousesToExlude = null)
        {
            Dictionary<int, string> warehouses = new Dictionary<int, string>();
            try
            {
                CR_ResultActionOfListOfWarehouseObject result;

                if (connectToWebServiceSecurity())
                {
                    if (warehousesToExlude == null)
                        warehousesToExlude = new int[] { };
                    result = webServiceSecurity.GetListWarehouses(userToken, warehousesToExlude);

                    if (result.Success)
                    {
                        foreach (WarehouseObject warehouse in result.ReturnValue)
                            warehouses.Add(warehouse.Id, warehouse.Code);
                    }
                    else
                    {
                        //
                    }
                }
            }
            catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //
                            break;
                        }

                    default:
                        {
                            //
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
                            //
                            break;
                        }

                    default:
                        {
                            //
                            break;
                        }
                }
            }

            return warehouses;
        }


        /*
         * Méthode qui permet de récupérer la liste des compagnies d'un user
         * 
         */
        public static Dictionary<int, string> getListCompanies(string userToken, int companyType)
        {
            Dictionary<int, string> companies = new Dictionary<int, string>();
            try
            {
                CR_ResultActionOfListOfCompanyObjectWS result;

                if (connectToWebServiceSecurity())
                {
                    result = webServiceSecurity.GetListCompanies(userToken, companyType);

                    if (result.Success)
                    {
                        foreach (CompanyObjectWS company in result.ReturnValue)
                            companies.Add(company.NRI, company.Code);
                    }
                    else
                    {
                        //
                    }
                }
            }
            catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //
                            break;
                        }

                    default:
                        {
                            //
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
                            //
                            break;
                        }

                    default:
                        {
                            //
                            break;
                        }
                }
            }

            return companies;
        }

        /*
         * Méthode qui permet de mettre à jour la liste des entrepots disponible pour un user
         * 
         */
        public static bool updateUserWarehouse(string userToken, int newWarehouseNRI)
        {
            bool isSuccess = false;
            try
            {
                CR_ResultActionOfBoolean result;

                if (connectToWebServiceSecurity())
                {
                    result = webServiceSecurity.UpdateUserWarehouse(userToken, newWarehouseNRI);

                    isSuccess = result.Success;

                    if (!result.Success)
                    {
                        //
                    }
                }
            }
            catch (WebException ex)
            {
                switch (Configuration.currentLanguage)
                {
                    case CR_TTLangue.French_France:
                        {
                            //
                            break;
                        }

                    default:
                        {
                            //
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
                            //
                            break;
                        }

                    default:
                        {
                            //
                            break;
                        }
                }
            }

            return isSuccess;
        }
    }
}