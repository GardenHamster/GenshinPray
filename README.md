# 原神模拟抽卡api

## 简介
 - 一个打算给qq机器人接入的小api，可以在群内模拟原神抽卡，根据米哈游公布的概率模拟生成十连或单抽结果图，返回图片链接和保底次数等信息
 - 相关功能还在摸鱼开发中，完善以后会发布release
 - api文档请参考 [Api文档](https://github.com/GardenHamster/GenshinPray/blob/main/Document.md)  
  
 ## 声明
 - 本项目为个人学习.net core、linux、docker、vue...时所开发出来的项目，并没有任何收益，不得将项目用于任何商业用途
  
## 特点
- 基于.net core 5.0 编写，可以在 Windows、macOS 和 Linux 操作系统上运行。

## 功能
- 记录群员祈愿次数和出货信息，完整模拟群员从零到恰保底的全过程
- 统计群内成员的出货率，统计群员的欧气排行
- 允许自定义卡池，可在后台界面配置注册并申请授权码和配置卡池。没有配置卡池时，默认使用admin配置的卡池

## 进度
- [x] 角色祈愿
- [x] 武器祈愿
- [x] 常驻祈愿
- [x] 武器定轨功能
- [x] 自定义卡池
- [x] 自定义图片大小，返回base64
- [ ] 图片上传到云盘，返回网络地址
- [x] 获取群成员祈愿信息
- [x] 统计群成员欧气排行
- [x] 接口授权，根据授权码限制每日调用次数
- [x] 定时清理历史图片
- [ ] 重复获取角色时，展示相应的转化素材和转化效果
- [x] 运行时自动建库建表
- [ ] docker支持
- [ ] 后台管理web界面

## 其他
- 目前缺少的素材有：天空之翼、和璞鸢，风鹰剑。如果有解包素材的大佬请务必发issuse联系我...
- 目前合成出来的图片中光效会比实际游戏画面中的暗一些，这是由于我使用背景橡皮擦擦除背景后导致的，还请各位PS大佬赐教...
- 目前主要的祈愿逻辑有[角色祈愿](https://github.com/GardenHamster/GenshinPray/blob/main/GenshinPray/Service/PrayService/RolePrayService.cs)，[武器祈愿](https://github.com/GardenHamster/GenshinPray/blob/main/GenshinPray/Service/PrayService/ArmPrayService.cs)，[常驻祈愿](https://github.com/GardenHamster/GenshinPray/blob/main/GenshinPray/Service/PrayService/PermPrayService.cs)，如果有逻辑上的问题还请大佬指正...
- 如果有更好的想法或者建议欢迎在issuse中提出来...

## 效果图
![20210822022412](https://user-images.githubusercontent.com/89188316/130333062-ef0a7f35-72c1-44d9-89be-e09e91c61e07.jpg)
![20210822022422](https://user-images.githubusercontent.com/89188316/130333063-747a3086-0646-40e2-b21a-83d7b9d659d5.jpg)
