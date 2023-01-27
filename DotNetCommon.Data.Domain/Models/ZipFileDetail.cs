using System.IO;

namespace DotNetCommon.Data.Domain.Models;

public class ZipFileDetail
{
    public ZipFileDetail(string name, Stream file)
    {
        File = file;
        Name = name;
    }

    public ZipFileDetail(string name, byte[] content)
    {
        Content = content;
        Name = name;
    }

    public Stream File { get; set; }

    public byte[] Content { get; set; }

    public string Name { get; set; }
}
