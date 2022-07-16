begin transaction

update WorkTask set addressText = 1006 where addressText = 'Taastrup Hovedgade 10, Tåstrup'
update WorkTask set addressText = 1006 where addressText = 'Tåstrup'
update WorkTask set addressText = 1006 where addressText = 'Hjem'
update WorkTask set addressText = 1002 where addressText = 'Leman, Taulov'
update WorkTask set addressText = 1001 where addressText = 'Leman, Greve'
update WorkTask set addressText = 1001 where addressText = 'Greve'
update WorkTask set addressText = 1000 where addressText = 'Terminal'
update WorkTask set addressText = 'Godshotellet, Ventrupparken 3, 2670 Greve' where addressText like 'godshotellet%'
update WorkTask set vehicleNumberplate = null where patindex(' %', vehicleNumberplate) = 1
update WorkTask set dollyNumberplate = null where patindex(' %', dollyNumberplate) = 1
update WorkTask set linkNumberplate = null where patindex(' %', linkNumberplate) = 1
update WorkTask set trailerNumberplate = null where patindex(' %', trailerNumberplate) = 1
update WorkTask set vehicleText = isnull(vehicleNumberplate, '')+isnull(dollyNumberplate, '')+isnull(linkNumberplate, '')+isnull(trailerNumberplate, '')
update WorkTask set startDateTime= isnull(startDateTime, endDateTime)
update WorkTask set addressSetup = null where patindex(' %', addressSetup) = 1
update WorkTask set freighterSetup = null where patindex(' %', freighterSetup) = 1

