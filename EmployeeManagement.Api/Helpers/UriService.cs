using EmployeeManagement.Api.Models.Filter;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Helpers
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
