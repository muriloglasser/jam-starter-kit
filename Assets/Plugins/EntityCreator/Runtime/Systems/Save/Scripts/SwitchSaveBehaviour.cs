using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SwitchSaveBehaviour : MonoBehaviour,  ISave
{
    private string _mountPoint;
    public SwitchSaveBehaviour(string mountPoint = "")
    {
        _mountPoint = mountPoint;
       // InitializeFileSystem();
    }
    /// <summary>
    /// Initialize switch save system
    /// </summary>
   // private void InitializeFileSystem() => SwitchIOHelper.InitializeFileSystem(_mountPoint);

    /// <summary>
    /// Delete save data 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="onComplete"></param>
    public void Delete(string path, Action onComplete = null, Action complete = null)
    {
        try
        {
            System.IO.File.Delete(GetFullPath(path));
            onComplete?.Invoke();
        }
        catch
        {

        }
    }

    /// <summary>
    /// Check save data existence
    /// </summary>
    /// <param name="path"></param>
    /// <param name="onComplete"></param>
    public void Exists(string path, Action<bool> onComplete = null)
    {
        onComplete?.Invoke(System.IO.File.Exists(GetFullPath(path)));
    }

    /// <summary>
    /// Load save
    /// </summary>
    /// <param name="path"></param>
    /// <param name="onComplete"></param>
    public void Load(string path, Action<byte[]> onComplete)
    {
        onComplete?.Invoke(System.IO.File.ReadAllBytes(GetFullPath(path)));
    }

    /// <summary>
    /// Save data
    /// </summary>
    /// <param name="path"></param>
    /// <param name="data"></param>
    /// <param name="onComplete"></param>
    public void Save(string path, byte[] data, Action onComplete = null)
    {
        System.IO.File.WriteAllBytes(GetFullPath(path), data);
        onComplete?.Invoke();
    }

    /// <summary>
    /// Get path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private string GetFullPath(string path) => $"{_mountPoint}:/{path}";

}
