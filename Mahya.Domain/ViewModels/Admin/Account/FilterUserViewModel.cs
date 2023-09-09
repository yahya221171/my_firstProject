using System.Collections.Generic;
using Mahya.Domain.Models.Account;
using Mahya.Domain.ViewModels.Paging;

namespace Mahya.Domain.ViewModels.Admin.Account
{

    public class FilterUserViewModel:BasePaging
    {
        public string Email { get; set; }
        public List<User> Users { get; set; }

        #region methods
        public FilterUserViewModel SetUsers(List<User> users)
        {
            this.Users = users;
            return this;
        }

        public FilterUserViewModel SetPaging(BasePaging paging)
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
