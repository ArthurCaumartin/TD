using UnityEngine;
using BehaviorComposition.Decorator;


public class ProjectileInstaller : DecoratorInstaller
{
    public void Init(StatContainer stats, Decorator decoratorComposition)
    {
        // behavior = new Decorator_MoveToTarget(null, transform, target, stats.speed);
        // behavior = new Decorator_HitOnPath(behavior, transform, stats.damage, stats.layerMask);
        behavior = decoratorComposition;
        behavior.Spawn();
        SubToDecoratorEvent();
    }
}