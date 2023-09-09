﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Mahya.App.Extenstion
{
    public static class CommonExtentions
    {
        public static string GetEnumName(this System.Enum dataEnum)
        {
            var enumDisplayName = dataEnum.GetType().GetMember(dataEnum.ToString()).FirstOrDefault();

            if(enumDisplayName != null)
            {
                return enumDisplayName.GetCustomAttribute<DisplayAttribute>()?.GetName();
            }

            return "";
        }

    }
}
