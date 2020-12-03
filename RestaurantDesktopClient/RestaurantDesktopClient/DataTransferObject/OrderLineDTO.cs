﻿using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RestaurantDesktopClient.DataTransferObject
{
    class OrderLineDTO : INotifyPropertyChanged
    {
        private int _quantity = 0;
        [JsonProperty("quantity")]
        public int Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged("Quantity"); } }
        [JsonProperty("food")]
        public FoodDTO Food { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
