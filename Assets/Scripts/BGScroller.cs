using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ;
    public GameController gameConroller;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        gameConroller = gameConroller.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        if (gameConroller.winGame == true)
        {
            scrollSpeed = -3f;
        }

        transform.position = startPosition + Vector3.forward * newPosition;

    }
}
