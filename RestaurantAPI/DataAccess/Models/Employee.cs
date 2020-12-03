﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccess.Models
{
    public partial class Employee
    {
        public Employee()
        {
            RestaurantOrder = new HashSet<RestaurantOrder>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("person_id")]
        public int? PersonId { get; set; }
        [Column("title_id")]
        public int TitleId { get; set; }
        [Column("salary", TypeName = "decimal(18, 0)")]
        public decimal? Salary { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("Employee")]
        public virtual Person Person { get; set; }
        [ForeignKey(nameof(TitleId))]
        [InverseProperty(nameof(EmployeeTitle.Employee))]
        public virtual EmployeeTitle Title { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<RestaurantOrder> RestaurantOrder { get; set; }
    }
}