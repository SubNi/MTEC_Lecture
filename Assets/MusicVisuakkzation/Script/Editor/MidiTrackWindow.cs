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
        rect = new Rect(0, titleHeight + timeHeight, musicalScaleWidth, position.height - titleHeight);
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

        int sNote = (int)(_noteAreaScroll.y / GridY);
        int eNote = (int)((_noteAreaScroll.y + height) / GridY);
        float sY = -(_noteAreaScroll.y % GridY);
        float eY = 0;

        Rect rect2 = new Rect(0, sY, width, GridY);

        for(int i=sNote;i<=eNote;i++)
        {
            GUI.Box(rect2, "");
            GUI.Label(rect2, NoteNumberToString(i), style);
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
        Rect areaRect = new Rect(0,0,width,height);
        int sTime = (int)(_noteAreaScroll.x / GridX);
        int eTime = (int)((_noteAreaScroll.x + width) / GridX);
        // Draw Text Area
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleLeft;

        int sText = (int)sTime/100;
        int eText = (int)eTime/100;
        float TextW = GridX * 100;
        float TextH = height * 0.4f;
        float sTextX = -(_noteAreaScroll.x % TextW);

        Rect textRect = new Rect(sTextX, 0, TextW, TextH);
        //Rect textRect = new Rect(0,0,GridX * 100,height*0.4f);
        for(int i=sText;i<eText;i++)
        {
            GUI.Label(textRect, string.Format("{0:f1}", i * 0.1f), style);
            textRect.x += TextW;
        }
        // Draw Line Area
        int sLine = (int)(sTime / 10);
        int eLine = (int)(eTime / 10);
        float LineW = GridX * 10;
        float LineH = height - TextH;
        float sLineX = (_noteAreaScroll.x % LineW);
        Texture2D lineTexture = new Texture2D(1, 1);
        lineTexture.SetPixel(0, 0, Color.black);
        lineTexture.Apply();

        Rect pixelRect = new Rect(0, 0, 1, 1);
        int longLineY = (int)TextH;
        int shortLineY = (int)(longLineY + LineH * 0.5f);
        int eLineY = (int)(height);

        //Rect LineRect = new Rect(0,0,GridX * 10,height*0.6f);
        
        for(int i=sLine;i<eLine;i++)
        {
            pixelRect.x = sLineX;

            if((i%10) == 0) // Long Line
            {
                for (int j = longLineY; j <= eLineY; j++)
                {
                    pixelRect.y = j;

                    GUI.DrawTexture(pixelRect, lineTexture);
                }
            }
            else//Short Line
            {
                for (int j = shortLineY; j <= eLineY; j++)
                {
                    pixelRect.y = j;

                    GUI.DrawTexture(pixelRect, lineTexture);
                }
            }
            sLineX += LineW;
        }
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
