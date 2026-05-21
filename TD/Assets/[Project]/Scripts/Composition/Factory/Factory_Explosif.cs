using UnityEngine;

namespace BehaviorComposition.Decorator.Factory
{
    public class Factory_Explosif : CompositionFactory
    {
        [SerializeField] private float _radius = 2.5f;
        public override Decorator BuildInstance(Composable warpedComposable, CompositionContext context)
        {
            return new Decorator_Explosif(
                warpedComposable,
                context.ownerTransform,
                context.statContainer,
                _radius
            );
        }
    }
}