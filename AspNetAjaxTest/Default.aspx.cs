using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetAjaxTest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ListBox1.Attributes.Add("onchange", "listItemClicked('" + ListBox1.ID + "');");
            ClientScriptManager scriptManager = Page.ClientScript;
            string scriptCode = "<script type=\"text/javascript\">textInputId='" + TextBoxInput.ClientID + "';</script>";
            scriptManager.RegisterStartupScript(GetType(), "initTextInputId", scriptCode);
            scriptCode = "<script type=\"text/javascript\">textOutputId='" + TextOutput.ClientID + "';</script>";
            scriptManager.RegisterStartupScript(GetType(), "initTextOutputId", scriptCode);
        }
    }
}