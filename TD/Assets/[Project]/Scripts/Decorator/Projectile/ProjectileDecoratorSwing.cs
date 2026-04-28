using System;
using UnityEngine;

[Serializable]
public class ProjectileDecoratorSwing : ProjectileDecoratorBase
{
    private float _amplitude;
    private float _frequency;
    public override void ComposableUpdate()
    {
        base.ComposableUpdate();
        transform.Translate(Vector2.right * Time.deltaTime * Mathf.Cos(Time.time * _frequency) * _amplitude);
    }

    public ProjectileDecoratorSwing(ProjectileDecoratorBase projectileBase, float amplitude, float frequency)
    : base(projectileBase)
    {
        _amplitude = amplitude;
        _frequency = frequency;
    }
}
