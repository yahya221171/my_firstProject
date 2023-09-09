﻿using System.Linq;

namespace Mahya.Domain.ViewModels.Paging
{
    public static class PagingExtentions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> query,BasePaging basePaging)
        {
            return query.Skip(basePaging.SkipEntitiy).Take(basePaging.TakeEntity);
        }
    }
}
