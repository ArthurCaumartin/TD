using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BehaviorComposition;
using BehaviorComposition.Decorator;
using BehaviorComposition.Decorator.Factory;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_", menuName = "Mwa/Upgrade")]
public class ScriptableUpgarde : ScriptableObject
{
    [SerializeField] public List<DecoratorSelector> selectorList;

    private void Awake()
    {
        FillSelectorList(selectorList);
    }

    // private void OnEnable()
    // {
    //     FillSelectorList(selectorList);
    // }

    // private void OnDisable()
    // {
    //     FillSelectorList(selectorList);
    // }

    private void Reset()
    {
        selectorList.Clear();
        FillSelectorList(selectorList);
    }

    public Decorator GetComposition(Decorator composable, CompositionContext context)
    {
        for (int i = 0; i < selectorList.Count; i++)
        {
            if (selectorList[i].isSelected)
                composable = selectorList[i].compositionFactory.BuildInstance(composable, context);
        }
        return composable as Decorator;
    }

    public List<CompositionFactory> GetSelectedDecorator()
    {
        List<CompositionFactory> selected = new List<CompositionFactory>();
        for (int i = 0; i < selectorList.Count; i++)
        {
            if (selectorList[i].isSelected)
                selected.Add(selectorList[i].compositionFactory);
        }
        return selected;
    }

    [ContextMenu("Refresh Selector List")]
    private void FillSelectorList(List<DecoratorSelector> selectors)
    {
        selectors.Clear();

        Type[] factoryArray =
        (from t in Assembly.GetExecutingAssembly().GetTypes()
         where t.IsClass
         && t.Namespace == "BehaviorComposition.Decorator.Factory"
         && t.BaseType == typeof(CompositionFactory)
         select t).ToArray();

        for (int i = 0; i < factoryArray.Length; i++)
        {
            string name = factoryArray[i].ToString().Split('_')[1];
            // les upgrade doivent display les Factory, pas les decorator directs
            // donc le script Editor dois les instancier et gerer le display des parametres :)
            // EZ, on stock des type dans le Selector, et on fetch des selector les field pour les get/set
            selectors.Add(new DecoratorSelector(
                name,
                false,
                (CompositionFactory)Activator.CreateInstance(factoryArray[i])
            ));
        }
    }
}
