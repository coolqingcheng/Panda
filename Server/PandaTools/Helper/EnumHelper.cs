﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaTools.Helper
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum obj)
        {
            if (obj == null)
                return "";

            var type = obj.GetType();
            //获取到:Admin
            var enumName = Enum.GetName(type, obj);
            var field = type.GetField(enumName);
            //获取到:0
            var val = (int)field.GetValue(null);
            var atts = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (atts != null && atts.Length > 0)
            {
                //获取到：超级管理员
                var att = ((DescriptionAttribute[])atts)[0];
                var des = att.Description;
                return des;
            }
            return "-";
        }
    }
}