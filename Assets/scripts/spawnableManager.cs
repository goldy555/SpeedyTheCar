using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class spawnableManager : MonoBehaviour
{
    [SerializeField] private ARRaycastManager m_RaycastManager;
    [SerializeField] private GameObject spawnablePrefab;
    private Camera arCam;
    private GameObject spawnedObject;
    private bool hasSpawnedObject;

    private void Start()
    {
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        spawnedObject = null;
        hasSpawnedObject = false;
    }

    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began && !hasSpawnedObject)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            Ray ray = arCam.ScreenPointToRay(touch.position);

            if (m_RaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                spawnedObject = Instantiate(spawnablePrefab, hitPose.position, hitPose.rotation);
                hasSpawnedObject = true;
            }
        }
    }
}