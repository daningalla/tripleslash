using System.Globalization;

namespace Tripleslash.Infrastructure;

public static class TaskExtensions
{
    public static TResult GetSynchronously<TResult>(this Task<TResult> task)
    {
        var cultureUi = CultureInfo.CurrentUICulture;
        var culture = CultureInfo.CurrentCulture;
        var factoryTask = Task.Factory.StartNew(() =>
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = cultureUi;
            return task;
        });

        return factoryTask
            .Unwrap()
            .GetAwaiter()
            .GetResult();
    }
}