# API 文档

## 数据库
- 数据库为mysql，使用code first自动建库建表，初次运行后请手动导入初始数据[initData.sql](https://github.com/GardenHamster/GenshinPray/blob/main/GenshinPray/Sql/initData.sql)
- 表和字段说明请参考数据表中的[注释](https://github.com/GardenHamster/GenshinPray/tree/main/GenshinPray/Models/PO)
- 注：项目启动时会将默认蛋池信息加载到内存，目前后台界面正在开发中，修改默认蛋池后请手动重启服务

## 枚举
- 数据表中如字段名为 *Type 等字段值请参考[GenshinPray/Type](https://github.com/GardenHamster/GenshinPray/tree/main/GenshinPray/Type)

## 状态码
- 所有接口返回的数据都包含一个 `code` 字段的状态码, 分别代表不同的响应状态. 详细请参考[ResultCode.cs](https://github.com/GardenHamster/GenshinPray/blob/main/GenshinPray/Common/ResultCode.cs)

## 授权码
- 大部分接口都需要在调用时在Http Header中传入授权码`authorzation`
- 一般一个群号对应着一个授权码，授权码可在`authorize`数据表中添加
- 可以为每个授权码配置自己的自定义蛋池，以及每日api调用次数

## 祈愿接口
| 请求类型 | 请求地址                     |  说明                        |
| ------ | ---------------------------- | ---------------------------- |
| Get    | /api/RolePray/PrayOne        | 角色池单抽                    |    
| Get    | /api/RolePray/PrayTen        | 角色池十连                    |
| Get    | /api/ArmPray/PrayOne         | 武器池单抽                    |
| Get    | /api/ArmPray/PrayTen         | 武器池十连                    |
| Get    | /api/PermPray/PrayOne        | 常驻单抽                      |
| Get    | /api/PermPray/PrayTen        | 常驻十连                      |

#### 参数

| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中          | 是    |                       |  
| memberCode    | 成员编号                      | 是    |                       |    
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
        "surplus10": 3,                   //当前蛋池剩余多少抽十连保底
        "star5Cost": 0,                   //本次获取五星物品累计消耗多少抽，如果本次未抽出五星时，值未0
        "apiDailyCallSurplus": 299,       //本日Api剩余可调用次数
        "imgHttpUrl": null,               //如果配置云盘时,返回图片在云盘中的http地址
        "imgSize": 97162,                 //图片大小(byte)
        "imgPath": "20210913/202109130127013500.jpg",   //相对于图片生成目录的地址
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
| Post   | /api/PrayInfo/SetMemberAssign    | 武器定轨                      | 

#### 参数
| 键            | 说明                          | 必须  | 默认                  |
| ------------- | ---------------------------- | ----- | --------------------- |
| authorzation  | 授权码，放在Header中          | 是    |                       |
| memberCode    | 成员编号                      | 是    |                       |    
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
        "arm": {                                //武器池
            "star5UpList": [                    //五星UP列表
                {
                    "goodsName": "飞雷之弦振",   //物品名称
                    "goodsType": "武器",        //物品类型，武器/角色
                    "goodsSubType": "弓",       //物品子类型
                    "rareType": "五星"          //稀有类型
                }
            ],
            "star4UpList": []                   //四星UP列表
        },
        "role": {                               //角色池
            "star5UpList": [],
            "star4UpList": []
        },
        "perm": {                               //常驻池
            "star5UpList": [],
            "star4UpList": []
        }
    }
}
```

## 查询成员祈愿信息


