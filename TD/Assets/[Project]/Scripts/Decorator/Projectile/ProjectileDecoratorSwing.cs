using System;
using UnityEngine;

[Serializable]
public class ProjectileDecoratorSwing : ProjectileDecoratorBase
{
    public override void ComposableUpdate()
    {
        base.ComposableUpdate();
        transform.Translate(Vector2.right * Time.deltaTime * Mathf.Cos(Time.time * 10) * 5);
    }

    public ProjectileDecoratorSwing(Composable composable, ProjectileDecoratorBase projectileDecoratorBase)
    : base(composable, projectileDecoratorBase)
    {
    }
}
