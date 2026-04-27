using System.Linq;
using Alchemy.Inspector;
using UnityEngine;


public class ProjectileDescriptor : MonoBehaviour
{
    private ProjectileDecoratorBase _projectileBehavior;

    public virtual ProjectileDescriptor Init(float damage, float speed, Transform target, LayerMask layerMask)
    {
        _projectileBehavior = new Composable() as ProjectileDecoratorBase;
        _projectileBehavior = new ProjectileDecoratorBase(_projectileBehavior);

        _projectileBehavior.Init(transform,
                                 damage,
                                 speed,
                                 target,
                                 layerMask);

        return this;
    }

    protected virtual void Update()
    {
        if (!_projectileBehavior)
        {
            Destroy(gameObject);
            return;
        }
        _projectileBehavior.ComposableUpdate();
    }

}
