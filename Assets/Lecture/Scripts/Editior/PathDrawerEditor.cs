using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PathDrawer))]
public class PathDrawerEditor : Editor
{

    void OnSceneGUI()
    {
        PathDrawer pathDrawer = (PathDrawer)target;

        Texture2D texture = new Texture2D(1,1);
        texture.SetPixel(0,0,Color.white);

        Handles.color = Color.blue;
        for(int i=0;i<pathDrawer.Nodes.Length-1; i++)
        {
            GameObject startNode = pathDrawer.Nodes[i];
            GameObject endNode = pathDrawer.Nodes[i + 1];

            Vector3 startpos = startNode.transform.position;
            Vector3 endpos = endNode.transform.position;

            Vector3 startTangent = startpos + startNode.transform.forward;
            Vector3 endTangent = endpos + endNode.transform.forward;

            Handles.DrawBezier(startpos,endpos, startTangent,endTangent,Color.green,texture,3f);
        }
    }
}
