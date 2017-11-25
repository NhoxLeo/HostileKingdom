﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    bool gamePause;

    [Header("MouseInputsManager")]
    [SerializeField]
    MouseBehaviour mouse; //Coje el Script de MouseBehaviour para actualizar su comportamiento.
    //LevelLogic levelLogic; 
    Vector3 formationPosition;

    [Header("CameraInputs")]
    [SerializeField]
    CameraController cameraController;
    [SerializeField]
    CameraZoom cameraZoom; 
    float scrollAxis;
    float rotateAxis;
    float mouseAxis; 
    Vector2 inputAxis;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) gamePause = !gamePause;
        if (gamePause == true)
        {
            if (Time.timeScale == 1.0f) Time.timeScale = 0.0f; 
            Paused();
        }
        else
        {
            if(Time.timeScale != 1.0f) Time.timeScale = 1.0f;
            NoPaused();
        }
    }

    void Paused()
    {
        
    }

    void NoPaused()
    {
        if (Input.GetMouseButton(0)) mouse.isDragging = true;
        if (Input.GetMouseButtonUp(0)) mouse.MouseButtonUp();
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                mouse.multipleUnitSelection = true;
            }
            mouse.ClickState();
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (mouse.selectedUnit != null)
            {
                formationPosition = Vector3.zero;
                mouse.selectedUnit.ClickUpdate(formationPosition);
            }
            if (mouse.selectedUnits != null)
            {
                for (int i = 0; i < mouse.selectedUnits.Count; i++)
                {
                    if (i == 0) formationPosition = Vector3.zero;
                    if (i == 1) formationPosition = new Vector3(-4, 0, 0);
                    if (i == 2) formationPosition = new Vector3(4, 0, 0);
                    if (i == 3) formationPosition = new Vector3(0, 0, 4);

                    mouse.selectedUnits[i].ClickUpdate(formationPosition);
                }
            }
        }
        /*if (Input.GetKey(KeyCode.AltGr))
        {
            if (Input.GetKeyDown(KeyCode.N)) levelLogic.StartLoad(levelLogic.nextScene);
            if (Input.GetKeyDown(KeyCode.B)) levelLogic.StartLoad(levelLogic.backScene);
            if (Input.GetKeyDown(KeyCode.R)) levelLogic.StartLoad(levelLogic.currentScene);
        }*/
        #region CameraControllerAndZoom
        cameraController.mousePosition = Input.mousePosition;
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
        rotateAxis = Input.GetAxis("Rotation");
        mouseAxis = Input.GetAxis("Mouse X");
        scrollAxis = Input.GetAxis("Mouse ScrollWheel");

        cameraController.SetInputAxis(inputAxis);
        cameraController.SetRotationAxis(rotateAxis); 
        cameraZoom.SetAxis(scrollAxis);

        if (Input.GetButton("Fire3"))
        {
            cameraController.SetMouseRotationAxis(mouseAxis);
        }
        #endregion
    }
}