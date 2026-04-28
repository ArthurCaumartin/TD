using UnityEngine;


public class ProjectileDescriptor : MonoBehaviour
{
    public ProjectileDecoratorBase _projectileBehavior;

    public virtual ProjectileDescriptor Init(float damage, float speed, Transform target, LayerMask layerMask)
    {
        _projectileBehavior = new ProjectileDecoratorBase(
            null,
            transform,
            damage,
            speed,
            target,
            layerMask
        );
        _projectileBehavior = new ProjectileDecoratorSwing(_projectileBehavior, 10, 10);
        _projectileBehavior = new ProjectileDecoratorExplosionDelay(_projectileBehavior, 5);

        return this;
    }

    protected virtual void Update()
    {
        if (!_projectileBehavior)
        {
            Destroy(gameObject);
            return;
        }
        _projectileBehavior.ComposableUpdate();
    }

    void OnDrawGizmos()
    {
        if (!_projectileBehavior) return;
        _projectileBehavior.DrawGizmoDebug()?.Invoke();
    }
}
