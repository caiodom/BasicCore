using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class TaskExtensions
    {
        public static void Continuing<T>(this Task<T> task, Action<T> sucessHandler,Action<Exception> exceptionHandler)
        {
            task.ContinueWith(t => t.Exception.Handle((ex) =>
            {
                exceptionHandler(ex);
                return true;


            }), TaskContinuationOptions.OnlyOnFaulted);



            if(!task.IsFaulted)
            {
                T result = task.Result;
                sucessHandler(result);
            }
        }

    }
}
