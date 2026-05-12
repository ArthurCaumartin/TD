using UnityEngine;
using BehaviorComposition.Decorator;

public class DecoratorInstaller : MonoBehaviour
{
    [SerializeField] protected Decorator behavior;

    protected void SubToDecoratorEvent()
    {
        behavior.SubToKillEvent(KillBehavior);
    }

    protected virtual void Update()
    {
        behavior.ComposableUpdate();
    }

    private void KillBehavior()
    {
        //TODO fix abonement multi call :) jsp
        // print("KillBehavior");
        behavior.Kill();
        Destroy(gameObject);
    }

    protected virtual void OnDrawGizmos()
    {
        if (!behavior) return;
        behavior.DrawGizmoDebug();
    }
}
