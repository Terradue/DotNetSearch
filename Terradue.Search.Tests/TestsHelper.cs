using System;
using System.Collections.Generic;

namespace Terradue.Search.Tests
{
    internal class TestsHelper
    {
        internal static IEnumerable<string> CreateStringEnumerable(int count, string prefix = "item")
        {
            List<string> stringList = new List<string>();
            for (int i = 1; i <= count; i++)
            {
                stringList.Add(string.Format("{0}-{1}", prefix, i));
            }

            return stringList;

        }
    }
}