BACKUP DATABASE [AquaDB] TO  
DISK = N'source\repos\AquaMarket\AquaMarket\App_Data\AquaDBbackup.bak ' WITH NOFORMAT, NOINIT,  
NAME = N'AquaDB-Full Database Backup', 
SKIP, NOREWIND, NOUNLOAD,  STATS = 10