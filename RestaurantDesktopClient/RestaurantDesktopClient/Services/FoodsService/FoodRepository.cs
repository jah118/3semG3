﻿using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Configuration;
using RestaurantDesktopClient.Services;
using System.Net;

namespace RestaurantDesktopClient.Views.ViewModels
{
    internal class FoodRepository : IRepository<FoodDTO>
    {
        private readonly string _constring;
        private readonly IAuthRepository _authRepository;

        public FoodRepository(string constring, IAuthRepository authRepository)
        {
            this._constring = constring;
            _authRepository = authRepository;
        }

        public FoodDTO Create(FoodDTO t)
        {
            throw new System.NotImplementedException();
        }

        public HttpStatusCode Delete(FoodDTO t)
        {
            throw new System.NotImplementedException();
        }

        public FoodDTO Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<FoodDTO> GetAll()
        {
            try
            {
                var client = new RestClient(_constring);
                var request = new RestRequest("/Food", Method.GET);
                var content = client.Execute(request).Content;
                return JsonConvert.DeserializeObject<List<FoodDTO>>(content);
            }
            catch
            {
                return new List<FoodDTO>();
            }
        }

        public FoodDTO Update(FoodDTO t)
        {
            throw new System.NotImplementedException();
        }
    }
}