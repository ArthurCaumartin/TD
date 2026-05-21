using UnityEngine;

namespace BehaviorComposition.Decorator.Factory
{
    public class Factory_MoveSwing : CompositionFactory
    {
        [SerializeField] private float _amplitude;
        [SerializeField] private float _speed;

        public override Decorator BuildInstance(Composable warpedComposable, CompositionContext context)
        {
            return new Decorator_MoveSwing(
                warpedComposable,
                context.ownerTransform,
                _amplitude,
                _speed
            );
        }
    }
}