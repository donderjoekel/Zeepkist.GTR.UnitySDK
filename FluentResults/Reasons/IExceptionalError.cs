using System;

// ReSharper disable once CheckNamespace
namespace TNRD.Zeepkist.GTR.FluentResults
{
    public interface IExceptionalError : IError
    {
        Exception Exception { get; }

    }
}
