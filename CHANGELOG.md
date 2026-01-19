# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-01-19

### Added
- Initial release of HTML5VideoPlayer script
- Support for multiple video groups with independent controls
- Play, pause, and stop video playback functionality
- Async video loading with StreamingAssets support
- Comprehensive error handling and validation
- Full XML documentation for IntelliSense support
- Automatic prevention of overlapping video playback
- OnDestroy cleanup for proper resource management
- Helper methods for checking video load status and group count

### Features
- Bind UI buttons directly through Unity Inspector
- Support for AVPro Video MediaPlayer and DisplayUGUI components
- Detailed debug logging for troubleshooting
- Null reference and validation checks
- Production-ready error messages

### Documentation
- Comprehensive README with quick start guide
- API reference with code examples
- Troubleshooting section
- Performance considerations
- MIT License included

---

## Future Enhancements (Roadmap)

- [ ] Event callbacks (OnVideoFinished, OnVideoLoaded, etc.)
- [ ] Playback speed control
- [ ] Volume control integration
- [ ] Subtitle/Caption support
- [ ] Seek bar synchronization
- [ ] Full-screen mode support
- [ ] Video queue management
- [ ] Streaming support
