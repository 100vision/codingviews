using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;

namespace ManageGroups
{
    public partial class GroupDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string selectedGroupCN = (string)Session["selectedGroupCN"];
                String selectedGroupDN = (string)Session["selectedGroupDN"];
                ADAgent activeADAgent = (ADAgent)Session["agentCache"];

                Label1.Text = selectedGroupCN;
                //ListBox1.Items.Clear();
                DirectoryEntry entry = activeADAgent.FindPeopleByDN(selectedGroupDN);

                if (entry != null)
                {
                    Dictionary<string, string> members = activeADAgent.PopulateMembers(selectedGroupDN);
                    foreach (string s in members.Keys)
                    {
                        ListBox1.Items.Add(new ListItem(s, members[s].ToString()));

                    }

                }
            }


            }


        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ListBox1.Items.Count > 0)
            {

                string groupdn = (string)Session["selectedGroupDN"];
                ADAgent activeADAgent = (ADAgent)Session["agentCache"];
                activeADAgent.RemoveGroupMember(groupdn, ListBox1.SelectedValue.ToString());
                ListBox1.Items.Remove(ListBox1.SelectedItem);
            }






            
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Server.Transfer("AddMember.aspx");
        }
    }
}