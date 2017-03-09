/*
set @name='LeZhaiQuanStart'
set @connectionString='Server=192.168.1.102;Database=lezhaiquanstart;Uid=root;Pwd=root;'
set @providerName='MySql.Data.MySqlClient'
set @generateAllTable=false
*/
--UserA
select *,'nameA' as '(DateTime)' from user;
--UserB
select *,'arr' as '(List<NFinalServer.Entity.UserA>)' from user;