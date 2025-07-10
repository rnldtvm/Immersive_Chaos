// using UnityEngine;
// using System.Collections.Generic;

// public class LorenzAttractor : MonoBehaviour
// {
//     [Header("Attractor Parameters")]
//     public float v1 = -1f;
//     public float v2 = 1.7f;
//     public float v3 = 0.8f;
//     public float alpha = -1f;

//     [Header("Dot Settings")]
//     public GameObject dotPrefab;
//     public int maxDots = 5000;
//     public float spawnInterval = 0.01f;
//     public float dotSize = 0.1f;
//     public Transform dotsParent;

//     [Header("Graph Settings")]
//     public float xScale = 25f, yScale = 25f, zScale = 25f, zOffset = 0f;

//     [Header("Camera Settings")]
//     public Camera mainCamera;
//     public Vector3 cameraOffset = new Vector3(1f, 1f, -2f); // Adjustable offset in inspector
//     public float followSpeed = 5f;

//     private float x = 0.1f, y = 0f, z = 0f;
//     private float timer = 0f;
//     private int dotCount = 0;

//     // Camera pose variables
//     private Vector3 defaultCameraPos;
//     private Quaternion defaultCameraRot;
//     private Vector3 currentCameraPos;
//     private Quaternion currentCameraRot;

//     // Boolean flags
//     public bool useDefaultCameraPose = true;
//     public bool useCurrentCameraPose = false;
//     public bool isPlottingEnabled = false;
//     public bool isFollowingPath = false;

//     private Vector3 lastPlotPosition = Vector3.zero;

//     void Start()
//     {
//         defaultCameraPos = mainCamera.transform.position;
//         defaultCameraRot = mainCamera.transform.rotation;

//         currentCameraPos = mainCamera.transform.position;
//         currentCameraRot = mainCamera.transform.rotation;
//     }

//     void Update()
//     {
//         // Camera follow logic
//         if (isFollowingPath)
//         {
//             Vector3 targetPos = lastPlotPosition + cameraOffset;
//             mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, Time.deltaTime * followSpeed);
//             mainCamera.transform.LookAt(lastPlotPosition); // Optional: look at trajectory
//         }
//         else
//         {
//             if (useDefaultCameraPose)
//             {
//                 mainCamera.transform.position = defaultCameraPos;
//                 mainCamera.transform.rotation = defaultCameraRot;
//             }
//             else if (useCurrentCameraPose)
//             {
//                 mainCamera.transform.position = currentCameraPos;
//                 mainCamera.transform.rotation = currentCameraRot;
//             }
//         }

//         // Plotting logic
//         if (isPlottingEnabled && dotCount < maxDots)
//         {
//             timer += Time.deltaTime;
//             if (timer >= spawnInterval)
//             {
//                 Vector3 next = StepChaosEquation();
//                 GameObject dot = Instantiate(dotPrefab, next, Quaternion.identity, dotsParent);
//                 dot.transform.localScale = Vector3.one * dotSize;

//                 dotCount++;
//                 timer = 0f;
//                 lastPlotPosition = next; // Save last position for camera following
//             }
//         }
//     }

//     Vector3 StepChaosEquation()
//     {
//         float xNext = y;
//         float yNext = v1 * x + v2 * y + y * z;
//         float zNext = v3 * z + alpha * y * y;

//         x = xNext;
//         y = yNext;
//         z = zNext;

//         return new Vector3(x * xScale, y * yScale, z * zScale + zOffset);
//     }

//     public void ResetView()
//     {
//         useDefaultCameraPose = true;
//         useCurrentCameraPose = false;
//         isFollowingPath = false;
//     }

//     public void ResetPlot()
//     {
//         foreach (Transform child in dotsParent)
//             Destroy(child.gameObject);

//         dotCount = 0;
//         x = 0.1f; y = 0f; z = 0f;

//         isPlottingEnabled = true;
//         useCurrentCameraPose = true;
//         useDefaultCameraPose = false;
//         isFollowingPath = false;
//     }

//     public void PlayLorenz()
//     {
//         isPlottingEnabled = true;
//     }

//     public void FollowPath()
//     {
//         isFollowingPath = true;
//         useCurrentCameraPose = false;
//         useDefaultCameraPose = false;
//     }
// }
// using UnityEngine;
// using System.Collections.Generic;

