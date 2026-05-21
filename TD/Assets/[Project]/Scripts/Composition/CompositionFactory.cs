using System;

namespace BehaviorComposition.Decorator.Factory
{
    public abstract class CompositionFactory
    {
        public virtual Decorator BuildInstance(Composable warpedComposable, CompositionContext context)
        {
            return null;
        }
    }
}
