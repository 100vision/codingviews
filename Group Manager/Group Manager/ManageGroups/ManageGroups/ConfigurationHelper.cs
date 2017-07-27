using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace ManageGroups
{
    public class ConfigurationHelper:ConfigurationSection
    {
        [ConfigurationProperty("LDAPDomain")]
        public string LDAPDomain
        { get { return this["LDAPDomain"].ToString(); }
          set { this["LDAPDomain"] = value; }
        }

        [ConfigurationProperty("LDAPUserName")]
        public string LDAPUserName
        {
            get { return this["LDAPUserName"].ToString(); }
            set { this["LDAPUserName"] = value; }
        }

        [ConfigurationProperty("LDAPPassword")]
        public string LDAPPassword
        {
            get { return this["LDAPPassword"].ToString(); }
            set { this["LDAPPassword"] = value; }
        }


    }
}