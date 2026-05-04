using System;
using UnityEngine.Events;

[Serializable]
public abstract class Composable
{
    public virtual void Spawn() { }
    public virtual void Kill() { }

    public virtual void ComposableUpdate() { }
    public virtual void DrawGizmoDebug() { }

    public virtual void SubToKillEvent(UnityAction action) { }


    public static bool operator !(Composable state)
    {
        return state == null;
    }
}
