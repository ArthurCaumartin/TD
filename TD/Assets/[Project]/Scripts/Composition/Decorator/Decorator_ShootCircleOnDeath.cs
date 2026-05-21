using UnityEngine;

namespace BehaviorComposition.Decorator
{
    public class Decorator_ShootCircleOnDeath : Decorator
    {
        private Transform _transform;
        private ProjectileInstaller _projectilePrefab;
        private StatContainer _stats;
        private int _projectileCount;

        public Decorator_ShootCircleOnDeath(Composable composable, Transform transform, ProjectileInstaller projectilePrefab, StatContainer stats, int projectileCount)
        : base(composable)
        {
            this._transform = transform;
            this._projectilePrefab = projectilePrefab;
            this._projectileCount = projectileCount;
            this._stats = stats;
        }

        public override void Kill()
        {
            base.Kill();

            for (float i = 0; i < 1; i += 1f / _projectileCount)
            {
                float x = Mathf.Cos(2 * Mathf.PI * i);
                float y = Mathf.Sin(2 * Mathf.PI * i);
                Vector3 spawnPoint = _transform.position + new Vector3(x, y, 0);
                Vector2 dir = (spawnPoint - _transform.position).normalized;
                ProjectileInstaller p = 
                GameObject.Instantiate(_projectilePrefab, spawnPoint, Quaternion.LookRotation(Vector3.forward, dir));

                p.Init(_stats);
            }
        }
    }
}
