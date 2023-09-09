using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mahya.Domain.Models.BaseEntities;
using Mahya.Domain.Models.ProductEntity;

namespace Mahya.Domain.Models.Account
{
    public class UserFavorite:BaseEntity
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public bool IsFavorite { get; set; }

        #region Relation

        public User User { get; set; }
        public Product Product { get; set; }

        #endregion
    }
}
