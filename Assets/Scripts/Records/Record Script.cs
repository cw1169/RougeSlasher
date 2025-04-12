using System.Data.Common;
using UnityEngine;

public class RecordScript : MonoBehaviour
{

    public Record record;
    public AudioManager audioManager;
    public int recordID;
    public bool isPickedUp = false;


    private Collider2D recordCollider;

    void Start()
    {
        recordCollider = GetComponent<Collider2D>();
        audioManager = AudioManager.Instance;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPickedUp = true;
            recordCollider.enabled = false; // Disable the collider to prevent multiple pickups
            Debug.Log("Record picked up: " + record.name);
            audioManager.setAudioClip(record.recordAudioClip);
            Destroy(gameObject);
        }
    }
}
