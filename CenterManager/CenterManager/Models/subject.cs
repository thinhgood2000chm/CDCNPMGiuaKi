﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CenterManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class subject
    {
        public int id { get; set; }

        [Display(Name = "Mã môn học")]
        public string subject_id { get; set; }

        [Display(Name = "Tên môn học")]
        public string name { get; set; }
    }
}
