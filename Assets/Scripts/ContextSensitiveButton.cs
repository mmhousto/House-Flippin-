using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ContextSensitiveButton : MonoBehaviour
{

    public TextMeshProUGUI promptUGUI;
    public PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePrompt();
    }

    void UpdatePrompt()
    {
        var controlScheme = playerInput.currentControlScheme;
        if (controlScheme == "Gamepad" && promptUGUI.text != "Press X to FLIP!")
        {
            promptUGUI.text = "Press X to FLIP!";
        }
        else if (controlScheme == "KeyboardMouse" && promptUGUI.text != "Press 'E' to FLIP!")
        {
            promptUGUI.text = "Press 'E' to FLIP!";
        }
    }
}
