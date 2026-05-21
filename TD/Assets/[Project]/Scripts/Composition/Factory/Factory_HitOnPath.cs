
namespace BehaviorComposition.Decorator.Factory
{
    public class Factory_HitOnPath : CompositionFactory
    {
        public override Decorator BuildInstance(Composable warpedComposable, CompositionContext context)
        {
            return new Decorator_HitOnPath(
                warpedComposable,
                context.shootPoint,
                context.statContainer
            );
        }
    }
}