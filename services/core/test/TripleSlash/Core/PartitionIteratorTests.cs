using System;
using System.Collections.Generic;
using Shouldly;
using Tripleslash.Core;
using Xunit;

namespace TripleSlash.Core;

public class PartitionIteratorTests
{
#pragma warning disable CA2211

    [Theory, MemberData(nameof(Theories))]
    public void ShouldSplitToExpectedPartitions(string source, IEnumerable<string> expected)
    {
        var queue = new Queue<string>(expected);

        for (var iterator = source.PartitionBy(','); !iterator.Current.IsEmpty; iterator.MoveNext())
        {
            queue.Dequeue().ShouldBe(new string(iterator.Current));
        }
    }

    public static object[][] Theories = 
    {
        new object[] { ".", Array.Empty<string>()},
        new object[] { "one", new[]{"one"} },
        new object[] { "one.two", new[]{"one","two"} },
        new object[] { "one.two.three", new[]{"one","two","three"} },
        new object[] { "one.two.three.", new[]{"one","two","three"} },
        new object[] { "", Array.Empty<string>() }
    };
    
#pragma warning restore CA2211
}

