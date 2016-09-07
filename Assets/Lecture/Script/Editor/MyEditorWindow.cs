using UnityEngine;
using UnityEditor;
//using UnityEditor.SceneManagment;
using System.Collections;

[CustomEditor(typeof(MyComponent))]
public class MyEditorWindow : EditorWindow
{
    [MenuItem("My Menu/Show My Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyEditorWindow), false, "MyWinodw");
    }

    [MenuItem("My Menu/Add MyComponent", true)]
    public static bool validateAddMyCompoent()
    {
        if (Selection.activeGameObject == null)
            return false;
        else
            return true;
    }

    [MenuItem("My Menu/Add MyComponent")]
    public static void a()
    {
        if(Selection.activeGameObject != null)
        {
            Selection.activeGameObject.AddComponent<MyComponent>();
        }
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Button("MyButton1");
        GUILayout.Button("MyButton2");
        GUILayout.Button("MyButton3");
        GUILayout.EndHorizontal();

        Rect rectGUI = new Rect(100,100, 100, 50);
        GUI.Button(rectGUI, "GUI BUTTON");

        rectGUI = new Rect(100, 200, 200, 30);
        if (Selection.activeGameObject == null)
        {
            GUI.Label(rectGUI, "No Selection");
        }
        else
        {
            GUI.Label(rectGUI, Selection.activeGameObject.name);

            rectGUI = new Rect(100, 300, 200, 30);
            if(GUI.Button(rectGUI,"Add MyComponent") == true)
            {
                Selection.activeGameObject.AddComponent<MyComponent>();
            }
        }

        if(Event.current.button == 1)
        {
            if(Event.current.type == EventType.MouseUp)
            {
                //Debug.Log("OwO");
                GenericMenu Contextmenu = new GenericMenu();
                Contextmenu.AddItem(new GUIContent("Menu 1"), false, DoMenu1);
                Contextmenu.AddItem(new GUIContent("Menu 2"), false, DoMenu2);
                Contextmenu.ShowAsContext();
            }
        }
    }

    void DoMenu1()
    {
        Debug.Log("owo1");
    }
    
    void DoMenu2()
    {
        Debug.Log("owo2");
    }
}
