﻿using System.Collections.Generic;

namespace DotNetCommon.Data.Domain.Models
{
    public class SearchResult
    {
        public List<object> Items { get; set; }

        public long Total { get; set; }
    }
}
