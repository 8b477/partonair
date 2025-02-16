EXECUTE sp_executesql N'$(FULL_PATH)Scripts\schema.sql';
GO
EXECUTE sp_executesql N'$(FULL_PATH)Scripts\seed-data.sql';
GO