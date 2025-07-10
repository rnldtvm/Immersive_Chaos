
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Neuron : MonoBehaviour
{
    [Header("Variable Parameter")]
    public float a = 0.95f;
    public Slider aSlider;
    public Text aValueText;

    [Header("Analyze UI")]
    public Button analyzeButton;
    public GameObject Graph;
    public GameObject Equation;
    // public GameObject analyzeCanvas;

    [Header("Dot Settings")]
    public GameObject dotPrefab;
    public int maxDots = 5000;
    public float spawnInterval = 0.01f;
    public float dotSize = 0.1f;
    public Transform dotsParent;
    public GameObject detailstext;

    [Header("Graph Scaling")]
    public float xScale = 25f, yScale = 25f, zScale = 25f, zOffset = 0f, xOffset = 0f, yOffset = 0f;

    private const float delta = 0.11f;
    private const float b = 4f;
    private const float I = 9.28f;
    private const float c = 1f;
    private const float d = 5f;
    private const float r = 6f;
    private const float s = 4f;
    private const float x0 = -1.6f;

    private float x = 0.1f, y = 0f, z = 0f;
    private float timer = 0f;
    private int dotCount = 0;
    public bool plottingActive = false;

    private List<Vector3> pathPoints = new List<Vector3>();

    // void Start()
    // {


    //     // Do NOT start plotting automatically
    // }

    void Update()
    {
        if (plottingActive && dotCount < maxDots)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                Vector3 next = StepChaosEquation();
                GameObject dot = Instantiate(dotPrefab, next, Quaternion.identity, dotsParent);
                dot.transform.localScale = Vector3.one * dotSize;

                pathPoints.Add(next);
                dotCount++;
                timer = 0f;
            }
        }
    }

    Vector3 StepChaosEquation()
    {
        float xNext = x + delta * (y - (a * x * x * x) + (b * x * x) + I - z);
        float yNext = y + delta * (c - (d * x * x) - y);
        float zNext = z + delta * r * (s * (x - x0) - z);

        x = xNext;
        y = yNext;
        z = zNext;

        return new Vector3(x * xScale + xOffset, y * yScale + yOffset, z * zScale + zOffset);
    }

    public void UpdateAValue(float val)
    {
        a = val;
        if (aValueText != null)
            aValueText.text = $"a = {a:F2}";
        ResetPlottingState();
    }

    public void StartPlotting()
    {
        Graph.SetActive(false);
        Equation.SetActive(false);
        ResetPlottingState();
        plottingActive = true;
        aSlider.onValueChanged.AddListener(UpdateAValue);
        // analyzeButton.onClick.AddListener(ShowAnalyzeCanvas);
        UpdateAValue(aSlider.value);
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

    // void ShowAnalyzeCanvas()
    // {
    //     if (analyzeCanvas != null)
    //         analyzeCanvas.SetActive(true);
    // }

    // public void Details()
    // {

    //     Graph.SetActive(true);
    //     Equation.SetActive(true);
    // }
    public void Details()
    {
        bool isActive = Graph.activeSelf;

        // Toggle both Graph and Equation based on current state
        Graph.SetActive(!isActive);
        Equation.SetActive(!isActive);
    }

}
