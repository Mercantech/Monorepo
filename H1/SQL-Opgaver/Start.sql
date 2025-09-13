-- =============================================
-- Database Information Script
-- Henter alle databaser, tabeller, kolonner og relaterede objekter
-- =============================================

-- 1. HENT ALLE DATABASER (SQL Server specifik)
-- =============================================
-- @block ALLE DATABASER
-- Bemærk: Dette virker kun i SQL Server. For andre databaser, brug:
-- MySQL: SHOW DATABASES;
-- PostgreSQL: SELECT datname FROM pg_database;
-- SQLite: .databases (i SQLite CLI)

-- SQL Server version:
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'tempdb')
BEGIN
    SELECT 
        name AS DatabaseName,
        database_id,
        create_date,
        collation_name,
        compatibility_level,
        state_desc AS Status
    FROM sys.databases
    WHERE state = 0  -- Kun aktive databaser
    ORDER BY name;
END
ELSE
BEGIN
    PRINT 'sys.databases er ikke tilgængelig i denne database type.';
    PRINT 'Brug database-specifikke kommandoer i stedet.';
END

-- 2. HENT ALLE TABELLER I DEN NUVÆRENDE DATABASE
-- =============================================
-- @block ALLE TABELLER
PRINT '=== ALLE TABELLER I NUVÆRENDE DATABASE ==='

-- Standard INFORMATION_SCHEMA version (virker i de fleste databaser):
SELECT 
    t.TABLE_SCHEMA AS SchemaName,
    t.TABLE_NAME AS TableName,
    t.TABLE_TYPE AS TableType
FROM INFORMATION_SCHEMA.TABLES t
WHERE t.TABLE_TYPE = 'BASE TABLE'
ORDER BY t.TABLE_SCHEMA, t.TABLE_NAME;

-- SQL Server specifik version med rækkeantal og størrelse:
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'sysdatabases')
BEGIN
    SELECT 
        t.TABLE_SCHEMA AS SchemaName,
        t.TABLE_NAME AS TableName,
        t.TABLE_TYPE AS TableType,
        p.rows AS RowCount,
        CAST(ROUND(((SUM(a.total_pages) * 8) / 1024.00), 2) AS NUMERIC(36, 2)) AS TotalSpaceMB
    FROM INFORMATION_SCHEMA.TABLES t
    LEFT JOIN sys.tables st ON t.TABLE_NAME = st.name
    LEFT JOIN sys.partitions p ON st.object_id = p.object_id
    LEFT JOIN sys.allocation_units a ON p.partition_id = a.container_id
    WHERE t.TABLE_TYPE = 'BASE TABLE'
    GROUP BY t.TABLE_SCHEMA, t.TABLE_NAME, t.TABLE_TYPE, p.rows
    ORDER BY t.TABLE_SCHEMA, t.TABLE_NAME;
END

-- 3. HENT ALLE KOLONNER MED DETALJER
-- =============================================
PRINT '=== ALLE KOLONNER MED DETALJER ==='
SELECT 
    c.TABLE_SCHEMA AS SchemaName,
    c.TABLE_NAME AS TableName,
    c.COLUMN_NAME AS ColumnName,
    c.ORDINAL_POSITION AS Position,
    c.DATA_TYPE AS DataType,
    c.CHARACTER_MAXIMUM_LENGTH AS MaxLength,
    c.NUMERIC_PRECISION AS Precision,
    c.NUMERIC_SCALE AS Scale,
    c.IS_NULLABLE AS IsNullable,
    c.COLUMN_DEFAULT AS DefaultValue,
    CASE 
        WHEN pk.COLUMN_NAME IS NOT NULL THEN 'YES'
        ELSE 'NO'
    END AS IsPrimaryKey
FROM INFORMATION_SCHEMA.COLUMNS c
LEFT JOIN (
    SELECT ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
        ON tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
    WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
) pk ON c.TABLE_SCHEMA = pk.TABLE_SCHEMA 
    AND c.TABLE_NAME = pk.TABLE_NAME 
    AND c.COLUMN_NAME = pk.COLUMN_NAME
ORDER BY c.TABLE_SCHEMA, c.TABLE_NAME, c.ORDINAL_POSITION;

-- 4. HENT ALLE FOREIGN KEYS
-- =============================================
-- @block FOREIGN KEYS
PRINT '=== ALLE FOREIGN KEYS ==='

-- Standard INFORMATION_SCHEMA version (virker i de fleste databaser):
SELECT 
    tc.CONSTRAINT_NAME AS ForeignKeyName,
    tc.TABLE_NAME AS TableName,
    kcu.COLUMN_NAME AS ColumnName,
    ccu.TABLE_NAME AS ReferencedTableName,
    ccu.COLUMN_NAME AS ReferencedColumnName
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu 
    ON tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu 
    ON ccu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME
WHERE tc.CONSTRAINT_TYPE = 'FOREIGN KEY'
ORDER BY tc.TABLE_NAME, tc.CONSTRAINT_NAME;

