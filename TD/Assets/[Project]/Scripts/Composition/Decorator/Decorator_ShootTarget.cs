using UnityEngine;

//TODO Set la reference au TargetFinder pour get la target, plutot que de la psser en parametre a Shoot
namespace BehaviorComposition.Decorator
{
    public class Decorator_ShootTarget : Decorator
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private int _projectileCount;
        [SerializeField] private StatContainer _stats;
        private TargetFinder _targetFinder;

        public Decorator_ShootTarget(Composable composable, Transform shootPoint, TargetFinder targetFinder, StatContainer stats, int projectileCount)
        : base(composable)
        {
            this._projectileCount = projectileCount;
            this._shootPoint = shootPoint;
            this._stats = stats;
            this._targetFinder = targetFinder;
        }

        public override void Shoot(ProjectileInstaller projectile, StatContainer stats)
        {
            if(!_targetFinder.CurrentTarget) return;
            base.Shoot(projectile, stats);
            if (_projectileCount == 1)
            {
                InstantiateProjectile(projectile, _targetFinder, stats, _shootPoint.position);
                return;
            }
            Debug.Log("----I----");
            for (int i = 0; i < _projectileCount; i++)
            {
                float time = Mathf.InverseLerp(0, _projectileCount - 1, i);

                Debug.Log("i = " + time);
                Vector2 pos = Vector2.Lerp(
                    _shootPoint.position - _shootPoint.right,
                    _shootPoint.position + _shootPoint.right,
                    time);

                InstantiateProjectile(projectile, _targetFinder, stats, pos);
            }
        }

        private void InstantiateProjectile(ProjectileInstaller projectile, TargetFinder targetFinder, StatContainer stats, Vector2 position)
        {
            ProjectileInstaller p = GameObject.Instantiate(projectile, position, Quaternion.identity);
            p.transform.up = (targetFinder.CurrentTarget.position - _shootPoint.position).normalized;
            p.Init(stats);
        }
    }
}