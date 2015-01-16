using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace AspNetAjaxTest
{
   [ServiceContract(Namespace = "AspNetAjaxTest")]
   [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
   public class AjaxService
   {

      private SqlConnection con = new SqlConnection("Data Source=tfs2010\\sqlexpress;Initial Catalog=EWFAst;Persist Security Info=True;User ID=Ansat;Password=ansatuser");

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