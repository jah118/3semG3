﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccess.Models
{
    public partial class Location
    {
        public Location()
        {
            Person = new HashSet<Person>();
        }

        [Key]
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("address")]
        [StringLength(50)]
        public string Address { get; set; }
        [Column("zip_code")]
        [StringLength(6)]
        public string ZipCode { get; set; }

        [ForeignKey(nameof(ZipCode))]
        [InverseProperty(nameof(ZipId.Location))]
        public virtual ZipId ZipCodeNavigation { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<Person> Person { get; set; }
    }
}