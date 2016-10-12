using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class MidiAssetImporter : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            //Debug.Log(asset);
            string extension = Path.GetExtension(asset);

            if (extension.Equals(".mid") == true)
            {
                MidiAsset createdAsset = ScriptableObject.CreateInstance<MidiAsset>();

                string newFileName = Path.ChangeExtension(asset, ".asset");

                createdAsset.FileLoad(asset);
                //Midiloadfile

                AssetDatabase.CreateAsset(createdAsset, newFileName);
                AssetDatabase.SaveAssets();
            }
        }
    }
}