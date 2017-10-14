using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    [SerializeField]
    float rotateSpeedX = 100f;
    float rotateSpeedY = 100f;
    float rotateSpeedZ = 100f;

    float rotateAngleX;
    float rotateAngleY;
    float rotateAngleZ;

    void Start () {
        if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            rotateAngleX = 0;
            rotateAngleY = 0;
            rotateAngleZ = 0;
            transform.rotation = Quaternion.Euler(rotateAngleX, rotateAngleY, rotateAngleZ);
        }
       
    }
	
	void Update () {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                rotateAngleX -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeedY;
                rotateAngleY += Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeedY;

                while (rotateAngleX > 360)
                {
                    rotateAngleX -= 360;
                }
                while (rotateAngleX < -360)
                {
                    rotateAngleX += 360;
                }
                while (rotateAngleY > 360)
                {
                    rotateAngleY -= 360;
                }
                while (rotateAngleY < -360)
                {
                    rotateAngleY += 360;
                }

                transform.rotation = Quaternion.Euler(rotateAngleX, rotateAngleY, rotateAngleZ);

            }else if (Input.GetKey(KeyCode.LeftControl))
            {
                rotateAngleZ += Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeedZ;

                while (rotateAngleZ > 360)
                {
                    rotateAngleZ -= 360;
                }
                while (rotateAngleZ < -360)
                {
                    rotateAngleZ += 360;
                }

                transform.rotation = Quaternion.Euler(rotateAngleX, rotateAngleY, rotateAngleZ);
            }
        }
        
    }
}
