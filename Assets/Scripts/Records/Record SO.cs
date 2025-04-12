using UnityEngine;

[CreateAssetMenu(fileName = "New Record", menuName = "Scriptable Objects/New Record")]
public class Record : ScriptableObject
{
    public int ID; // unique identifier for the record
    public new string name;
    public string recordDescription;
    public int attackIncrease;
    public int defenseIncrease;
    public int speedIncrease;
    public int maxHealthIncrease;
    public Sprite recordImage; // sprite of the record
    public AudioClip recordAudioClip; // audio clip to play the sound
}
