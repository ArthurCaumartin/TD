using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Flags]
public enum DecoratorTag
{
    Tag1,
    Tag2,
    Tag3,
    Tag4
}


[CustomEditor(typeof(ScriptableUpgarde))]
public class ScriptableUpgardeEditor : Editor
{
    private ScriptableUpgarde _upgarde;
    private string _searchBarInput = "";
    private DecoratorTag _tag;

    public override void OnInspectorGUI()
    {
        _upgarde = target as ScriptableUpgarde;
        if (_upgarde.selectorList == null || _upgarde.selectorList.Count == 0) return;

        float viewWidth = EditorGUIUtility.currentViewWidth;
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float lineSpacing = EditorGUIUtility.standardVerticalSpacing;

        GUILayoutUtility.GetRect(viewWidth, 1000);

        Rect searchBarRect = new Rect(10, 25, viewWidth / 2, lineHeight);
        DisplaySearchBar(searchBarRect, out List<DecoratorSelector> filterSelector);

        Rect rect = new Rect(10, 25, viewWidth - 30, lineHeight);
        rect.y += 25;

        if (filterSelector == null || filterSelector.Count == 0) return;
        for (int i = 0; i < filterSelector.Count; i++)
        {
            DisplaySelector(filterSelector[i], rect);
            rect.y += 25;
        }
    }

    private void DisplaySearchBar(Rect rect, out List<DecoratorSelector> filterSelector)
    {
        // GUILayoutUtility.GetRect(rect.width, rect.height);
        filterSelector = new List<DecoratorSelector>();
        _searchBarInput = EditorGUI.TextField(rect, _searchBarInput);
        foreach (DecoratorSelector selector in _upgarde.selectorList)
        {
            if (selector.name.ToLower().Contains(_searchBarInput.ToLower()))
                filterSelector.Add(selector);
        }
    }

    private void DisplayEnumTag(Rect rect, out List<DecoratorSelector> filterSelector)
    {
        filterSelector = new List<DecoratorSelector>();
        // EditorGUI.EnumPopup(rect, _tag);
        //TODO
    }

    private void DisplaySelector(DecoratorSelector selector, Rect rect)
    {
        Texture2D backTex = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/[Project]/Editor/10x10_gray.png");
        GUI.DrawTexture(rect, backTex);

        Debug.Log("_________");
        Debug.Log("Selector for : " + selector.name);
        string s = "";
        foreach (var item in selector.constructorInput)
            s += item.ToString() + " / ";
        Debug.Log("Consrt Input : " + s);

        Rect toogleRect = new Rect(rect.x + 2.5f, rect.y + 5, rect.width, 10);
        selector.isSelected = EditorGUI.ToggleLeft(toogleRect, selector.name, selector.isSelected);

        // Rect labelRect = new Rect((rect.x / 2) + 10, rect.y, rect.width - 10, 10);
        // EditorGUI.LabelField(labelRect, selector.name);

        // foreach (Type item in selector.constructorInput)
        // {
        //     rect.y += 20;
        //     Rect r = rect;
        //     r.x += 40;
        //     EditorGUI.TextField(r, item.ToString());
        // }
    }
}
