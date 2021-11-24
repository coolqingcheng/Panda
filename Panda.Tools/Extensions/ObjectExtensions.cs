using Panda.Tools.Exception;

namespace Panda
{
    public static class ObjectExtensions
    {
        public static void IsNullThrow(this Object? obj, string message)
        {
            if (obj == null)
            {
                throw new UserException(message);
            }
        }
    }
}
