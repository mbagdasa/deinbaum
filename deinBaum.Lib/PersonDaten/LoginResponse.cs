using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.Lib.PersonDaten
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;

        public bool IstAdminBerechtigt { get; set; } = false;
    }
}
