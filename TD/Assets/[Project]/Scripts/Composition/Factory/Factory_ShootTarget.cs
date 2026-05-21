using UnityEngine;

namespace BehaviorComposition.Decorator.Factory
{
    public class Factory_ShootTarget : CompositionFactory
    {
        [SerializeField] private int _projectileCount;

        public override Decorator BuildInstance(Composable warpedComposable, CompositionContext context)
        {
            return new Decorator_ShootTarget(
                warpedComposable,
                context.shootPoint,
                context.targetFinder,
                context.statContainer,
                _projectileCount
            );
        }
    }
}
