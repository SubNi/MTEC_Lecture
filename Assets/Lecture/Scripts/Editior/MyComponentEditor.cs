using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(MyComponent))]
public class MyComponentEditor : Editor
{

    SerializedProperty intVairable;
    SerializedProperty floatVariable;
    SerializedProperty gameObjects;


    void OnEnable()
    {
        intVairable = serializedObject.FindProperty("intVariable");
        floatVariable = serializedObject.FindProperty("floatVariable");
        gameObjects = serializedObject.FindProperty("gameObjects");
    }


    public override void OnInspectorGUI()
    {
        //업데이트와 같은...
       // base.OnInspectorGUI();
        serializedObject.Update();

        EditorGUILayout.PropertyField(intVairable);
        EditorGUILayout.PropertyField(floatVariable); //
        EditorGUILayout.PropertyField(gameObjects, new GUIContent("List"), true);


        serializedObject.ApplyModifiedProperties();     //자도오옹


        MyComponent myComponent = (MyComponent)target;
        myComponent.intVariable = EditorGUILayout.IntField("int variable", myComponent.intVariable);
        myComponent.floatVariable = EditorGUILayout.Slider("float variable", myComponent.floatVariable, 0f, 100f);
        //myComponent.IntVar = EditorGUILayout.IntField("int Var", myComponent.IntVar);


        int a = EditorGUILayout.IntField("Int Var", myComponent.IntVar);
        if(a!=myComponent.IntVar)
        {
            myComponent.IntVar = a;
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }


        if(GUILayout.Button("Do Something") == true)
        {
            myComponent.DoSomething();
        }

        if (GUILayout.Button("Show My Window") == true)
        {
            MyEditorWindow.ShowWindow();
        }

    }

}
