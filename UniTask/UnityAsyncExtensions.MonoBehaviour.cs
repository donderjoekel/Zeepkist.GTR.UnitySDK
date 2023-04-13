﻿using System;
using System.Threading;

namespace TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks
{
    public static partial class UnityAsyncExtensions
    {
        public static UniTask StartAsyncCoroutine(this UnityEngine.MonoBehaviour monoBehaviour, Func<CancellationToken, UniTask> asyncCoroutine)
        {
            var token = monoBehaviour.GetCancellationTokenOnDestroy();
            return asyncCoroutine(token);
        }
    }
}
