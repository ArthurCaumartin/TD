using UnityEngine;

namespace BehaviorComposition.Decorator
{
    public class Decorator_Explosif : Decorator
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private StatContainer _stats;
        private float _radius;

        public Decorator_Explosif(Composable composable, Transform transform, StatContainer stats, float radius)
        : base(composable)
        {
            _transform = transform;
            this._stats = stats;
            this._radius = radius;
        }

        private void Explode()
        {
            Debug.Log("explosifDamage");
            Damagable[] dmg = PhysicsCastUtils2D.GetTypeInOverlapCircle<Damagable>(_transform.position, _radius, _stats.layerMask);
            Debug.Log(dmg.Length);
            foreach (var item in dmg)
            {
                item.TakeDamage(_stats.damage);
            }
        }

        public override void Kill()
        {
            Explode();
            base.Kill();
        }

        public override void DrawGizmoDebug()
        {
            base.DrawGizmoDebug();
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_transform.position, _stats.range / 5);
        }
    }
}