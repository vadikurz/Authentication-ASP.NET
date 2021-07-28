using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Mappers
{
    public interface IMapper<TSource, TDestination>
    {
        public TDestination Map(TSource source);
    }
}
