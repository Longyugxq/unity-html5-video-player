# HTML5 Video Player for Unity

A lightweight, easy-to-use video playback manager for Unity projects using the [AVPro Video](https://www.renderheads.com/content/apis/). Manage multiple video groups with independent play, pause, and stop controls through UI buttons.

## Features

✨ **Multi-Video Support** - Manage multiple video groups simultaneously  
🎮 **Simple UI Integration** - Bind buttons directly through Inspector  
⚡ **Async Loading** - Non-blocking video initialization  
🛡️ **Robust Error Handling** - Comprehensive validation and logging  
📋 **Well-Documented** - Full XML documentation for IntelliSense support  
🎯 **Production-Ready** - Includes automatic cleanup and safety checks  

## Requirements

- **Unity 2020.3+** (or any version supporting AVPro Video)
- **AVPro Video Plugin** - Must be installed in your project
- Video files should be placed in the `StreamingAssets` folder

## Installation

1. Copy `HTML5VideoPlayer.cs` to your project's `Assets/Scripts` folder
2. Ensure AVPro Video plugin is installed and configured
3. Attach the script to any GameObject in your scene

## Quick Start

### Basic Setup

1. **Create a GameObject** with the `HTML5VideoPlayer` component
2. **Configure Video Groups** in the Inspector:
   - Set the number of video groups you need
   - For each group:
     - Assign the **Play Button** (UI Button)
     - Assign the **Pause Button** (UI Button)
     - Assign the **Stop Button** (UI Button)
     - Assign the **MediaPlayer** (AVPro Video MediaPlayer component)
     - Assign the **DisplayUGUI** (AVPro Video DisplayUGUI component)
     - Set the **Video File Name** (relative to StreamingAssets folder)

3. **Place Videos** in `Assets/StreamingAssets/` directory

### Example Inspector Configuration

```
HTML5VideoPlayer (Component)
├─ Video Groups
│  ├─ [0]
│  │  ├─ Play Button: [Reference to UI Button]
│  │  ├─ Pause Button: [Reference to UI Button]
│  │  ├─ Stop Button: [Reference to UI Button]
│  │  ├─ MediaPlayer: [Reference to MediaPlayer]
│  │  ├─ DisplayUGUI: [Reference to DisplayUGUI]
│  │  └─ Video File Name: "myvideo.mp4"
│  └─ [1]
│     └─ [Similar configuration for second video]
```

## API Reference

### Public Methods

#### `PlayVideo(int groupIndex)`
Plays the video of the specified group. Automatically stops other videos to prevent overlap.

```csharp
videoPlayer.PlayVideo(0); // Play first video group
```

#### `PauseVideo(int groupIndex)`
Pauses the video of the specified group.

```csharp
videoPlayer.PauseVideo(0); // Pause first video group
```

#### `StopVideo(int groupIndex)`
Stops the video and resets playback to the beginning.

```csharp
videoPlayer.StopVideo(0); // Stop first video group
```

#### `GetVideoGroupCount()`
Returns the total number of configured video groups.

```csharp
int count = videoPlayer.GetVideoGroupCount();
```

#### `IsVideoGroupLoaded(int groupIndex)`
Checks if a specific video group has been successfully loaded.

```csharp
if (videoPlayer.IsVideoGroupLoaded(0))
{
    Debug.Log("Video 0 is ready to play");
}
```

## Advanced Usage

### Programmatic Control

```csharp
public class VideoController : MonoBehaviour
{
    [SerializeField] private HTML5VideoPlayer videoPlayer;
    
    public void OnPlayButtonClicked()
    {
        videoPlayer.PlayVideo(0);
    }
    
    public void OnPauseButtonClicked()
    {
        videoPlayer.PauseVideo(0);
    }
    
    public void OnStopButtonClicked()
    {
        videoPlayer.StopVideo(0);
    }
}
```

### Checking Video Status

```csharp
// Check if all videos are loaded
for (int i = 0; i < videoPlayer.GetVideoGroupCount(); i++)
{
    if (videoPlayer.IsVideoGroupLoaded(i))
    {
        Debug.Log($"Video {i} is loaded and ready");
    }
}
```

## Video File Path Notes

- Video files should be placed in `Assets/StreamingAssets/`
- Use relative paths without the `StreamingAssets/` prefix
- Example: If your video is at `Assets/StreamingAssets/videos/intro.mp4`, set the filename to `videos/intro.mp4`
- Supported formats depend on your platform and AVPro Video configuration

## Troubleshooting

### Videos Not Loading

1. **Check Console Logs** - Look for error messages prefixed with `[HTML5VideoPlayer]`
2. **Verify File Path** - Ensure video files are in `StreamingAssets` folder
3. **Check AVPro Settings** - Ensure AVPro Video is properly configured
4. **Verify References** - Make sure all Inspector assignments are correct (no null references)

### Common Issues

| Issue | Solution |
|-------|----------|
| "VideoGroup X has no MediaPlayer" | Assign a MediaPlayer component to the video group in Inspector |
| "Failed to open video" | Check video file path and ensure file exists in StreamingAssets |
| Videos not displaying | Assign DisplayUGUI component and ensure UI Canvas is properly set up |
| Buttons not responding | Verify buttons are assigned and not disabled in hierarchy |

## Performance Considerations

- Initialize videos during loading screens for smoother playback
- Avoid loading too many videos simultaneously
- Use compressed video formats for better performance
- Consider platform-specific video codecs (H.264, VP8, etc.)

## License

This script is provided as-is for use in Unity projects. Modify and distribute freely in accordance with the MIT License.

## Changelog

### Version 1.0.0 (2026-01-19)
- Initial release
- Support for multiple video groups
- Play/Pause/Stop controls
- Async loading with validation
- Comprehensive error handling
- Full API documentation

## Support

For issues, questions, or feature requests, please open an issue on the repository.

---

**Note:** This script is designed as a helper utility for video playback management. For advanced video features, consult the [AVPro Video documentation](https://www.renderheads.com/content/documentation/).
