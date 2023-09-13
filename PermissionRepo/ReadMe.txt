Create this two table in database.


create table  ControllerAction
(
id int primary key identity(1,1),
Controller varchar(100),
[Action] varchar(200),
ActionUrl varchar(200)
)

Create table RoleManageModel
(
Id int primary key identity(1,1),
CAId int,
RoleId int ,
HasAccess bit
)



superadmin 
user 
admin

First login with superadmin. go to givepermission and click get all url. 
give permission to role. 

