using InputKit.Shared.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.Validations
{
    /// <summary>
    /// Methode um UI-Uranium Validation mit Double Werten zu erweitern
    /// Meldet mit einem roten Text, dass Eingabe des Users kein Double Wert ist
    /// </summary>
    public class DoubleValidation : IValidation
    {
        public string Message { get; set; } = "Keine gültige Zahl";

        /// <summary>
        /// Ueberprueft ob Wert eine Zahl (double) ist
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Validate(object value)
        {
            if (value is string text)
            {
                double dummy;
                var val = ((string)value).Replace(',', '.');
                var res = Double.TryParse(val, out dummy);
                return res;
            }
            return false;
        }
    }
}