--select 'exec dbo.stp_WorkTask_UpdateFields ''' + cast(workId as varchar(max)) + '''' from work
exec dbo.stp_WorkTask_UpdateFields '06151733-CD5E-4A9C-9DF5-21FC0DEADABE'
exec dbo.stp_WorkTask_UpdateFields '01FE8D3E-76B0-4E38-A559-960B4F8F1594'
exec dbo.stp_WorkTask_UpdateFields '6BD4B5E2-F97E-46FC-9D08-6FF0C5E93EB9'
exec dbo.stp_WorkTask_UpdateFields 'B6250AF3-7995-405D-9CAB-4E82BF40A7F1'
exec dbo.stp_WorkTask_UpdateFields '8FD3793D-16D6-4F63-B7E9-A08EB310B569'
exec dbo.stp_WorkTask_UpdateFields '9AEE23B4-E107-4EB0-99F7-C37A3E1E710C'
exec dbo.stp_WorkTask_UpdateFields '0613073B-6831-454C-AE53-90F8BFE2AB93'
exec dbo.stp_WorkTask_UpdateFields 'E4F98464-BDEE-4B1F-A9B2-3DAE9E30AABD'
exec dbo.stp_WorkTask_UpdateFields '022B5916-ACD2-4D65-8EA7-687B7C68CB43'
exec dbo.stp_WorkTask_UpdateFields '20E63641-5261-446D-9DAC-56E6A9493D47'
exec dbo.stp_WorkTask_UpdateFields '7FFBD0A3-D056-4F31-A423-52D3F0F4FE6B'
exec dbo.stp_WorkTask_UpdateFields '4EAF1788-FCE5-40B2-A966-1F95A3D3969C'
exec dbo.stp_WorkTask_UpdateFields 'E260660B-8C0B-4D72-AE18-4AF31E387507'
exec dbo.stp_WorkTask_UpdateFields '62C7B53A-5F0F-40FF-ABDE-717E1DB9980B'
exec dbo.stp_WorkTask_UpdateFields 'C6E604F3-E97F-4B41-86C2-FAC053E1C8B3'
exec dbo.stp_WorkTask_UpdateFields 'DE677FCF-B35F-425F-9B79-6790F72F23F9'
exec dbo.stp_WorkTask_UpdateFields '5C2D777D-6C1A-4A67-A98E-FE4F41F94701'
exec dbo.stp_WorkTask_UpdateFields 'ECC74131-BF0E-42CE-9E88-7661285733A2'
exec dbo.stp_WorkTask_UpdateFields 'BF4BADCB-987B-465B-98AB-630E02C88943'
exec dbo.stp_WorkTask_UpdateFields '8BE7615C-C478-431F-9FFA-5E1807CBDA0F'
exec dbo.stp_WorkTask_UpdateFields '5EC66D6C-83C1-4D63-916A-C2C7EBAD090E'
exec dbo.stp_WorkTask_UpdateFields 'E040F9A7-4E6F-4ADD-8107-79755E3F65EC'
exec dbo.stp_WorkTask_UpdateFields '48A57425-3443-4F35-B82A-4D501984747D'
exec dbo.stp_WorkTask_UpdateFields 'C6CEF0CA-77D9-45EF-BE9C-9C59C36466BD'
exec dbo.stp_WorkTask_UpdateFields 'C182645E-BF30-4A11-A0AA-1F8529E3FD38'
exec dbo.stp_WorkTask_UpdateFields '22F1A2CF-A2F5-4044-851B-64C5BB6A1C5B'
exec dbo.stp_WorkTask_UpdateFields 'ACB5FA97-95A6-43A5-B46C-AEA743188E37'
exec dbo.stp_WorkTask_UpdateFields '605AE21C-E4B4-499D-85EC-4932CF7399AA'
exec dbo.stp_WorkTask_UpdateFields 'C641BDCD-119E-4F9B-B504-A46513524506'
exec dbo.stp_WorkTask_UpdateFields '22AC70A4-D6C0-41A7-B0AF-53CFEDFF6F73'
exec dbo.stp_WorkTask_UpdateFields '92BDDA03-9BE2-4BDE-BE90-C5DB152F7E20'
exec dbo.stp_WorkTask_UpdateFields '234B1E81-E176-4059-945D-6CB58D134AD9'
exec dbo.stp_WorkTask_UpdateFields 'A29AEE70-ACBA-4243-A098-B194F33D0E7D'
exec dbo.stp_WorkTask_UpdateFields 'BAD3D648-CBC2-42BE-BFE3-386465BB8B06'
exec dbo.stp_WorkTask_UpdateFields '2791B99C-94E0-4FB6-AA32-986608D408D5'
exec dbo.stp_WorkTask_UpdateFields '4ADE4FED-4E78-4331-944A-71D83814DF8D'
exec dbo.stp_WorkTask_UpdateFields 'B5376464-66AB-4948-9FE1-59971482894A'
exec dbo.stp_WorkTask_UpdateFields '9FC84177-50DD-40C5-9618-CD98938FF645'
exec dbo.stp_WorkTask_UpdateFields '14404186-57EA-472E-9B25-8DE72C46ADFF'
exec dbo.stp_WorkTask_UpdateFields '4F88912F-0D04-428E-B048-4AACE60F3F89'
exec dbo.stp_WorkTask_UpdateFields '0CAA3A39-231C-4022-ABCC-8B8DCF1E888C'
exec dbo.stp_WorkTask_UpdateFields '5FD71011-565A-44F1-AB69-7424D563B905'
exec dbo.stp_WorkTask_UpdateFields '4DEE1134-C745-43C0-9FC8-8CD2852614CA'
exec dbo.stp_WorkTask_UpdateFields '6596D69B-704C-4F3E-9F67-23CD7A2ECF83'
exec dbo.stp_WorkTask_UpdateFields '0CB6FCD9-B9AF-4DE2-A653-DAAF6790932D'
exec dbo.stp_WorkTask_UpdateFields '4299D021-BCE7-4FB5-B9C0-8DDFC6160004'
'
select addressText, addressSetup, vehicleText, freighterSetup, * from WorkTask order by workId, sortOrder
--select addressText as a, addressSetup, vehicleText, freighterSetup, * from WorkTask order by addressText
rollback transaction