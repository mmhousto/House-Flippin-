using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Flippable flippable;
    public Rigidbody door;

    private void Start()
    {
        flippable = GetComponent<Flippable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flippable.flipped && door.isKinematic) door.isKinematic = false;
    }
}
