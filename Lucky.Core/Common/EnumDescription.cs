﻿/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Common
*文件名： EnumDescription
*创建人： Lxsh
*创建时间：2019/1/11 9:19:12
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/11 9:19:12
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.Common
{
    public static class EnumDescription
    {
        /// <summary>
        /// 得到Flags特性的枚举的集合
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<Enum> GetEnumValuesFromFlagsEnum(this Enum value)
        {
            List<Enum> values = Enum.GetValues(value.GetType()).Cast<Enum>().ToList();
            List<Enum> res = new List<Enum>();
            foreach (var itemValue in values)
            {
                if (value.GetHashCode() >= itemValue.GetHashCode())//防止一些左而数小，后面数大的情况，严格规定左而有大数，右面为小数
                    if ((value.GetHashCode() & itemValue.GetHashCode()) > 0
                       || (value.GetHashCode() == 0 && itemValue.GetHashCode() == 0))//输出为0的枚举元素
                        res.Add(itemValue);
            }
            return res;
        }

        /// <summary>  
        /// 获取枚举变量值的 Description 属性  
        /// </summary>  
        /// <param name="obj">枚举变量</param>  
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns>  
        public static string GetDescription(this Enum obj)
        {
            string description = string.Empty;
            try
            {
                Type _enumType = obj.GetType();
                DescriptionAttribute dna = null;
                FieldInfo fi = null;
                var fields = _enumType.GetCustomAttributesData();

                if (!fields.Where(i => i.Constructor.DeclaringType.Name == "FlagsAttribute").Any())
                {
                    fi = _enumType.GetField(Enum.GetName(_enumType, obj));
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                    if (dna != null && !string.IsNullOrEmpty(dna.Description))
                        return dna.Description;
                    return null;
                }

                GetEnumValuesFromFlagsEnum(obj).ToList().ForEach(i =>
                {
                    fi = _enumType.GetField(Enum.GetName(_enumType, i));
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                    if (dna != null && !string.IsNullOrEmpty(dna.Description))
                        description += dna.Description + ",";
                });

                return description.EndsWith(",")
                    ? description.Remove(description.LastIndexOf(','))
                    : description;
            }
            catch
            {
                throw;
            }

        }
    }
}