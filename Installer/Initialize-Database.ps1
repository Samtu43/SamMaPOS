$ErrorActionPreference = 'Stop'
$connection = $null
try {
    $service = Get-Service -Name 'MSSQL$SQLEXPRESS'
    if ($service.Status -ne 'Running') {
        Start-Service -InputObject $service
    }

    $connection = New-Object System.Data.SqlClient.SqlConnection(
        'Data Source=lpc:.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5')
    for ($attempt = 1; $attempt -le 6; $attempt++) {
        try {
            $connection.Open()
            break
        } catch [System.Data.SqlClient.SqlException] {
            if ($attempt -eq 6 -or $_.Exception.Number -eq 18456) { throw }
            Start-Sleep -Seconds 2
        }
    }

    $command = $connection.CreateCommand()
    $command.CommandTimeout = 60
    $command.CommandText = "DECLARE @result int; EXEC @result = sp_getapplock @Resource = N'SamMaPOS.Initialize', @LockMode = 'Exclusive', @LockOwner = 'Session', @LockTimeout = 60000; SELECT @result;"
    if ($command.ExecuteScalar() -lt 0) { throw 'Could not lock PosDB for initialization.' }

    $command.CommandText = "IF DB_ID(N'PosDB') IS NULL CREATE DATABASE PosDB;"
    [void]$command.ExecuteNonQuery()
    $connection.ChangeDatabase('PosDB')
    $command.CommandText = "SELECT COUNT(*) FROM sys.objects WHERE is_ms_shipped = 0 AND type IN ('U', 'V', 'P', 'FN', 'IF', 'TF')"
    if ($command.ExecuteScalar() -eq 0) {
        $script = Get-Content -Raw -LiteralPath (Join-Path $PSScriptRoot 'PosDB.sql')
        $transaction = $connection.BeginTransaction()
        try {
            $command.Transaction = $transaction
            foreach ($batch in [regex]::Split($script, '(?im)^[ \t]*GO[ \t]*\r?$')) {
                if ([string]::IsNullOrWhiteSpace($batch)) { continue }
                $command.CommandText = $batch
                [void]$command.ExecuteNonQuery()
            }
            $transaction.Commit()
        } finally {
            $command.Transaction = $null
            $transaction.Dispose()
        }
    }

    $command.CommandText = @'
SELECT COUNT(*) FROM sys.objects
WHERE schema_id = SCHEMA_ID(N'dbo')
AND name IN (
    N'Categories',
    N'Customers',
    N'InventoryLog',
    N'OrderDetails',
    N'Orders',
    N'Products',
    N'Roles',
    N'Users',
    N'usp_Inventory_Adjust',
    N'usp_Login',
    N'usp_Order_Create',
    N'usp_Product_Delete',
    N'usp_Product_Insert',
    N'usp_Product_Update',
    N'usp_Report_DailySales',
    N'usp_Report_Dashboard',
    N'usp_Report_LowStock',
    N'usp_Report_SalesByCategory',
    N'usp_Report_TopProducts',
    N'usp_User_TouchLogin'
)
'@
    if ($command.ExecuteScalar() -ne 20) {
        throw 'PosDB exists but is missing required objects. Have the database administrator repair it before retrying.'
    }
} catch {
    $_.Exception.Message | Set-Content -Encoding ASCII -LiteralPath (Join-Path $PSScriptRoot 'database-error.txt')
    exit 1
} finally {
    if ($null -ne $connection) { $connection.Dispose() }
}

