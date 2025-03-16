using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float offset; // offset position of the camera (see whats coming)
    public float offsetSmoothing;
    private Vector3 playerPosition;
    private SpriteRenderer playerSpriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        if(playerSpriteRenderer.flipX){
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        }
        else{
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }

        // Catchup to player position with offset smoothing based on current camera position and new camera position
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);

    }
}