// public class LorenzAttractor : MonoBehaviour
// {
//     [Header("Attractor Parameters")]
//     public float v1 = -1f;
//     public float v2 = 1.7f;
//     public float v3 = 0.8f;
//     public float alpha = -1f;

//     [Header("Dot Settings")]
//     public GameObject dotPrefab;
//     public int maxDots = 5000;
//     public float spawnInterval = 0.01f;
//     public float dotSize = 0.1f;
//     public Transform dotsParent;

//     [Header("Graph Settings")]
//     public float xScale = 25f, yScale = 25f, zScale = 25f, zOffset = 0f;

//     [Header("Camera Settings")]
//     public Camera mainCamera;
//     public Vector3 cameraOffset = new Vector3(1f, 1f, -2f); // Adjustable offset in inspector
//     public float followSpeed = 5f;

//     private float x = 0.1f, y = 0f, z = 0f;
//     private float timer = 0f;
//     private int dotCount = 0;

//     // Camera pose variables
//     private Vector3 defaultCameraPos;
//     private Quaternion defaultCameraRot;
//     private Vector3 currentCameraPos;
//     private Quaternion currentCameraRot;

//     // Boolean flags
//     public bool useDefaultCameraPose = true;
//     public bool useCurrentCameraPose = false;
//     public bool isPlottingEnabled = false;
//     public bool isFollowingPath = false;
//     private List<Vector3> pathPoints = new List<Vector3>();
//     private float pathProgress = 0f;


//     private Vector3 lastPlotPosition = Vector3.zero;

//     void Start()
//     {
//         defaultCameraPos = mainCamera.transform.position;
//         defaultCameraRot = mainCamera.transform.rotation;

//         currentCameraPos = mainCamera.transform.position;
//         currentCameraRot = mainCamera.transform.rotation;
//     }

//     void Update()
//     {
//         // Camera follow logic
//         if (isFollowingPath && pathPoints.Count > 1)
//         {
//             pathProgress += followSpeed * Time.deltaTime;
//             if (pathProgress >= pathPoints.Count - 1)
//             {
//                 pathProgress = pathPoints.Count - 1;
//             }

//             int index = Mathf.FloorToInt(pathProgress);
//             int nextIndex = Mathf.Min(index + 1, pathPoints.Count - 1);
//             float t = pathProgress - index;

//             Vector3 currentPos = Vector3.Lerp(pathPoints[index], pathPoints[nextIndex], t);
//             Vector3 lookPos = Vector3.Lerp(pathPoints[nextIndex], pathPoints[Mathf.Min(nextIndex + 1, pathPoints.Count - 1)], t);

//             mainCamera.transform.position = currentPos + cameraOffset;
//             mainCamera.transform.LookAt(lookPos);
//         }

//         else
//         {
//             if (useDefaultCameraPose)
//             {
//                 mainCamera.transform.position = defaultCameraPos;
//                 mainCamera.transform.rotation = defaultCameraRot;
//             }
//             else if (useCurrentCameraPose)
//             {
//                 mainCamera.transform.position = currentCameraPos;
//                 mainCamera.transform.rotation = currentCameraRot;
//             }
//         }

//         // Plotting logic
//         if (isPlottingEnabled && dotCount < maxDots)
//         {
//             timer += Time.deltaTime;
//             if (timer >= spawnInterval)
//             {
                
//                 Vector3 next = StepChaosEquation();
//                 GameObject dot = Instantiate(dotPrefab, next, Quaternion.identity, dotsParent);
//                 dot.transform.localScale = Vector3.one * dotSize;

//                 dotCount++;
//                 timer = 0f;
//                 lastPlotPosition = next;
//                 pathPoints.Add(lastPlotPosition);  // Store for path following
//  // Save last position for camera following
//             }
//         }
//     }

//     Vector3 StepChaosEquation()
//     {
//         float xNext = y;
//         float yNext = v1 * x + v2 * y + y * z;
//         float zNext = v3 * z + alpha * y * y;

//         x = xNext;
//         y = yNext;
//         z = zNext;

//         return new Vector3(x * xScale, y * yScale, z * zScale + zOffset);
//     }

