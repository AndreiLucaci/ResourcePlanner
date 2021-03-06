﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using TestScheduler.ViewModels;

namespace TestScheduler.Converters
{
    public class ExpandoToUserConverter : IOneWayConverter<ExpandoObject, UserViewModel>
    {
        public UserViewModel Convert(ExpandoObject input)
        {
            var dic = input as IDictionary<string, object>;
            var res = new UserViewModel
            {
                Id = System.Convert.ToInt32(dic["Id"]),
                Color = dic.ContainsKey("Color") ? System.Convert.ToString(dic["Color"]) : "#FFF",
                Name = dic.ContainsKey("Name") ? System.Convert.ToString(dic["Name"]) : string.Empty,

            };

            if (dic.ContainsKey("ParentId"))
            {
                res.ParentId = System.Convert.ToInt32(dic["ParentId"]);
            }

            if (dic.ContainsKey("IsVisible"))
            {
                res.IsVisible = System.Convert.ToBoolean(dic["IsVisible"]);
            }

            if (dic.ContainsKey("Department"))
            {
                res.Department = System.Convert.ToString(dic["Department"]);
                res.ParentId = (int)Enum.Parse(typeof(Department), res.Department);
            }

            return res;
        }
    }
}
