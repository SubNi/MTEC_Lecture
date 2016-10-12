using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class MidiTrackWindow : EditorWindow
{

    static private MidiAsset _midi;
    private Vector2 _trackListScroll;
    private Vector2 _noteAreaScroll;
    private bool[] _enableTracks;
    private float GridX = 0.5f;
    private float GridY = 30f;

    public static void ShowWindow(MidiAsset Midi)
    {
        _midi = Midi;
        EditorWindow.GetWindow(typeof(MidiTrackWindow), false, "Midi Track");
    }

    void OnGUI()
    {
        Rect rect;
        float titleHeight = 30f;
        float musicalScaleWidth = 60f;
        float timeHeight = 50f;

        //title Area
        rect = new Rect(0, 0, position.width, titleHeight);
        GUI.Box(rect, "");
        GUILayout.BeginArea(rect);
        GUILayout.EndArea();

        // Draw Musical Scale Area
        rect = new Rect(0, titleHeight, musicalScaleWidth, position.height - titleHeight);
        GUI.Box(rect, "");
        GUI.BeginGroup(rect);
        GUI.EndGroup();

        // Draw Track List Area
        rect = new Rect(position.width * 0.75f, titleHeight, position.width * 0.25f, position.height - titleHeight);
        GUI.Box(rect, "");
        GUILayout.BeginArea(rect);
        GUILayout.EndArea();

        // Draw Time Area
        rect = new Rect(musicalScaleWidth, titleHeight, position.width - musicalScaleWidth - position.width * 0.25f, timeHeight);
        GUI.Box(rect, "");
        GUI.BeginGroup(rect);
        GUI.EndGroup();


        // Draw Nore Area
        rect = new Rect(musicalScaleWidth, titleHeight + timeHeight, position.width - musicalScaleWidth - position.width * 0.25f, position.height - titleHeight - timeHeight);
        GUI.Box(rect, "");
        GUI.BeginGroup(rect);
        GUI.EndGroup();
    }
}