using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.Animations;

public class CanFlip : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject contextPrompt;
    public bool canFlip = false;

    private StarterAssetsInputs _inputs;
    private Animator _anim;
    private Rigidbody _rb;

    private Vector3 newSize;
    private bool isChanging;
    private Vector3 offset;

    [SerializeField]
    private float flipForce = 500f;
    [SerializeField]
    private float flipRadius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        SetPrompt(false);
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _inputs = GetComponent<StarterAssetsInputs>();
        offset = new Vector3(0.01f, 0.01f, 0.01f);
        flipRadius = 5 * gameManager.level;
        flipForce = 500 * gameManager.level;
    }

    private void Update()
    {
        if(flipRadius != 5 * gameManager.level)
        {
            flipRadius = 5 * gameManager.level;
            flipForce = 500 * gameManager.level;
        }
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
            _anim.SetTrigger("Flip");
            _inputs.flip = false;
            isChanging = true;
            StartCoroutine(Flip(other.gameObject));

        }
    }

    private void SetPrompt(bool newState)
    {
        contextPrompt.SetActive(newState);
    }

    private IEnumerator Flip(GameObject house)
    {
        if(house.name == "BarbieHouse")
        {
            GameObject.FindWithTag("Door").GetComponent<Rigidbody>().isKinematic = false;
        }

        yield return new WaitForSeconds(.5f);
        var rb = house.GetComponent<Rigidbody>();
        var flippable = house.GetComponent<Flippable>();
        flippable.flipped = true;
        rb.isKinematic = false;
        rb.AddExplosionForce(flipForce, flippable.explosionPoint.position, flipRadius);
        flippable.EnableDisableLookAt(false);
        if (isChanging)
        {
            newSize = transform.localScale * 2f;
            isChanging = false;
        }

        while (transform.localScale.x <= (newSize - offset).x)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newSize, 1f * Time.deltaTime);
            yield return null;
        }
        _rb.mass *= 2;

        StopAllCoroutines();
        //flippable.Invoke("EnableKinematics", 2f);
        
    }
}
