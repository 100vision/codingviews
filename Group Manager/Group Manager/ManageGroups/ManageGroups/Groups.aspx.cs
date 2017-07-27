using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.Configuration;

namespace ManageGroups
{
    public partial class Groups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ConfigurationHelper confighelper = ConfigurationManager.GetSection("AppInit") as ConfigurationHelper;
                var ldapDomain = confighelper.LDAPDomain;
                var ldapUserName = confighelper.LDAPUserName;
                var ldapPassword = confighelper.LDAPPassword;

                ADAgent adagent = new ADAgent("LDAP://" + ldapDomain, ldapUserName, ldapPassword);
                Session["agentCache"] = adagent;
                string[] longUserName = Context.User.Identity.Name.Split('\\');
                Label2.Text = Context.User.Identity.Name;
                try
                {
                    DirectoryEntry deToQuery = adagent.FindPeopleBySamID(longUserName[1]);
                    if (deToQuery != null)
                    {
                        List<DirectoryEntry> groups = new List<DirectoryEntry>();
                        string groupfilter = "(&(objectCategory=group)(managedBy=" + deToQuery.Properties["distinguishedName"].Value.ToString() + "))";
                        groups = adagent.GetDirectoryEntries(groupfilter);
                        if (null != groups)
                        {
                            foreach (var g in groups)
                            {

                                ListItem li = new ListItem(g.Properties["cn"][0].ToString(), g.Properties["distinguishedName"].Value.ToString());
                                ListBox1.Items.Add(li);

                            }


                        }
                    }
                }

                catch (DirectoryServicesCOMException ex)
                {
                    throw new DirectoryServicesCOMException(ex.Message);
                }
            }
            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (ListBox1.SelectedItem !=null)
            {
                Session["selectedGroupCN"] = ListBox1.SelectedItem.Text;
                Session["selectedGroupDN"] = ListBox1.SelectedValue.ToString();
                Server.Transfer("GroupDetails.aspx");
            }

            
        }
    }
}