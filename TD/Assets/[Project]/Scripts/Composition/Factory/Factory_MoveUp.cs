
namespace BehaviorComposition.Decorator.Factory
{
    public class Factory_MoveUp : CompositionFactory
    {
        public override Decorator BuildInstance(Composable warpedComposable, CompositionContext context)
        {
            return new Decorator_MoveUp(
                warpedComposable,
                context.shootPoint,
                context.statContainer
            );
        }
    }
}