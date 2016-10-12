using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PathNode))]
public class PathNodeEditor : Editor
{
    void OnSceneGUI()
    {
        PathNode pathNode = (PathNode)target;

        Handles.color = Color.magenta;
        Handles.Label(pathNode.transform.position, pathNode.name);
    }
}
