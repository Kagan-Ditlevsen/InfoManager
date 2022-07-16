set datefirst 1;
declare @sal money = 152

select
	(datepart(week, startDateTime) - 1) as [week],
	format(startDateTime, 'ddd') as [weekday],
	format(startDateTime, 'yyyy-MM-dd HH:mm') as startDateTime,
	format(endDateTime, 'yyyy-MM-dd HH:mm') as endDateTime,
	left(cast(cast(endDateTime-startDateTime as time) as varchar), 5) as delta,
	left(cast(cast(cast(datefromparts(year(startDateTime), month(startDateTime), day(startDateTime)) as varchar) + ' 18:00' as datetime) - startDateTime as time), 5) as [0618],
	left(cast('5:00' as time), 5) as [1823],
	left(cast(endDateTime - cast(cast(datefromparts(year(startDateTime), month(startDateTime), day(startDateTime)) as varchar) + ' 23:00' as datetime) as time), 5) as [2306]
	,0 as [0618_min], 0 as [1823_min], 0 as [2306_min], 0 as salary
into #tmp
from work

update #tmp set
	[0618_min] = isnull((((datepart(hour, [0618]) * 60) + datepart(minute, [0618])) * (@sal / 60)), 0),
	[1823_min] = isnull((((datepart(hour, [1823]) * 60) + datepart(minute, [1823])) * (@sal / 60)) + 40.40, 0),
	[2306_min] = isnull((((datepart(hour, [2306]) * 60) + datepart(minute, [2306])) * (@sal / 60)) + 45.45, 0)
update #tmp set salary = [0618_min] + [1823_min] + [2306_min]

select *, (select sum(salary) from #tmp x where x.week = #tmp.week) as salaryWeekly from #tmp order by startDateTime desc

drop table #tmp

