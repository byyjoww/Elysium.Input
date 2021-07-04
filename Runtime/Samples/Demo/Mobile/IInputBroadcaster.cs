using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.Input
{
    public interface IInputBroadcaster<T>
    {
        event UnityAction<T> OnBroadcast;
    }
}