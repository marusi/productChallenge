using ChamaSubscription.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace ChamaSubscription.Domain.Extensions
{
    public static class IQueryableExtensions
    {

      public static IQueryable<ProductSku> ApplyFiltering(this IQueryable<ProductSku> query, ProductSkuQuery queryObj)
      {
        if (queryObj.ProductCategoryId.HasValue)
          query = query.Where(v => v.SkuValue.OptionValueId == queryObj.ProductCategoryId.Value);

      //  if (queryObj.OptionValueId.HasValue)
         // query = query.Where(v => v.OptionValue.OptionValueId == queryObj.Id.Value);

        return query; 
      }

      public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
      {
        if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
          return query;

        if (queryObj.IsSortAscending)
          return query.OrderBy(columnsMap[queryObj.SortBy]);
        else
          return query.OrderByDescending(columnsMap[queryObj.SortBy]);
      }

      public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj) 
      {
         if (queryObj.Page <= 0)
          queryObj.Page = 1; 
          
         if (queryObj.PageSize <= 0)
          queryObj.PageSize = 10; 

         return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
      }
    }
}