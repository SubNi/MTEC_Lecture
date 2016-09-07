using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(MyComponent))]
public class MyComponentEditor : Editor {

    void onEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        MyComponent myComponent = (MyComponent)target;

        myComponent.intVariable = EditorGUILayout.IntField("Int Varialbe", myComponent.intVariable);
        myComponent.floatVariable = EditorGUILayout.Slider("float Varialbe", myComponent.floatVariable, 0.0f, 100.0f);
        myComponent.IntVar = EditorGUILayout.IntField("int var", myComponent.IntVar); // property? 대박 같이변한다!

        if(GUILayout.Button("Do something") == true) //리턴이 bool값!
        {
            myComponent.DoSomething();
        }
    }
}
