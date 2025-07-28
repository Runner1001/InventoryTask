using System;
using System.Collections;
using UnityEngine;

public interface IAsyncItem
{
    IEnumerator UseAsync(GameObject player, Action onComplete);
}
