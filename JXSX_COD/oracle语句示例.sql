declare
  v_java_score number(5):=&n;
  v_music_score number(5):=&b;
begin
  if v_java_score>90 and v_music_score>80 then
    dbms_output.put_line('良好');
  else
    dbms_output.put_line('闭门思过');
  end if;
end;


begin
	for i in 1..10 loop
 		dbms_output.put_line(i);
	end loop;
end;


declare
  v_garde varchar2(20) := '&n';
begin
  case 
  when upper(v_garde)='A' then
    dbms_output.put_line('优秀');
  when upper(v_garde)='B' then
    dbms_output.put_line('良好');
  when upper(v_garde)='C' then
    dbms_output.put_line('中等');
  when upper(v_garde)='D' then
    dbms_output.put_line('及格');
  else
    dbms_output.put_line('稀烂');
  end case;
end;

declare
    v_index number(3):=0;
    v_sum number(5):=0;
begin
    while v_index<=100 loop
        v_sum := v_sum + v_index;
        v_index := v_index + 1;
    end loop;
        dbms_output.put_line('1到100累加之和：'||v_sum);
end;

declare
    v_index number(3):=6;
    v_sum number(5):=6;
begin
    loop
        v_sum := v_sum + v_index;
        v_index := v_index + 1;
    exit when v_index<=100;
    end loop;
        dbms_output.put_line('至少执行一次，结果为：'||v_sum);
end;
