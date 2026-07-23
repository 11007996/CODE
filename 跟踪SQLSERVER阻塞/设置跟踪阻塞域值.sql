EXEC sp_configure 'show advanced options',1
RECONFIGURE WITH OVERRIDE;
GO
exec sp_configure  'blocked process threshold (s)',2  --设置阈值为2秒
RECONFIGURE WITH OVERRIDE;
GO

EXEC sp_configure 'show advanced options',0
RECONFIGURE WITH OVERRIDE;