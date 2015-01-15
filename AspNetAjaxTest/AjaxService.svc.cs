using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace AspNetAjaxTest
{
    [ServiceContract(Namespace = "AspNetAjaxTest")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AjaxService
    {
        private SqlConnection con = new SqlConnection("Data Source=VIRTUALBOX;Initial Catalog=Ast;Persist Security Info=True;User ID=Markus;Password=0773");

        [OperationContract]
        public WebHst[] getHst(int minHstPs, int limit)
        {
            List<WebHst> result = new List<WebHst>();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT top " + limit + " [hstPs], [HstBez] FROM [Hst] WHERE hstPs>" + minHstPs + " order by hstPs", con);
                try
                {
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
                catch (Exception)
                {
                }
            }
            return result.ToArray();
        }

        [OperationContract]
        public string getHstByPs(int hstPs)
        {
            using (con)
            {
                SqlCommand cmd = new SqlCommand("SELECT  [hstPs], [HstBez], [gemeindePs] FROM [Hst] WHERE hstPs=" + hstPs, con);
                try
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        return rdr.GetSqlInt32(0) + ": " + rdr.GetString(1) + " - Gemeinde=" + rdr.GetInt32(2);
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