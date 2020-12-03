﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccess.Models
{
    public partial class PaymentCondition
    {
        public PaymentCondition()
        {
            RestaurantOrder = new HashSet<RestaurantOrder>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("condition")]
        [StringLength(20)]
        public string Condition { get; set; }

        [InverseProperty("PaymentCondition")]
        public virtual ICollection<RestaurantOrder> RestaurantOrder { get; set; }
    }
}