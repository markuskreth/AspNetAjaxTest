using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;

namespace AspNetAjaxTest
{
    [ServiceContract(Namespace = "AspNetAjaxTest")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AjaxService
    {
        private SqlConnection con = new SqlConnection("Data Source=VIRTUALBOX;Initial Catalog=Ast;Persist Security Info=True;User ID=markus;Password=0773");
        private const string nominatimParams = "&format=json&countrycodes=de&polygon=1&addressdetails=1";

        [OperationContract]
        public String queryNoatim(String request)
        {
            string uri = "http://nominatim.openstreetmap.org/search.php?q=" + request + nominatimParams;
            string response = "from ajax";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.UserAgent = HttpContext.Current.Request.UserAgent;
            req.Method = "POST";
            WebResponse resp = req.GetResponse();
            StreamReader reader = new StreamReader(resp.GetResponseStream());
            response = reader.ReadToEnd();
            reader.Close();
            return response;
        }

        [OperationContract]
        public WebHst[] getHst(int minHstPs, int limit)
        {
            List<WebHst> result = new List<WebHst>();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT top " + limit + " [hstPs], [HstBez] FROM [Hst] WHERE hstPs>" + minHstPs + " order by hstPs", con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    WebHst hst = new WebHst();
                    hst.HstPs = rdr.GetInt32(0);
                    hst.HstBez = rdr.GetString(1);
                    result.Add(hst);
                }
            }
            return result.ToArray();
        }

        [OperationContract]
        public string getHstByPs(int hstPs)
        {
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT  [hstPs], [HstBez], [HstBreite], [HstLaenge] FROM [Hst] WHERE hstPs=" + hstPs, con);
                try
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        string hstBez = rdr.GetString(1);
                        double breite = rdr.GetDouble(2);
                        double laenge = rdr.GetDouble(3);
                        return rdr.GetInt32(0) + ": " + hstBez + " - Koord=[" + breite.ToString("0.0#######################", CultureInfo.InvariantCulture) + "," + laenge.ToString("0.0#######################", CultureInfo.InvariantCulture) + "]";
                    }
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return hstPs + ": Eine Haltestelle";
        }
    }

    public class WebHst
    {
        public int HstPs { get; set; }

        public string HstBez { get; set; }
    }
}