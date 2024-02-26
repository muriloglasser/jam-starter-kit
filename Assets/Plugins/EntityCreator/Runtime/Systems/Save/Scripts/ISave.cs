using System;

public interface ISave
{
    public void Save(string _path, byte[] data, Action onComplete = null);
    public void Load(string _path, Action<byte[]> onComplete);
    public void Delete(string path, Action onComplete = null, Action complete = null);
    public void Exists(string path, Action<bool> onComplete = null);
}
