using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CurlingSweeperController : MonoBehaviour
{

    /// <summary>
    /// Obstacle Type Definitions
    /// TODO: revise to a string based methodology?
    /// </summary>
    /// 

    public string obstacleType = "OBSTACLE_SNOW";
    public bool isObstacleOil;
    public bool isObstacleTrash;
    public bool isObstacleSnow;
    public bool isObstacleBoost;
    public bool isObstacleBomb;
    public bool isObstacleWall;
    public bool isObstacleStone;
    /// <summary>
    /// Set Game Object
    /// </summary>
    private GameObject currentObstacle;

    /// <summary>
    /// Obstacle Position Definitions
    /// </summary>
    private double positionX;
    private double positionY;
    private double positionZ;
    private double scaleX;
    private double scaleY;
    private double scaleZ;
    
    /// <summary>
    /// Obstacle Type Definitions
    /// </summary>

    void Start()
    {
        
    }


    void Instatiate(
        GameObject obj,
        // position coordinates
        // scale coordinates
    ){
        // SetObstacleType(currentObstacle)
        // SetCoursePlacement(positionX, positionY, positionZ, scaleX, scaleY, scaleZ);
    }

    /// <summary>
    /// Set the type of obstacle
    /// </summary>
    void SetObstacleType(
        GameObject obj
        /// string definine obstacle type
    ){
        currentObstacle = obj;
        // need a switch statement to set the obstacle type

    }


    /// <summary>
    /// Programmatically set the position of this obstacle, based on other scripts
    /// </summary>
    void SetCoursePlacement(
        double px,
        double py,
        double pz,
        double sx = 1,
        double sy = 1,
        double sz = 1,
    ){
        positionX = px;
        positionY = py;
        positionZ = pz;
        scaleX = sx;
        scaleY = sy;
        scaleZ = sz;
        // Set the course placement
    }   

    // If this object comes in collision with another, do something
    void OnTriggerEnter(
        Collider other
    ) {


    }



 


}