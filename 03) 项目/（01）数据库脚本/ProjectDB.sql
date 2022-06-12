use master
if exists(select * from sysdatabases where name='ProjectDB')
alter database ProjectDB set single_user with rollback immediate
if exists(select * from sysdatabases where name='ProjectDB')
drop database ProjectDB
go
create database ProjectDB
go
use ProjectDB
go
--用户表
create table UserInfo
(
user_uid int primary key identity(1,1),
user_uname nvarchar(50) not null,
user_password nvarchar(30) not null,
)

go
--论坛表
create table Talk(
talk_tid int primary key identity(1,1),
talk_title varchar(50) not null,
talk_content text not null,
user_uid int foreign key references UserInfo(user_uid) not null,
)
go
--热门表
create table Hot(
rid int primary key identity(1,1),
Title nvarchar(20)not null,
imgurl text not null,
imgtext text not null,
)
go
--所有品牌表
create table brands(
brand_id int primary key identity(1,1),
brand_name varchar(20) not null,--品牌名称
)
go
--处理器接口表
create table cpu_sockets(
sockets_id int primary key identity(1,1),
sockets_name varchar(20) not null,--接口型号
)
go
--处理器信息表
create table cpu_info(
cpu_id int primary key identity(1,1),
cpu_Name varchar(20) not null,--名称
cpu_core int not null,--核心数
cpu_threads int not null,--线程数
cpu_base_frequency numeric(4,2) not null,--基准频率
cpu_turbo_frequency numeric(4,2) not null,--加速频率
cpu_tdp int not null,--散热设计功耗
cpu_brandId int foreign key references brands(brand_id),--品牌
cpu_sockets_id int foreign key references cpu_sockets(sockets_id),--接口(插槽)
)
go
--主板信息表
create table mainboard_info(
mainboard_id int primary key identity(1,1),--主板编号
mainboard_name varchar(30) not null,--主板型号
mainboard_sizeType varchar(20) not null,--主板尺寸类型
mainboard_brand int foreign key references brands(brand_id),--主板品牌
mainboard_sockets_id int foreign key references cpu_sockets(sockets_id)--CPU插槽
)
go
--内存表
create table mem(
mem_id int primary key identity(1,1),
mem_brand_id int foreign key references brands(brand_id), --品牌
mem_capacity int not null,--容量
mem_frenquence int not null,--频率
)
go
--显卡表
create table GPU(
gpu_id int primary key identity(1,1),
gpu_brand int foreign key references brands(brand_id),--品牌
gpu_model varchar(50) not null,--型号
gpu_size_lenth int not null,--尺寸-长
)
go
--硬盘表
create table disks(
disks_id int primary key identity(1,1),
disks_brand_id int foreign key references brands(brand_id),--品牌
disks_model varchar(50) not null,--型号
disks_connection varchar(20) not null,--接口
)
go
--电源表
create table powers(
powers_id int primary key identity(1,1),
powers_brands int foreign key references brands(brand_id),--品牌
powers_warts int not null,--最大负载功率
powers_size varchar(20) not null,--尺寸
)
--==数据录入===========================================
--用户表
go 
insert into UserInfo(user_uname,user_password)values('admin','123456')
insert into UserInfo(user_uname,user_password)values('123456','123456')

--论坛
go
insert into Talk(talk_title,talk_content,user_uid)
values
('显卡是矿卡怎么办','618买了张显卡，买回来发现是矿卡，包装被人拆过，上机能亮屏，低负载使用没有问题，打开大型游戏就黑屏重启',1)

