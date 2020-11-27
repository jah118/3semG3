using System;
using System.Collections.Generic;

namespace DataTransferObjects
{
    public class FoodCategoryDTO
    {
        public int Id { get; }
        public string Name { get; }

        public FoodCategoryDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
         
    }
    
    
 
 
}
