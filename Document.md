# API 文档

## 数据库
- 数据库为mysql，项目启动后会自动建库建表，初次运行请手动导入初始数据 [initData.sql](https://github.com/GardenHamster/GenshinPray/blob/main/GenshinPray/Sql/initData.sql)
- 导入完成后在 `authorize` 表中添加自己的授权码，表和字段说明请参考数据表中的 [注释](https://github.com/GardenHamster/GenshinPray/tree/main/GenshinPray/Models/PO)
- 注：项目启动时会将默认蛋池(authId=0)加载到内存，修改默认蛋池后请手动重启服务

## 字体
- 安装[Fonts目录](https://github.com/GardenHamster/GenshinPray/tree/main/GenshinPray/Fonts)下的字体，否则可能会出现中文乱码的问题

## 部署
- 从 [releases](https://github.com/GardenHamster/GenshinPray/releases) 处下载最新版本，然后解压到某处
- 修改目录下的配置文件`appsettings.Production.json`
```json5
{
  "ConnectionString": "Data Source=127.0.0.1;port=3306;Initial Catalog=genshinpray;uid=root;pwd=123456;CharSet=utf8mb4;SslMode=None;",   //数据库链接字符串
  "PrayImgSavePath": "C:\\tool\\apache-tomcat-8.5.65\\webapps\\prayImg",              //祈愿结果图片保存目录
  "PrayMaterialSavePath": "C:\\PrayMaterial",                                         //祈愿素材图片目录
  "PrayImgHttpUrl": "https://127.0.0.1/prayImg/{imgPath}"                             //祈愿接口中返回的图片地址,需要自行配置tomcat等图片下载服务
}
```
- 然后将 [PrayMaterial](https://github.com/GardenHamster/GenshinPray/tree/main/GenshinPray/PrayMaterial) 中的素材复制到PrayMaterialSavePath配置的路径下


### centos7下部署
1、签名密钥并添加 Microsoft 包存储库
```bash
sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
```
2、安装ASP.NET Core 6.0 运行时
```bash
sudo yum install aspnetcore-runtime-6.0
```
3、安装libgdiplus
```bash
yum install epel-release
```
```bash
sudo yum install libgdiplus
```
```bash
sudo ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll     （32位系统）
sudo ln -s /usr/lib64/libgdiplus.so /usr/lib64/gdiplus.dll （64位系统）
```
4、切换到GenshinPray.dll所在目录下，运行GenshinPray.dll，根据自己的需要修改端口和http或https
```bash
nohup dotnet GenshinPray.dll --launch-profile Production --urls http://0.0.0.0:8080
```
正常运行结果如下
```bash
2021-09-23 21:48:26,890 [1] INFO  ConsoleLog - 读取配置文件...
2021-09-23 21:48:26,909 [1] INFO  ConsoleLog - 初始化数据库...
2021-09-23 21:48:35,453 [1] INFO  ConsoleLog - 数据库初始化完毕...
2021-09-23 21:48:35,512 [1] INFO  ConsoleLog - 正在初始化定时任务...
2021-09-23 21:48:35,801 [1] INFO  ConsoleLog - 正在初始化定时清理任务...
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://0.0.0.0:8080
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /srv/GenshinPray
```

### windows下部署
- 下载并安装 [ASP.NET Core Runtime 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)，推荐下载页面中的[Hosting Bundle](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-6.0.2-windows-hosting-bundle-installer)
- 启动powershell并将路径切换到GenshinPray.dll所在目录下(没有这个dll说明你下载错文件了，要在Release里面下载)

- 运行GenshinPray.dll，根据自己的需要修改端口和http或https
```bash
dotnet GenshinPray.dll --launch-profile Production --urls http://0.0.0.0:8080
```
- 可以在桌面创建一个powershell.ps1脚本方便一键启动，注意将路径和端口改成自己的
```bash
$host.ui.RawUI.WindowTitle="GenshinPray"
cd C:\GenshinPray
dotnet GenshinPray.dll --launch-profile Production --urls http://0.0.0.0:8080
```

### 搭建图片下载服务
- 使用 tomcat 或 nginx 等工具搭建下载服务，然后修改 `PrayImgHttpUrl` 和 `PrayImgHttpUrl` 的值确保图片能够被其他客户端访问

## 枚举
- 数据表中如字段名为 *Type 等字段值请参考[GenshinPray/Type](https://github.com/GardenHamster/GenshinPray/tree/main/GenshinPray/Type)

## 状态码
- 所有接口返回的数据都包含一个 `code` 字段的状态码, 分别代表不同的响应状态. 详细请参考[ResultCode.cs](https://github.com/GardenHamster/GenshinPray/blob/main/GenshinPray/Common/ResultCode.cs)

## 授权码
- 大部分接口都需要在调用时在Http Header中传入授权码`authorzation`
- 可以为每个授权码配置自己的自定义蛋池，以及每日api调用次数`DailyCall`，以及皮肤出现的概率`SkinRate`

## 祈愿接口
| 请求类型 | 请求地址                     |  说明                        |
| ------ | ---------------------------- | ---------------------------- |
| Get    | /api/RolePray/PrayOne        | 角色池单抽                    |
| Get    | /api/RolePray/PrayTen        | 角色池十连                    |
| Get    | /api/ArmPray/PrayOne         | 武器池单抽                    |
| Get    | /api/ArmPray/PrayTen         | 武器池十连                    |
| Get    | /api/PermPray/PrayOne        | 常驻单抽                      |
| Get    | /api/PermPray/PrayTen        | 常驻十连                      |
| Get    | /api/FullRolePray/PrayOne    | 全角色池单抽                   |
| Get    | /api/FullRolePray/PrayTen    | 全角色池十连                   |
| Get    | /api/FullArmPray/PrayOne     | 全武器池单抽                   |
| Get    | /api/FullArmPray/PrayTen     | 全武器池十连                   |

#### 参数

| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中          | 是     |                       | 
| memberCode    | 成员编号                     | 是     |                       |
| memberName    | 成员名称                     | 否     |                       |
| pondIndex     | 蛋池编号,角色池可选           | 否     | 0                     |
| imgWidth      | 生成图片宽度,单位px           | 否     | 1920                  |
| toBase64      | 是否返回base64字符串          | 否     | false                |

#### 响应

```json5
{
    "code": 0,                            //状态码
    "message": "ok",                      //处理结果
    "data": {                             
        "prayCount": 10,                  //祈愿次数     
        "role180Surplus": 10,             //角色池剩余多少抽大保底
        "role90Surplus": 10,              //角色池剩余多少抽保底
        "arm80Surplus": 62,               //武器池剩余多少抽保底
        "armAssignValue": 2,              //武器池当前命定值
        "perm90Surplus": 89,              //常驻池剩余多少抽保底
        "fullRole90Surplus": 90,          //全角色池剩余多少抽保底
        "fullArm80Surplus": 64,           //全武器池剩余多少抽保底
        "surplus10": 3,                   //当前蛋池剩余多少抽十连保底
        "star5Cost": 0,                   //本次获取五星物品累计消耗多少抽，如果本次未抽出五星时，值为0
        "apiDailyCallSurplus": 299,       //本日Api剩余可调用次数
        "imgHttpUrl": "https://127.0.0.1/prayImg/GenshinPray20220411/11/202204111151335928.jpg",  //图片在tomcat中的http地址
        "imgSize": 97162,                 //图片大小(byte)
        "imgPath": "GenshinPray20220411\\11\\202204111151335928.jpg",   //相对于图片生成目录的地址
        "imgBase64": null,                //图片的base64字符串
        "star3Goods": [                   //本次祈愿获取的3星物品列表
            {
                "goodsName": "翡玉法球",   //物品名称
                "goodsType": "武器",       //物品类型，武器/角色
                "goodsSubType": "法器",    //物品子类型
                "rareType": "三星"         //稀有类型
            }
        ],
        "star4Goods": [                    //本次祈愿获取的4星物品列表
            {
                "goodsName": "早柚",
                "goodsType": "角色",
                "goodsSubType": "风",
                "rareType": "四星"
            }
        ],
        "star5Goods": [],                  //本次祈愿获取的5星物品列表
        "star4Up": [                       //当前蛋池中的4星UP列表
            {
                "goodsName": "迪奥娜",
                "goodsType": "角色",
                "goodsSubType": "冰",
                "rareType": "四星"
            }
        ],
        "star5Up": [                       //当前蛋池中的5星UP列表
            {
                "goodsName": "宵宫",
                "goodsType": "角色",
                "goodsSubType": "火",
                "rareType": "五星"
            }
        ]
    }
}
```

## 武器定轨
- 如果默认或自定义蛋池中不包含目标武器，将视为定轨失败。
- 如果祈愿时已定轨武器但定轨目标不在当前蛋池中，将视为未定轨处理
- 在后台修改自定义蛋池时，将会清空定轨目标级命定值

| 请求类型 | 请求地址                        |  说明                         |
| ------ | -------------------------------- | ---------------------------- |
| Get    | /api/PrayInfo/SetMemberAssign    | 武器定轨                      | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中          | 是    |                       |
| memberCode    | 成员编号                      | 是    |                       |
| memberName    | 成员名称                      | 否    |                       |
| goodsName     | 武器名称                      | 是    |                       |

#### 响应

```json5
{
    "code": 0,
    "message": "ok",
    "data": null
}
```


## 查询定轨

| 请求类型 | 请求地址                        |  说明                         |
| ------ | -------------------------------- | ---------------------------- |
| Get    | /api/PrayInfo/GetMemberAssign    | 查询定轨                      | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中           | 是    |                       |
| memberCode    | 成员编号                      | 是    |                       |

#### 响应

```json5
{
    "code": 0,
    "message": "ok",
    "data": {                               //值为null时表示未找到定轨信息                               
        "goodsName": "飞雷之弦振",           //物品名称
        "goodsType": "武器",                //物品类型，武器/角色
        "goodsSubType": "弓",               //物品子类型
        "rareType": "五星",                 //稀有类型
        "assignValue": 0                    //命定值
    }
}
```

## 查询蛋池

- 查询当前蛋池的所有UP信息，如果未配置自定义蛋池时，返回默认蛋池信息

| 请求类型 | 请求地址                        |  说明                         |
| ------ | -------------------------------- | ---------------------------- |
| Get    | /api/PrayInfo/GetPondInfo       | 查询蛋池                      | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中           | 是    |                       |

#### 响应

```json5
{
    "code": 0,
    "message": "ok",
    "data": {
        "arm": [                                     //武器池
            {
                "pondIndex": 0,                      //蛋池编号,武器池只有一个蛋池
                "pondInfo": {
                    "star5UpList": [],               //5星列表,这里省略里面的内容...
                    "star4UpList": []                //4星列表,这里省略里面的内容...
                }
            }
        ],
        "role": [
            {
                "pondIndex": 0,                      //蛋池编号,用于角色池可以配置多个蛋池
                "pondInfo": {
                    "star5UpList": [
                        {
                            "goodsName": "八重神子",
                            "goodsType": "角色",
                            "goodsSubType": "雷",
                            "rareType": "五星"
                        }
                    ],
                    "star4UpList": [
                        {
                            "goodsName": "托马",
                            "goodsType": "角色",
                            "goodsSubType": "火",
                            "rareType": "四星"
                        },
                        {
                            "goodsName": "菲谢尔",
                            "goodsType": "角色",
                            "goodsSubType": "雷",
                            "rareType": "四星"
                        },
                        {
                            "goodsName": "迪奥娜",
                            "goodsType": "角色",
                            "goodsSubType": "冰",
                            "rareType": "四星"
                        }
                    ]
                }
            },
            {
                "pondIndex": 1,                   //蛋池编号,用于角色池可以配置多个蛋池
                "pondInfo": {
                    "star5UpList": [
                        {
                            "goodsName": "雷电将军",
                            "goodsType": "角色",
                            "goodsSubType": "雷",
                            "rareType": "五星"
                        }
                    ],
                    "star4UpList": []]            //4星列表,这里省略里面的内容...
                }
            }
        ],
        "perm": [
            {
                "pondIndex": 0,                   //蛋池编号,常驻池只有一个蛋池
                "pondInfo": {
                    "star5UpList": [],
                    "star4UpList": []
                }
            }
        ]
    }
}
```

## 查询成员祈愿信息

- 获取成员保底剩余次数，累计获取5星数量，5星出货律等数据

| 请求类型 | 请求地址                                |  说明                        |
| ------ | --------------------------------------- | ---------------------------- |
| Get    | /api/PrayInfo/GetMemberPrayDetail       | 查询成员祈愿信息              | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中           | 是    |                       |
| memberCode    | 成员编号                      | 是    |                       |

#### 响应

```json5
{
    "code": 0,
    "message": "ok",
    "data": {
        "role180Surplus": 90,       //角色池剩余多少抽大保底
        "role90Surplus": 90,        //角色池剩余多少抽保底五星
        "role10Surplus": 10,        //角色池剩余多少抽十连保底
        "armAssignValue": 0,        //武器池命定值
        "arm80Surplus": 52,         //武器池剩余多少抽保底五星
        "arm10Surplus": 8,          //武器池剩余多少抽十连保底
        "perm90Surplus": 89,        //常驻池剩余多少抽保底五星
        "perm10Surplus": 9,         //常驻池剩余多少抽十连保底
        "rolePrayTimes": 212,       //角色池祈愿次数
        "armPrayTimes": 520,        //武器池祈愿次数
        "permPrayTimes": 181,       //常驻池祈愿次数
        "totalPrayTimes": 811,      //总祈愿次数
        "star4Count": 31,           //累计获得4星物品数量
        "star5Count": 4,            //累计获得5星物品数量
        "roleStar4Count": 10,       //角色池累计获得4星物品数量
        "armStar4Count": 11,        //武器池累计获得4星物品数量
        "permStar4Count": 10,       //常驻池累计获得4星物品数量
        "roleStar5Count": 2,        //角色池累计获得5星物品数量
        "armStar5Count": 1,         //武器池累计获得5星物品数量
        "permStar5Count": 1,        //常驻池累计获得5星物品数量
        "star4Rate": 3.82,          //4星物品出货率(%)
        "star5Rate": 0.49,          //5星物品出货率(%)
        "roleStar4Rate": 4.71,      //角色池4星出货率(%)
        "armStar4Rate": 2.11,       //武器池4星出货率(%)
        "permStar4Rate": 5.52,      //常驻池4星出货率(%)
        "roleStar5Rate": 0.94,      //角色池5星出货率(%)
        "armStar5Rate": 0.19,       //武器池5星出货率(%)
        "permStar5Rate": 0.55       //常驻池5星出货率(%)
    }
}
```

## 查询成员祈愿记录

- 获取成员前20条5星和4星物品记录,按照出货时间降序排序,以及所消耗抽数

| 请求类型 | 请求地址                                |  说明                        |
| ------ | --------------------------------------- | ---------------------------- |
| Get    | /api/PrayInfo/GetMemberPrayRecords      | 查询成员祈愿记录              | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中           | 是    |                       |
| memberCode    | 成员编号                      | 是    |                       |

#### 响应

```json5
{
    "code": 0,
    "message": "ok",
    "data": {
        "star5": {                                               //五星记录
            "arm": [                                             //武器池记录
                {
                    "goodsName": "和璞鸢",
                    "goodsType": "武器",
                    "goodsSubType": "长柄武器",
                    "rareType": "五星",
                    "cost": 69,
                    "createDate": "2022-02-23T15:19:10"
                }
            ],
            "role": [                                            //角色池记录
                {
                    "goodsName": "莫娜",
                    "goodsType": "角色",
                    "goodsSubType": "水",
                    "rareType": "五星",
                    "cost": 78,
                    "createDate": "2022-02-23T15:18:03"
                }
            ],
            "perm": [],                                          //常驻池记录
            "all": [                                             //武器+角色+常驻池记录,按照时间排序
                {
                    "goodsName": "和璞鸢",
                    "goodsType": "武器",
                    "goodsSubType": "长柄武器",
                    "rareType": "五星",
                    "cost": 69,
                    "createDate": "2022-02-23T15:19:10"
                },
                {
                    "goodsName": "莫娜",
                    "goodsType": "角色",
                    "goodsSubType": "水",
                    "rareType": "五星",
                    "cost": 78,
                    "createDate": "2022-02-23T15:18:03"
                }
            ]
        },
        "star4": {                                               //同5星记录,这里不显示相关内容
            "arm": [],
            "role": [],
            "perm": [],
            "all": []
        }
    }
}
```

## 获取欧气排行

- 根据授权码，获取成员近15天内5星级4星物品的出货率，按照出货率从高到低排序后返回前20名成员
- 该接口数据每5分钟缓存一次

| 请求类型 | 请求地址                                |  说明                        |
| ------ | --------------------------------------- | ---------------------------- |
| Get    | /api/PrayInfo/GetLuckRanking            | 获取欧气排行                  | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中           | 是    |                       |

#### 响应

```json5
{
    "code": 0,
    "message": "ok",
    "data": {
        "star5Ranking": [                       //5星出货率排行
            {
                "memberCode": "123",            //成员编号
                "memberName": "花园仓鼠",        //成员名称
                "count": 4,                     //累计获得5星物品数量
                "totalPrayTimes": 240,          //总祈愿次数
                "rate": 1.67                    //5星物品出货率(%)
            },
            {
                "memberCode": "456",
                "memberName": "花园仓鼠",
                "count": 4,
                "totalPrayTimes": 270,
                "rate": 1.48
            }
        ],
        "star4Ranking": [                       //4星出货率排行
            {
                "memberCode": "123",            //成员编号
                "memberName": "花园仓鼠",        //成员名称
                "count": 31,                    //累计获得4星物品数量
                "totalPrayTimes": 240,          //总祈愿次数
                "rate": 12.92                   //4星物品出货率(%)
            },
            {
                "memberCode": "456",
                "memberName": "花园仓鼠",
                "count": 32,
                "totalPrayTimes": 270,
                "rate": 11.85
            }
        ],
        "startDate": "2021-09-04T00:14:57.9916966+08:00",       //统计开始时间
        "endDate": "2021-09-19T00:14:57.9916981+08:00",         //统计结束时间
        "cacheDate": "2021-09-19T00:15:09.0795016+08:00",       //数据缓存时间
        "top": 20                                               //获取前N条排行数据
    }
}
```

## 自定义角色池
- 根据授权码配置角色池
- 需要传入一个5星和三个4星角色全称

| 请求类型 | 请求地址                        |  说明                         |
| ------ | -------------------------------- | ---------------------------- |
| Post   | /api/PrayInfo/SetRolePond        | 配置角色池                    | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中          | 是    |                       |

#### 请求体
```json5
{
    "pondIndex":1,                                   //蛋池编号
    "upItems":["雷电将军","五郎","云堇","香菱"]        //蛋池内容
}
```

#### 响应
```json5
{
    "code": 0,
    "message": "ok",
    "data": null
}
```

## 自定义武器池
- 根据授权码配置武器池
- 需要传入两个5星和五个4星武器全称

| 请求类型 | 请求地址                        |  说明                         |
| ------ | -------------------------------- | ---------------------------- |
| Post   | /api/PrayInfo/SetArmPond         | 配置角色池                    | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中          | 是    |                       |

#### 请求体
```json5
{
    "upItems":["薙草之稻光","不灭月华","恶王丸","曚云之月","匣里龙吟","西风长枪","祭礼残章"]
}
```

#### 响应
```json5
{
    "code": 0,
    "message": "ok",
    "data": null
}
```

## 清空角色池
- 清空授权码配置的角色池

| 请求类型 | 请求地址                        |  说明                         |
| ------ | -------------------------------- | ---------------------------- |
| Post   | /api/PrayInfo/ResetRolePond      | 清空角色池                    | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中          | 是    |                       |

#### 响应
```json5
{
    "code": 0,
    "message": "ok",
    "data": null
}
```

## 清空武器池
- 清空授权码配置的武器池

| 请求类型 | 请求地址                        |  说明                         |
| ------ | -------------------------------- | ---------------------------- |
| Post   | /api/PrayInfo/ResetArmPond       | 清空角色池                    | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中          | 是    |                       |

#### 响应
```json5
{
    "code": 0,
    "message": "ok",
    "data": null
}
```

## 设定服装概率
- 设定服装素材出现的概率

| 请求类型 | 请求地址                        |  说明                         |
| ------ | -------------------------------- | ---------------------------- |
| Post   | /api/PrayInfo/SetSkinRate        | 设定服装概率                   | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中          | 是    |                       |
| rare          | 概率0~100                    | 是    |                       |

#### 响应
```json5
{
    "code": 0,
    "message": "ok",
    "data": null
}
```
