# 原神模拟抽卡api

## 简介
 - 一个打算给qq机器人接入的小api，可以在群内模拟原神抽卡，根据米哈游公布的概率进行简单模拟，生成十连或单抽结果图，返回图片链接和保底次数等信息
 - 可以下载 [jar包](https://github.com/GardenHamster/GenshinGacha)，并放入到在 [mirai-console-loader](https://github.com/iTXTech/mirai-console-loader) 中运行该项目， [食用方法请参考Mirai社区中的贴子](https://mirai.mamoe.net/topic/1036/genshingacha-%E6%A8%A1%E6%8B%9F%E5%8E%9F%E7%A5%9E%E6%8A%BD%E5%8D%A1%E6%8F%92%E4%BB%B6)
 - [部署方法和接口文档点击这里](https://github.com/GardenHamster/GenshinPray/blob/main/Document.md)
  
  ## 特点
- 基于.net core 6.0 编写，可以在 Windows 和 Linux 操作系统上运行。
  
 ## 声明
 - 本项目为个人学习.net core、linux、docker、vue...时所开发出来的项目，并没有任何收益，不得将项目用于任何商业用途
  
## 功能
- 记录群员祈愿次数和出货信息，完整模拟群员从零到恰保底的全过程，统计群内成员的出货率，统计群员的欧气排行
- 允许自定义卡池，可在后台界面配置注册并申请授权码和配置卡池。没有配置卡池时，默认使用admin配置的卡池(authId=0)
- 允许使用服装素材替换原素材，需要在authorize表中SkinRate配置服装素材出现的概率

## 进度
- [x] 角色祈愿
- [x] 武器祈愿
- [x] 常驻祈愿
- [x] 武器定轨功能
- [x] 全角色/全武器卡池
- [x] 自定义卡池，可配置多个角色池
- [x] 自定义图片大小，返回base64
- [x] 获取群成员祈愿信息
- [x] 获取群成员祈愿历史
- [x] 统计群成员欧气排行
- [x] 接口授权，根据授权码限制每日调用次数
- [x] 定时清理历史图片
- [x] 运行时自动建库建表
- [x] 加入服装素材，根据概率使用服装素材替换原素材 
- [x] 重复获取角色时，展示相应的转化素材和转化效果
- [ ] 合成自定义单抽/十连结果图
- [ ] docker部署

## 其他
- 原本想将效果异得更逼真一点，但是PS技术太渣，很多小细节都没法处理，所以爬了，~~有生之年再继续优化~~
- 感谢作者Razmoth的解包项目[GenshinStudio](https://github.com/Razmoth/GenshinStudio)，现在可以定期更新素材了...
- 部分测试服中的素材来源于内鬼网 [genshin.honeyhunterworld.com](https://genshin.honeyhunterworld.com/fam_chars/?lang=EN)，这部分素材将会在官服预更新出来以后再替换掉
- 如果有bug或者建议欢迎在issuse中提出来...

## 效果图
![202202250152519250](https://user-images.githubusercontent.com/89188316/155640554-4a4b8228-5727-438b-b94d-592a5c15852d.jpg)

![202202250155180561](https://user-images.githubusercontent.com/89188316/155640578-4cbd76f6-b17a-4063-be8f-f6e7fe76c3cb.jpg)

![202202250132080800](https://user-images.githubusercontent.com/89188316/155640699-13f71dc3-6774-45a7-89fe-abb580f6afea.jpg)

![202202250136247688](https://user-images.githubusercontent.com/89188316/155640601-1784817a-1901-403e-bba2-807fe269b41c.jpg)

## 服装
![202202250132589202](https://user-images.githubusercontent.com/89188316/155640796-5295cb2a-a942-4db8-add5-f76720dd7db9.jpg)

![202202250131555284](https://user-images.githubusercontent.com/89188316/155640898-a05c4574-d107-4f11-9683-6349ebb4ca13.jpg)

![202202250131573224](https://user-images.githubusercontent.com/89188316/155640823-3184fd2f-de5a-43fe-bb3f-38905482e117.jpg)
