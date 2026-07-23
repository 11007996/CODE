select id,status from sys.traces
-- exec sp_trace_setstatus id,num
exec sp_trace_setstatus 3,0    --停止跟踪
exec sp_trace_setstatus 3,1    --启动跟踪
exec sp_trace_setstatus 3,2    --删除跟踪