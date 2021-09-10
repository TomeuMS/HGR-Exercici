using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Text.RegularExpressions;

namespace PruebaHGR
{
    class Program
    {
        static void Main(string[] args)
        {
            //Imprimimos el valor
            Console.WriteLine(LoginBusqueda());
        }

        //Función de login
        static string LoginBusqueda()
        {
            //Login a la web
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://app.hustlegotreal.com/Account/Login");

            string values = "Email=testing%40hustlegotreal.com&Password=HGR2021&__RequestVerificationToken=CfDJ8P_T_PlrtHZAh18ySvxm-lMPUxA6-AuR5IhTTpSBrmZxMFjF69Lx3igLsuY77UVzOQRcMLC296kYCGe7wpumdoKrVUSuGjbayAOS5HxMA-x5wcuInkgyibnyRzvOw61tQ2PPA_eHxNDuQkAugenp406LxBaWgMPAzW5Mvyw2qvJjwl8v4dHo-6odqJ5vAhpP1g&RememberMe=false";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = values.Length;
            System.Net.ServicePointManager.Expect100Continue = false; // prevents 417 error
            using (StreamWriter writer = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII)) { writer.Write(values); }


            //PROBLEMA: DEVUELVE BAD REQUEST, ERROR DE PROTOCOLO
            HttpWebResponse c = (HttpWebResponse)req.GetResponse();

            //Recoger Respuesta
            Stream ResponseStream = c.GetResponseStream();
            StreamReader LeerResult = new StreamReader(ResponseStream);
            string Source = LeerResult.ReadToEnd();

            //Busqueda del Valor
            Stream st = c.GetResponseStream();
            StreamReader sr = new StreamReader(st);
            string buffer = sr.ReadToEnd();
            ArrayList uniqueMatches = new ArrayList();
            Regex RE = new Regex("<p class=\"subt\">Subscription allowance</p><p class=\"value\">(.*?)</p>", RegexOptions.Multiline);
            MatchCollection theMatches = RE.Matches(buffer);

            //NO ESTA ACABADO
            return Source;
        }
    }
}
