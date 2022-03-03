# 原神模拟抽卡api

## 简介
 - 一个打算给qq机器人接入的小api，可以在群内模拟原神抽卡，根据米哈游公布的概率进行简单模拟，生成十连或单抽结果图，返回图片链接和保底次数等信息
 - 部署方法和接口文档请参考 [Document.md](https://github.com/GardenHamster/GenshinPray/blob/main/Document.md)  
  
 ## 声明
 - 本项目为个人学习.net core、linux、docker、vue...时所开发出来的项目，并没有任何收益，不得将项目用于任何商业用途
  
## 特点
- 基于.net core 6.0 编写，可以在 Windows 和 Linux 操作系统上运行。

## 功能
- 记录群员祈愿次数和出货信息，完整模拟群员从零到恰保底的全过程，统计群内成员的出货率，统计群员的欧气排行
- 允许自定义卡池，可在后台界面配置注册并申请授权码和配置卡池。没有配置卡池时，默认使用admin配置的卡池(authId=0)
- 允许使用服装素材替换原素材，需要在authorize表中SkinRate配置服装素材出现的概率

## 进度
- [x] 角色祈愿
- [x] 武器祈愿
- [x] 常驻祈愿
- [x] 武器定轨功能
- [x] 自定义卡池
- [x] 自定义图片大小，返回base64
- [x] 获取群成员祈愿信息
- [x] 获取群成员祈愿历史
- [x] 统计群成员欧气排行
- [x] 接口授权，根据授权码限制每日调用次数
- [x] 定时清理历史图片
- [x] 运行时自动建库建表
- [x] 双（多）角色池配置
- [x] 加入服装素材，根据概率使用服装素材替换原素材 
- [x] 重复获取角色时，展示相应的转化素材和转化效果
- [ ] docker支持
- [ ] 后台管理web界面

## 其他
- 原本想将效果异得更逼真一点，但是因为不会解包也不会PS，很多小细节都没法处理，所以爬了，有生之年再继续优化
- 素材会定期更新，但是要等到换池子以后才会更新，~~通常会在换池子后一两天才更新~~
- 服装类的贴图素材只能随缘更新了，因为以后出的服装我也不一定能全部找的到
- 如果有更好的想法或者建议欢迎在issuse中提出来...

## 效果图
![202202250152519250](https://user-images.githubusercontent.com/89188316/155640554-4a4b8228-5727-438b-b94d-592a5c15852d.jpg)

![202202250155180561](https://user-images.githubusercontent.com/89188316/155640578-4cbd76f6-b17a-4063-be8f-f6e7fe76c3cb.jpg)

![202202250132080800](https://user-images.githubusercontent.com/89188316/155640699-13f71dc3-6774-45a7-89fe-abb580f6afea.jpg)

![202202250136247688](https://user-images.githubusercontent.com/89188316/155640601-1784817a-1901-403e-bba2-807fe269b41c.jpg)

## 服装
![202202250132589202](https://user-images.githubusercontent.com/89188316/155640796-5295cb2a-a942-4db8-add5-f76720dd7db9.jpg)

![202202250131555284](https://user-images.githubusercontent.com/89188316/155640898-a05c4574-d107-4f11-9683-6349ebb4ca13.jpg)

![202202250131573224](https://user-images.githubusercontent.com/89188316/155640823-3184fd2f-de5a-43fe-bb3f-38905482e117.jpg)
