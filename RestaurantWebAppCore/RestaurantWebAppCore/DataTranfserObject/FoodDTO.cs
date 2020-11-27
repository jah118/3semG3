﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class FoodDTO
    {
        public FoodDTO()
        {

        }
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public FoodCategoryDTO FoodCategoryName { get;  }
        public decimal Price { get; set; }
    }
}