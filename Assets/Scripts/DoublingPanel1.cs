using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DoublingPanel1 : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject IntroductionCanvas;

    public GameObject DoublingPanel;     // For Ergodic
    public GameObject IntroPanel;    // For Hindmarsh-Rose
    public GameObject MainMenu;    // For Lorenz

    public GameObject Ergodic;
    public GameObject Neuron;
        

    public GameObject Lorenz;

    public GameObject LorenzObject;
    public GameObject NeuronObject;
    public GameObject ErgodicObject;
    public LorenzAttractor Lorenzscript;
    public Neuron Neuronscript;
    
    public Ergodic Ergodicscript; 

 
    public void Openpanel(){
        DoublingPanel.SetActive(true);
        IntroPanel.SetActive(false);


    
    }
    public void Mainpanel()
    {
        IntroductionCanvas.SetActive(true);
        IntroPanel.SetActive(true);
        DoublingPanel.SetActive(false);
        LorenzObject.SetActive(false);
        ErgodicObject.SetActive(false);
        NeuronObject.SetActive(false);
        Ergodic.SetActive(false);
        Neuron.SetActive(false);
        Lorenz.SetActive(false);
        Lorenzscript.isPlottingEnabled = false;
        Lorenzscript.ClearAllDots();
        Ergodicscript.plottingActive = false;
        Ergodicscript.ResetPlottingState();
        Neuronscript.plottingActive = false;
        Neuronscript.ResetPlottingState();
    }

	

    public void OpenLorenz() {

        Lorenz.SetActive(true);
        LorenzObject.SetActive(true);
        Lorenzscript.PlayLorenz();

        IntroductionCanvas.SetActive(false);
    }



    public void OpenNeuron(){
        Neuron.SetActive(true);
        NeuronObject.SetActive(true);
        
        Neuronscript.StartPlotting();

        IntroductionCanvas.SetActive(false);
        DoublingPanel.SetActive(false);
        IntroPanel.SetActive(false);
        
    }
    public void OpenErgodic(){
        Ergodic.SetActive(true);
        ErgodicObject.SetActive(true);
        
        Ergodicscript.StartPlotting();

        IntroductionCanvas.SetActive(false);
        DoublingPanel.SetActive(false);
        IntroPanel.SetActive(false);

    }
}
