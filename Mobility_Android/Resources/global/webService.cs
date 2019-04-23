/*
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
using Mobility_Android.svr_net15_temp;


public class WebServiceSecurityManager
{
    private const string urlExtension = "services/security.asmx";

    private static Mobility_Android.svr_net15_temp.Security webServiceSecurity;



    private static bool connectToWebServiceSecurity()
    {
        bool isSuccess = false;
        try
        {
            webServiceSecurity = new Mobility_Android.svr_net15_temp.Security();
            webServiceSecurity.Url = gcCurrentConfiguration.webServiceURL + urlExtension;
            isSuccess = true;
        }
        catch (UriFormatException ex)
        {
            if (gcCurrentConfiguration.currentLanguage == Ceritar.Common.CR_TTLangue.French)
                MessageBox.Show(My.Resources.RessourceFR.errWebServiceUrlIncorrect);
            else
                MessageBox.Show(My.Resources.RessourceEN.errWebServiceUrlIncorrect);
            isSuccess = false;
        }
        catch (Exception ex)
        {
            if (gcCurrentConfiguration.currentLanguage == Ceritar.Common.CR_TTLangue.French)
                MessageBox.Show(My.Resources.RessourceFR.errWebserviceGeneric);
            else
                MessageBox.Show(My.Resources.RessourceEN.errWebserviceGeneric);
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
            result = webServiceSecurity.authentificateUser(username, password, System.Convert.ToInt32(gcCurrentConfiguration.currentLanguage));

            if (result.Success)
            {
                gcCurrentConfiguration.securityToken = result.ReturnValue.Token;
                isSuccess = true;
            }
            else
            {
                MessageBox.Show(result.Errors(0).Message.ToString);
                isSuccess = false;
            }
        }
        return isSuccess;
    }
}
*/