using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour {

    public float speed = 1.0f;
    public float rotateSpeed = 0.5f;
    public float mouseMovement;

    void Start()
    {
        this.transform.localPosition = new Vector3(256f, 300f, 20f);
        this.transform.localRotation *= Quaternion.AngleAxis(40f, Vector3.right);        
    }
    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.W)){
            //keyboard w move forward the camera
            this.transform.Translate(Vector3.forward * speed);
        }
        if(Input.GetKey(KeyCode.S)){
            //keyboard s move backward the camera   
            this.transform.Translate(Vector3.back * speed);
        }
        if(Input.GetKey(KeyCode.A)){
            //keyboard a move left the camera
            this.transform.Translate(Vector3.left * speed);
        }
        if(Input.GetKey(KeyCode.D)){
            //keyboard a move right the camera
            this.transform.Translate(Vector3.right * speed);
        }
        if(Input.GetKey(KeyCode.Q)){
            //keyboard q roll the camera
            this.transform.localRotation *= Quaternion.AngleAxis(rotateSpeed, Vector3.forward);
        }
        if(Input.GetKey(KeyCode.E)){
            //keyboard e roll the camera
            this.transform.localRotation *= Quaternion.AngleAxis(rotateSpeed, Vector3.back);
        }
        //Control the camera pitch and yaw by click and hold the mouse
        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    this.transform.localRotation *= Quaternion.AngleAxis(rspeed, Vector3.right);
        //}
        //if (Input.GetKey(KeyCode.Mouse1))
        //{
        //    this.transform.localRotation *= Quaternion.AngleAxis(rspeed, Vector3.left);
        //}

        //Control the camera pitch and yaw by moving the mouse
        if (Input.mousePresent == true)
        {
            mouseMovement = Input.GetAxis("Mouse Y") * rotateSpeed * 3;
            this.transform.Rotate(-mouseMovement, 0, 0);
        }
    }

}
