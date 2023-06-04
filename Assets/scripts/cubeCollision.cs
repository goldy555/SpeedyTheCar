using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cubeCollision : MonoBehaviour
{
     private int points = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spawnable"))
        {
            scoreCount scoreManager = FindObjectOfType<scoreCount>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(points);
            }

            Destroy(gameObject);
        }
    }
}

