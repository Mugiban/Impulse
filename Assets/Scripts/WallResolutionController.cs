using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallResolutionController : MonoBehaviour
{
    public Transform leftWall;
    public Transform rightWall;

    private void Start()
    {
        SetResolution();
        Camera.main.orthographicSize = (20.0f / Screen.width * Screen.height / 2.0f);
    }

    void SetResolution()
    {
        float halfWithLeft = leftWall.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        float wallLeftX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        leftWall.transform.position = new Vector3(wallLeftX - halfWithLeft, leftWall.transform.position.y, leftWall.transform.position.z);

        float halfWallRight = rightWall.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        float wallRightX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        rightWall.transform.position = new Vector3(wallRightX +halfWallRight, rightWall.transform.position.y, rightWall.transform.position.z);
    }

}
