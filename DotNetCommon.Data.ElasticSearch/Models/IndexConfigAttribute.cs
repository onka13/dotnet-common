﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCommon.Data.ElasticSearch.Base
{
    public class IndexConfigAttribute : Attribute
    {
        public string Name { get; set; }

        public IndexConfigAttribute(string name)
        {
            Name = name;
        }
    }
}
