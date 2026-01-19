using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using RenderHeads.Media.AVProVideo;

/// <summary>
/// Video playback group configuration for HTML5VideoPlayer.
/// Each group contains buttons and a media player for controlling video playback.
/// </summary>
[System.Serializable]
public class VideoGroup
{
    [Tooltip("Button to play the video")]
    public UnityEngine.UI.Button playButton;
    
    [Tooltip("Button to pause the video")]
    public UnityEngine.UI.Button pauseButton;
    
    [Tooltip("Button to stop and reset the video")]
    public UnityEngine.UI.Button stopButton;
    
    [Tooltip("UI display component for showing the video")]
    public DisplayUGUI displayUGUI;
    
    [Tooltip("AVPro Video media player component")]
    public MediaPlayer mediaPlayer;
    
    [Tooltip("Video file name relative to StreamingAssets folder")]
    public string videoFileName = "11.longyugxq";
    
    [HideInInspector] public bool isLoaded = false;
}

/// <summary>
/// Manages HTML5 video playback in Unity using AVPro Video plugin.
/// Supports multiple video groups with independent play/pause/stop controls.
/// 
/// Requirements:
/// - AVPro Video plugin must be installed in the project
/// - Video files should be located in StreamingAssets folder
/// 
/// Usage:
/// 1. Add this script to a GameObject
/// 2. Configure VideoGroup entries in the Inspector
/// 3. Assign UI buttons and MediaPlayer components
/// 4. Set the video file name for each group
/// 5. Buttons will automatically control video playback
/// </summary>
public class HTML5VideoPlayer : MonoBehaviour
{
    /// <summary>List of video groups to manage</summary>
    [SerializeField] private List<VideoGroup> videoGroups = new List<VideoGroup>();
    
    /// <summary>Prevents multiple simultaneous initialization</summary>
    private bool isInitialized = false;
    
    /// <summary>Tracks which video is currently playing to prevent overlap</summary>
    private int currentPlayingGroupIndex = -1;

    private void Start()
    {
        if (!isInitialized)
        {
            StartCoroutine(InitializeAsync());
        }
    }

    /// <summary>
    /// Initializes all video groups asynchronously.
    /// Loads videos and binds button events.
    /// </summary>
    private IEnumerator InitializeAsync()
    {
        isInitialized = true;
        
        for (int i = 0; i < videoGroups.Count; i++)
        {
            yield return InitializeVideoGroupAsync(videoGroups[i], i);
        }
        
        Debug.Log($"[HTML5VideoPlayer] All {videoGroups.Count} video groups initialized successfully");
    }

    /// <summary>
    /// Initializes a single video group.
    /// </summary>
    /// <param name="group">The video group to initialize</param>
    /// <param name="groupIndex">Index of the group in the list</param>
    private IEnumerator InitializeVideoGroupAsync(VideoGroup group, int groupIndex)
    {
        // Validate MediaPlayer assignment
        if (group.mediaPlayer == null)
        {
            Debug.LogError($"[HTML5VideoPlayer] VideoGroup {groupIndex} has no MediaPlayer assigned in Inspector");
            yield break;
        }

        // Validate video file name
        if (string.IsNullOrEmpty(group.videoFileName))
        {
            Debug.LogError($"[HTML5VideoPlayer] VideoGroup {groupIndex} has empty video file name");
            yield break;
        }

        // Open video file from StreamingAssets
        bool opened = group.mediaPlayer.OpenMedia(
            new MediaPath(group.videoFileName, MediaPathType.RelativeToStreamingAssetsFolder),
            autoPlay: false
        );

        if (!opened)
        {
            Debug.LogError($"[HTML5VideoPlayer] Failed to open video: {group.videoFileName}");
            yield break;
        }

        // Wait for video loading
        yield return new WaitForSeconds(0.1f);

        Debug.Log($"[HTML5VideoPlayer] VideoGroup {groupIndex} loaded: {group.videoFileName}");

        // Bind button events
        BindButtonEvents(group, groupIndex);

        group.isLoaded = true;
    }

