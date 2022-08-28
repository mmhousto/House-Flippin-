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

    // Start is called before the first frame update
    void Start()
    {
        lookAt = GetComponentInChildren<LookAtConstraint>();
        rb = GetComponent<Rigidbody>();
    }

    public void EnableDisableLookAt(bool newState)
    {
        lookAt.enabled = newState;
    }

    public void EnableKinematics()
    {
        rb.isKinematic = true;
    }
}
