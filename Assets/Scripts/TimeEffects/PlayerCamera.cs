using Cinemachine;
using RichiGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace asset
{
    public class PlayerCamera : MonoBehaviour
    {
        public float SensX;
        public float SensY;

        public Transform Orientation;
        public CinemachineFreeLook FreeLookCamera;
        public CinemachineFreeLook TimeStoppedCarema;

        float xRotation;
        float yRotation;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            if (TimeController.Instance.IsRewinding == true)
            {
                if (TimeStoppedCarema.Priority != 10)
                {
                    TimeStoppedCarema.m_XAxis.Value = FreeLookCamera.m_XAxis.Value;
                    TimeStoppedCarema.m_YAxis.Value = FreeLookCamera.m_YAxis.Value;
                    TimeStoppedCarema.Priority = 10;
                    FreeLookCamera.Priority = 1;
                }
                return;
            }

            if (FreeLookCamera.Priority != 10)
            {
                TimeStoppedCarema.m_XAxis.Value = FreeLookCamera.m_XAxis.Value;
                TimeStoppedCarema.m_YAxis.Value = FreeLookCamera.m_YAxis.Value;
                FreeLookCamera.Priority = 10;
                TimeStoppedCarema.Priority = 1;
            }

            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            Orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
