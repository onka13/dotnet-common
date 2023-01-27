using Xunit;

namespace DotNetCommon.Application.APIBase.TestComponents;

public class FactDebugOnlyAttribute : FactAttribute
{
    public FactDebugOnlyAttribute()
    {
#if !DEBUG
        Skip = "Only running in debug mode.";
#endif
    }
}
