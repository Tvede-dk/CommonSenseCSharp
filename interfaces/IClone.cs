using JetBrains.Annotations;

namespace CommonSenseCSharp.interfaces
{

    public interface IClone<out T>
    {

        [NotNull]
        T CloneDeep();
    }



}
public static class CommonICloneExtensions
{
    public static string CloneDeep([NotNull] this string str) => str.Substring(0, str.Length);
}
