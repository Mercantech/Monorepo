-- =============================================
-- Database Information Script - PostgreSQL Version
-- Henter alle databaser, tabeller, kolonner og relaterede objekter
-- =============================================

-- 1. HENT ALLE TABELLER I DEN NUVÆRENDE DATABASE
-- =============================================
-- @block ALLE TABELLER
SELECT '=== ALLE TABELLER I NUVÆRENDE DATABASE ===' AS Info;

-- PostgreSQL version med rækkeantal og størrelse:
SELECT 
    schemaname AS SchemaName,
    tablename AS TableName,
    tableowner AS TableOwner,
    hasindexes AS HasIndexes,
    hasrules AS HasRules,
    hastriggers AS HasTriggers,
    rowsecurity AS RowSecurity,
    pg_size_pretty(pg_total_relation_size(schemaname||'.'||tablename)) AS TotalSize,
    pg_size_pretty(pg_relation_size(schemaname||'.'||tablename)) AS TableSize,
    pg_size_pretty(pg_indexes_size(schemaname||'.'||tablename)) AS IndexSize
FROM pg_tables
WHERE schemaname NOT IN ('information_schema', 'pg_catalog')
ORDER BY schemaname, tablename;

-- 2. HENT ALLE KOLONNER MED DETALJER
-- =============================================
-- @block KOLONNER
SELECT '=== ALLE KOLONNER MED DETALJER ===' AS Info;

-- PostgreSQL version med detaljerede kolonne informationer:
SELECT 
    c.table_schema AS SchemaName,
    c.table_name AS TableName,
    c.column_name AS ColumnName,
    c.ordinal_position AS Position,
    c.data_type AS DataType,
    c.character_maximum_length AS MaxLength,
    c.numeric_precision AS Precision,
    c.numeric_scale AS Scale,
    c.is_nullable AS IsNullable,
    c.column_default AS DefaultValue,
    CASE 
        WHEN pk.column_name IS NOT NULL THEN 'YES'
        ELSE 'NO'
    END AS IsPrimaryKey,
    CASE 
        WHEN fk.column_name IS NOT NULL THEN 'YES'
        ELSE 'NO'
    END AS IsForeignKey
FROM information_schema.columns c
LEFT JOIN (
    SELECT ku.table_schema, ku.table_name, ku.column_name
    FROM information_schema.table_constraints tc
    INNER JOIN information_schema.key_column_usage ku
        ON tc.constraint_name = ku.constraint_name
    WHERE tc.constraint_type = 'PRIMARY KEY'
) pk ON c.table_schema = pk.table_schema 
    AND c.table_name = pk.table_name 
    AND c.column_name = pk.column_name
LEFT JOIN (
    SELECT ku.table_schema, ku.table_name, ku.column_name
    FROM information_schema.table_constraints tc
    INNER JOIN information_schema.key_column_usage ku
        ON tc.constraint_name = ku.constraint_name
    WHERE tc.constraint_type = 'FOREIGN KEY'
) fk ON c.table_schema = fk.table_schema 
    AND c.table_name = fk.table_name 
    AND c.column_name = fk.column_name
WHERE c.table_schema NOT IN ('information_schema', 'pg_catalog')
ORDER BY c.table_schema, c.table_name, c.ordinal_position;

-- 3. HENT ALLE FOREIGN KEYS
-- =============================================
-- @block FOREIGN KEYS
SELECT '=== ALLE FOREIGN KEYS ===' AS Info;

-- PostgreSQL version med delete/update actions:
SELECT 
    tc.constraint_name AS ForeignKeyName,
    tc.table_name AS TableName,
    kcu.column_name AS ColumnName,
    ccu.table_name AS ReferencedTableName,
    ccu.column_name AS ReferencedColumnName,
    rc.delete_rule AS DeleteAction,
    rc.update_rule AS UpdateAction
FROM information_schema.table_constraints tc
INNER JOIN information_schema.key_column_usage kcu 
    ON tc.constraint_name = kcu.constraint_name
INNER JOIN information_schema.constraint_column_usage ccu 
    ON ccu.constraint_name = tc.constraint_name
INNER JOIN information_schema.referential_constraints rc
    ON tc.constraint_name = rc.constraint_name
WHERE tc.constraint_type = 'FOREIGN KEY'
    AND tc.table_schema NOT IN ('information_schema', 'pg_catalog')
ORDER BY tc.table_name, tc.constraint_name;

-- 4. HENT ALLE INDEKSER
-- =============================================
-- @block INDEKSER
SELECT '=== ALLE INDEKSER ===' AS Info;

-- PostgreSQL version:
SELECT 
    schemaname AS SchemaName,
    tablename AS TableName,
    indexname AS IndexName,
    indexdef AS IndexDefinition,
    CASE 
        WHEN indexdef LIKE '%UNIQUE%' THEN 'YES'
        ELSE 'NO'
    END AS IsUnique,
    CASE 
        WHEN indexdef LIKE '%PRIMARY KEY%' THEN 'YES'
        ELSE 'NO'
    END AS IsPrimaryKey
