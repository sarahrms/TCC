using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class CanvasController : MonoBehaviour {
   public Camera frontCamera, topCamera, rightCamera, leftCamera;
   public SignCaptureController controller;
   public GROUP selectedGroup;
   public InputField idInputField;
   public Symbol symbol;
   public Dropdown cameraDropdown, groupDropdown;

   void Start() {
        setCameras();
        addGroupOptions();
   }
   void disableAllCameras() {
        frontCamera.enabled = false;
        topCamera.enabled = false;
        leftCamera.enabled = false;
        rightCamera.enabled = false;
   }
   void setCameras() {
        disableAllCameras();
        frontCamera.enabled = true;
   }
   public void changeCamera() {
        int option = cameraDropdown.value;
        switch (option) {
            case 0:
                disableAllCameras();
                frontCamera.enabled = true;
                break;
            case 1:
                disableAllCameras();
                rightCamera.enabled = true;
                break;
            case 2:
                disableAllCameras();
                leftCamera.enabled = true;
                break;
            case 3:
                disableAllCameras();
                topCamera.enabled = true;
                break;
        }
   }

    void addGroupOptions() {
        foreach(GROUP group in Enum.GetValues(typeof(GROUP))) {
            String groupName = new CultureInfo("en-US", false).TextInfo.ToTitleCase(group.ToString().Replace('_', ' ').ToLower());
            groupDropdown.options.Add(new Dropdown.OptionData(groupName));
        }
        groupDropdown.value = 0;
    }

    public void changeSelectedGroup() {
        selectedGroup = (GROUP) Enum.GetValues(typeof(GROUP)).GetValue(groupDropdown.value);
        controller.changeGroup(getId(), selectedGroup);
    }
    public int getId() {
        return Convert.ToInt32(idInputField.text);
    }

}
