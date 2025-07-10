// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.InputSystem;

// public class ThumbstickController : MonoBehaviour
// {
//     // Input Actions reference
//     private XRIDefaultInputActions inputActions;

//     [Header("Camera Control")]
//     public Transform cameraTransform;
//     public float panSpeed = 0.1f;
//     public float zoomSpeed = 0.1f;

//     [Header("Slider Control")]
//     public Slider targetSlider;
//     public float sliderSpeed = 0.01f;

//     [Header("Settings")]
//     public float deadzone = 0.1f;

//     private void OnEnable()
//     {
//         inputActions = new XRIDefaultInputActions();
//         inputActions.Player.Enable();
//         inputActions.UI.Enable();
//     }

//     private void OnDisable()
//     {
//         inputActions.Player.Disable();
//         inputActions.UI.Disable();
//     }

//     private void Update()
//     {
//         HandleRightStickCamera();
//         HandleLeftStickSlider();
//     }

//     private void HandleRightStickCamera()
//     {
//         Vector2 rightStick = inputActions.Player.RightStick.ReadValue<Vector2>();

//         // Deadzone filtering
//         if (Mathf.Abs(rightStick.x) > deadzone)
//         {
//             Vector3 panDirection = cameraTransform.right * rightStick.x * panSpeed;
//             cameraTransform.position += panDirection;
//         }

//         if (Mathf.Abs(rightStick.y) > deadzone)
//         {
//             Vector3 zoomDirection = cameraTransform.forward * rightStick.y * zoomSpeed;
//             cameraTransform.position += zoomDirection;
//         }
//     }

//     private void HandleLeftStickSlider()
//     {
//         Vector2 leftStick = inputActions.UI.LeftStick.ReadValue<Vector2>();

//         // Deadzone filtering
//         if (Mathf.Abs(leftStick.y) > deadzone)
//         {
//             float newValue = targetSlider.value + (leftStick.y * sliderSpeed);
//             newValue = Mathf.Clamp(newValue, targetSlider.minValue, targetSlider.maxValue);
//             targetSlider.value = newValue;
//         }
//     }
// }
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ThumbstickController : MonoBehaviour
{
    private XRIDefaultInputActions inputActions;

    [Header("Camera Control (Right Thumbstick)")]
    public Transform cameraTransform;
    public float panSpeed = 0.1f;
    public float zoomSpeed = 0.1f;

    [Header("Control Settings")]
    public float sliderSpeed = 0.01f;
    public float deadzone = 0.1f;

    [Header("GameObjects with Sliders")]
    public GameObject[] graphObjects; // Each one has its own slider
    

    private void OnEnable()
    {
        inputActions = new XRIDefaultInputActions();
        inputActions.Player.Enable();
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.UI.Disable();
    }

    private void Update()
    {
        HandleRightStickCamera();
        HandleLeftStickSlider();
    }

    private void HandleRightStickCamera()
    {
        Vector2 rightStick = inputActions.Player.RightStick.ReadValue<Vector2>();

        if (Mathf.Abs(rightStick.x) > deadzone)
        {
            cameraTransform.position += cameraTransform.right * rightStick.x * panSpeed;
        }

        if (Mathf.Abs(rightStick.y) > deadzone)
        {
            cameraTransform.position += cameraTransform.forward * rightStick.y * zoomSpeed;
        }
    }

    private void HandleLeftStickSlider()
    {
        Vector2 leftStick = inputActions.UI.LeftStick.ReadValue<Vector2>();
        if (Mathf.Abs(leftStick.y) < deadzone) return;

        // Find the currently active graph object
        foreach (GameObject obj in graphObjects)
        {
            if (obj.activeInHierarchy)
            {
                Slider slider = obj.GetComponentInChildren<Slider>();
                if (slider != null)
                {
                    float newValue = slider.value + (leftStick.y * sliderSpeed);
                    slider.value = Mathf.Clamp(newValue, slider.minValue, slider.maxValue);
                }
                break; // Only one active object should exist at a time
            }
        }
    }
}
