using System;
using System.Collections.Generic;
using Terradue.Search.Engines.Utils;
using Terradue.Search.Model;

namespace Terradue.Search.Engines.Simple
{
    public class PaginationParameters{

        public int PageSize = 20;
        public long StartPage = 1;
        public long StartIndex = 1;

        public static PaginationParameters Default = new PaginationParameters();

        public PaginationParameters(long startIndexParameter, long startPageParameter, int pageSizeParameter)
        {
            StartIndex= startIndexParameter;
            StartPage = startPageParameter;
            PageSize = pageSizeParameter;
        }

        public PaginationParameters()
        {
        }
    }
}