using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mobility_Android.WebService.Security;

namespace Mobility_Android.Resources.global
{
    /**
     * Class Configuration
     * 
     * Classe qui permet la centralisation de données utilisés dans
     * tout le code, tel que l'URL pour le web Service ou encore la
     * langue utilisée.
     * 
     **/

    class Configuration
    {
        public static CR_TTLangue currentLanguage = CR_TTLangue.French_Canada;

        public static String fileConfigLanguage = "@.\ressourceFR.resx";

        public static String webServiceURL = "";

        public static String termicon_IP = "";

        public static String termicon_Port = "";

        // public currentEquipment As EquipementType = EquipementType.Inconnu

        public static String logoutPassword= "Ceritar1706";

        public static String securityToken = "";

        public static UserInfosWS userInfos = new UserInfosWS();

        /*
         * Methode setLanguage
         * 
         * Méthode permettant de modifier le current language et ainsi
         * changer le fichier de configuration de la langue.
         * 
         */

        public void setLanguage(CR_TTLangue language)
        {
            if (language.Equals(CR_TTLangue.English))
            {
                Configuration.currentLanguage = language;
                Configuration.fileConfigLanguage = "RessourceANG";
            } else {
                Configuration.currentLanguage = language;
                Configuration.fileConfigLanguage = "RessourceFR";
            }
        }
    }
}