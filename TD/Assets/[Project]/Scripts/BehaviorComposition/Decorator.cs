using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class Decorator : Composable
{
    protected Composable warpedComposable;
    public Composable WarrpedComposable => warpedComposable;

    protected UnityEvent killCallEvent = new UnityEvent();

    public Decorator(Composable composable) { warpedComposable = composable; }

    public override void Spawn() { warpedComposable?.Spawn(); }
    public override void Kill() { warpedComposable?.Kill(); }

    public override void ComposableUpdate() { warpedComposable?.ComposableUpdate(); }
    public override void DrawGizmoDebug() { warpedComposable?.DrawGizmoDebug(); }

    public override void SubToKillEvent(UnityAction action)
    {
        base.SubToKillEvent(action);
        killCallEvent.AddListener(action);
        if (warpedComposable != null)
            warpedComposable.SubToKillEvent(action);
    }

}
