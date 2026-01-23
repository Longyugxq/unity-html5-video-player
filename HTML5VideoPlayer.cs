using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using RenderHeads.Media.AVProVideo;

[System.Serializable]
public class VideoGroup
{
    public UnityEngine.UI.Button playButton;
    public UnityEngine.UI.Button pauseButton;
    public UnityEngine.UI.Button stopButton;
    public DisplayUGUI displayUGUI;
    public MediaPlayer mediaPlayer;  
    public string videoFileName = "11.longyugxq";
    
    [HideInInspector] public bool isLoaded = false;
}

public class HTML5VideoPlayer : MonoBehaviour
{
    [SerializeField] private List<VideoGroup> videoGroups = new List<VideoGroup>();

    private void Start()
    {
        StartCoroutine(InitializeAsync());
    }

    private IEnumerator InitializeAsync()
    {
        for (int i = 0; i < videoGroups.Count; i++)
        {
            yield return InitializeVideoGroupAsync(videoGroups[i], i);
        }
    }

    private IEnumerator InitializeVideoGroupAsync(VideoGroup group, int groupIndex)
    {
        // 检查MediaPlayer是否已在Inspector中设置
        if (group.mediaPlayer == null)
        {
            Debug.LogError($"VideoGroup {groupIndex} 未配置 MediaPlayer");
            yield break;
        }

        // 打开视频文件
        bool opened = group.mediaPlayer.OpenMedia(
            new MediaPath(group.videoFileName, MediaPathType.RelativeToStreamingAssetsFolder),
            autoPlay: false
        );

        if (!opened)
        {
            Debug.LogError($"打开视频失败: {group.videoFileName}");
            yield break;
        }

        // 等待加载完成
        yield return new WaitForSeconds(0.01f);

        Debug.Log($"VideoGroup {groupIndex} - {group.videoFileName} 加载完成");

        // 绑定按钮事件
        if (group.playButton != null)
            group.playButton.onClick.AddListener(() => PlayVideo(groupIndex));
        if (group.pauseButton != null)
            group.pauseButton.onClick.AddListener(() => PauseVideo(groupIndex));
        if (group.stopButton != null)
            group.stopButton.onClick.AddListener(() => StopVideo(groupIndex));

        group.isLoaded = true;
    }

    public void PlayVideo(int groupIndex)
    {
        if (groupIndex >= 0 && groupIndex < videoGroups.Count && videoGroups[groupIndex].isLoaded && videoGroups[groupIndex].mediaPlayer != null)
        {
            videoGroups[groupIndex].mediaPlayer.Play();
        }
    }

    public void PauseVideo(int groupIndex)
    {
        if (groupIndex >= 0 && groupIndex < videoGroups.Count && videoGroups[groupIndex].mediaPlayer != null)
        {
            videoGroups[groupIndex].mediaPlayer.Pause();
        }
    }

    public void StopVideo(int groupIndex)
    {
        if (groupIndex >= 0 && groupIndex < videoGroups.Count && videoGroups[groupIndex].mediaPlayer != null)
        {
            MediaPlayer mediaPlayer = videoGroups[groupIndex].mediaPlayer;
            mediaPlayer.Stop();
            mediaPlayer.Control.Seek(0.0);
        }
    }
}

