echo 开始安装客户管理平台的后台服务. . . 
echo 清理原有服务项. . . 
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil /U "%~dp0\MFPCheckService.exe"
echo. 
echo 清理完毕，开始安装后台服务. . . 
echo. 
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil "%~dp0\MFPCheckService.exe"
echo 服务安装完毕，启动服务. . . 
net start YDDataSvc
echo. 


