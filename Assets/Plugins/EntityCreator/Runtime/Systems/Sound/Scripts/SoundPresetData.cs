using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundPresetData", menuName = "ScriptableObjects/SoundPresetData", order = 2)]
public class SoundPresetData : ScriptableObject
{
    public AudioOption audioType;
    public AudioClip clip;
    public bool loop;
 }
public enum AudioOption
{
    vfx,
    music
}