using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecordDatabase", menuName = "Scriptable Objects/RecordDatabase")]
public class RecordDatabase : ScriptableObject
{
    public List<Record> records; // Array of Record objects

    public AudioClip GetAudioClip(int ID){
        AudioClip foundAudioClip = records.Find(record => record.ID == ID)?.recordAudioClip; // Initialize audioClip to null
        if (foundAudioClip == null) // Check if the audioClip is null
        {
            Debug.LogError($"AudioClip with ID {ID} not found in the database."); // Log an error message if not found
        }
        else
        {
            Debug.Log($"AudioClip with ID {ID} found in the database."); // Log a success message if found
        }
        return foundAudioClip; // Return the found audioClip or null if not found
    }
}
