
using System;

namespace TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks
{
    public static class ExceptionExtensions
    {
        public static bool IsOperationCanceledException(this Exception exception)
        {
            return exception is OperationCanceledException;
        }
    }
}

