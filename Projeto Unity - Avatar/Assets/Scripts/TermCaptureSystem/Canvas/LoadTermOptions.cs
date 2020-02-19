using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadTermOptions : MonoBehaviour {
    public Dropdown dropdown;
    void Start() {
        DirectoryInfo levelDirectoryPath = new DirectoryInfo("Assets\\Terms");
        changeFileOptions(dropdown, levelDirectoryPath);
    }

    public void changeFileOptions(Dropdown fileDropdown, DirectoryInfo levelDirectoryPath) {
        FileInfo[] fileInfo;
        fileInfo = levelDirectoryPath.GetFiles("*", SearchOption.AllDirectories);
        fileDropdown.ClearOptions();
        fileDropdown.options.Add(new Dropdown.OptionData("Nenhuma"));
        foreach (FileInfo file in fileInfo) {
            if (!file.Extension.Contains("meta")) {
                fileDropdown.options.Add(new Dropdown.OptionData(file.Name.Remove(file.Name.IndexOf(file.Extension))));
            }
        }
        fileDropdown.RefreshShownValue();
    }

}
