﻿
using Mahya.Domain.Models.Account;
using mahya.Domain.Models.BaseEntities;

namespace Shop.Domain.Models.Account
{
    public class UserRole:BaseEntity
    {
        #region properties
        public long UserId { get; set; }
        public long RoleId { get; set; }
        #endregion

        #region relations
        public User User { get; set; }
        public Role Role { get; set; }
        #endregion
    }
}
