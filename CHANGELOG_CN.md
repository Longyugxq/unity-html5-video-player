# 更新日志

本文档记录项目的所有重要变更。

格式参考 [Keep a Changelog](https://keepachangelog.com/zh-CN/1.0.0/)，
本项目遵循 [语义化版本](https://semver.org/lang/zh-CN/)。

## [1.0.0] - 2026-01-19

### 新增
- HTML5VideoPlayer 脚本初始发布
- 支持多个视频组，各组可独立控制
- 视频播放、暂停和停止功能
- 支持异步加载和 StreamingAssets 路径
- 完整的错误处理和数据校验
- 完整的 XML 文档注释（支持 IntelliSense）
- 自动防止多个视频重叠播放
- 销毁时自动清理资源
- 提供辅助方法检查视频加载状态和组数量

### 功能特性
- 可在 Unity Inspector 中直接绑定 UI 按钮
- 支持 AVPro Video 的 MediaPlayer 和 DisplayUGUI 组件
- 详细的调试日志便于故障排查
- 空引用检查和完整的数据校验
- 生产级别的错误提示信息

### 文档
- 包含快速开始指南的完整 README
- 附带代码示例的 API 参考
- 故障排查部分
- 性能考量建议
- 包含 MIT 许可证

---
