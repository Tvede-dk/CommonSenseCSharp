using System;
using System.Threading.Tasks;

public static class TaskExtensions {

    public static async void EndWithResult<T>(this Task<T> task, Action<T> after) {
        var val = await task;
        after(val);
    }

}