--热门
go
insert into Hot(Title,imgurl,imgtext)values
('处理器','AMD锐龙9_3900X处理器.png',' AMD 锐龙9 3900X处理器(r9)7nm 12核24线程 3.8GHz 105W AM4接口 盒装CPU 100-100000023CBXAMD 锐龙9 3900X处理器 64核心128线程，总缓存288MB，默认主频2.9GHz，最高加速频率4.3GHz，TDP 280W，价格3990美元，开售日期2月7号，简单的评价就是太强大了。很久以前Intel桌面平台就分为了2个等级，一个是面向普通用户的平台，其芯片组一般是Z、H、B开头，譬如Z97，Z170等，另外一个平台是面向发烧友的平台，其芯片组一般是X开头，譬如X79，X99等，其中普通平台的CPU核心数量一般都会小于发烧友平台。而AMD在Ryzen处理器发布后，也借鉴了对手的策略，主流平台是Ryzen系列处理器，而更高端的平台是Threadripper（线程撕裂者）系列处理器，同样的，两个平台也是采用核心数量来进行区分，AMD新架构采用CCX模块化设计，在堆核心上面比Intel更容易，因此在核心数上面是压着Intel的。英特尔在面对对AMD最新的32核Threadripper 3970X都显得很吃力，核心数量更少，关键是价格比AMD的贵一大截，在性能和成本上面没有多少优势，在面对Threadripper 3990X就更是无能为力了，HEDT平台上，Intel目前没有可以和AMD抗衡的产品。'),
('显卡','微星显卡.png','微星 RTX3090Ti 电竞显卡'),
('显示器','三星显示器.png','三星（SAMSUNG）23.8英寸 FreeSync 可壁挂 '),
('内存条','海盗船内存条.png','协德 散热片 台式机内存条'),
('键盘','罗技键盘.png','罗技键盘'),
('鼠标','罗技键盘.png','罗技鼠标'),
('硬盘','希捷机械硬盘 SATA.png','希捷机械硬盘'),
('电源','EVESKY电源.png','EVESKY电源'),
('主板','华硕主板.png','华硕主板'),
('散热器','猫头鹰散热器.png','猫头鹰散热器'),
('音箱','猫头鹰散热器.png','音箱'),
('麦克风','微星显卡.png','麦克风')

go

--所有品牌表
--cpu
insert into brands(brand_name) values('AMD')
insert into brands(brand_name) values('Intel')
--主板/显卡
insert into brands(brand_name) values('华硕')
insert into brands(brand_name) values('技嘉')
insert into brands(brand_name) values('微星')
--显卡
insert into brands(brand_name) values('七彩虹')
--内存
insert into brands(brand_name) values('光威')
insert into brands(brand_name) values('芝奇')
insert into brands(brand_name) values('金士顿')
insert into brands(brand_name) values('海盗船')
--硬盘
insert into brands(brand_name) values('三星')
insert into brands(brand_name) values('致钛')
insert into brands(brand_name) values('西部数据')
--散热
insert into brands(brand_name) values('猫头鹰')
insert into brands(brand_name) values('酷冷至尊')
insert into brands(brand_name) values('九州风神')
--电源
insert into brands(brand_name) values('海韵')
insert into brands(brand_name) values('长城')
insert into brands(brand_name) values('安钛克')
insert into brands(brand_name) values('全汉')

--处理器接口表
go
insert into cpu_sockets(sockets_name)
values
('Slot1')
,('Socket370')
,('Socket423')
,('Socket478')
,('LGA775')
,('LGA1366')
,('LGA1156')
,('LGA1155')
,('LGA2011-1')
,('LGA1150')
,('LGA2011-3')
,('LGA1151')
,('LGA1200')
,('LGA1700')
,('LGA2066')
,('SlotA')
,('Socket462')
,('Socket754')
,('Socket940')
,('Socket939')
,('SocketAM2')
,('Socket1207FX')
,('SocketAM2+')
,('SocketAM3')
,('SocketAM3+')
,('SocketFM1')
,('SocketFM2')
,('SocketFM2+')
,('SocketAM4')
,('SocketTR4')

