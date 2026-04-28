using System;
using UnityEngine;

[Serializable]
public class Decorator : Composable
{
    protected Composable warpedComposable;
    public Composable WarrpedComposable => warpedComposable;

    public Decorator(Composable composable) { warpedComposable = composable; }

    public override void ComposableUpdate()
    {
        warpedComposable?.ComposableUpdate();
    }

    public virtual Action DrawGizmoDebug()
    {
        return null;
    }
}
