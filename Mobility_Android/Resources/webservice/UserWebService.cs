using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mobility_Android.WebService.Security;
using Mobility_Android.Resources.global;


namespace Mobility_Android.Resources.webservice
{
    class UserWebService
    {
        private const string urlExtension = "services/security.asmx";

        private static Security webServiceSecurity;



        private static bool connectToWebServiceSecurity()
        {
            bool isSuccess = false;
            try
            {
                webServiceSecurity = new Security();
                webServiceSecurity.Url = Configuration.webServiceURL + urlExtension;
                isSuccess = true;
            }
            catch (UriFormatException)
            {
                if (Configuration.currentLanguage == CR_TTLangue.French_France)
                    Console.WriteLine("francais");
                else
                    Console.WriteLine("anglais");
                isSuccess = false;
            }
            catch (Exception)
            {
                if (Configuration.currentLanguage == CR_TTLangue.French_France)
                    Console.WriteLine("francais");
                else
                    Console.WriteLine("anglais");
                isSuccess = false;
            }
            return isSuccess;
        }

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
                        GetUserInfos(Configuration.securityToken);
                        isSuccess = true;
                    }
                    else
                    {
                        Console.WriteLine("erreur");
                        isSuccess = false;
                    }
                }
            }
            catch (WebException)
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
            catch (Exception)
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

        public static bool GetUserInfos(string userToken)
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
            catch (WebException)
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
            catch (Exception)
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