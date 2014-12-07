using System;
using System.Collections.Generic;

namespace AvDB_lab4.Business
{
    public static class Contract
    {
        public static void NotNullAndNotEmpty<T>(IList<T> source, string message = null)
        {
            if (source == null || source.Count == 0)
            {
                throw new ArgumentException(message);
            }
        }

        public static void NotNull(object source, string message = null)
        {
            if (source == null)
            {
                throw new ArgumentException(message);
            }
        }

        public static void NotEmptyGuid(Guid guid, string message = null)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentException(message);
            }
        }

        public static void NotNullAndNoEmpty(Guid? id, string message)
        {
            NotNull(id, message);
            NotEmptyGuid(id.Value, message);
        }
    }
}