using UnityEngine;
using BehaviorComposition.Decorator;
using BehaviorComposition;

public class ProjectileInstaller : DecoratorInstaller
{
    [SerializeField] private ScriptableUpgarde _upgrade;
    public void Init(StatContainer stats)
    {
        // behavior = new Decorator_MoveToTarget(null, transform, target, stats.speed);
        // behavior = new Decorator_HitOnPath(behavior, transform, stats.damage, stats.layerMask);
        CompositionContext context = new CompositionContext(
            transform,
            transform,
            null,
            null,
            stats
        );
        behavior = null;
        if (_upgrade)
            behavior = _upgrade.GetComposition(behavior, context);
        else
        {
            behavior = new Decorator_MoveUp(behavior, transform, stats);
            behavior = new Decorator_HitOnPath(behavior, transform, stats);
        }
        SubToDecoratorEvent();
    }
}