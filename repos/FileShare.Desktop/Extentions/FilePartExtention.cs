using System;
using System.Linq;

namespace FileShare.Desktop.Extentions
{
    public static class FilePartExtention
    {
        public static T[] GetFilePart<T>(this T[] array, int take, int skip = 0)
        {
            if (array.Length == 0 || take == 0)
                throw new ArgumentException(nameof(array));

            if (skip == 0)
                return array.Take(take).ToArray();

            return array.Skip(skip).Take(take).ToArray();
        }
    }
}
