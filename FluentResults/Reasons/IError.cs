using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace TNRD.Zeepkist.GTR.FluentResults
{
    public interface IError : IReason
    {
        /// <summary>
        /// Reasons of the error
        /// </summary>
        List<IError> Reasons { get; }
    }
}
