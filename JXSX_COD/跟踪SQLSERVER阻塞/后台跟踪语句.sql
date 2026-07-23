-- Create a Queue
declare @rc int
declare @traceid int    --跟踪分配的编号
declare @options int    --TRACE_FILE_ROLLOVER=2/SHUTDOWN_ON_ERROR=4/TRACE_PRODUCE_BLACKBOX=8
declare @tracefile nvarchar(500)--跟踪文件的存储路径
declare @maxfilesize bigint    --跟踪文件的大小，单位是mb，默认5mb
declare @endtime datetime    --停止跟踪的日期和时间,为NULL则表示一直跟踪
declare @filecount int    --跟踪文件的数量,其值大于1,TRACE_FILE_ROLLOVER=2 时有效
set @options = 2
set @tracefile = N'D:\work\data\trace'
set @maxfilesize = 20
set @endtime = DATEADD(D,1,GETDATE())
set @filecount = 5

-- exec @rc = sp_trace_create @TraceID output, 0, N'InsertFileNameHere', @maxfilesize, NULL 
exec @rc = sp_trace_Create @TraceID output,@options,@tracefile,@maxfilesize,@endtime,@filecount
if (@rc != 0) goto error

-- Client side File and Table cannot be scripted

-- Set the events
declare @on bit
set @on = 1
exec sp_trace_setevent @TraceID, 137, 3, @on
exec sp_trace_setevent @TraceID, 137, 15, @on
exec sp_trace_setevent @TraceID, 137, 51, @on
exec sp_trace_setevent @TraceID, 137, 4, @on
exec sp_trace_setevent @TraceID, 137, 24, @on
exec sp_trace_setevent @TraceID, 137, 32, @on
exec sp_trace_setevent @TraceID, 137, 60, @on
exec sp_trace_setevent @TraceID, 137, 64, @on
exec sp_trace_setevent @TraceID, 137, 1, @on
exec sp_trace_setevent @TraceID, 137, 13, @on
exec sp_trace_setevent @TraceID, 137, 41, @on
exec sp_trace_setevent @TraceID, 137, 22, @on
exec sp_trace_setevent @TraceID, 137, 26, @on


-- Set the Filters
declare @intfilter int
declare @bigintfilter bigint

-- Set the trace status to start
exec sp_trace_setstatus @TraceID, 1

-- display trace id for future references
select TraceID=@TraceID
goto finish

error: 
select ErrorCode=@rc

finish: 
go