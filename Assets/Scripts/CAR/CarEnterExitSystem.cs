using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CarEnterExitSystem : MonoBehaviour
{
    public MonoBehaviour CarController;
    public Transform Car;
    public Transform Player;

    public Rigidbody rbCar;

    [Header("Cameras")]
    public GameObject PlayerCam;
    public GameObject CarCam;

    public GameObject DriveUi;

    bool Candrive;

    public bool isDriving;

   public GameObject bobrRide;



    // Start is called before the first frame update
    void Start()
    {
        CarController.enabled = false;
        DriveUi.gameObject.SetActive(false);
        isDriving = false;
        rbCar.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && Candrive)  // Here After Click E button and trigger is true player is driving
        {
            rbCar.isKinematic = false;
            CarController.enabled = true; // After Click E button Car Controller Script is enabled

            


            DriveUi.gameObject.SetActive(false);


            // Here we parent Car with player
            Player.transform.SetParent(Car);
            Player.gameObject.SetActive(false);
            bobrRide.SetActive(true);

            // Camera
            PlayerCam.gameObject.SetActive(false);
            CarCam.gameObject.SetActive(true);


        }

        if (Input.GetKeyDown(KeyCode.G))
        {

            rbCar.isKinematic = true;
            CarController.enabled = false; // After Click G button Car Controller Script is disable


            // Here We Unparent the Player with Car
            Player.transform.SetParent(null);
            Player.gameObject.SetActive(true);
            bobrRide.SetActive(false);

            // Here If Player Is Not Driving So PlayerCamera turn On and Car Camera turn off

            PlayerCam.gameObject.SetActive(true);
            CarCam.gameObject.SetActive(false);
        }
    }


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            DriveUi.gameObject.SetActive(true);
            Candrive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            DriveUi.gameObject.SetActive(false);
            Candrive = false;
        }
    }
}
