﻿using System.ComponentModel.DataAnnotations;

namespace V308CMS.DataAnnotations
{
    public class CheckBirthYearAttribute : RegularExpressionAttribute
    {
        public CheckBirthYearAttribute() : base(@"^\d{4}$")
        {
            ErrorMessage = "Năm sinh không hợp lệ.";
        }
    }
}