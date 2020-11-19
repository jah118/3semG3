﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccess.Models
{
    [Table("Zip_id")]
    public partial class ZipId
    {
        public ZipId()
        {
            Location = new HashSet<Location>();
        }

        [Key]
        [Column("zip_code")]
        [StringLength(6)]
        public string ZipCode { get; set; }
        [Required]
        [Column("city")]
        [StringLength(50)]
        public string City { get; set; }

        [InverseProperty("ZipCodeNavigation")]
        public virtual ICollection<Location> Location { get; set; }
    }
}