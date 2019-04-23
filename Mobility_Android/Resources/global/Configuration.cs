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
        public CR_TTLangue currentLanguage = CR_TTLangue.French_France;

        public String fileConfigLanguage = "@.\ressourceFR.resx";

        public String webServiceURL = "http://clientservices.ceritar.com/MyCeritar_Test/";

        public String termicon_IP = "";

        public String termicon_Port = "";

        // public currentEquipment As EquipementType = EquipementType.Inconnu

        public String securityToken = "";

        public UserInfosWS userInfos = new UserInfosWS();

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
                this.currentLanguage = language;
                this.fileConfigLanguage = "RessourceANG";
            } else {
                this.currentLanguage = language;
                this.fileConfigLanguage = "RessourceFR";
            }
        }

        /*
        public void setItem()
        {
            using (ResXResourceSet fileConfigLanguage = new ResXResourceSet(resxFile))
            {
                resxSet.GetString("app_title");
            }
        }

        private static void CreateResXFile()
        {
            Bitmap logo = new Bitmap(@".\Logo.bmp");
            ResXDataNode node;

            ResXResourceWriter rw = new ResXResourceWriter(@".\StoreResources.resx");
            node = new ResXDataNode("Logo", logo);
            node.Comment = "The corporate logo.";
            rw.AddResource(node);
            rw.AddResource("AppTitle", "Store Locations");
            node = new ResXDataNode("nColumns", 5);
            node.Comment = "The number of columns in the Store Location table";
            rw.AddResource(node);
            rw.AddResource("City", "City");
            rw.AddResource("State", "State");
            rw.AddResource("Code", "Zip Code");
            rw.AddResource("Telephone", "Phone");
            rw.Generate();
            rw.Close();
        }
        */
    }
}