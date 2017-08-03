﻿using System.ComponentModel.DataAnnotations;

namespace V308CMS.Admin.DataAnnotations
{
    public class CheckBirthMonthAttribute : RegularExpressionAttribute
    {
        public CheckBirthMonthAttribute() : base(@"^\d{1,2}$")
        {
            ErrorMessage = "Tháng sinh không đúng.";
        }
    }
}