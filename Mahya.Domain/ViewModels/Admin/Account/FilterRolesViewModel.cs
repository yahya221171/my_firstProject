﻿using System.Collections.Generic;
using Mahya.Domain.Models.Account;
using Mahya.Domain.ViewModels.Paging;

namespace Mahya.Domain.ViewModels.Admin.Account
{
    public class FilterRolesViewModel:BasePaging
    {
        public string RoleName { get; set; }
        public List<Role> Roles { get; set; }


        #region methods
        public FilterRolesViewModel SetRoles(List<Role> roles)
        {
            this.Roles = roles;
            return this;
        }

        public FilterRolesViewModel SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntityCount = paging.AllEntityCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.CountForShowAfterAndBefor = paging.CountForShowAfterAndBefor;
            this.SkipEntitiy = paging.SkipEntitiy;
            this.PageCount = paging.PageCount;

            return this;
        }

        #endregion
    }
}
