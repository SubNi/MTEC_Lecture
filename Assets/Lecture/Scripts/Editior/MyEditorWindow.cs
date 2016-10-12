using UnityEngine;
using UnityEditor;
using System.Collections;

[AddComponentMenu("My Menu/MyComponent Script")]
public class MyEditorWindow : EditorWindow
{
    [MenuItem("My Menu/ShowMyWindow")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyEditorWindow), false, "My Window");
    }

    [MenuItem("My Menu/Add MyComponent",true)]
    public static bool ValidateAddMyComponent()
    {
        if (Selection.activeGameObject == null)
            return false;
        else
            return true;
    }

    [MenuItem("My Menu/Add MyComponent")]
    public static void AddMyComponent()
    {
        if(Selection.activeGameObject != null)
        {
            Selection.activeGameObject.AddComponent<MyComponent>();
        }
    }


    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Button("수라바보");
        GUILayout.Button("졸려");
        GUILayout.Button("나도");
        GUILayout.EndHorizontal();

        //Gui -> 내가 위치 수동으로 지정해줘야함
        //guiLayout -> 자동배치

        Rect rectGUI = new Rect(100,200,200,30);

        if(Selection.activeGameObject == null)
        {
            GUI.Label(rectGUI, "NoSelection");
        }
        else
        {
            Rect rectGUI_2 = new Rect(100, 200, 200, 30);
            if(GUI.Button(rectGUI_2, "GUI GameObject name") == true)
            {
                Selection.activeGameObject.AddComponent<MyComponent>();
            }

        }


        if(Event.current.button ==1)
        {
            if(Event.current.type == EventType.MouseUp)
            {
                Debug.Log("수라야ㅏㅏㅏ");
                GenericMenu contexMenu = new GenericMenu();

                contexMenu.AddItem(new GUIContent("Menu 1"), false, DoMenu1);
                contexMenu.AddItem(new GUIContent("Menu 2"), false, DoMenu2);

                contexMenu.ShowAsContext();
            }
        }

    }


    void DoMenu1()
    {
        Debug.Log("menu1111111111");

    }

    void DoMenu2()
    {
        Debug.Log("menu22222222222222");
    }
}


