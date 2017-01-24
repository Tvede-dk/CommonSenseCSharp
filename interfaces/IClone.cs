using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSenseCSharp.interfaces
{
    public interface IClone<T>
    {
        T CloneDeep();
    }



}
public static class CommonICloneExtensions
{
    public static string CloneDeep([NotNull] this string str)
    {
        return str.Substring(0, str.Length);
    }
}
