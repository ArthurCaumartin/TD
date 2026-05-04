using UnityEngine;

public class ProjectileDescriptor : MonoBehaviour
{
    public Decorator _behavior = null;

    public virtual ProjectileDescriptor Init(float damage, float speed, Transform target, LayerMask layerMask)
    {
        _behavior = new Decorator_MoveToTarget(null, transform, target, speed);
        _behavior = new Decorator_HitOnPath(_behavior, transform, 50, layerMask);
        _behavior = new Decorator_Explosif(_behavior, transform, 50, 3, layerMask);

        _behavior.Spawn();
        _behavior.SubToKillEvent(KillBehavior);

        return this;
    }

    protected virtual void Update()
    {
        _behavior.ComposableUpdate();
    }

    private void KillBehavior()
    {
        print("KillBehavior");
        _behavior.Kill();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!_behavior) return;
        _behavior.DrawGizmoDebug();
    }
}
