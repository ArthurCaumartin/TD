using UnityEngine;

namespace BehaviorComposition.Decorator
{
    public class Decorator_HitOnPath : Decorator
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private StatContainer _stats;
        private Vector3 _lastFramePosition;

        public Decorator_HitOnPath(Composable composable, Transform transform, StatContainer stats)
        : base(composable)
        {
            this._transform = transform;
            this._stats = stats;

            _lastFramePosition = _transform.position;
        }

        public override void ComposableUpdate()
        {
            base.ComposableUpdate();
            Damagable[] t = PhysicsCastUtils2D.GetTypeInLine<Damagable>(_transform.position, _lastFramePosition, _stats.layerMask);
            // Debug.DrawLine(_transform.position, _lastFramePosition, Color.red, 1f);
            // Debug.Log("HitOnPath count : " + t.Length);
            if (t.Length != 0)
            {
                Damagable d = t.GetNearset(_transform.position);
                if (d)
                {
                    d.TakeDamage(_stats.damage);
                    killCallEvent.Invoke();
                    return;
                }
            }
            _lastFramePosition = _transform.position;
        }
    }
}