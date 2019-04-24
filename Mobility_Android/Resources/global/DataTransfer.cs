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
        //Parse un objet et ses enfants
        public static String processObject(object i)
        {
            String output = "";
            //Si l'objet est un type générique (int, string...) on le traite différemment
            if (isOfGenericType(i))
            {
                output += i.GetType() + " " + i.GetType().Name + ": " + i.ToString();
            }
            else
            {
                output += i.GetType() + " " + i.GetType().Name + ": ";

                output += "(";
                //Pour chaque attributs PUBLICS dans un objet :
                foreach (var attribute in i.GetType().GetFields())
                {
                    var value = attribute.GetValue(i);
                    if (isOfGenericType(value))
                    {
                        output += attribute + " : " + value;
                    }
                    else
                    {
                        //On réexecute la fonction si c'est l'enfant est un objet non générique
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