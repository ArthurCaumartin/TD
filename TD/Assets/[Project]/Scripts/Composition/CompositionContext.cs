using UnityEngine;

namespace BehaviorComposition
{
    public struct CompositionContext
    {
        [SerializeField] public readonly Transform ownerTransform;
        [SerializeField] public readonly Transform shootPoint;
        [SerializeField] public readonly TargetFinder targetFinder;
        [SerializeField] public readonly ProjectileInstaller projectileInstaller;
        [SerializeField] public readonly StatContainer statContainer;

        public CompositionContext(
            Transform ownerTransform,
            Transform shootPoint,
            TargetFinder targetFinder,
            ProjectileInstaller projectileInstaller,
            StatContainer statContainer
        )
        {
            this.ownerTransform = ownerTransform;
            this.shootPoint = shootPoint;
            this.targetFinder = targetFinder;
            this.projectileInstaller = projectileInstaller;
            this.statContainer = statContainer;
        }
    }
}
