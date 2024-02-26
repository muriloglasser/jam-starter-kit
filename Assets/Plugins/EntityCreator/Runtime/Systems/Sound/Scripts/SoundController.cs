using EntityCreator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls game SFX and Music
/// </summary>
public class SoundController : MonoBehaviour
{
    #region Properties

    [Header("Audio sources lists")]
    public List<AudioSource> musicAudioSource = new List<AudioSource>(); // List of audio sources for music
    public List<AudioSource> sfxAudioSources = new List<AudioSource>(); // List of audio sources for sound effects
    private Dictionary<string, AudioSource> playingAudios = new Dictionary<string, AudioSource>(); // Dictionary to keep track of playing audio sources

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        EventDispatcher.RegisterObserver<PlayAudioStruct>(PlayAudio); 
        EventDispatcher.RegisterObserver<StopAudioStruct>(StopAudio); 
    }
    private void OnDisable()
    {
        EventDispatcher.UnregisterObserver<PlayAudioStruct>(PlayAudio); 
        EventDispatcher.UnregisterObserver<StopAudioStruct>(StopAudio);
    }

    #endregion

    #region Observers

    /// <summary>
    /// Load sound data from resources based on the provided sound name
    /// </summary>
    /// <param name="obj"> Sound to play </param>
    private void PlayAudio(PlayAudioStruct obj)
    {
        SoundPresetData myAudioClip = Resources.Load<SoundPresetData>("Assets/Sounds/SoundPresetsData/" + obj.soundName);
        var selectedAudioSource = AvailableAudioSource(myAudioClip.audioType); // Get an available audio source based on the audio type

        if (selectedAudioSource == null)
            return;

        selectedAudioSource.loop = myAudioClip.loop; // Set loop property based on the loaded sound data
        selectedAudioSource.clip = myAudioClip.clip; // Set the audio clip based on the loaded sound data

        if (selectedAudioSource.loop)
            selectedAudioSource.Play(); // Play the audio source if it's set to loop
        else
            selectedAudioSource.PlayOneShot(selectedAudioSource.clip); // Play a one-shot audio clip if it's not set to loop

        if (!playingAudios.ContainsKey(obj.soundName))
        {
            playingAudios.Add(obj.soundName, selectedAudioSource); // Add the playing audio source to the dictionary
            StartCoroutine(WaitForClipEnd(obj.soundName, selectedAudioSource)); // Start a coroutine to wait for the audio clip to end
        }
    }
    /// <summary>
    /// Stop audio 
    /// </summary>
    /// <param name="obj"> audio to stop </param>
    private void StopAudio(StopAudioStruct obj)
    {
        if (!playingAudios.ContainsKey(obj.soundName))
            return;

        playingAudios[obj.soundName].Stop(); // Stop the specified audio source if it's currently playing
    }

    #endregion

    #region Core

    /// <summary>
    /// // Return an available sound effect audio source
    /// </summary>
    /// <param name="audioType"> Audio mixer tyoe </param>
    /// <returns></returns>
    public AudioSource AvailableAudioSource(AudioOption audioType)
    {
        if (audioType == AudioOption.vfx)
        {
            for (int i = 0; i < sfxAudioSources.Count; i++)
            {
                if (!sfxAudioSources[i].isPlaying)
                {
                    sfxAudioSources[i].loop = false;
                    return sfxAudioSources[i]; 
                }
            }
        }
        else if (audioType == AudioOption.music)
        {
            for (int i = 0; i < musicAudioSource.Count; i++)
            {
                if (!musicAudioSource[i].isPlaying)
                {
                    musicAudioSource[i].loop = false;
                    return musicAudioSource[i]; // Return an available music audio source
                }
            }
        }

        Debug.Log("No audio sources available");
        return null; // Return null if no available audio sources are found
    }
    /// <summary>
    /// Wait until the audio clip ends
    /// </summary>
    /// <param name="audioSourceName"> Audio source name in dictionary </param>
    /// <param name="audioSource"> Audio source to check </param>
    /// <returns></returns>
    public IEnumerator WaitForClipEnd(string audioSourceName, AudioSource audioSource)
    {
        while (audioSource.isPlaying && playingAudios.ContainsKey(audioSourceName))
            yield return null; 

        if (playingAudios.ContainsKey(audioSourceName))
        {
            playingAudios.Remove(audioSourceName); // Remove the audio source from the dictionary when the clip ends
        }
    }

    #endregion
}

public struct PlayAudioStruct
{
    public string soundName;
}

public struct StopAudioStruct
{
    public string soundName;
}