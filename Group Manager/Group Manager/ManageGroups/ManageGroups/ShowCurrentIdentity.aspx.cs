using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ManageGroups
{
    public partial class ShowCurrentIdentity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string longuserName = Context.User.Identity.Name;
            //string[] splittedtext = longuserName.Split('\\');
            //TextBox1.Text = splittedtext[1];

            if (Context.User.Identity.IsAuthenticated)
            {

                ///test reading configuration file custom sections.
                ConfigurationHelper confighelper = ConfigurationManager.GetSection("AppInit") as ConfigurationHelper;
                TextBox1.Text = confighelper.LDAPDomain;
                Label1.Text = Context.User.Identity.AuthenticationType;
            }

            else
            {
                Response.Redirect("error.aspx");

            }

        }
    }
}