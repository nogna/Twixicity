using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Toxicity.Models
{
    
    public static class ConnectionStringHelper
    {
        /// <summary>
        /// Finds the right connectionstring with the identifier as name.
        /// </summary>
        /// <param name="name">The name of the connectionstring</param>
        /// <returns>The connectionstring</returns>
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}