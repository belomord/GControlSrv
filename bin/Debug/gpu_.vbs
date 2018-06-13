'************************************************************** 
' ���������� � ���������� 
' Win32_VideoController.vbs 
'************************************************************** 
Option explicit 

dim objService, colNetworkAdapters, objItem 
dim VideoInfo, objClassProperty 
dim FSO, TempPath, TxtFile 

Set objService = GetObject("WinMgmts:\\.\Root\CIMV2") 
Set colNetworkAdapters = objService.ExecQuery("SELECT * FROM Win32_VideoController") 

VideoInfo = "���������� � ����������" & vbCrLf & vbCrLf 

' �������� ������� ��������� 
For Each objItem in colNetworkAdapters 
  ' �������� ������� ���� ������� ��� �������� ���������� ������ WMI 
  For Each objClassProperty In objItem.Properties_ 
    ' ���� �������� ������ 0 
    If Len(objClassProperty.value)>0 Then 
      VideoInfo = VideoInfo & objClassProperty.Name &"= " & objClassProperty.value &vbCrLf 
    End If 
  Next 
  VideoInfo = VideoInfo & "----------------------------------------------" &vbCrLf &vbCrLf 
Next 

ShowInNotepad(VideoInfo) 

'��������� �������� ���������� ����� � ������� 
Sub ShowInNotepad(StrToFile) 
  Set FSO = CreateObject("Scripting.FileSystemObject") 
  'TempPath = CreateObject("WScript.Shell").ExpandEnvironmentStrings("%TEMP%") & "\" & FSO.GetTempName 
  'Set TxtFile = FSO.CreateTextFile(TempPath) 
  Set TxtFile = FSO.CreateTextFile("gpu_.txt") 
  TxtFile.WriteLine(StrToFile) 
  TxtFile.Close 
  CreateObject("WScript.Shell").Run "wordpad.exe " & TempPath 
End Sub

