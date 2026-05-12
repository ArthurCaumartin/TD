using System;
using System.Collections.Generic;

[Serializable]
public class DecoratorSelector
{
    public bool isSelected = false;
    public string name = "Not Set";
    public Type type;
    public List<Type> constructorInput = new List<Type>();

    public DecoratorSelector(string name, bool isSelected, Type type, List<Type> consturtorInput)
    {
        this.name = name;
        this.isSelected = isSelected;
        this.constructorInput = consturtorInput;
        this.type = type;
    }
}
