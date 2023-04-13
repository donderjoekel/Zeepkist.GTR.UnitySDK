using System;

// ReSharper disable once CheckNamespace
namespace TNRD.Zeepkist.GTR.FluentResults
{
    public class ResultSettings
    {
        public Func<Exception, IError> DefaultTryCatchHandler { get; set; }

        public Func<string, ISuccess> SuccessFactory { get; set; }

        public Func<string, IError> ErrorFactory { get; set; }

        public Func<string, Exception, IExceptionalError> ExceptionalErrorFactory { get; set; }
    }
}
