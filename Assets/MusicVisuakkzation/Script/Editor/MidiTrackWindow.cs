using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class MidiTrackWindow : EditorWindow {

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

        if (_midi == null)
            return;

        Rect rect;
        float titleHeight = 30f;
        float musicalScaleWidth = 60f;
        float timeHeight = 50f;
        //title Area
        rect = new Rect(0, 0, position.width, titleHeight);
        GUI.Box(rect, "");
        GUI.BeginGroup(rect);
        //GUILayout.BeginArea(rect);
        DrawTitleArea(position.width, titleHeight);        //타이틀
        GUI.EndGroup();
        //GUILayout.EndArea();

        // Draw Musical Scale Area
        rect = new Rect(0, titleHeight, musicalScaleWidth, position.height - titleHeight);
        GUI.Box(rect, "");
        GUI.BeginGroup(rect);
        DrawMusicalScaleArea(rect.width, rect.height);
        GUI.EndGroup();

        // Draw Track List Area
        rect = new Rect(position.width * 0.75f, titleHeight, position.width * 0.25f, position.height - titleHeight);
        GUI.Box(rect, ""); 
        GUILayout.BeginArea(rect);
        DrawTrackListArea();
        GUILayout.EndArea();

        // Draw Time Area
        rect = new Rect(musicalScaleWidth, titleHeight, position.width - musicalScaleWidth - position.width * 0.25f, timeHeight);
        GUI.Box(rect, "");
        GUI.BeginGroup(rect);
        DrawTimeArea(rect.width, rect.height);
        GUI.EndGroup();


        // Draw Nore Area
        rect = new Rect(musicalScaleWidth, titleHeight + timeHeight, position.width - musicalScaleWidth - position.width * 0.25f, position.height - titleHeight - timeHeight);
        GUI.Box(rect, "");
        GUI.BeginGroup(rect);
        DrawNoteArea(rect.width, rect.height);
        GUI.EndGroup();
    }


    void DrawTitleArea(float width, float height)
    {
        Rect rect = new Rect(0, 0, width, height);
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;
        style.fontStyle = FontStyle.Italic;
        GUI.Label(rect,Path.GetFileNameWithoutExtension(_midi.fileName));
    }



    void DrawMusicalScaleArea(float width, float height)
    {
        Rect rect = new Rect(0, 0, width, height);
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;


        Rect rect2 = new Rect(0, 0, width, GridY);
        for(int i=0;i<128;i++)
        {
            GUI.Box(rect2, "", style);
            GUI.Label(rect2, NoteNumberToString(i));
            rect2.y += GridY;
        }
    }

    void DrawTrackListArea()
    {
        if (_enableTracks == null)
        {
            _enableTracks = new bool[_midi.tracks.Length];
            for(int i=0;i<_enableTracks.Length; i++)
            {
                _enableTracks[i] = true;
            }
        }

        _trackListScroll = EditorGUILayout.BeginScrollView(_trackListScroll);
        for (int i = 0; i < _midi.tracks.Length;i++ )
        {
            _enableTracks[i] = GUILayout.Toggle(_enableTracks[i], _midi.tracks[i].InstrumentName);
        }

        EditorGUILayout.EndScrollView();
    }



    void DrawTimeArea(float width, float height)
    {
    }

    void DrawNoteArea(float width, float height)
    {
        Rect viewRect = new Rect(0, 0, width, height);
        Rect contextRect = new Rect(0, 0, GridX * _midi.totalTime * 1000F, GridY * 128);
        _noteAreaScroll = GUI.BeginScrollView(viewRect, _noteAreaScroll, contextRect);

        Rect scrollRect = new Rect(_noteAreaScroll.x, _noteAreaScroll.y, width, height);
        Texture2D boxTexture = new Texture2D(1, 1);
        boxTexture.SetPixel(0, 0, Color.white);
        boxTexture.Apply();
        for (int i = 0; i < _midi.tracks.Length; i++)
        {
            if (_enableTracks[i] == false)
                continue;

            MidiNote[] notes = _midi.tracks[i].Notes.ToArray();
            for (int j = 0; j < notes.Length; j++)
            {
                float sTime = notes[j].StartTime * _midi.pulseTime * 1000f;
                float eTime = notes[j].EndTime * _midi.pulseTime * 1000f;
                Rect noteRect = new Rect(GridX * sTime, GridY * notes[j].Number, GridX * (eTime - sTime), GridY);
                if (scrollRect.Overlaps(noteRect) == true)
                    GUI.Box(new Rect(noteRect.x - 1, noteRect.y - 1, noteRect.width + 2, noteRect.height + 2), "");
                GUI.DrawTexture(noteRect, boxTexture);
            }
        }
        GUI.EndScrollView();
    }

    string NoteNumberToString(int number)
    {
        int index = (int)(number % 12);
        int octave = (int)(number / 12);
        string noteString;

        switch (index)
        {
            case 0:
                return string.Format("C{0:d}", octave);

            case 1:
                return string.Format("C#{0:d}", octave);

            case 2:
                return string.Format("D{0:d}", octave);

            case 3:
                return string.Format("D#{0:d}", octave);

            case 4:
                return string.Format("E{0:d}", octave);

            case 5:
                return string.Format("F{0:d}", octave);

            case 6:
                return string.Format("F#{0:d}", octave);

            case 7:
                return string.Format("G{0:d}", octave);

            case 8:
                return string.Format("G#{0:d}", octave);

            case 9:
                return string.Format("A{0:d}", octave);

            case 10:
                return string.Format("A#{0:d}", octave);

            case 11:
                return string.Format("B{0:d}", octave);

            case 12:
                return string.Format("B#{0:d}", octave);

            default:
                break;
        }

        return "1";
    }
}
