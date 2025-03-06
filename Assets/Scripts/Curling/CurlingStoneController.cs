using UnityEngine;


public class CurlingStoneController : MonoBehaviour
{

    private double speed;
    private double angle;

    private double movementX;
    private double movementY;
    private double movementZ;
    private double angleX;
    private double angleY;
    private double angleZ;

    /// <summary>
    /// List of powers that can be applied to the stone
    /// </summary>
    /// 


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }

    public void SetSpeed(){
        
    }

    public void AdjustSpeed(
        // speed change
        // time of effect
    ){
        // increase speed
        // decrease speed
    }

    public void AdjustAngle(
        // angle change
        // time of effect
    ){
        // increase angle
        // decrease angle
    }

    public void AdjustHeight(
        
    ){
        // increase height
        // decrease height
    }




    void OnTriggerEnter(Collider other) 
   {

        /*
            if the sweeper encounters one of the walled snow edges, 
            the sweeper who runs into it should jump over the obstacle,
            and be prevented from moving much futher beyond it
        */
        
       if (other.gameObject.CompareTag("wall_curling_snow")) 
       {
            // The stone has hit a wall, and it should slow down
            // AdjustSpeed();
            // AdjustAngle();
            
       }
       else if (other.gameObject.CompareTag("curling_landing_area")) 
       {
            // The stone has entered the landing area
            // AdjustSpeed();
            
            
       }
       else if (other.gameObject.CompareTag("curling_obstacle_oil")) 
       {
            // The stone has encountered an oil spill
            // AdjustSpeed();
            // AdjustAngle();
            
       }
       else if (other.gameObject.CompareTag("curling_obstacle_trash")) 
       {
            // The stone has encountered a trash obstacle

            // AdjustSpeed();
            // AdjustAngle();
            
       }
       else if (other.gameObject.CompareTag("curling_obstacle_stone")) 
       {
            // The stone has encountered a stone obstacle
            
       }
       else if (other.gameObject.CompareTag("curling_obstacle_ice")) 
       {
            // The stone has encountered an ice obstacle
            
       }
       else if (other.gameObject.CompareTag("curling_obstacle_snow")) 
       {
            // The stone has encountered a snow obstacle
            
       }
       else if (other.gameObject.CompareTag("curling_obstacle_trash"))
       {
            // The stone has encountered a trash obstacle
            
       }
       else { 

       }
   
   }
}
