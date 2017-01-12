/* Add User Area and Role */

-- TODO: We should be able to bootstrap roles and permissions from the application

insert into Cofoundry.UserArea (UserAreaCode, [Name]) values ('SPA', 'SPACat Members')

insert into Cofoundry.[Role] ([Title], SpecialistRoleTypeCode, UserAreaCode) values ('User', 'USR','SPA')
declare @RoleId int = SCOPE_IDENTITY()

insert into Cofoundry.RolePermission (RoleId, PermissionId) 
select @RoleId, p.PermissionId
from Cofoundry.Permission p 
where PermissionCode = 'COMRED' and EntityDefinitionCode <> 'CMSUSN' and EntityDefinitionCode <> 'CMSUSR'