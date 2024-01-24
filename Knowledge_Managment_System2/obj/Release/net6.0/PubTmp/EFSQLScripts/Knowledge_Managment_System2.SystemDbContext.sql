IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [Departments] (
        [DepartmentId] int NOT NULL IDENTITY,
        [DepartmentName] varchar(50) NOT NULL,
        CONSTRAINT [PK_Departments] PRIMARY KEY ([DepartmentId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [Positions] (
        [PositionId] int NOT NULL IDENTITY,
        [PositionName] varchar(100) NOT NULL,
        [DepartmentId] int NOT NULL,
        CONSTRAINT [PK_Positions] PRIMARY KEY ([PositionId]),
        CONSTRAINT [FK_Positions_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([DepartmentId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [PositionId] int NOT NULL,
        [Experience_Years] int NULL,
        [Experience_Type] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NULL,
        [TwoFactorEnabled] bit NULL,
        [LockoutEnd] datetimeoffset NOT NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUsers_Positions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Positions] ([PositionId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [EmployeePermission] (
        [EmployeesId] nvarchar(450) NOT NULL,
        [PermissionsId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_EmployeePermission] PRIMARY KEY ([EmployeesId], [PermissionsId]),
        CONSTRAINT [FK_EmployeePermission_AspNetRoles_PermissionsId] FOREIGN KEY ([PermissionsId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_EmployeePermission_AspNetUsers_EmployeesId] FOREIGN KEY ([EmployeesId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [Tracks] (
        [TrackId] int NOT NULL IDENTITY,
        [TrackName] varchar(100) NOT NULL,
        [PositionId] int NOT NULL,
        [EmployeeId] nvarchar(450) NOT NULL,
        [RequiredSkills] varchar(250) NOT NULL,
        [Created] datetime2 NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_Tracks] PRIMARY KEY ([TrackId]),
        CONSTRAINT [FK_Tracks_AspNetUsers_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Tracks_Positions_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Positions] ([PositionId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [Courses] (
        [CourseId] int NOT NULL IDENTITY,
        [CourseName] varchar(100) NOT NULL,
        [RequiredSkills] varchar(250) NOT NULL,
        [Status] bit NOT NULL DEFAULT CAST(0 AS bit),
        [Wait] bit NOT NULL DEFAULT CAST(1 AS bit),
        [Mandantory] bit NOT NULL DEFAULT CAST(0 AS bit),
        [Link_course] varchar(400) NOT NULL,
        [Created] datetime2 NOT NULL DEFAULT (GETDATE()),
        [TrackId] int NOT NULL,
        CONSTRAINT [PK_Courses] PRIMARY KEY ([CourseId]),
        CONSTRAINT [FK_Courses_Tracks_TrackId] FOREIGN KEY ([TrackId]) REFERENCES [Tracks] ([TrackId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [Records] (
        [RecordId] int NOT NULL IDENTITY,
        [RecordName] varchar(100) NOT NULL,
        [Status] bit NOT NULL DEFAULT CAST(0 AS bit),
        [Wait] bit NOT NULL DEFAULT CAST(1 AS bit),
        [Description] varchar(500) NOT NULL,
        [Mandantory] bit NOT NULL DEFAULT CAST(0 AS bit),
        [Created] datetime2 NOT NULL DEFAULT (GETDATE()),
        [TrackId] int NOT NULL,
        [DepartmentId] int NOT NULL,
        [Id] nvarchar(max) NOT NULL,
        [EmployeeId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Records] PRIMARY KEY ([RecordId]),
        CONSTRAINT [FK_Records_AspNetUsers_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Records_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([DepartmentId]),
        CONSTRAINT [FK_Records_Tracks_TrackId] FOREIGN KEY ([TrackId]) REFERENCES [Tracks] ([TrackId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [Achievements] (
        [Id] nvarchar(450) NOT NULL,
        [CourseId] int NOT NULL,
        [StartDate] datetime2 NOT NULL DEFAULT (GETDATE()),
        [AchievDate] date NULL,
        [Description] varchar(500) NULL,
        CONSTRAINT [PK_Achievements] PRIMARY KEY ([CourseId], [Id]),
        CONSTRAINT [FK_Achievements_AspNetUsers_Id] FOREIGN KEY ([Id]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Achievements_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([CourseId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [File] (
        [FileName] varchar(550) NOT NULL,
        [RecordId] int NOT NULL,
        CONSTRAINT [PK_File] PRIMARY KEY ([FileName]),
        CONSTRAINT [FK_File_Records_RecordId] FOREIGN KEY ([RecordId]) REFERENCES [Records] ([RecordId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE TABLE [Links] (
        [LinkId] int NOT NULL IDENTITY,
        [LinkName] varchar(120) NOT NULL,
        [LinkData] varchar(250) NOT NULL,
        [RecordId] int NOT NULL,
        CONSTRAINT [PK_Links] PRIMARY KEY ([LinkId]),
        CONSTRAINT [FK_Links_Records_RecordId] FOREIGN KEY ([RecordId]) REFERENCES [Records] ([RecordId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_Achievements_Id] ON [Achievements] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_AspNetUsers_PositionId] ON [AspNetUsers] ([PositionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_Courses_TrackId] ON [Courses] ([TrackId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_EmployeePermission_PermissionsId] ON [EmployeePermission] ([PermissionsId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_File_RecordId] ON [File] ([RecordId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_Links_RecordId] ON [Links] ([RecordId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_Positions_DepartmentId] ON [Positions] ([DepartmentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_Records_DepartmentId] ON [Records] ([DepartmentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_Records_EmployeeId] ON [Records] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_Records_TrackId] ON [Records] ([TrackId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_Tracks_EmployeeId] ON [Tracks] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    CREATE INDEX [IX_Tracks_PositionId] ON [Tracks] ([PositionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230704082012_AddTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230704082012_AddTables', N'7.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Records]') AND [c].[name] = N'Id');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Records] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Records] DROP COLUMN [Id];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Experience_Type');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Experience_Type];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Experience_Years');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Experience_Years];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tracks]') AND [c].[name] = N'RequiredSkills');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Tracks] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Tracks] ALTER COLUMN [RequiredSkills] varchar(500) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Courses]') AND [c].[name] = N'RequiredSkills');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Courses] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Courses] ALTER COLUMN [RequiredSkills] varchar(500) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'PasswordHash');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var5 + '];');
    EXEC(N'UPDATE [AspNetUsers] SET [PasswordHash] = '''' WHERE [PasswordHash] IS NULL');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [PasswordHash] varchar(800) NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT '' FOR [PasswordHash];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'LastName');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [LastName] varchar(50) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'FirstName');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [FirstName] varchar(50) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Email');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var8 + '];');
    EXEC(N'UPDATE [AspNetUsers] SET [Email] = '''' WHERE [Email] IS NULL');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Email] varchar(256) NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT '' FOR [Email];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Address');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var9 + '];');
    EXEC(N'UPDATE [AspNetUsers] SET [Address] = '''' WHERE [Address] IS NULL');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Address] varchar(100) NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT '' FOR [Address];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'AccessFailedCount');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [AspNetUsers] ADD DEFAULT 4 FOR [AccessFailedCount];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    CREATE TABLE [Experiences] (
        [ExperienceId] int NOT NULL IDENTITY,
        [PositionName] varchar(250) NOT NULL,
        [Year] int NOT NULL,
        [Description] varchar(500) NOT NULL,
        [EmployeeId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Experiences] PRIMARY KEY ([ExperienceId]),
        CONSTRAINT [FK_Experiences_AspNetUsers_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    CREATE INDEX [IX_Experiences_EmployeeId] ON [Experiences] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230714194024_AddExperiences_EditProperty')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230714194024_AddExperiences_EditProperty', N'7.0.8');
END;
GO

COMMIT;
GO

