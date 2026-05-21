using UnityEngine;

namespace BehaviorComposition.Decorator.Factory
{
    public class Factory_ShootCircleOnDeath : CompositionFactory
    {
        [SerializeField] private ProjectileInstaller _projectile;
        [SerializeField] private int _projectileCount = 1;

        public override Decorator BuildInstance(Composable warpedComposable, CompositionContext context)
        {
            return new Decorator_ShootCircleOnDeath(
                warpedComposable,
                context.ownerTransform,
                _projectile,
                context.statContainer,
                _projectileCount
            );
        }
    }
}