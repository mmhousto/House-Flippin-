using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.Animations;

public class CanFlip : MonoBehaviour
{

    public GameObject contextPrompt;
    public bool canFlip = false;

    private StarterAssetsInputs _inputs;
    private Animator _anim;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        SetPrompt(false);
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _inputs = GetComponent<StarterAssetsInputs>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flippable") && other.GetComponent<Flippable>().flipped == false)
        {
            SetPrompt(true);
            canFlip = true;
            other.GetComponent<Flippable>().EnableDisableLookAt(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flippable"))
        {
            SetPrompt(false);
            canFlip = false;
            other.GetComponent<Flippable>().EnableDisableLookAt(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Flippable") && _inputs.flip && other.attachedRigidbody.mass <= _rb.mass && other.GetComponent<Flippable>().flipped == false)
        {
            Flip(other.gameObject);
        }
    }

    private void SetPrompt(bool newState)
    {
        contextPrompt.SetActive(newState);
    }

    private void Flip(GameObject house)
    {
        var rb = house.GetComponent<Rigidbody>();
        var flippable = house.GetComponent<Flippable>();
        flippable.flipped = true;
        rb.isKinematic = false;
        rb.AddExplosionForce(500f, flippable.explosionPoint.position, 5f);
        flippable.EnableDisableLookAt(false);
        _anim.SetTrigger("Flip");
        _inputs.flip = false;
        flippable.Invoke("EnableKinematics", 2f);
    }
}
