
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Username NVARCHAR(100) NOT NULL UNIQUE,
    FullName NVARCHAR(MAX) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Created DATETIME DEFAULT GETDATE(),
    CreatedBy UNIQUEIDENTIFIER NULL,
    Modified DATETIME NULL,
    ModifiedBy UNIQUEIDENTIFIER NULL,
    IsDeleted BIT DEFAULT 0 NOT NULL
);
-- Add Indexes to Users Table
CREATE INDEX IX_Users_Username ON Users (Username);
CREATE INDEX IX_Users_Email ON Users (Email);
CREATE INDEX IX_Users_CreatedBy ON Users (CreatedBy);
CREATE INDEX IX_Users_IsDeleted ON Users (IsDeleted);

CREATE TABLE Roles (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(50) NOT NULL UNIQUE
);
-- Add Index to Roles Table
CREATE INDEX IX_Roles_Name ON Roles (Name);

CREATE TABLE UserRoles (
    UserId UNIQUEIDENTIFIER NOT NULL,
    RoleId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE
);
-- Add Index to UserRoles Table
CREATE INDEX IX_UserRoles_UserId ON UserRoles (UserId);
CREATE INDEX IX_UserRoles_RoleId ON UserRoles (RoleId);

ALTER TABLE Users ADD CONSTRAINT FK_Users_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(Id) ON DELETE NO ACTION;
ALTER TABLE Users ADD CONSTRAINT FK_Users_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES Users(Id) ON DELETE NO ACTION;



DECLARE @AdminRoleId UNIQUEIDENTIFIER = 'CF17B7E5-BF05-4AC8-91F8-777CA4D434DB';
DECLARE @UserRoleId UNIQUEIDENTIFIER = 'E5159093-9BEA-44E4-807B-F83D8EB3FB22';
DECLARE @AdminUserId UNIQUEIDENTIFIER ='85ED7233-602F-47E3-AFCB-B1AA8BE36CF7';
DECLARE @password NVARCHAR(255) ='585a57021e3a392b0c0f72daee8c8dfe39126c4d22438853d0309f6fe33672a9';--P@ssw0rd

INSERT INTO Roles (Id, Name) VALUES 
    (@AdminRoleId, 'Admin'),
    (@UserRoleId, 'User');


INSERT INTO Users (Id, Username, FullName, Password, Email, Created, CreatedBy, IsDeleted) 
VALUES (
    @AdminUserId, 
    'admin', 
    'System Administrator', 
    @password, 
    'admin@example.com', 
     GETUTCDATE(), 
    NULL, 
    0
);

INSERT INTO UserRoles (UserId, RoleId) 
VALUES (@AdminUserId, @AdminRoleId);


CREATE TABLE AuditLogs (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TableName NVARCHAR(255) NOT NULL, 
    RecordId UNIQUEIDENTIFIER NOT NULL, 
    Action NVARCHAR(50) NOT NULL, 
    OldValues NVARCHAR(MAX) NULL, 
    NewValues NVARCHAR(MAX) NULL, 
    Timestamp DATETIME DEFAULT GETUTCDATE() NOT NULL, 
    UserId UNIQUEIDENTIFIER NULL 
);

CREATE INDEX IX_AuditLogs_TableName_RecordId ON AuditLogs (TableName, RecordId);

CREATE INDEX IX_AuditLogs_Timestamp ON AuditLogs (Timestamp);

CREATE INDEX IX_AuditLogs_UserId ON AuditLogs (UserId);