using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;

namespace ManageGroups
{
    public partial class AddMember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

                }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ADAgent activeAgent = (ADAgent)Session["agentCache"];
            if (TextBox1.Text != "")
            {
                var ldapfilter = "(&(|(sAMAccountType=805306368)(objectCategory=group))(SAMAccountName=*" + TextBox1.Text.Trim() + "*))";
                List<DirectoryEntry> entrySearchResults = activeAgent.GetDirectoryEntries(ldapfilter);
                try
                {
                    if (entrySearchResults != null)
                    {
                        foreach (var r in entrySearchResults)
                        {
                            ListBox1.Items.Add(new ListItem(r.Properties["cn"][0].ToString(), r.Properties["distinguishedName"].Value.ToString()));
                        }


                    }

                    else
                    {
                        ListBox1.Items.Clear();
                        
                        Label1.Text = "no match found!"; }
                }

                catch (DirectoryServicesCOMException ex)
                {
                    throw new DirectoryServicesCOMException(ex.Message);
                }
            }
                   


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem != null)
            {
                ADAgent activeAgent = (ADAgent)Session["agentCache"];
                string groupdn = (string)Session["selectedGroupDN"];
                activeAgent.AddGroupMember(groupdn, ListBox1.SelectedValue.ToString());
            }

            else
            { Label1.Text = "No user/group object selected!"; }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Server.Transfer("GroupDetails.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            ListBox1.Items.Clear();
            Label1.Text = "";
        }
                  

    }
}