using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SeminarUpisi.Models.Validacije
{
    public class SeminarNeUProslostiAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime)
            {
                if (value == null || (DateTime)value < DateTime.Today)
                {
                    return false;
                }
            }
            return true;
        }
    }
}