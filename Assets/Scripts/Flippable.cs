using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Flippable : MonoBehaviour
{

    private LookAtConstraint lookAt;
    private Rigidbody rb;

    public Transform explosionPoint;
    public bool flipped = false;
    public bool canFlip;
    public int houseNumber;

    // Start is called before the first frame update
    void Start()
    {
        lookAt = GetComponentInChildren<LookAtConstraint>();
        rb = GetComponent<Rigidbody>();
        if (houseNumber == 0)
            canFlip = true;
        else
            canFlip = false;
    }

    private void Update()
    {
        if (houseNumber == 0)
            canFlip = true;
        else if (GameManager.Instance.housesFlipped[houseNumber - 1] == true)
            canFlip = true;
    }

    public void EnableDisableLookAt(bool newState)
    {
        lookAt.enabled = newState;
    }

    public void EnableKinematics()
    {
        //rb.isKinematic = true;
        rb.mass = 50;
    }
}
