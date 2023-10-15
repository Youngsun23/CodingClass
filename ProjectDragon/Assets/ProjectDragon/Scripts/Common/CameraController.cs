using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Dragon
{
    public enum IngameCameraType
    {
        None = 0,
        FollowCamera,
        FreelookCamera,
    }

    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance { get; private set; } = null;

        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public CinemachineVirtualCamera FollowCamera { get; private set; }
        [field: SerializeField] public CinemachineFreeLook FreeLookCamera { get; private set; }


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SetCameraActive(IngameCameraType.FreelookCamera);
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void SetCameraActive(IngameCameraType cameraType)
        {
            switch (cameraType)
            {
                case IngameCameraType.FollowCamera:
                    FollowCamera.gameObject.SetActive(true);
                    FreeLookCamera.gameObject.SetActive(false);
                    break;
                case IngameCameraType.FreelookCamera:
                    FollowCamera.gameObject.SetActive(false);
                    FreeLookCamera.gameObject.SetActive(true);
                    break;
            }
        }
    }
}