--处理器信息表
go
insert into cpu_info(cpu_Name,cpu_core,cpu_threads,cpu_base_frequency,cpu_turbo_frequency,cpu_tdp,cpu_brandId,cpu_sockets_id)
values
--Intel
('i9-12900',16,24,1.8,5.1,202,
(select brand_id from brands where brand_name='Intel'),
(select sockets_id from cpu_sockets where sockets_name='LGA1700')),
('i7-12700',12,20,1.6,4.9,180,
(select brand_id from brands where brand_name='Intel'),
(select sockets_id from cpu_sockets where sockets_name='LGA1700')),
('i5-12600',6,12,3.3,4.8,117,
(select brand_id from brands where brand_name='Intel'),
(select sockets_id from cpu_sockets where sockets_name='LGA1700')),
('i5-12400',6,12,2.5,4.4,117,
(select brand_id from brands where brand_name='Intel'),
(select sockets_id from cpu_sockets where sockets_name='LGA1700')),
('i3-12100',4,8,3.3,4.3,89,
(select brand_id from brands where brand_name='Intel'),
(select sockets_id from cpu_sockets where sockets_name='LGA1700')),
--AMD
('R9-5950X',16,32,3.4,4.9,105,
(select brand_id from brands where brand_name='AMD'),
(select sockets_id from cpu_sockets where sockets_name='SocketAM4')),
('R9-5900X',12,24,3.7,4.8,105,
(select brand_id from brands where brand_name='AMD'),
(select sockets_id from cpu_sockets where sockets_name='SocketAM4')),
('R7-5800X',8,16,3.8,4.7,105,
(select brand_id from brands where brand_name='AMD'),
(select sockets_id from cpu_sockets where sockets_name='SocketAM4')),
('R5-5600X',6,12,3.7,4.6,65,
(select brand_id from brands where brand_name='AMD'),
(select sockets_id from cpu_sockets where sockets_name='SocketAM4')),
('R5-5600G',6,12,3.9,4.4,65,
(select brand_id from brands where brand_name='AMD'),
(select sockets_id from cpu_sockets where sockets_name='SocketAM4'))


--主板信息表
go
insert into mainboard_info(mainboard_name,mainboard_sizeType,mainboard_sockets_id)
values
('B550M Motar','M-ATX',(select sockets_id from cpu_sockets where sockets_name='SocketAM4')),
('B560M Motar','M-ATX',(select sockets_id from cpu_sockets where sockets_name='LGA1700')),
('B450M Motar','M-ATX',(select sockets_id from cpu_sockets where sockets_name='SocketAM4')),
('B460M Motar','M-ATX',(select sockets_id from cpu_sockets where sockets_name='LGA1700'))

--内存表
go
insert into mem(mem_brand_id,mem_capacity,mem_frenquence)
values
((select brand_id from brands where brand_name='光威'),8,3200 ),
((select brand_id from brands where brand_name='光威'),16,3600 ),
((select brand_id from brands where brand_name='芝奇'),8,3200 ),
((select brand_id from brands where brand_name='芝奇'),16,3600 ),
((select brand_id from brands where brand_name='金士顿'),8,3200 ),
((select brand_id from brands where brand_name='金士顿'),16,3600 ),
((select brand_id from brands where brand_name='海盗船'),8,3200 ),
((select brand_id from brands where brand_name='海盗船'),16,3600 )

--显卡表
go
insert into GPU(gpu_brand,gpu_model,gpu_size_lenth)
values
((select brand_id from brands where brand_name='华硕'),'华硕TUF-RTX3090-24G-GAMING',30),
((select brand_id from brands where brand_name='微星'),'微星GeForce RTX 3090 SUPRIM X 24G',34),
((select brand_id from brands where brand_name='七彩虹'),'七彩虹iGame GeForce RTX 3090 kudan',32)


--硬盘表
go
insert into disks(disks_brand_id,disks_model,disks_connection)
values
((select brand_id from brands where brand_name='三星'),'SAMSUNG 980 PRO','m.2'),
((select brand_id from brands where brand_name='致钛'),'ZhiTai TiPro7000','m.2'),
((select brand_id from brands where brand_name='西部数据'),'Western Digital SN850','m.2')

--电源表
go
insert into powers(powers_brands,powers_warts,powers_size)
values
((select brand_id from brands where brand_name='海韵'),500,'ATX'),
((select brand_id from brands where brand_name='长城'),1000,'ATX'),
((select brand_id from brands where brand_name='安钛克'),1000,'ATX'),
((select brand_id from brands where brand_name='全汉'),850,'ATX')

go
select * from UserInfo
select * from Talk
select * from brands
select * from cpu_sockets
select * from cpu_info
select * from mainboard_info
select * from mem
select * from GPU
select * from disks
select * from powers
