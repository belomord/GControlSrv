Const WBEM_RETURN_IMMEDIATELY = &h10
Const WBEM_FORWARD_ONLY = &h20

'Set objWMIService = GetObject("winmgmts://" & strComputer & "/root/cimv2")
Set objWMIService = GetObject("WinMgmts:\\.\Root\CIMV2") 


' Manufacturer is held in the Win32_ComputerSystem Namespace along with TotalPhysicalMemory:

Set colComputerSystem = objWMIService.ExecQuery("SELECT * FROM Win32_ComputerSystem", "WQL", WBEM_RETURN_IMMEDIATELY + WBEM_FORWARD_ONLY)
For Each objItem in colComputerSystem
  WScript.Echo objItem.Manufacturer
  WScript.Echo objItem.TotalPhysicalMemory
Next

Set colComputerSystem = Nothing

' MaxClockSpeed in the Win32_Processor Namespace:

Set colProcessor = objWMIService.ExecQuery("SELECT * FROM Win32_Processor", "WQL", WBEM_RETURN_IMMEDIATELY + WBEM_FORWARD_ONLY)
For Each objItem in colProcessor
  WScript.Echo objItem.MaxClockSpeed
Next
Set colProcessor = Nothing

' VideoMemory is in Win32_VideoController as AdapterRAM

Set colVideoController = objWMIService.ExecQuery("SELECT * FROM Win32_VideoController", "WQL", WBEM_RETURN_IMMEDIATELY + WBEM_FORWARD_ONLY)

For Each objItem in colVideoController
  WScript.Echo objItem.AdapterRAM
Next


