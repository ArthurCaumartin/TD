using UnityEngine;

public class ProjectileInstaller : DecoratorInstaller
{
    public void Init(float damage, float speed, Transform target, LayerMask layerMask)
    {
        behavior = new Decorator_MoveToTarget(null, transform, target, speed);
        behavior = new Decorator_HitOnPath(behavior, transform, damage, layerMask);

        behavior.Spawn();
        SubToDecoratorEvent();
    }
}