//     public void ResetView()
//     {
//         useDefaultCameraPose = true;
//         useCurrentCameraPose = false;
//         isFollowingPath = false;
//     }

//     // public void ResetPlot()
//     // {
//     //     foreach (Transform child in dotsParent)
//     //         Destroy(child.gameObject);

//     //     dotCount = 0;
//     //     x = 0.1f; y = 0f; z = 0f;

//     //     isPlottingEnabled = true;
//     //     useCurrentCameraPose = true;
//     //     useDefaultCameraPose = false;
//     //     isFollowingPath = false;
//     // }
//     public void ResetPlot()
//     {
//         foreach (Transform child in dotsParent)
//             Destroy(child.gameObject);

//         dotCount = 0;
//         x = 0.1f; y = 0f; z = 0f;

//         pathPoints.Clear();
//         pathProgress = 0f;

//         isPlottingEnabled = true;

//         if (isFollowingPath)
//         {
//             // Keep following path, but reset camera to start
//             useCurrentCameraPose = false;
//             useDefaultCameraPose = false;
//         }
//         else
//         {
//             // Not following, so keep current camera pose
//             useCurrentCameraPose = true;
//             useDefaultCameraPose = false;
//             isFollowingPath = false;
//         }
//     }

//     public void PlayLorenz()
//     {
//         isPlottingEnabled = true;
//     }

//     public void FollowPath()
//     {
//         isFollowingPath = true;
//         useCurrentCameraPose = false;
//         useDefaultCameraPose = false;
//     }
// }
// using UnityEngine;
// using System.Collections.Generic;
// using UnityEngine.UI;
// public class LorenzAttractor : MonoBehaviour
// {
//     [Header("Attractor Parameters")]
//     public float v1 = -0.2f;
//     public float v2 = 1.9f;
//     public float v3 = 0.8f;
//     public float alpha = -1f;

//     [Header("Dot Settings")]
//     public GameObject dotPrefab;
//     public int maxDots = 5000;
//     public float spawnInterval = 0.01f;
//     public float dotSize = 0.1f;
//     public Transform dotsParent;

//     [Header("Graph Settings")]
//     public float xScale = 25f, yScale = 25f, zScale = 25f, zOffset = 0f;

//     [Header("Camera Settings")]
//     public Camera mainCamera;
//     public Vector3 cameraOffset = new Vector3(1f, 1f, -2f); // Adjustable offset in inspector
//     public float followSpeed = 5f;

//     private float x = 0.1f, y = 0f, z = 0f;
//     private float timer = 0f;
//     private int dotCount = 0;

//     // Camera pose variables
//     private Vector3 defaultCameraPos;
//     private Quaternion defaultCameraRot;
//     private Vector3 currentCameraPos;
//     private Quaternion currentCameraRot;

//     // Boolean flags
//     public bool useDefaultCameraPose = true;
//     public bool useCurrentCameraPose = false;
//     public bool isPlottingEnabled = false;
//     public bool isFollowingPath = false;
//     private List<Vector3> pathPoints = new List<Vector3>();
//     private float pathProgress = 0f;
//     public Slider v2slider;


//     private Vector3 lastPlotPosition = Vector3.zero;

//     // void Start()
//     // {
//     //     defaultCameraPos = mainCamera.transform.position;
//     //     defaultCameraRot = mainCamera.transform.rotation;

//     //     currentCameraPos = mainCamera.transform.position;
//     //     currentCameraRot = mainCamera.transform.rotation;
//     // }

//     void Update()
//     {
//         // Camera follow logic
//         if (isFollowingPath && pathPoints.Count > 1)
//         {
//             pathProgress += followSpeed * Time.deltaTime;
//             if (pathProgress >= pathPoints.Count - 1)
//             {
//                 pathProgress = pathPoints.Count - 1;
//             }

//             int index = Mathf.FloorToInt(pathProgress);
//             int nextIndex = Mathf.Min(index + 1, pathPoints.Count - 1);
//             float t = pathProgress - index;

//             Vector3 currentPos = Vector3.Lerp(pathPoints[index], pathPoints[nextIndex], t);
//             Vector3 lookPos = Vector3.Lerp(pathPoints[nextIndex], pathPoints[Mathf.Min(nextIndex + 1, pathPoints.Count - 1)], t);

