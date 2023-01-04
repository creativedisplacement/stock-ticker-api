param([Parameter(Mandatory=$true)][string]$migrationName)
dotnet ef --project ../ --startup-project ../../StockTicker.Api migrations add $migrationName