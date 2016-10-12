using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MyAsset))]
public class MyAssetEditor : Editor {

    void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {

        MyAsset myAsset = (MyAsset)target;
        //EditorGUILayout.IntField("intvar", myAsset.inVar);
        //EditorGUILayout.FloatField("floatVar", myAsset.floatVar);

        myAsset.intVar = EditorGUILayout.IntField("intvar", myAsset.intVar);
        myAsset.floatVar = EditorGUILayout.FloatField("floatVar", myAsset.floatVar);

        if (GUILayout.Button("Apply") == true)
        {
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }

        if (GUILayout.Button("Revert") == true)
        {
            myAsset.Revert();
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
    }

    [MenuItem("Assets/Create/MyAsset")]
    public static void CreateMyAsset()
    {
        MyAsset asset = CreateInstance<MyAsset>();
        //EditorUtility.OpenFilePanel("Save",null, null);
        AssetDatabase.CreateAsset(asset, "Assets/MyAsset.asset");
        AssetDatabase.SaveAssets();
    }
}
