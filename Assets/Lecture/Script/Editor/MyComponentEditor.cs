using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

[CustomEditor(typeof(MyComponent))]
public class MyComponentEditor : Editor {

    // 이것은 private이다.
    SerializedProperty intNum;
    SerializedProperty floatNum;
    SerializedProperty gameObjectList;

    void onEnable()
    {
        intNum = serializedObject.FindProperty("intNum"); // <<가져오기
        floatNum = serializedObject.FindProperty("floatNum");
        gameObjectList = serializedObject.FindProperty("gameObjectList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(intNum, new GUIContent("Var1"));
        EditorGUILayout.PropertyField(floatNum, new GUIContent("Var1"));
        EditorGUILayout.PropertyField(gameObjectList);
        serializedObject.ApplyModifiedProperties(); // 자동관리..?


        MyComponent myComponent = (MyComponent)target;
        //myComponent.intVariable = EditorGUILayout.IntField("Int Varialbe", myComponent.intVariable);
        //myComponent.floatVariable = EditorGUILayout.Slider("float Varialbe", myComponent.floatVariable, 0.0f, 100.0f);
        myComponent.IntVar = EditorGUILayout.IntField("int var", myComponent.IntVar); // property? 대박 같이변한다!

        int a = EditorGUILayout.IntField("int Var", myComponent.IntVar);
        if ( a != myComponent.IntVar)
        {
            myComponent.IntVar = a;
            //변경여부는 직접 관리해야 한다!
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        if(GUILayout.Button("Do something") == true) //리턴이 bool값!
        {
            myComponent.DoSomething();

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}
