using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull<T>(this T value)
        {
            bool nullable = Nullable.GetUnderlyingType(typeof(T)) != null;
            return nullable ? EqualityComparer<T>.Default.Equals(value, default) : false;
        }
    }
}
