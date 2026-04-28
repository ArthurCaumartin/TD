using UnityEngine;

public class DecoratorEntity : Decorator
{
    protected Transform transform;

    public virtual void Spawn() { }
    public virtual void Kill()
    {
        GameObject.Destroy(transform.gameObject);
        return;
    }

    public DecoratorEntity(Composable composable, Transform transform) : base(composable)
    {
        this.transform = transform;
    }
}