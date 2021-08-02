using System;
using System.Collections.Generic;
using System.Linq;


namespace WebApplication.Utils.Extensions
{
    public static class Enumerables  
    {
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> collection) where T: class => collection.Where(user => user is not null);

        public static void ForEach<T>(this IEnumerable<T> collection , Action<T> action) => collection.ToList().ForEach(action);
    }
}