//             mainCamera.transform.position = currentPos + cameraOffset;
//             mainCamera.transform.LookAt(lookPos);
//         }

//         else
//         {
//             if (useDefaultCameraPose)
//             {
//                 mainCamera.transform.position = defaultCameraPos;
//                 mainCamera.transform.rotation = defaultCameraRot;
//             }
//             else if (useCurrentCameraPose)
//             {
//                 mainCamera.transform.position = currentCameraPos;
//                 mainCamera.transform.rotation = currentCameraRot;
//             }
//         }

//         // Plotting logic
//         if (isPlottingEnabled && dotCount < maxDots)
//         {
//             timer += Time.deltaTime;
//             if (timer >= spawnInterval)
//             {
                
//                 Vector3 next = StepChaosEquation();
//                 GameObject dot = Instantiate(dotPrefab, next, Quaternion.identity, dotsParent);
//                 dot.transform.localScale = Vector3.one * dotSize;

//                 dotCount++;
//                 timer = 0f;
//                 lastPlotPosition = next;
//                 pathPoints.Add(lastPlotPosition);  // Store for path following
//  // Save last position for camera following
//             }
//         }
//     }

//     Vector3 StepChaosEquation()
//     {
//         float xNext = y;
//         float yNext = v1 * x + v2 * y + y * z;
//         float zNext = v3 * z + alpha * y * y;

//         x = xNext;
//         y = yNext;
//         z = zNext;

//         return new Vector3(x * xScale, y * yScale, z * zScale + zOffset);
//     }

//     public void ResetView()
//     {
//         useDefaultCameraPose = true;
//         useCurrentCameraPose = false;
//         isFollowingPath = false;
//     }

//     // public void ResetPlot()
//     // {
//     //     foreach (Transform child in dotsParent)
//     //         Destroy(child.gameObject);

//     //     dotCount = 0;
//     //     x = 0.1f; y = 0f; z = 0f;

//     //     isPlottingEnabled = true;
//     //     useCurrentCameraPose = true;
//     //     useDefaultCameraPose = false;
//     //     isFollowingPath = false;
//     // }
//     public void ResetPlot()
//     {
//         foreach (Transform child in dotsParent)
//             Destroy(child.gameObject);

//         dotCount = 0;
//         x = 0.1f; y = 0f; z = 0f;

//         pathPoints.Clear();
//         pathProgress = 0f;

//         isPlottingEnabled = true;

//         if (isFollowingPath)
//         {
//             // Keep following path, but reset camera to start
//             useCurrentCameraPose = false;
//             useDefaultCameraPose = false;
//         }
//         else
//         {
//             // Not following, so keep current camera pose
//             useCurrentCameraPose = true;
//             useDefaultCameraPose = false;
//             isFollowingPath = false;
//         }
//     }

//     public void PlayLorenz()
//     {
//         isPlottingEnabled = true;
//         defaultCameraPos = mainCamera.transform.position;
//         defaultCameraRot = mainCamera.transform.rotation;

//         currentCameraPos = mainCamera.transform.position;
//         currentCameraRot = mainCamera.transform.rotation;
//         v2slider.onValueChanged.AddListener(Slider);
//         v2slider.value = v2;
//     }

//     public void FollowPath()
//     {
//         isFollowingPath = true;
//         useCurrentCameraPose = false;
//         useDefaultCameraPose = false;
//     }
//     public void Slider(float value){
//         v2 = value;
//         ResetPlot();
//     } 
// }
// using UnityEngine;
// using System.Collections.Generic;
// using UnityEngine.UI;

// public class LorenzAttractor : MonoBehaviour
// {
//     [Header("Attractor Parameters")]
//     public float v1 = -0.2f;
//     public float v2 = 1.9f;
//     public float v3 = 0.8f;
//     public float alpha = -1f;

//     [Header("Graph Rotation")]
//     public Vector3 initialRotation = Vector3.zero;
//     public bool applyInitialRotation = true;

//     [Header("UI Elements")]
//     public Slider v2slider;
//     public Text statusText;
//     public Text v2ValueText;
//     public Slider xRotationSlider;
//     public Slider yRotationSlider;
//     public Slider zRotationSlider;

