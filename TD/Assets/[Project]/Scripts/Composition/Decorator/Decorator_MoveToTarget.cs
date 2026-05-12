using UnityEngine;

namespace BehaviorComposition.Decorator
{
    public class Decorator_MoveToTarget : Decorator
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _target;
        [SerializeField] private StatContainer _stats;

        private Vector3 _targetPos;
        private float _sqrDistance;

        public Decorator_MoveToTarget(Composable composable, Transform transform, Transform target, StatContainer stats)
        : base(composable)
        {
            this._transform = transform;
            this._target = target;
            this._stats = stats;

            _targetPos = _transform.position;
        }

        public override void ComposableUpdate()
        {
            base.ComposableUpdate();

            if (_target) _targetPos = _target.position;

            Vector3 dir = _targetPos - _transform.position;
            dir = dir.normalized;
            _transform.up = dir;
            _transform.Translate(Vector3.up * Time.deltaTime * _stats.speed, Space.Self);
            _sqrDistance = (_targetPos - _transform.position).sqrMagnitude;
            if (_sqrDistance < 0.01f)
                killCallEvent.Invoke();
        }
    }
}