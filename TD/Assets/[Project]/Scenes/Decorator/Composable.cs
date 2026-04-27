


public class Composable
{
    public virtual void ComposableUpdate() { }

    public static bool operator !(Composable state)
    {
        return state == null;
    }
}
