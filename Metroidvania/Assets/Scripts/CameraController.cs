using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private PlayerController playerControllerREF;
    private float halthHeight;
    private float halthWidth;

    [SerializeField]private Vector3 cameraOffset;
    [SerializeField]private BoxCollider2D boundBox;

    // Start is called before the first frame update
    void Start()
    {
        
        playerControllerREF = FindObjectOfType<PlayerController>();
        halthHeight = Camera.main.orthographicSize;
        halthWidth = halthHeight * Camera.main.aspect;

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }


    void FollowPlayer()
    {
        if (playerControllerREF != null)
        {
            Vector3 cameraPosition = new Vector3(Mathf.Clamp(playerControllerREF.transform.position.x, boundBox.bounds.min.x + halthWidth, boundBox.bounds.max.x - halthWidth),
                Mathf.Clamp(playerControllerREF.transform.position.y, boundBox.bounds.min.y + halthHeight, boundBox.bounds.max.y -halthHeight), cameraOffset.z);

            transform.position = cameraPosition;
        }
    }

}
