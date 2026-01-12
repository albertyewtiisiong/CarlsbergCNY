using UnityEngine;
using System.Collections;
// Make sure you have the Kinect scripts imported for this to work
// Usually requires: using com.rfilkov.kinect; or similar namespace if using that package

public class KinectInput : MonoBehaviour
{
    public static KinectInput Instance;

    [Header("Settings")]
    public float jumpThreshold = 0.15f; // How high (in meters) to jump to trigger
    public bool isKinectConnected = false;

    private KinectManager manager;
    private float initialSpineY = 0f;
    private bool isCalibrated = false;

    public bool jumpDetected = false;

    void Awake() { Instance = this; }

    void Start()
    {
        manager = KinectManager.Instance; // From the downloaded asset
    }

    void Update()
    {
        jumpDetected = false; // Reset every frame

        if (manager && manager.IsInitialized())
        {
            isKinectConnected = true;
            long userId = manager.GetPrimaryUserID();

            if (userId != 0) // We found a user
            {
                // Get Spine Position (Hips)
                Vector3 spinePos = manager.GetJointPosition(userId, KinectInterop.JointType.SpineBase);

                // Simple Calibration: First frame we see a user, set that as "Floor"
                if (!isCalibrated && spinePos.y != 0)
                {
                    initialSpineY = spinePos.y;
                    isCalibrated = true;
                }

                // Check for Jump
                if (isCalibrated && spinePos.y > (initialSpineY + jumpThreshold))
                {
                    jumpDetected = true;
                }
            }
        }
    }

    // Call this to reset height (e.g., between games)
    public void Recalibrate() { isCalibrated = false; }
}