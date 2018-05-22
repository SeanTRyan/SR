using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Broadcasts
{
    /// <summary>
    /// Interface that alerts behaviours of relevant states in other classes.
    /// </summary>
    public interface IBroadcast : IEventSystemHandler
    {
        void Inform(BroadcastMessage message);
    }
}