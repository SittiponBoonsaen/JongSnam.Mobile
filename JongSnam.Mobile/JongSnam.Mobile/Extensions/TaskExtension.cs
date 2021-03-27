using System;
using System.Threading.Tasks;

namespace JongSnam.Mobile.Extensions
{
    public static class TaskExtension
    {
        // NOTE: Async void is intentional here. This provides a way
        // to call an async method from the constructor while
        // communicating intent to fire and forget, and allow
        // handling of exceptions
        public static async void SafeFireAndForget(this Task task, bool returnToCallingContext, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }
            catch (Exception ex) // if the provided action is not null, catch and pass the thrown exception.
            when (onException != null)
            {
                onException(ex);
            }
        }
    }
}