//     [Header("Dot Settings")]
//     public GameObject dotPrefab;
//     public int maxDots = 5000;
//     public float spawnInterval = 0.01f;
//     public float dotSize = 0.1f;
//     public Transform dotsParent; // Parent transform for rotation

//     [Header("Graph Settings")]
//     public float xScale = 25f, yScale = 25f, zScale = 25f, zOffset = 0f;

//     [Header("Camera Settings")]
//     public Camera mainCamera;
//     public Vector3 cameraOffset = new Vector3(1f, 1f, -2f);
//     public float followSpeed = 5f;

//     private float x = 0.1f, y = 0f, z = 0f;
//     private float timer = 0f;
//     private int dotCount = 0;
//     private List<Vector3> pathPoints = new List<Vector3>();
//     private float pathProgress = 0f;
//     private Vector3 defaultCameraPos;
//     private Quaternion defaultCameraRot;
//     private Vector3 currentCameraPos;
//     private Quaternion currentCameraRot;
//     public bool useDefaultCameraPose = true;
//     public bool useCurrentCameraPose = false;
//     public bool isPlottingEnabled = false;
//     public bool isFollowingPath = false;

//     void Start()
//     {
//         InitializeSystem();
//         SetupUI();
//         if (applyInitialRotation) SetGraphRotation(initialRotation);
//     }

//     void InitializeSystem()
//     {
//         defaultCameraPos = mainCamera.transform.position;
//         defaultCameraRot = mainCamera.transform.rotation;
//         currentCameraPos = mainCamera.transform.position;
//         currentCameraRot = mainCamera.transform.rotation;
//     }

//     void SetupUI()
//     {
//         if (v2slider != null)
//         {
//             v2slider.value = v2;
//             v2slider.onValueChanged.AddListener(OnV2Changed);
//         }

//         SetupRotationSliders();
//         UpdateV2Text();
//         UpdateStatusText("System Ready");
//     }

//     void SetupRotationSliders()
//     {
//         if (xRotationSlider != null)
//         {
//             xRotationSlider.value = initialRotation.x;
//             xRotationSlider.onValueChanged.AddListener((val) => OnRotationChanged(0, val));
//         }
//         if (yRotationSlider != null)
//         {
//             yRotationSlider.value = initialRotation.y;
//             yRotationSlider.onValueChanged.AddListener((val) => OnRotationChanged(1, val));
//         }
//         if (zRotationSlider != null)
//         {
//             zRotationSlider.value = initialRotation.z;
//             zRotationSlider.onValueChanged.AddListener((val) => OnRotationChanged(2, val));
//         }
//     }

//     void Update()
//     {
//         HandleCameraMovement();
//         HandlePlotting();
//     }

//     void HandleCameraMovement()
//     {
//         if (isFollowingPath && pathPoints.Count > 1)
//         {
//             UpdatePathFollowing();
//         }
//         else
//         {
//             if (useDefaultCameraPose) ResetCameraToDefault();
//             else if (useCurrentCameraPose) SetCameraToCurrent();
//         }
//     }

//     void HandlePlotting()
//     {
//         if (isPlottingEnabled && dotCount < maxDots)
//         {
//             timer += Time.deltaTime;
//             if (timer >= spawnInterval)
//             {
//                 PlotNewPoint();
//                 timer = 0f;
//             }
//         }
//     }

//     void PlotNewPoint()
//     {
//         Vector3 next = StepChaosEquation();
//         Vector3 worldPosition = dotsParent != null ? 
//             dotsParent.TransformPoint(next) : 
//             next;

//         GameObject dot = Instantiate(dotPrefab, worldPosition, Quaternion.identity, dotsParent);
//         dot.transform.localScale = Vector3.one * dotSize;
//         pathPoints.Add(worldPosition);
//         dotCount++;
//     }

//     Vector3 StepChaosEquation()
//     {
//         float xNext = y;
//         float yNext = v1 * x + v2 * y + y * z;
//         float zNext = v3 * z + alpha * y * y;

//         x = xNext;
//         y = yNext;
//         z = zNext;

//         return new Vector3(x * xScale, y * yScale, z * zScale + zOffset);
//     }

