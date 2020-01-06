sleep 10s
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P [DB_PASSWORD] -i ./setup-db.sql