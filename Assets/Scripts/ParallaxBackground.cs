using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
//{

//    private GameObject cam;

//    [SerializeField] private float parallaxEffect;

//    private float xPosition;

//    private float length;
//    void Start()
//    {
//        cam = GameObject.Find("Main Camera");


//        length = GetComponent<SpriteRenderer>().bounds.size.x;
//        xPosition = transform.position.x;
//    }


//    void Update()
//    {
//        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);

//        float distanceToMove = cam.transform.position.x * parallaxEffect;

//        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

//        if (distanceMoved > xPosition + length)
//        {
//            xPosition = xPosition + length;
//        }
//        else if (distanceMoved < xPosition - length)
//        {
//            xPosition -= length;
//        }
//    }
//}
{
    private GameObject cam;

    [SerializeField] private float parallaxEffectX = 0.5f;
    [SerializeField] private float parallaxEffectY = 0.5f;

    private Vector2 startPosition;
    private float lengthX;
    private float lengthY;

    void Start()
    {
        cam = GameObject.Find("Main Camera");


        startPosition = transform.position;


        var bounds = GetComponent<SpriteRenderer>().bounds;
        lengthX = bounds.size.x;
        lengthY = bounds.size.y;
    }

    void Update()
    {
        float distanceMovedX = cam.transform.position.x * (1 - parallaxEffectX);
        float distanceToMoveX = cam.transform.position.x * parallaxEffectX;

        float distanceMovedY = cam.transform.position.y * (1 - parallaxEffectY);
        float distanceToMoveY = cam.transform.position.y * parallaxEffectY;

        transform.position = new Vector3(startPosition.x + distanceToMoveX, startPosition.y + distanceToMoveY, transform.position.z);


        if (distanceMovedX > startPosition.x + lengthX)
            startPosition.x += lengthX;
        else if (distanceMovedX < startPosition.x - lengthX)
            startPosition.x -= lengthX;

        if (distanceMovedY > startPosition.y + lengthY)
            startPosition.y += lengthY;
        else if (distanceMovedY < startPosition.y - lengthY)
            startPosition.y -= lengthY;
    }
}