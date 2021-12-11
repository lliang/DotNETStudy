using System.Reflection;
using System.Diagnostics;

namespace DotNETStudy.Filter.WebApi.Helpers
{
    public static class MyDebug
    {
        public static void Write(MethodBase m, string path)
        {
            Debug.WriteLine($"{m.ReflectedType?.Name}.{m.Name} {path}");
        }
    }
}
