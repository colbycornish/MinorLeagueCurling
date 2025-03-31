using UnityEngine;


public class CurlingCameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;

    private string currentPositionType = "STARTING_CAMERA_POSITION";
    private string nextPositionType = "STARTING_CAMERA_POSITION";

    private bool isChangingPosition = false;

    
    /// <summary>
    /// Starting position coordinates
    /// </summary>
    /// public ??? positionStart;
    

    /// <summary>
    /// Ending position coordinates
    /// </summary>
    /// public ??? positionEnd;
    
    /// <summary>
    /// Stone position coordinates
    /// </summary>
    /// public ??? positionStone;
    
    /// <summary>
    /// Obstacle placement position coordinates
    /// </summary>
    /// public ??? positionObstaclePlacement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        
    }

    void GoToStartingPosition(){

    }

    void GoToStonePosition(){

    }

    void GoToFinalCurlingPosition(){

    }

    void GoToCourseViewPosition(){

    }
}
