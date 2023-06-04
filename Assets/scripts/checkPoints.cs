using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class checkPoints : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject car;
    private int cubeCount = 5; // Number of cubes to spawn at the start
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    private bool isPlaneDetected = false;

    private void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        if (!isPlaneDetected && raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hitResults, TrackableType.PlaneWithinPolygon))
        {
            isPlaneDetected = true;
            SpawnCubes(cubeCount); // Spawn cubes on the detected plane
        }
    }

    private void SpawnCubes(int count)
    {
        Vector3 carPosition = car.transform.position;

        for (int i = 0; i < count; i++)
        {
            if (isPlaneDetected && hitResults.Count > 0)
            {
                Pose hitPose = hitResults[0].pose;
                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                Vector3 randomPosition = hitPose.position + randomOffset;
                Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                GameObject cube = Instantiate(cubePrefab, randomPosition, randomRotation);
                cube.AddComponent<cubeCollision>();
            }
        }
    }
}


