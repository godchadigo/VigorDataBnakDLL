# VigorDataBankDLL 是可以支援Vigor PLC ， 由此擴充庫直接生成資料銀行，省去了開發者在數據編輯以及生成上的麻煩
# !!!目前只支援寫入32bit數據!!!
# 如有需要其他格式請在聯繫我 email: asd281194533@gmail.com

## 使用方法

### 第一步 寫程式
``` C#
public void CreateDataBank(){
    List<byte> TotalData = new List<byte>();
    TotalData.add(1);
    TotalData.add(2);
    TotalData.add(3);
    
    //指定創建路徑
    string path = "C:\\Users\\PC\\Downloads\\BigData1.DB";
    //初始化擴充件
    var vigor = new VigorDataBnakDLL.VigorDataBankDLL();
    //創建資料銀行DB (參數1 檔案路徑，參數二 資料)
    vigor.WriteHex(path , TotalData );
}
```

### 第二步 打開LadderMasterS.exe(Vigor編輯器)
- 在工具列找到工具這個選單
- 點擊資料銀行
- 點擊檔案
- 點擊開啟舊檔
- enjoy!

成品圖
![image](picture or gif url)
