using System;

namespace DotNetCommon.Data.ElasticSearch.Base;

public class IndexConfigAttribute : Attribute
{
    public IndexConfigAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}
