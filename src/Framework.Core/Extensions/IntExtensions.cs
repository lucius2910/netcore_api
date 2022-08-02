namespace Framework.Core.Extensions
{
    public static class IntExtensions
    {
        public static int ToInt(this int? oldInt)
        {
            return oldInt ?? 0;
        }
    }
}
