delete appdbo.StageHistory
delete appdbo.Provider
delete appdbo.Stage

insert appdbo.Provider ([Provider], [Show]) VALUES
('Provider_1', 1)
,('Provider_2', 1)
,('Provider_3', 1)
,('Provider_4', 1)
,('Provider_5', 1)
,('Provider_6', 1)

insert appdbo.Stage ([Stage], [OrderNumber]) VALUES
('Stage_1', 0)
,('Stage_2', 1)
,('Stage_3', 2)
,('Stage_4', 3)
,('Stage_5', 4)
,('Stage_6', 5)
,('Stage_7', 6)
,('Stage_8', 7)
,('Stage_9', 8)
,('Stage_10', 9)

exec app.[Set-StageHistory] @Provider = 'Provider_1', @Stage = 'Stage_1', @StartDate = null, @CompletedDate = null;
exec app.[Set-StageHistory] @Provider = 'Provider_1', @Stage = 'Stage_2', @StartDate = '2021-04-01', @CompletedDate = null;

