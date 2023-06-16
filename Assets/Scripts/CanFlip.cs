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
    public AnimationCurve easingCurve;

    private StarterAssetsInputs _inputs;
    private ThirdPersonController _playerController;
    private Animator _anim;
    private Rigidbody _rb;

    private Vector3 newSize;
    private bool isChanging;
    private Vector3 offset;
    private float duration = 1.0f;
    private float elapsedTime = 0.0f;

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
        _playerController = GetComponent<ThirdPersonController>();
        offset = new Vector3(0.01f, 0.01f, 0.01f);
        flipRadius = 5 * gameManager.level;
        flipForce = 500 * gameManager.level;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flippable") && other.GetComponent<Flippable>().canFlip == true)
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
        if(other.CompareTag("Flippable") && _inputs.flip && other.attachedRigidbody.mass <= _rb.mass && other.GetComponent<Flippable>().canFlip == true)
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
        
        yield return new WaitForSeconds(.5f);
        var rb = house.GetComponent<Rigidbody>();
        var flippable = house.GetComponent<Flippable>();
        
        rb.isKinematic = false;
        rb.AddExplosionForce(flipForce, flippable.explosionPoint.position, flipRadius);
        flippable.EnableDisableLookAt(false);

        if (flippable.flipped == false)
        {

            if (isChanging)
            {
                newSize = transform.localScale * 2f;
                isChanging = false;
            }

            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration);
            float easedValue = easingCurve.Evaluate(t);

            while (transform.localScale.x <= (newSize - offset).x)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, newSize, easedValue);
                yield return null;
            }

            _rb.mass *= 2;
            flipRadius += 5;
            flipForce += 500;
            _playerController.MoveSpeed += 1;
            _playerController.SprintSpeed += 2.5f;
            _playerController.JumpHeight += 0.5f;

            if (house.name == "BarbieHouse")
            {
                GameObject.FindWithTag("Door").GetComponent<Rigidbody>().isKinematic = false;
            }
            if (house.name == "Shed")
            {
                gameManager.Invoke("NextLevel", 1f);
            }
            if (house.name == "SuburbanHouse")
            {
                gameManager.Invoke("NextLevel", 1f);
            }

            flippable.flipped = true;
            gameManager.housesFlipped[flippable.houseNumber] = true;

        }

        StopAllCoroutines();
        //flippable.Invoke("EnableKinematics", 2f);
        
    }
}
