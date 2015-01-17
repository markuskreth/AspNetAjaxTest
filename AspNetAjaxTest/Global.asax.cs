using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AspNetAjaxTest
{
    public class Global : System.Web.HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            // Code, der beim Starten der Anwendung ausgeführt wird.
        }

        private void Application_End(object sender, EventArgs e)
        {
            //  Code, der beim Herunterfahren der Anwendung ausgeführt wird.
        }

        private void Application_Error(object sender, EventArgs e)
        {
            // Code, der bei einem nicht behandelten Fehler ausgeführt wird.
        }

        private void Session_Start(object sender, EventArgs e)
        {
            // Code, der beim Starten einer neuen Sitzung ausgeführt wird.
        }

        private void Session_End(object sender, EventArgs e)
        {
            // Code, der am Ende einer Sitzung ausgeführt wird.
            // Hinweis: Das Session_End-Ereignis wird nur ausgelöst, wenn der sessionstate-Modus
            // in der Datei "Web.config" auf InProc festgelegt wird. Wenn der Sitzungsmodus auf StateServer festgelegt wird
            // oder auf SQLServer, wird das Ereignis nicht ausgelöst.
        }
    }
}