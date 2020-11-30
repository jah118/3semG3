﻿using DataAccess.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Views.ViewModels
{
    class SearchDriksViewModel
    {
        private DataTable _foodTable;
        private readonly IRepository<FoodDTO> repository;

        public SearchDriksViewModel()
        {
            repository = new FoodRepository();
        }

        private void CreateSearchTable()
        {
            _foodTable = new DataTable();
            _foodTable.Columns.Add("Name", typeof(String));
            _foodTable.Columns.Add("Price", typeof(String));
            _foodTable.Columns.Add("Description", typeof(String));
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

                    var sorted = from FoodDTO in foods.AsEnumerable() where FoodDTO.foodCategoryName.Equals("Drikkevare") select FoodDTO;
                    sorted.ToList<FoodDTO>().ForEach(x =>
                    {
                        DataRow dr = _foodTable.NewRow();
                        _foodTable.Rows.Add(dr.ItemArray = new[] { x.Name, x.Price.ToString("#.##"), x.Description});
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
