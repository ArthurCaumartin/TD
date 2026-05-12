using System;
using BehaviorComposition;
using UnityEngine;
using UnityEngine.Events;

namespace BehaviorComposition
{
    [Serializable]
    public abstract class Composable
    {
        public virtual void Spawn() { }
        public virtual void Kill() { }

        public virtual void Shoot(ProjectileInstaller projectile, Transform target, StatContainer stat) { }

        public virtual void ComposableUpdate() { }
        public virtual void DrawGizmoDebug() { }

        public virtual void SubToKillEvent(UnityAction action) { }


        public static bool operator !(Composable state)
        {
            return state == null;
        }
    }
}
