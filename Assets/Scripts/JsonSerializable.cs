using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonSerial : MonoBehaviour
{
    [SerializeField] private VolumeData _VolumeData = new VolumeData();

    public void saveSettings()
    {
        string Settings = JsonUtility.ToJson(_VolumeData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Settings.json", Settings);
    }
    public VolumeData getSettings()
    {
        VolumeData Settings = JsonUtility.FromJson<VolumeData>(System.IO.File.ReadAllText(Application.persistentDataPath + "/Settings.json"));
        return getSettings();
    }
}
[System.Serializable]
public class VolumeData
{
    public float volumeFx;
    public float volumeBackground;
    public bool OpeningsAnimation;
}
