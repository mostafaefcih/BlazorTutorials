using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models.Wrappers
{
    public class PagedResponse<T>:Response<T>
    {
        public PagedResponse(T data,int TotalRecords, int pageNumber, int pageSize)

        {
            this.TotalRecords = TotalRecords;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }

        }
        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
             
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
       
    }
}