FROM pg_indexes
WHERE schemaname NOT IN ('information_schema', 'pg_catalog')
ORDER BY schemaname, tablename, indexname;

-- 5. HENT ALLE STORED PROCEDURES
-- =============================================
-- @block STORED PROCEDURES
SELECT '=== ALLE STORED PROCEDURES ===' AS Info;

-- PostgreSQL version (funktioner og procedurer):
SELECT 
    routine_schema AS SchemaName,
    routine_name AS ProcedureName,
    routine_type AS RoutineType,
    data_type AS ReturnType,
    created AS CreatedDate,
    last_altered AS LastModified
FROM information_schema.routines
WHERE routine_schema NOT IN ('information_schema', 'pg_catalog')
    AND routine_type IN ('FUNCTION', 'PROCEDURE')
ORDER BY routine_schema, routine_name;

-- 6. HENT ALLE FUNKTIONER
-- =============================================
-- @block FUNKTIONER
SELECT '=== ALLE FUNKTIONER ===' AS Info;

-- PostgreSQL version (kun funktioner):
SELECT 
    routine_schema AS SchemaName,
    routine_name AS FunctionName,
    routine_type AS RoutineType,
    data_type AS ReturnType,
    created AS CreatedDate,
    last_altered AS LastModified
FROM information_schema.routines
WHERE routine_schema NOT IN ('information_schema', 'pg_catalog')
    AND routine_type = 'FUNCTION'
ORDER BY routine_schema, routine_name;

-- 7. HENT ALLE VIEWS
-- =============================================
-- @block VIEWS
SELECT '=== ALLE VIEWS ===' AS Info;

-- PostgreSQL version:
SELECT 
    table_schema AS SchemaName,
    table_name AS ViewName,
    view_definition AS Definition
FROM information_schema.views
WHERE table_schema NOT IN ('information_schema', 'pg_catalog')
ORDER BY table_schema, table_name;

-- 8. HENT ALLE TRIGGERS
-- =============================================
-- @block TRIGGERS
SELECT '=== ALLE TRIGGERS ===' AS Info;

-- PostgreSQL version:
SELECT 
    trigger_name AS TriggerName,
    event_object_table AS TableName,
    event_manipulation AS EventType,
    action_timing AS Timing,
    action_statement AS ActionStatement,
    action_orientation AS Orientation
FROM information_schema.triggers
WHERE trigger_schema NOT IN ('information_schema', 'pg_catalog')
ORDER BY event_object_table, trigger_name;

-- 9. HENT DATABASE STATISTIKKER
-- =============================================
-- @block STATISTIKKER
SELECT '=== DATABASE STATISTIKKER ===' AS Info;

-- PostgreSQL version:
SELECT 
    current_database() AS CurrentDatabase,
    (SELECT COUNT(*) FROM information_schema.tables WHERE table_type = 'BASE TABLE' AND table_schema NOT IN ('information_schema', 'pg_catalog')) AS TableCount,
    (SELECT COUNT(*) FROM information_schema.views WHERE table_schema NOT IN ('information_schema', 'pg_catalog')) AS ViewCount,
    (SELECT COUNT(*) FROM information_schema.routines WHERE routine_type = 'PROCEDURE' AND routine_schema NOT IN ('information_schema', 'pg_catalog')) AS ProcedureCount,
    (SELECT COUNT(*) FROM information_schema.routines WHERE routine_type = 'FUNCTION' AND routine_schema NOT IN ('information_schema', 'pg_catalog')) AS FunctionCount,
    (SELECT COUNT(*) FROM information_schema.triggers WHERE trigger_schema NOT IN ('information_schema', 'pg_catalog')) AS TriggerCount,
    (SELECT COUNT(*) FROM pg_indexes WHERE schemaname NOT IN ('information_schema', 'pg_catalog')) AS IndexCount;

-- 10. HENT TABEL RELATIONER (ER DIAGRAM DATA)
-- =============================================
-- @block RELATIONER
SELECT '=== TABEL RELATIONER ===' AS Info;

-- PostgreSQL version:
SELECT 
    tc.constraint_name AS ForeignKeyName,
    tc.table_name AS FromTable,
    kcu.column_name AS FromColumn,
    ccu.table_name AS ToTable,
    ccu.column_name AS ToColumn
FROM information_schema.table_constraints tc
INNER JOIN information_schema.key_column_usage kcu 
    ON tc.constraint_name = kcu.constraint_name
INNER JOIN information_schema.constraint_column_usage ccu 
    ON ccu.constraint_name = tc.constraint_name
WHERE tc.constraint_type = 'FOREIGN KEY'
    AND tc.table_schema NOT IN ('information_schema', 'pg_catalog')
ORDER BY tc.table_name, ccu.table_name;

SELECT '=== SCRIPT FULDFØRT ===' AS Info;
