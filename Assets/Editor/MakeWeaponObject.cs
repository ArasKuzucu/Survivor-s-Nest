using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MakeWeaponObject{

    
    [UnityEditor.MenuItem("Assets/Create/Weapon Object")]
	public static void CreateWeaponObject()
    {
        Weapon asset = ScriptableObject.CreateInstance<Weapon>();
        AssetDatabase.CreateAsset(asset, "Assets/ScriptableObjects/NewWeaponObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }


}
