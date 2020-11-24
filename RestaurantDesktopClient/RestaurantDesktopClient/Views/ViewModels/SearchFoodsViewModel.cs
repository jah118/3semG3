﻿using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Services.OrderService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Views.ViewModels
{
    class SearchFoodsViewModel
    {
        private DataTable _foodTable;
        private readonly IRepository<FoodDTO> repository;

        public SearchFoodsViewModel(IRepository<FoodDTO> rep)
        {
            repository = rep;
        }

        private void CreateSearchTable()
        {
            _foodTable = new DataTable();
            _foodTable.Columns.Add("Name", typeof(String));
            _foodTable.Columns.Add("Description", typeof(String));
            _foodTable.Columns.Add("Price", typeof(String));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public DataTable SearchTable
        {
            get
            {
                IEnumerable<FoodDTO> foods = repository.GetAll();
                if (foods != null)
                {
                    if (_foodTable == null) CreateSearchTable();

                    var sorted = from FoodDTO in foods.AsEnumerable() where FoodDTO.FoodCategory.Equals("Food") select FoodDTO;
                    sorted.ToList<FoodDTO>().ForEach(x =>
                    {
                        DataRow dr = _foodTable.NewRow();
                        _foodTable.Rows.Add(dr.ItemArray = new[] { x.Name, x.Description, x.Price });
                    });
                }
                return _foodTable;
            }
            set
            {
                _foodTable = value;
                PropertyChanged(this, new PropertyChangedEventArgs("_foodTable"));
            }
        }


    }
}

