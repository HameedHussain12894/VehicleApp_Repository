using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Vehicle.Dal.Helper
{
    public class Paginated<T> :List<T>
    {
        public int pageIndex { get; set; }
        public int totalPages { get; set; }

        public Paginated(List<T> items,int count,int _pageIndex,int _pageSize)
        {
            pageIndex=_pageIndex; 
            totalPages=(int)Math.Ceiling(count/(double)_pageSize);
            this.AddRange(items);
        }
        public bool HasPreviousPage => pageIndex > 1;
        public bool HasNextPage => pageIndex < totalPages;

        public static Paginated<T> Create(List<T> source,int pageIndex,int pageSize)
        {
            var count= source.Count;
            var items=source.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return new Paginated<T>(items,count,pageIndex,pageSize);
        }
    }
}
