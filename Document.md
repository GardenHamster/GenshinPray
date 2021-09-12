# API 文档

## 数据库
- 数据库为mysql，使用code first自动建库建表，初次运行后请手动导入初始数据[initData.sql](https://github.com/GardenHamster/GenshinPray/blob/main/GenshinPray/Sql/initData.sql)
- 表和字段说明请参考数据表中的[注释](https://github.com/GardenHamster/GenshinPray/tree/main/GenshinPray/Models/PO)

## 枚举
- 数据表中如GoodsType等值请参考[GenshinPray/Type](https://github.com/GardenHamster/GenshinPray/tree/main/GenshinPray/Type)

## 状态码
- 所有接口返回的数据都包含一个 `code` 字段的状态码, 分别代表不同的响应状态. 详细请参考[ResultCode.cs](https://github.com/GardenHamster/GenshinPray/blob/main/GenshinPray/Common/ResultCode.cs)

## 授权码
- 大部分接口都需要在调用时在Http Header中传入授权码`authorzation`，一般一个群号对应着一个授权码，授权码可在`authorize`表中添加


## 祈愿接口

### 十连祈愿

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
| memberCode    | 成员编号                      | 是    |                       |    
| imgWidth      | 生成图片宽度,单位p             | 否    | 1920                  |
| toBase64      | 是否返回base64字符串          | 否     | false                |

#### 响应

```json5
{
    "code": 0,
    "message": "ok",
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







