using System;

namespace DotNetCommon.Data.MongoDBBase.Base;

public class CollectionAttribute : Attribute
{
    public CollectionAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}
