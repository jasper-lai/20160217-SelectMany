
provide sample code for  傑士伯的IT學習之路 C# LINQ: SelectMany() 擴充方法

http://jasper-it.blogspot.com/2016/02/c-linq-selectmany.html


相關程式檔案建立過程, 最後的版本, 可自行下載本範例.

1. 自行建立 MyEnumerable 類別, 將 .NET Framework 源碼中 SelectMany() 相關擴充方法收錄至該類別, 以供除錯時, 得以明確看到程式執行過程.
總共有 3 個 overloading 的擴充方法, 
(1) 修改方法名稱, 在每個方法加上 My, 以與 .NET Framework 的源碼的原有方法作區隔
(2) 將有 Error.xxxx 的程式段註解掉, 
註: .NET Framework 源碼 連結: http://referencesource.microsoft.com/#System.Core/System/Linq/Enumerable.cs,8f3471331178bcb0 ) 

2. 建立 PetOwner 類別

3. 在 Program.cs, 加入 3 個 static methods, 並修改 Main() 方法, 以呼叫那 3 個 methods
(1) SelectManyEx1()
(2) SelectManyEx2()
(3) SelectManyEx3()
