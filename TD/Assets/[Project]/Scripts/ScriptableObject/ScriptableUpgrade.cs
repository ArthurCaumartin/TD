using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BehaviorComposition.Decorator;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_", menuName = "Mwa/Upgrade")]
public class ScriptableUpgarde : ScriptableObject
{
    [SerializeField] public List<DecoratorSelector> selectorList = new List<DecoratorSelector>();

    private void Awake()
    {
        FillSelectorList(selectorList);
    }

    private void OnEnable()
    {
        FillSelectorList(selectorList);
    }

    private void OnDisable()
    {
        FillSelectorList(selectorList);
    }

    public List<Type> GetSelectedDecorator()
    {
        List<Type> selected = new List<Type>();
        for (int i = 0; i < selectorList.Count; i++)
        {
            if (selectorList[i].isSelected)
                selected.Add(selectorList[i].type);
        }
        return selected;
    }

    private void FillSelectorList(List<DecoratorSelector> selectors)
    {
        selectors.Clear();

        Type[] typeArray =
        (from t in Assembly.GetExecutingAssembly().GetTypes()
         where t.IsClass && t.Namespace == "BehaviorComposition.Decorator" && t.BaseType == typeof(Decorator)
         select t).ToArray();

        for (int i = 0; i < typeArray.Length; i++)
        {
            string name = typeArray[i].ToString().Split('_')[1];

            List<Type> constrParType = new List<Type>();
            ConstructorInfo[] t = typeArray[i].GetConstructors();

            for (int j = 0; j < t.Length; j++)
            {
                ParameterInfo[] pInfos = t[j].GetParameters();
                foreach (var item in pInfos)
                    constrParType.Add(item.ParameterType);
            }

            selectors.Add(new DecoratorSelector(name, false, typeArray[i], constrParType));
        }
    }
}
