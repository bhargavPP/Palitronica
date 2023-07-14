Add-Migration -Name "InitialDbContextMigration" -OutputDir "Migrations" -Context "PalitronicaDemo.Model.MyDbContext" -Project "PalitronicaDemo"
Update Migration
Update-Database -Context "PalitronicaDemo.Model.MyDbContext" -Project "PalitronicaDemo"