//     public void SetGraphRotation(Vector3 eulerAngles)
//     {
//         if (dotsParent != null)
//         {
//             dotsParent.rotation = Quaternion.Euler(eulerAngles);
//             initialRotation = eulerAngles;
//             UpdateStatusText($"Rotation Set: X={eulerAngles.x:F0}°, Y={eulerAngles.y:F0}°, Z={eulerAngles.z:F0}°");
//         }
//     }

//     public void ResetPlot()
//     {
//         foreach (Transform child in dotsParent)
//             Destroy(child.gameObject);

//         dotCount = 0;
//         x = 0.1f; y = 0f; z = 0f;
//         pathPoints.Clear();
//         pathProgress = 0f;
//         isPlottingEnabled = true;
//         UpdateStatusText("Plot Reset");
//     }

//     public void ResetView()
//     {
//         useDefaultCameraPose = true;
//         isFollowingPath = false;
//         ResetCameraToDefault();
//         UpdateStatusText("View Reset");
//     }

//     public void PlayLorenz()
//     {
//         isPlottingEnabled = true;
//         UpdateStatusText("Simulation Started");
//     }

//     public void FollowPath()
//     {
//         isFollowingPath = true;
//         pathProgress = 0f;
//         UpdateStatusText("Following Path");
//     }

//     void OnRotationChanged(int axis, float value)
//     {
//         Vector3 newRotation = initialRotation;
//         newRotation[axis] = value;
//         SetGraphRotation(newRotation);
//     }

//     void OnV2Changed(float value)
//     {
//         v2 = value;
//         UpdateV2Text();
//         ResetPlot();
//     }

//     void UpdatePathFollowing()
//     {
//         pathProgress = Mathf.Clamp(pathProgress + followSpeed * Time.deltaTime, 0, pathPoints.Count - 1);
//         int index = Mathf.FloorToInt(pathProgress);
//         int nextIndex = Mathf.Min(index + 1, pathPoints.Count - 1);
//         float t = pathProgress - index;

//         Vector3 currentPos = Vector3.Lerp(pathPoints[index], pathPoints[nextIndex], t);
//         Vector3 lookPos = Vector3.Lerp(pathPoints[nextIndex], pathPoints[Mathf.Min(nextIndex + 1, pathPoints.Count - 1)], t);

//         mainCamera.transform.position = currentPos + cameraOffset;
//         mainCamera.transform.LookAt(lookPos);
//     }

//     void ResetCameraToDefault()
//     {
//         mainCamera.transform.position = defaultCameraPos;
//         mainCamera.transform.rotation = defaultCameraRot;
//     }

//     void SetCameraToCurrent()
//     {
//         mainCamera.transform.position = currentCameraPos;
//         mainCamera.transform.rotation = currentCameraRot;
//     }

//     void UpdateStatusText(string message)
//     {
//         if (statusText != null) statusText.text = "Status: " + message;
//     }

