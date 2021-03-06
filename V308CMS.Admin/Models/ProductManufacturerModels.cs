﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace V308CMS.Admin.Models
{
    public class ProductManufacturerModels:BaseModels
    {
        public ProductManufacturerModels()
        {
            CreatedDate = DateTime.Now;
            Status = true;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên nhà sản xuất không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên nhà sản xuất không được vượt quá 100 ký tự.")]
        [Display(Name="Tên")]
        public string Name { get; set; }
        
        [Display(Name = "Mô tả")]
        public string Detail { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
        [Display(Name = "Thứ tự")]
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
        [StringLength(100, ErrorMessage = "Link ảnh không được vượt quá 250 ký tự.")]
        public string ImageUrl { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}