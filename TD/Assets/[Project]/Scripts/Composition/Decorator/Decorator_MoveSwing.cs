using System;
using UnityEngine;

[Serializable]
public class Decorator_MoveSwing : Decorator
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;

    //TODO ajouter la direction du swing dans le constructeur
    public Decorator_MoveSwing(Composable composable, Transform transform, float amplitude, float frequency)
    : base(composable)
    {
        _transform = transform;
        _amplitude = amplitude;
        _frequency = frequency;
    }

    public override void ComposableUpdate()
    {
        base.ComposableUpdate();
        _transform.Translate(Vector2.right * Time.deltaTime * Mathf.Cos(Time.time * _frequency) * _amplitude);
    }
}