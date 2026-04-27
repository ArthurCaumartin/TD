using System;

[Serializable]
public class Decorator : Composable
{
    protected Composable warpedComposable;

    public Decorator(Composable composable) { warpedComposable = composable; }

    public override void ComposableUpdate()
    {
        warpedComposable?.ComposableUpdate();
    }
}
