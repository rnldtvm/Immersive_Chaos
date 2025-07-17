# Immersive Chaos: A VR Framework for Exploring Chaotic Systems

This repository contains the Unity3D-based implementation of our research framework titled:

**"Immersive Virtual Reality for Exploring Chaotic Systems: A Framework for Dynamic Attractor Visualization"**  
*(Currently under review)*

## 🧠 Overview

Understanding chaotic systems often demands more than traditional 2D or 3D visualizations can offer. This project introduces an immersive virtual reality (VR) framework that enables intuitive exploration of nonlinear dynamical systems and strange attractors.

The framework is:

- Built in **Unity3D**
- Deployed on the **Meta Quest 2** headset
- Designed to enable **real-time interaction, manipulation, and observation** of chaotic trajectories

## 🌀 Case Studies

The current implementation features two representative chaotic systems:

1. **Discrete Lorenz Attractor**
2. **Bifurcation behavior** in:
   - A **3D quadratic map** (demonstrating torus and length doubling)
   - The **discrete Hindmarsh-Rose neuron model**

Geometric metrics such as **curve length** are computed to quantify dynamic transitions.

## 🎮 Features

- Full 6DoF immersive interaction
- Real-time trajectory evolution
- Parameter tuning and system manipulation in VR
- Visual cues for bifurcation and attractor behavior
- Educational and research-oriented visualization

## 📽️ Demo Video (Coming Soon)

*A short walkthrough of the system in action will be uploaded here.*

## 🛠️ Getting Started

### Prerequisites

- Unity version: `2021.3.14f1`
- Meta Quest 2 + Oculus Link (or build APK for standalone deployment)
- XR Plugin Management installed and set up in Unity

### Steps to Run

1. **Clone the repository**
   ```bash
   git clone https://github.com/rnldtvm/Immersive_Chaos.git

2. Open the project in Unity

3. Set the build target to Android (for Meta Quest)

4. Ensure XR settings are configured

5. Install Oculus Integration if needed

6. Enable XR Plugin for Oculus

7. Play in Editor using Unity Game view, or Build & Deploy to Meta Quest 2
   
