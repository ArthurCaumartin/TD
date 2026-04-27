using UnityEngine;


public class ProjectileDescriptor : MonoBehaviour
{
    public Composable _projectileBehavior;

    public virtual ProjectileDescriptor Init(float damage, float speed, Transform target, LayerMask layerMask)
    {
        _projectileBehavior = new Composable();
        _projectileBehavior = new ProjectileDecoratorBase(
            _projectileBehavior,
            transform,
            damage,
            speed,
            target,
            layerMask
        );
        _projectileBehavior = new ProjectileDecoratorSwing(_projectileBehavior, _projectileBehavior as ProjectileDecoratorBase);
        _projectileBehavior = new ProjectileDecoratorExplosionDelay(_projectileBehavior, _projectileBehavior as ProjectileDecoratorBase, 5);

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

}
