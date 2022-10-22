using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{

    public GameManager gameManager;
    private bool hasTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            gameManager.NextLevel();
            hasTriggered = true;
        }
    }
}
