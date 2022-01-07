# 原神模拟抽卡api

## 简介
 - 一个打算给qq机器人接入的小api，可以在群内模拟原神抽卡，根据米哈游公布的概率模拟生成十连或单抽结果图，返回图片链接和保底次数等信息
 - 相关功能还在摸鱼开发中，部署方法和接口文档请参考 [Document.md](https://github.com/GardenHamster/GenshinPray/blob/main/Document.md)  
  
 ## 声明
 - 本项目为个人学习.net core、linux、docker、vue...时所开发出来的项目，并没有任何收益，不得将项目用于任何商业用途
  
## 特点
- 基于.net core 5.0 编写，可以在 Windows、macOS 和 Linux 操作系统上运行。

## 功能
- 记录群员祈愿次数和出货信息，完整模拟群员从零到恰保底的全过程，统计群内成员的出货率，统计群员的欧气排行
- 允许自定义卡池，可在后台界面配置注册并申请授权码和配置卡池。没有配置卡池时，默认使用admin配置的卡池

## 进度
- [x] 角色祈愿
- [x] 武器祈愿
- [x] 常驻祈愿
- [x] 武器定轨功能
- [x] 自定义卡池
- [x] 自定义图片大小，返回base64
- [x] 获取群成员祈愿信息
- [x] 统计群成员欧气排行
- [x] 接口授权，根据授权码限制每日调用次数
- [x] 定时清理历史图片
- [x] 运行时自动建库建表
- [ ] 重复获取角色时，展示相应的转化素材和转化效果
- [ ] 双限定角色池
- [ ] docker支持
- [ ] 后台管理web界面

## 其他
- 如果有更好的想法或者建议欢迎在issuse中提出来...

## 效果图
![202112271711243926](https://user-images.githubusercontent.com/89188316/147456428-7f6e1d5e-309d-41fb-bfe6-c2081e5ed2a2.jpg)
![202112271711329931](https://user-images.githubusercontent.com/89188316/147456514-65f5bbcc-7e90-4488-9742-77194f2d6c0b.jpg)
![202112271712160461](https://user-images.githubusercontent.com/89188316/147456549-654ef560-bddb-4ec6-85a6-1a380189af73.jpg)
![202112271712129935](https://user-images.githubusercontent.com/89188316/147456560-49fb3b9a-161b-4565-8407-9e5fbcd359f2.jpg)


