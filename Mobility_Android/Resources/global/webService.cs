using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Mobility_Android.WebService.Security;
using Mobility_Android.Resources.global;


public class WebServiceSecurityManager
{
    private const string urlExtension = "services/security.asmx";

    private static Security webServiceSecurity;

    private static Configuration gcCurrentConfiguration = new Configuration();



    private static bool connectToWebServiceSecurity()
    {
        bool isSuccess = false;
        try
        {
            webServiceSecurity = new Security();
            webServiceSecurity.Url = gcCurrentConfiguration.webServiceURL + urlExtension;
            isSuccess = true;
            Console.WriteLine("YESSSSSSSSSSSSSSSSS");
        }
        catch (UriFormatException ex)
        {
            if (gcCurrentConfiguration.currentLanguage == CR_TTLangue.French_France)
                Console.WriteLine("url incorrect fr");
            else
                Console.WriteLine("url incorrect ang");
            isSuccess = false;
        }
        catch (Exception ex)
        {
            if (gcCurrentConfiguration.currentLanguage == CR_TTLangue.French_France)
                Console.WriteLine("generic incorrect fr");
            else
                Console.WriteLine("generic incorrect ang");
            isSuccess = false;
        }
        return isSuccess;
    }

    public static bool doLogin(string username, string password)
    {
        bool isSuccess = false;

        CR_ResultActionOfCR_WebAuthentification result;

        if (connectToWebServiceSecurity())
        {
            result = webServiceSecurity.authentificateUser(username, password, gcCurrentConfiguration.currentLanguage);

            if (result.Success)
            {
                gcCurrentConfiguration.securityToken = result.ReturnValue.Token;
                isSuccess = true;
                Console.WriteLine("YESSSSSSSSSSSSSSSSS");
                Console.WriteLine("YESSSSSSSSSSSSSSSSS");
                Console.WriteLine("YESSSSSSSSSSSSSSSSS");
                Console.WriteLine("YESSSSSSSSSSSSSSSSS");
                Console.WriteLine("YESSSSSSSSSSSSSSSSS");
                Console.WriteLine("YESSSSSSSSSSSSSSSSS");
                Console.WriteLine("YESSSSSSSSSSSSSSSSS");
            }
            else
            {
                Console.WriteLine("erreur");
                isSuccess = false;
            }
        }
        return isSuccess;
    }
}
