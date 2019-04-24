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

namespace Mobility_Android.Resources.global
{
    public class DataTransfer
    {
        public static String parse(params object[] inputs)
        {
            String output = "{";
            foreach (object i in inputs)
            {
                //On rentre dans la fonction pour traiter un objet et ses fils
                output += processObject(i);

                //Si l'objet n'est pas le dernier dans la liste on rajoute une ,
                if (!i.Equals(inputs.Last()))
                {
                    output += ", ";
                }
            }

            //Fin de l'expression
            output += "}";

            return output;
        }

        public static String processObject(object i)
        {
            String output = "";
           
            if (isOfGenericType(i))
            {
                output += i.GetType() + " " + i.GetType().Name + ": " + i.ToString();
            }
            else
            {
                output += i.GetType() + " " + i.GetType().Name + ": ";

                output += "(";
                foreach (var attribute in i.GetType().GetFields())
                {
                    var value = attribute.GetValue(i);
                    if (isOfGenericType(value))
                    {
                        output += attribute + " : " + value;
                    }
                    else
                    {

                        output += processObject(value);

                    }
                    if (!attribute.Equals(i.GetType().GetFields().Last()))
                    {
                        output += ", ";
                    }
                }
                output += ")";

            }
            return output;
        }
        static bool isOfGenericType(object i)
        {
            return i.GetType().ToString().Contains("System.String") ||
                    i.GetType().ToString().Contains("Int32") ||
                    i.GetType().ToString().Contains("Bool") ||
                    i.GetType().ToString().Contains("Double");
        } 
    }
}