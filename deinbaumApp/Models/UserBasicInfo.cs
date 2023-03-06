using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.Models
{
    /// <summary>
    /// Model um die Informationen des eingeloggten Users festzuhalten
    /// </summary>
    public class UserBasicInfo
    {
        public string Login { get; set; } = string.Empty;
        public bool IstAdminBerechtigt { get; set; } = false;
        public string Token { get; set; } = string.Empty;

        public string RoleText
        {
            get 
            {
                if (IstAdminBerechtigt)
                {
                    return "Admin";
                }
                return string.Empty;
            }
        }
    }

    public enum RoleDetails
    {
        Normal = 1,
        Admin,
    }
}
