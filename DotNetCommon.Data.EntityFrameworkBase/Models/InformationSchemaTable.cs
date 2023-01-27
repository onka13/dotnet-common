using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCommon.Data.EntityFrameworkBase.Models;

public class InformationSchemaTable
{
    [Column("TABLE_NAME")]
    public string TableName { get; set; }

    [Column("TABLE_SCHEMA")]
    public string TableSchema { get; set; }

    [Column("TABLE_TYPE")]
    public string TableType { get; set; }

    public override string ToString()
    {
        return $"{TableSchema}.{TableName}";
    }
}
