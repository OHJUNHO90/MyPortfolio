

	var delRows = from tempitem in previousRows
				  where rows.Any(p => p.PROC_CD == tempitem.PROC_CD) == false
				  select tempitem;

	foreach (var item in delRows)
	{
		//비지니스 로직 수행
	}


	var modRows = from tempitem in rows
				  join item in previousRows on tempitem.PROC_CD equals item.PROC_CD
				  select tempitem;

	foreach (var item in modRows)
	{
		//비지니스 로직 수행
	}