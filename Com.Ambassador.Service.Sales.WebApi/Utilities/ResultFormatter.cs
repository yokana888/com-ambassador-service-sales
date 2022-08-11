using AutoMapper;
using Com.Ambassador.Service.Sales.Lib.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Com.Ambassador.Service.Sales.WebApi.Utilities
{
    public class ResultFormatter
    {
        public Dictionary<string, object> Result { get; set; }

        public ResultFormatter(string ApiVersion, int StatusCode, string Message)
        {
            Result = new Dictionary<string, object>();
            AddResponseInformation(Result, ApiVersion, StatusCode, Message);
        }

        public Dictionary<string, object> Ok()
        {
            return Result;
        }

        public Dictionary<string, object> Ok<TViewModel>(TViewModel Data)
        {
            Result.Add("data", Data);
            return Result;
        }

        public Dictionary<string, object> Ok<TViewModel>(TViewModel Data, int TotalData)
        {
            Dictionary<string, object> Info = new Dictionary<string, object>
            {
                { "total", TotalData },
            };
            Result.Add("info", Info);
            Result.Add("data", Data);
            return Result;
        }

        public Dictionary<string, object> Ok<TViewModel>(IMapper mapper, List<TViewModel> Data, int Page, int Size, int TotalData, int TotalPageData, Dictionary<string, string> Order, List<string> Select)
        {
            Dictionary<string, object> Info = new Dictionary<string, object>
            {
                { "count", TotalPageData },
                { "page", Page },
                { "size", Size },
                { "total", TotalData },
                { "order", Order }
            };

            if (Select.Count > 0)
            {
                var DataObj = Data.AsQueryable().Select(string.Concat("new(", string.Join(",", Select), ")"));
                Result.Add("data", DataObj);
                Info.Add("select", Select);
            }
            else
            {
                Result.Add("data", Data);
            }

            Result.Add("info", Info);

            return Result;
        }

        public Dictionary<string, object> Fail(string Error)
        {
            Result.Add("error", Error);
            return Result;
        }

        public Dictionary<string, object> Fail()
        {
            Result.Add("error", "Request Failed");
            return Result;
        }

        public Dictionary<string, object> Fail(ServiceValidationException e)
        {
            Dictionary<string, object> Errors = new Dictionary<string, object>();

            foreach (ValidationResult error in e.ValidationResults)
            {
                string key = error.MemberNames.First();

                try
                {
                    Errors.Add(error.MemberNames.First(), JsonConvert.DeserializeObject(error.ErrorMessage));
                }
                catch (Exception)
                {
                    Errors.Add(error.MemberNames.First(), error.ErrorMessage);
                }
            }

            Result.Add("error", Errors);
            return Result;
        }

        public void AddResponseInformation(Dictionary<string, object> Result, string ApiVersion, int StatusCode, string Message)
        {
            Result.Add("apiVersion", ApiVersion);
            Result.Add("statusCode", StatusCode);
            Result.Add("message", Message);
        }
    }
}