-- SQL Server specifik version med delete/update actions:
IF EXISTS (SELECT * FROM sys.foreign_keys)
BEGIN
    SELECT 
        fk.name AS ForeignKeyName,
        OBJECT_NAME(fk.parent_object_id) AS TableName,
        COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS ColumnName,
        OBJECT_NAME(fk.referenced_object_id) AS ReferencedTableName,
        COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS ReferencedColumnName,
        fk.delete_referential_action_desc AS DeleteAction,
        fk.update_referential_action_desc AS UpdateAction
    FROM sys.foreign_keys fk
    INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
    ORDER BY TableName, ForeignKeyName;
END

-- 5. HENT ALLE INDEKSER
-- =============================================
PRINT '=== ALLE INDEKSER ==='
SELECT 
    t.name AS TableName,
    i.name AS IndexName,
    i.type_desc AS IndexType,
    i.is_unique AS IsUnique,
    i.is_primary_key AS IsPrimaryKey,
    STUFF((
        SELECT ', ' + c.name + CASE WHEN ic.is_descending_key = 1 THEN ' DESC' ELSE ' ASC' END
        FROM sys.index_columns ic
        INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
        WHERE ic.object_id = i.object_id AND ic.index_id = i.index_id
        ORDER BY ic.key_ordinal
        FOR XML PATH('')
    ), 1, 2, '') AS IndexColumns
FROM sys.indexes i
INNER JOIN sys.tables t ON i.object_id = t.object_id
WHERE i.index_id > 0  -- Ekskluder heap
ORDER BY t.name, i.name;

-- 6. HENT ALLE STORED PROCEDURES
-- =============================================
PRINT '=== ALLE STORED PROCEDURES ==='
SELECT 
    ROUTINE_SCHEMA AS SchemaName,
    ROUTINE_NAME AS ProcedureName,
    ROUTINE_TYPE AS RoutineType,
    CREATED AS CreatedDate,
    LAST_ALTERED AS LastModified
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_TYPE = 'PROCEDURE'
ORDER BY ROUTINE_SCHEMA, ROUTINE_NAME;

-- 7. HENT ALLE FUNKTIONER
-- =============================================
PRINT '=== ALLE FUNKTIONER ==='
SELECT 
    ROUTINE_SCHEMA AS SchemaName,
    ROUTINE_NAME AS FunctionName,
    ROUTINE_TYPE AS RoutineType,
    DATA_TYPE AS ReturnType,
    CREATED AS CreatedDate,
    LAST_ALTERED AS LastModified
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_TYPE = 'FUNCTION'
ORDER BY ROUTINE_SCHEMA, ROUTINE_NAME;

-- 8. HENT ALLE VIEWS
-- =============================================
PRINT '=== ALLE VIEWS ==='
SELECT 
    TABLE_SCHEMA AS SchemaName,
    TABLE_NAME AS ViewName,
    VIEW_DEFINITION AS Definition
FROM INFORMATION_SCHEMA.VIEWS
ORDER BY TABLE_SCHEMA, TABLE_NAME;

-- 9. HENT ALLE TRIGGERS
-- =============================================
PRINT '=== ALLE TRIGGERS ==='
SELECT 
    t.name AS TriggerName,
    OBJECT_NAME(t.parent_id) AS TableName,
    t.is_disabled AS IsDisabled,
    t.is_not_for_replication AS IsNotForReplication,
    t.create_date AS CreatedDate,
    t.modify_date AS LastModified
FROM sys.triggers t
WHERE t.parent_class = 1  -- Kun table triggers
ORDER BY TableName, TriggerName;

-- 10. HENT DATABASE STATISTIKKER
-- =============================================
PRINT '=== DATABASE STATISTIKKER ==='
SELECT 
    DB_NAME() AS CurrentDatabase,
    (SELECT COUNT(*) FROM sys.tables) AS TableCount,
    (SELECT COUNT(*) FROM sys.views) AS ViewCount,
    (SELECT COUNT(*) FROM sys.procedures) AS ProcedureCount,
    (SELECT COUNT(*) FROM sys.objects WHERE type = 'FN') AS FunctionCount,
    (SELECT COUNT(*) FROM sys.triggers) AS TriggerCount,
    (SELECT COUNT(*) FROM sys.indexes WHERE index_id > 0) AS IndexCount;

-- 11. HENT TABEL RELATIONER (ER DIAGRAM DATA)
-- =============================================
PRINT '=== TABEL RELATIONER ==='
SELECT 
    fk.name AS ForeignKeyName,
    OBJECT_NAME(fk.parent_object_id) AS FromTable,
    COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS FromColumn,
    OBJECT_NAME(fk.referenced_object_id) AS ToTable,
    COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS ToColumn
FROM sys.foreign_keys fk
INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
ORDER BY FromTable, ToTable;

PRINT '=== SCRIPT FULDFØRT ==='
