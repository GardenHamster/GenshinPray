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












