# 校验、解析身份证号码

## 通过身份证号码解析的信息

* 校验：

```csharp

 var verify = IDCardHelper.TryVerify("610402196903194412", out var error);

```

`verify`等于`true`时校验通过，`error`为`null`;
`verify`等于`false`时，`error`为错误原因；

* 解析：

```csharp

 var idCard = IDCardHelper.Parse("14010319660730424x");

```

结果示例：

```json
{
    "Province":"山西省",
    "Prefecture":"山西省太原市",
    "County":  "山西省太原市北城区",
    "Birthday": "1966/7/30 00:00:00",
    "Gender":  1
}
```

## 行政区划代码来源

民政部网站公开数据：http://www.mca.gov.cn/article/sj/xzqh/