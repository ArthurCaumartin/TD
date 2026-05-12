using UnityEngine;

namespace BehaviorComposition.Decorator
{
    public class Decorator_MoveUp : Decorator
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private StatContainer _stats;

        public Decorator_MoveUp(Composable composable, Transform transform, StatContainer stats)
        : base(composable)
        {
            this._transform = transform;
            this._stats = stats;
        }

        public override void ComposableUpdate()
        {
            base.ComposableUpdate();
            _transform.Translate(Vector2.up * Time.deltaTime * _stats.speed, Space.Self);
        }
    }
}
