echo ��ʼ��װ�ͻ�����ƽ̨�ĺ�̨����. . . 
echo ����ԭ�з�����. . . 
%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\installutil /U "%~dp0\FollowMEService.exe"
echo. 
echo ������ϣ���ʼ��װ��̨����. . . 
echo. 
%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\installutil "%~dp0\FollowMEService.exe"
echo ����װ��ϣ���������. . . 
net start YDDataSvc
echo. 


