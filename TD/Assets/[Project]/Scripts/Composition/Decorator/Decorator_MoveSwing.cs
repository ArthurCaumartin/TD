using System;
using UnityEngine;

namespace BehaviorComposition.Decorator
{
    [Serializable]
    public class Decorator_MoveSwing : Decorator
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _amplitude;
        [SerializeField] private float _speed;

        //TODO ajouter la direction du swing dans le constructeur
        public Decorator_MoveSwing(Composable composable, Transform transform, float amplitude, float speed)
        : base(composable)
        {
            _transform = transform;
            _amplitude = amplitude;
            _speed = speed;
        }

        public override void ComposableUpdate()
        {
            base.ComposableUpdate();
            _transform.Translate(Vector2.right * Time.deltaTime * Mathf.Cos(Time.time * _speed) * _amplitude);
        }
    }
}