    /// <summary>
    /// Binds UI button click events to video control methods.
    /// </summary>
    private void BindButtonEvents(VideoGroup group, int groupIndex)
    {
        if (group.playButton != null)
        {
            group.playButton.onClick.AddListener(() => PlayVideo(groupIndex));
        }
        else
        {
            Debug.LogWarning($"[HTML5VideoPlayer] VideoGroup {groupIndex} has no Play button assigned");
        }

        if (group.pauseButton != null)
        {
            group.pauseButton.onClick.AddListener(() => PauseVideo(groupIndex));
        }
        else
        {
            Debug.LogWarning($"[HTML5VideoPlayer] VideoGroup {groupIndex} has no Pause button assigned");
        }

        if (group.stopButton != null)
        {
            group.stopButton.onClick.AddListener(() => StopVideo(groupIndex));
        }
        else
        {
            Debug.LogWarning($"[HTML5VideoPlayer] VideoGroup {groupIndex} has no Stop button assigned");
        }
    }

    /// <summary>
    /// Plays the video of the specified group.
    /// Stops other videos if playing to prevent overlap.
    /// </summary>
    /// <param name="groupIndex">Index of the video group to play</param>
    public void PlayVideo(int groupIndex)
    {
        if (!IsValidGroupIndex(groupIndex))
            return;

        VideoGroup targetGroup = videoGroups[groupIndex];
        
        if (!targetGroup.isLoaded || targetGroup.mediaPlayer == null)
        {
            Debug.LogWarning($"[HTML5VideoPlayer] Cannot play: VideoGroup {groupIndex} is not loaded");
            return;
        }

        // Stop other playing videos to prevent overlap
        if (currentPlayingGroupIndex >= 0 && currentPlayingGroupIndex != groupIndex)
        {
            StopVideo(currentPlayingGroupIndex);
        }

        targetGroup.mediaPlayer.Play();
        currentPlayingGroupIndex = groupIndex;
        Debug.Log($"[HTML5VideoPlayer] Playing VideoGroup {groupIndex}");
    }

    /// <summary>
    /// Pauses the video of the specified group.
    /// </summary>
    /// <param name="groupIndex">Index of the video group to pause</param>
    public void PauseVideo(int groupIndex)
    {
        if (!IsValidGroupIndex(groupIndex))
            return;

        VideoGroup targetGroup = videoGroups[groupIndex];
        
        if (targetGroup.mediaPlayer == null)
        {
            Debug.LogWarning($"[HTML5VideoPlayer] Cannot pause: VideoGroup {groupIndex} has no MediaPlayer");
            return;
        }

        targetGroup.mediaPlayer.Pause();
        Debug.Log($"[HTML5VideoPlayer] Paused VideoGroup {groupIndex}");
    }

    /// <summary>
    /// Stops the video of the specified group and resets playback to the beginning.
    /// </summary>
    /// <param name="groupIndex">Index of the video group to stop</param>
    public void StopVideo(int groupIndex)
    {
        if (!IsValidGroupIndex(groupIndex))
            return;

        VideoGroup targetGroup = videoGroups[groupIndex];
        
        if (targetGroup.mediaPlayer == null)
        {
            Debug.LogWarning($"[HTML5VideoPlayer] Cannot stop: VideoGroup {groupIndex} has no MediaPlayer");
            return;
        }

        MediaPlayer mediaPlayer = targetGroup.mediaPlayer;
        mediaPlayer.Stop();
        mediaPlayer.Control.Seek(0.0);
        
        if (currentPlayingGroupIndex == groupIndex)
        {
            currentPlayingGroupIndex = -1;
        }

        Debug.Log($"[HTML5VideoPlayer] Stopped VideoGroup {groupIndex}");
    }

    /// <summary>
    /// Checks if the group index is valid.
    /// </summary>
    private bool IsValidGroupIndex(int groupIndex)
    {
        return groupIndex >= 0 && groupIndex < videoGroups.Count;
    }

    /// <summary>
    /// Gets the current number of video groups.
    /// </summary>
    public int GetVideoGroupCount()
    {
        return videoGroups.Count;
    }

    /// <summary>
    /// Gets the loaded status of a specific video group.
    /// </summary>
    public bool IsVideoGroupLoaded(int groupIndex)
    {
        return IsValidGroupIndex(groupIndex) && videoGroups[groupIndex].isLoaded;
    }

    /// <summary>
    /// Stops all videos when the object is destroyed.
    /// </summary>
    private void OnDestroy()
    {
        for (int i = 0; i < videoGroups.Count; i++)
        {
            if (videoGroups[i].mediaPlayer != null)
            {
                videoGroups[i].mediaPlayer.Stop();
            }
        }
    }
}
