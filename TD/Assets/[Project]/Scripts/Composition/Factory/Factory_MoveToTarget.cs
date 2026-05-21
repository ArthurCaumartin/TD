
namespace BehaviorComposition.Decorator.Factory
{
    public class Factory_MoveToTarget : CompositionFactory
    {
        public override Decorator BuildInstance(Composable warpedComposable, CompositionContext context)
        {
            return new Decorator_MoveToTarget(
                warpedComposable,
                context.ownerTransform,
                context.targetFinder,
                context.statContainer
            );
        }
    }
}