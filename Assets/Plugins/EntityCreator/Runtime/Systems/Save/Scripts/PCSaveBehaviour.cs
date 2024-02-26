using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PCSaveBehaviour : MonoBehaviour, ISave
{   
    /// <summary>
    /// Delete file 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="onComplete"></param>
    public void Delete(string path, Action onComplete = null, Action completed = null)
    {
        try
        {
            System.IO.File.Delete(GetFullPath(path));
            onComplete?.Invoke();
            completed.Invoke();
        }
        catch
        {
        
        }
    }

    /// <summary>
    /// Check data existence
    /// </summary>
    /// <param name="path"></param>
    /// <param name="onComplete"></param>
    public void Exists(string path, Action<bool> onComplete = null)
    {
        onComplete?.Invoke(System.IO.File.Exists(GetFullPath(path)));
    }

    /// <summary>
    /// Load save file
    /// </summary>
    /// <param name="path"></param>
    /// <param name="onComplete"></param>
    public void Load(string path, Action<byte[]> onComplete)
    {
        onComplete?.Invoke(System.IO.File.ReadAllBytes(GetFullPath(path)));
    }

    /// <summary>
    /// Save file
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
    /// Get full path with file name
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private string GetFullPath(string fileName)
    {
        var path = string.Concat(Application.persistentDataPath, "/", fileName);
        return path;
    }
}
