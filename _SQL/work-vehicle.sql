BEGIN TRANSACTION
DECLARE @workId UNIQUEIDENTIFIER = '4dee1134-c745-43c0-9fc8-8cd2852614ca'
SET @workId = '6596d69b-704c-4f3e-9f67-23cd7a2ecf83'

UPDATE WorkTask SET vehicleNumberplate = NULL WHERE workId = @workId AND PATINDEX(' %', vehicleNumberplate) = 1
UPDATE WorkTask SET addressText = NULL WHERE workId = @workId AND addressText = ''
UPDATE WorkTask SET vehicleText = ISNULL(vehicleNumberplate, '') + ISNULL(linkNumberplate, '') + ISNULL(dollyNumberplate, '') + ISNULL(trailerNumberplate, '') WHERE workId = @workId
UPDATE WorkTask SET vehicleText = NULL WHERE workId = @workId AND vehicleText = ''
EXEC dbo.stp_WorkTask_UpdateFields @workId

SELECT typeId, addressText, vehicleText, freighterSetup, addressSetup, * FROM WorkTask WHERE workId = @workId ORDER BY sortOrder

ROLLBACK TRANSACTION
--SELECT * FROM WorkTaskType
--select * from WorkAddress
--EXEC dbo.stp_WorkTask_UpdateFields '6596d69b-704c-4f3e-9f67-23cd7a2ecf83'
--UPDATE WorkTask set addressText = 1006 where taskId = '25CB3CEB-1363-4C97-9C4C-1B3263521545'