# 原神模拟抽卡

## 简介
 - 一个打算给qq机器人接入的小api，可以在群内模拟原神抽卡，根据米哈游公布的概率模拟生成十连或单抽结果图，并返回图片链接
 - 相关功能还在摸鱼开发中，完善以后会发布release
  
## 特点
- 基于.net core 5.0 编写，可以在 Windows、macOS 和 Linux 操作系统上运行。

## 功能
- 记录群员祈愿次数和出货信息，完整模拟群员从零到恰保底的全过程
- 统计群内成员的出货率，统计群员的欧气排行
- 允许自定义卡池，可在调用api时传入up信息

## 其他
- 目前还缺少常驻池中天空之翼、天空之卷，狼的末路...等武器素材，如果有相关素材的大佬请务必发issuse联系我...
- 目前合成出来的图片中光效会比实际游戏画面中的暗一些，这是由于我使用背景橡皮擦擦除背景后导致的，还请各位PS大佬赐教...
- 主要的抽奖逻辑写在[BasePrayService.cs](https://github.com/GardenHamster/GenshinPray/blob/main/GenshinPray/Service/BasePrayService.cs)，如果有逻辑上的问题还请大佬指正...
- 有更好的想法或者建议欢迎在issues中提出来...

## 效果图
![20210822022412](https://user-images.githubusercontent.com/89188316/130333062-ef0a7f35-72c1-44d9-89be-e09e91c61e07.jpg)
![20210822022422](https://user-images.githubusercontent.com/89188316/130333063-747a3086-0646-40e2-b21a-83d7b9d659d5.jpg)

