# SnowFlake-To-GeckoBoard-Sync
Simple utility to sync datda from Snowflake Db to Geckoboard datasets

Uses - https://github.com/snowflakedb/snowflake-connector-net and https://github.com/geckoboard/geckoboard-c-sharp

Use SnowFlakeSyncService.Net/pie.json TO: 
  Update connection strings 
  And configure dataset settings
  
1. The tool allows multliple sync processes to executes at given intervals
2. Hangfire dashboards can be used to monitor the scheduled tasks http://localhost:8000/hangfire


CODE is written to work. Can be further beutified and optimized.

