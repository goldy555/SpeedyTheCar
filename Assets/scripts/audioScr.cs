using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioScr : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    
}