//     void UpdateV2Text()
//     {
//         if (v2ValueText != null) v2ValueText.text = "v2: " + v2.ToString("F2");
//     }
// }
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LorenzAttractor : MonoBehaviour
{
    [Header("Attractor Parameters")]
    public float v1 = -0.2f;
    public float v2 = 1.9f;
    public float v3 = 0.8f;
    public float alpha = -1f;

    [Header("Graph Rotation")]
    public Vector3 initialRotation = Vector3.zero;
    public bool applyInitialRotation = true;

    [Header("UI Elements")]
    public Slider v2slider;
    public Text statusText;
    public Text v2ValueText;
    public Slider xRotationSlider;
    public Slider yRotationSlider;
    public Slider zRotationSlider;

    [Header("Dot Settings")]
    public GameObject dotPrefab;
    public int maxDots = 5000;
    public float spawnInterval = 0.01f;
    public float dotSize = 0.1f;
    public Transform dotsParent; // Parent transform for rotation

    [Header("Graph Settings")]
    public float xScale = 25f, yScale = 25f, zScale = 25f, zOffset = 0f;

    [Header("Camera Settings")]
    public Camera mainCamera;
    public Vector3 cameraOffset = new Vector3(1f, 1f, -2f);
    public float followSpeed = 5f;

    private float x = 0.1f, y = 0f, z = 0f;
    private float timer = 0f;
    private int dotCount = 0;
    private List<Vector3> pathPoints = new List<Vector3>();
    private float pathProgress = 0f;
    private Vector3 defaultCameraPos;
    private Quaternion defaultCameraRot;
    private Vector3 currentCameraPos;
    private Quaternion currentCameraRot;
    public bool useDefaultCameraPose = true;
    public bool useCurrentCameraPose = false;
    public bool isPlottingEnabled = false;
    public bool isFollowingPath = false;
    [Header("XR Settings")]
    public GameObject xrRig;
    public GameObject Equationimage;

    void Start()
    {
        
        InitializeSystem();
        SetupUI();
        if (applyInitialRotation) SetGraphRotation(initialRotation);
        
    }

    void InitializeSystem()

    {
    
        
        if (xrRig != null)
        {
            defaultCameraPos = xrRig.transform.position;
            defaultCameraRot = xrRig.transform.rotation;
            currentCameraPos = xrRig.transform.position;
            currentCameraRot = xrRig.transform.rotation;
        }
        else
        {
            defaultCameraPos = mainCamera.transform.position;
            defaultCameraRot = mainCamera.transform.rotation;
            currentCameraPos = mainCamera.transform.position;
            currentCameraRot = mainCamera.transform.rotation;
        }

 
    }

    void SetupUI()
    {
        if (v2slider != null)
        {
            v2slider.value = v2;
            v2slider.onValueChanged.AddListener(OnV2Changed);
        }

        // SetupRotationSliders();
        UpdateV2Text();
        UpdateStatusText("System Ready");
    }

    // void SetupRotationSliders()
    // {
    //     if (xRotationSlider != null)
    //     {
    //         xRotationSlider.value = initialRotation.x;
    //         xRotationSlider.onValueChanged.AddListener((val) => OnRotationChanged(0, val));
    //     }
    //     if (yRotationSlider != null)
    //     {
    //         yRotationSlider.value = initialRotation.y;
    //         yRotationSlider.onValueChanged.AddListener((val) => OnRotationChanged(1, val));
    //     }
    //     if (zRotationSlider != null)
    //     {
    //         zRotationSlider.value = initialRotation.z;
    //         zRotationSlider.onValueChanged.AddListener((val) => OnRotationChanged(2, val));
    //     }
    // }

    void Update()
    {
        HandleCameraMovement();
        HandlePlotting();
    }

    void HandleCameraMovement()
    {
        if (isFollowingPath && pathPoints.Count > 1)
        {
            UpdatePathFollowing();
        }
        else
        {
            if (useDefaultCameraPose) ResetCameraToDefault();
            else if (useCurrentCameraPose) SetCameraToCurrent();
        }
    }

    void HandlePlotting()
    {
        if (isPlottingEnabled && dotCount < maxDots)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                PlotNewPoint();
                timer = 0f;
            }
        }
    }

    void PlotNewPoint()
    {
        Vector3 next = StepChaosEquation();
        Vector3 worldPosition = dotsParent != null ? 
            dotsParent.TransformPoint(next) : 
            next;

        GameObject dot = Instantiate(dotPrefab, worldPosition, Quaternion.identity, dotsParent);
        dot.transform.localScale = Vector3.one * dotSize;
        pathPoints.Add(worldPosition);
        dotCount++;
    }

    Vector3 StepChaosEquation()
    {
        float xNext = y;
        float yNext = v1 * x + v2 * y + y * z;
        float zNext = v3 * z + alpha * y * y;

        x = xNext;
        y = yNext;
        z = zNext;

        return new Vector3(x * xScale, y * yScale, z * zScale + zOffset);
    }

    public void SetGraphRotation(Vector3 eulerAngles)
    {
        if (dotsParent != null)
        {
            dotsParent.rotation = Quaternion.Euler(eulerAngles);
            initialRotation = eulerAngles;
            UpdateStatusText($"Rotation Set: X={eulerAngles.x:F0}°, Y={eulerAngles.y:F0}°, Z={eulerAngles.z:F0}°");
        }
    }
    public void ClearAllDots()
    {
        if (dotsParent != null)
        {
            foreach (Transform child in dotsParent)
                Destroy(child.gameObject);
        }

        dotCount = 0;
        pathPoints.Clear();
        pathProgress = 0f;
    }
    public void ResetPlot()
    {
        ClearAllDots(); // Clear existing dots

        // Reset system state
        x = 0.1f;
        y = 0f;
        z = 0f;
        isPlottingEnabled = true;

        UpdateStatusText("Reset Plotting ");
    }
    public void Details()

    {
        bool isActive = Equationimage.activeSelf;

        // Toggle both Graph and Equation based on current state
        // Graph.SetActive(!isActive);
        Equationimage.SetActive(!isActive);

        
        // Equationimage.SetActive(true);
    }
    // public void ResetPlot()
    // {
    //     foreach (Transform child in dotsParent)
    //         Destroy(child.gameObject);

    //     dotCount = 0;
    //     x = 0.1f; y = 0f; z = 0f;
    //     pathPoints.Clear();
    //     pathProgress = 0f;
    //     isPlottingEnabled = true;
    //     UpdateStatusText("Plot Reset");
    // }

    public void ResetView()
    {
        useDefaultCameraPose = true;
        isFollowingPath = false;
        ResetCameraToDefault();
        UpdateStatusText("Reset View");
    }

    public void PlayLorenz()
    {
        Equationimage.SetActive(false);
        isPlottingEnabled = true;
        UpdateStatusText("Simulation Started");
    }

    public void FollowPath()
    {
        isFollowingPath = true;
        pathProgress = 0f;
        UpdateStatusText("Following Trajectory");
    }

    void OnRotationChanged(int axis, float value)
    {
        Vector3 newRotation = initialRotation;
        newRotation[axis] = value;
        SetGraphRotation(newRotation);
    }

    void OnV2Changed(float value)
    {
        v2 = value;
        UpdateV2Text();
        ResetPlot();
    }

    void UpdatePathFollowing()
    {
        pathProgress = Mathf.Clamp(pathProgress + followSpeed * Time.deltaTime, 0, pathPoints.Count - 1);
        int index = Mathf.FloorToInt(pathProgress);
        int nextIndex = Mathf.Min(index + 1, pathPoints.Count - 1);
        float t = pathProgress - index;

        Vector3 currentPos = Vector3.Lerp(pathPoints[index], pathPoints[nextIndex], t);
        Vector3 lookPos = Vector3.Lerp(pathPoints[nextIndex], pathPoints[Mathf.Min(nextIndex + 1, pathPoints.Count - 1)], t);

        // mainCamera.transform.position = currentPos + cameraOffset;
        // mainCamera.transform.LookAt(lookPos);
        if (xrRig != null)
        {
            xrRig.transform.position = currentPos + cameraOffset;
            xrRig.transform.LookAt(lookPos);
        }
        else
        {
            mainCamera.transform.position = currentPos + cameraOffset;
            mainCamera.transform.LookAt(lookPos);
        }

    }

    void ResetCameraToDefault()

    {
        if (xrRig != null)
        {
            // If XR Rig is being used (VR mode)
            xrRig.transform.position = defaultCameraPos;
            xrRig.transform.rotation = defaultCameraRot;

            // Optional: Recenter the headset pose to match rig reset

        }
        else
        {
            // Normal (non-VR) camera reset
            mainCamera.transform.position = defaultCameraPos;
            mainCamera.transform.rotation = defaultCameraRot;
        }

    }

    void SetCameraToCurrent()
    {
            if (xrRig != null)
    {
        // If XR Rig is being used (VR mode)
        xrRig.transform.position = currentCameraPos;
        xrRig.transform.rotation = currentCameraRot;

        // Optional: Recenter the headset pose to match rig reset
        // if (OVRManager.isHmdPresent)
        // {
        //     OVRManager.display.RecenterPose();
        // }
    }
    else
    {
        // Normal (non-VR) camera reset
        mainCamera.transform.position = currentCameraPos;
        mainCamera.transform.rotation = currentCameraRot;
    }

    }

    void UpdateStatusText(string message)
    {
        if (statusText != null) statusText.text = "Status: " + message;
    }

    void UpdateV2Text()
    {
        if (v2ValueText != null) v2ValueText.text = "v2: " + v2.ToString("F2");
    }
}