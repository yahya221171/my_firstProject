﻿using Mahya.Domain.Models.Account;

namespace Mahya.App.Extenstion
{
    public static class UserExtentions
    {
        public static string GetUserName(this User user)
        {
            //if(!string.IsNullOrWhiteSpace(user.FirstName) && !string.IsNullOrWhiteSpace(user.LastName))
            //{
            //    return $"{user.FirstName} {user.LastName}";
            //}
            //return user.PhoneNumber;
            return $"{user.FirstName} {user.LastName}";
        }
    }
}
