﻿using System;
using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public partial class TablesDTO
    {
        public TablesDTO()
        {

        }

        public int Id { get; }
        public int NoOfSeats { get; set; }
        public int TableNumber { get; set; }

       
    }
}