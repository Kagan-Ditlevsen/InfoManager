WITH cte
AS
(
	SELECT accountId, parentAccountId, title, FinanceAccountType.internalTitle AS type, 0 AS _lvl, CAST(title AS VARCHAR) AS _breadcrum, CAST(accountId AS VARCHAR) AS _sortOrder, CAST(title AS VARCHAR) AS _displayTitle
	FROM FinanceAccount
	LEFT JOIN FinanceAccountType ON FinanceAccount.typeId = FinanceAccountType.typeId
	WHERE parentAccountId IS NULL

	UNION ALL

	SELECT fa.accountId, fa.parentAccountId, fa.title, fat.internalTitle, (CAST(cte._lvl AS INT) + 1), CAST(cte.title + ' > ' + fa.title AS VARCHAR), CAST(CAST(cte.accountId AS VARCHAR) + '.' + CAST(fa.accountId AS VARCHAR) AS VARCHAR), CAST(REPLICATE(' ', cte._lvl * 5) + fa.title AS VARCHAR)
	FROM FinanceAccount fa
	INNER JOIN FinanceAccountType fat ON fa.typeId = fat.typeId
	INNER JOIN cte ON cte.accountId = fa.parentAccountId
)
SELECT * FROM cte ORDER BY _sortOrder