using System;
using BehaviorComposition.Decorator.Factory;

[Serializable]
public class DecoratorSelector
{
    public bool isSelected = false;
    public string name = "Not Set";
    public CompositionFactory compositionFactory;

    public DecoratorSelector(string name, bool isSelected, CompositionFactory compositionFactory)
    {
        this.name = name;
        this.isSelected = isSelected;
        this.compositionFactory = compositionFactory;
    }
}
