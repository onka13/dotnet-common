using System.ComponentModel;

namespace DotNetCommon.Infrastructure.Enums
{
    public enum AuthenticationScheme
    {
        [Description("Basic")]
        Basic = 0,

        [Description("Bearer")]
        Bearer = 1,
    }
}
