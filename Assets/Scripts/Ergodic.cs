using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Ergodic : MonoBehaviour
{
    [Header("Variable Parameter")]
    public float b2 = 1.0f;
    public Slider b2Slider;
    public Text b2ValueText;

    [Header("Analyze UI")]
    public Button analyzeButton;
    public GameObject analyzeCanvas;

    [Header("Dot Settings")]
    public GameObject dotPrefab;
    public int maxDots = 5000;
    public float spawnInterval = 0.01f;
    public float dotSize = 0.1f;
    public Transform dotsParent;

    [Header("Graph Scaling")]
    public float xScale = 25f, yScale = 25f, zScale = 25f;
    public float xOffset = 0f, yOffset = 0f, zOffset = 0f;

    [Header("Fixed Parameters")]
    public float a1 = 0.5f;
    public float a2 = -0.3f;
    public float a3 = 0.1f;
    public float b1 = 1.0f;
    public float c = 0.9f;

    private float x = 0.1f, y = 0f, z = 0f;
    private float timer = 0f;
    private int dotCount = 0;
    public bool plottingActive = false;
    private List<Vector3> pathPoints = new List<Vector3>();
    [Header("Rotation Settings")]
    public Vector3 graphRotationEuler = Vector3.zero;  // Store the rotation values
    private Vector3 lastAppliedRotation = Vector3.zero;
    private bool isRotationSet = false;
    public GameObject Graph;
    public GameObject Equation;
    




    void Update()

    {
        
        if (!isRotationSet || graphRotationEuler != lastAppliedRotation)
        {
            SetGraphRotation(graphRotationEuler);
            lastAppliedRotation = graphRotationEuler;
            isRotationSet = true;  // Mark rotation as set
        }
        if (plottingActive && dotCount < maxDots)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            
            {
                Vector3 next = StepMapEquation();
                // GameObject dot = Instantiate(dotPrefab, next, Quaternion.identity, dotsParent);
                GameObject dot = Instantiate(dotPrefab, next, Quaternion.identity, dotsParent);

                dot.transform.localScale = Vector3.one * dotSize;

                pathPoints.Add(next);
                dotCount++;
                timer = 0f;
            }
        }
    }
    public void SetGraphRotation(Vector3 eulerAngles)
    {
        if (dotsParent != null)
        {
            dotsParent.rotation = Quaternion.Euler(eulerAngles);
        }
    }



    Vector3 StepMapEquation()
    {
        float xNext = a1 * x + a2 * y + a3 * y * y;
        float yNext = b1 - b2 * z;
        float zNext = c * x;

        x = xNext;
        y = yNext;
        z = zNext;

        return new Vector3(x * xScale + xOffset, y * yScale + yOffset, z * zScale + zOffset);
    }

    public void UpdateB2Value(float val)
    {
        b2 = val;
        if (b2ValueText != null)
            b2ValueText.text = $"b2 = {b2:F2}";
        ResetPlottingState();
    }

    public void StartPlotting()
    {
        Graph.SetActive(false);
        Equation.SetActive(false);
        ResetPlottingState();
        plottingActive = true;
        b2Slider.onValueChanged.AddListener(UpdateB2Value);
        // analyzeButton.onClick.AddListener(ShowAnalyzeCanvas);
        UpdateB2Value(b2Slider.value);
    }

    public void ResetPlottingState()
    {
        foreach (Transform child in dotsParent)
            Destroy(child.gameObject);

        x = 0.1f;
        y = 0f;
        z = 0f;
        dotCount = 0;
        timer = 0f;
        pathPoints.Clear();
    }

    public void Details()
    {
        bool isActive = Graph.activeSelf;

        // Toggle both Graph and Equation based on current state
        Graph.SetActive(!isActive);
        Equation.SetActive(!isActive);
    }
}
