'************************************************************** 
' Информация о видеокарте 
' Win32_VideoController.vbs 
'************************************************************** 
Option explicit 

dim objService, colNetworkAdapters, objItem 
dim VideoInfo, objClassProperty 
dim FSO, TempPath, TxtFile 

Set objService = GetObject("WinMgmts:\\.\Root\CIMV2") 
Set colNetworkAdapters = objService.ExecQuery("SELECT * FROM Win32_VideoController") 

VideoInfo = "Информация о видеокарте" & vbCrLf & vbCrLf 

' Начинаем перебор коллекции 
For Each objItem in colNetworkAdapters 
  ' Начинаем перебор всех свойств для текущего экземпляра класса WMI 
  For Each objClassProperty In objItem.Properties_ 
    ' Если значение больше 0 
    If Len(objClassProperty.value)>0 Then 
      VideoInfo = VideoInfo & objClassProperty.Name &"= " & objClassProperty.value &vbCrLf 
    End If 
  Next 
  VideoInfo = VideoInfo & "----------------------------------------------" &vbCrLf &vbCrLf 
Next 

ShowInNotepad(VideoInfo) 

'Процедура создания временного файла с данными 
Sub ShowInNotepad(StrToFile) 
  Set FSO = CreateObject("Scripting.FileSystemObject") 
  'TempPath = CreateObject("WScript.Shell").ExpandEnvironmentStrings("%TEMP%") & "\" & FSO.GetTempName 
  'Set TxtFile = FSO.CreateTextFile(TempPath) 
  Set TxtFile = FSO.CreateTextFile("gpu_.txt") 
  TxtFile.WriteLine(StrToFile) 
  TxtFile.Close 
  CreateObject("WScript.Shell").Run "wordpad.exe " & TempPath 
End Sub

