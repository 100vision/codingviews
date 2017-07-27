using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Configuration;

namespace ManageGroups
{
    public class ADAgent
    {
        private string _ldapDN;
        private string _ldapUID;
        private string _ldapPWD;


        public ADAgent(string ldapDN, string ldapUID, string ldapPWD)
        {
            _ldapDN = ldapDN;
            _ldapUID = ldapUID;
            _ldapPWD = ldapPWD;

        }
        public List<DirectoryEntry> GetDirectoryEntries(string ldapfilter)
        {
            List<DirectoryEntry> groups = new List<DirectoryEntry>();
            DirectoryEntry parentEntry = new DirectoryEntry(_ldapDN, _ldapUID, _ldapPWD);
            DirectorySearcher searcher = new DirectorySearcher(parentEntry);
            //searcher.PropertiesToLoad.Add("disguishingedName");
            //searcher.PropertiesToLoad.Add("cn");
            //searcher.PropertiesToLoad.Add("displayName");
            /// searcher.PropertiesToLoad.Add("mail");
            /// searcher.PropertiesToLoad.Add("cn");
            searcher.Filter = ldapfilter;
            searcher.PageSize = 1000;
            searcher.SearchScope = SearchScope.Subtree;
            try
            {
                SearchResultCollection results = searcher.FindAll();
                if (null != results)
                {
                    foreach (SearchResult r in results)
                    {
                        DirectoryEntry group = r.GetDirectoryEntry();
                        groups.Add(group);

                    }
                    return groups;

                }
                else
                {
                    return null;
                }

            }
            catch (DirectoryServicesCOMException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                searcher.Dispose();
                parentEntry.Dispose();

            }



        }
        public void RemoveGroupMember(string GroupDN, string userDN)
        {
            try
            {
                DirectoryEntry groupEntry = new DirectoryEntry("LDAP://" + GroupDN,_ldapUID,_ldapPWD);
                groupEntry.Properties["member"].Remove(userDN);
                groupEntry.CommitChanges();
                groupEntry.Dispose();

            }

            catch (DirectoryServicesCOMException ex)
            { throw new DirectoryServicesCOMException(ex.Message); }




        }
        public void AddGroupMember(string GroupDN, string userDN)
        {
            try
            {
                DirectoryEntry groupEntry = new DirectoryEntry("LDAP://" + GroupDN,_ldapUID,_ldapPWD);
                groupEntry.Properties["member"].Add(userDN);
                groupEntry.CommitChanges();
            }

            catch (DirectoryServicesCOMException ex)
            { throw new DirectoryServicesCOMException(ex.Message); }
        }
        public List<string> GetGroupMembers(string groupDN)
        {
            DirectoryEntry groupEntry = new DirectoryEntry("LDAP://" + groupDN);
            List<string> members = new List<string>();
            try
            {
                if (groupEntry != null)
                {
                    foreach (string m in groupEntry.Properties["member"])
                    {
                        DirectoryEntry memberEntry = new DirectoryEntry("LDAP://" + m);
                        members.Add(memberEntry.Properties["cn"].Value.ToString());
                    }
                    return members;
                }
                else
                { return null; }

            }
            catch (DirectoryServicesCOMException e)
            { throw new DirectoryServicesCOMException(e.Message); }
        }

        /// <summary>
        /// Method to retrieve group members and to return a cn-dn key/value.
        /// </summary>
        /// <param name="groupDN"></param>
        /// <returns>dictonary</returns>
        public Dictionary<string, string> PopulateMembers(string groupDN)
        {

            DirectoryEntry groupEntry = new DirectoryEntry("LDAP://" + groupDN);
            Dictionary<string, string> members = new Dictionary<string, string>();
            try
            {
                if (groupEntry != null)
                {
                    foreach (string m in groupEntry.Properties["member"])
                    {
                        DirectoryEntry memberEntry = new DirectoryEntry("LDAP://" + m);
                        members.Add(memberEntry.Properties["cn"].Value.ToString(), memberEntry.Properties["distinguishedName"].Value.ToString());

                    }
                    return members;
                }
                else
                { return null; }

            }
            catch (DirectoryServicesCOMException e)
            { throw new DirectoryServicesCOMException(e.Message); }
        }


        /// <summary>
        /// Method to find a user object or group by samaccountname
        /// </summary>
        /// <param name="samid">SamAccountName</param>
        /// <returns>Single DirectoryEntry object</returns>
        public DirectoryEntry FindPeopleBySamID(string samid)
        {
            DirectorySearcher searcher = new DirectorySearcher();
            searcher.PageSize = 1000;
            searcher.SearchScope = SearchScope.Subtree;
            searcher.Filter = "(samaccountname=" + samid.Trim() + ")";
            try
            {
                if (searcher != null)
                {
                    SearchResult result = searcher.FindOne();
                    return result.GetDirectoryEntry();

                }
                else
                {
                    return null;
                }
            }

            catch (DirectoryServicesCOMException e)
            { throw new DirectoryServicesCOMException(e.Message); }

            finally
            { searcher.Dispose(); }
        }
        /// <summary>
        /// Method to find a directory entry by distinguishedName
        /// </summary>
        /// <param name="objectDN">DistinguishedName</param>
        /// <returns>DirectoryEntry object</returns>
        public DirectoryEntry FindPeopleByDN(string objectDN)
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://" + objectDN, _ldapUID, _ldapPWD);
            try
            {
                if (null != entry)
                {
                    return entry;

                }

                else
                { return null; }

            }

            catch (DirectoryServicesCOMException ex)
            { throw new DirectoryServicesCOMException(ex.Message); }
        }






    }


}
