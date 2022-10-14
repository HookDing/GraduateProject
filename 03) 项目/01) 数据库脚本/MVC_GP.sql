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
--�û���
create table UserInfo
(
user_uid int primary key identity(1,1),
user_uname nvarchar(50) not null,
user_password nvarchar(30) not null,
)

go
--��̳��
create table Talk(
talk_tid int primary key identity(1,1),
talk_title varchar(50) not null,
talk_content text not null,
user_uid int foreign key references UserInfo(user_uid) not null,
)
go
--���ű�
create table Hot(
rid int primary key identity(1,1),
Title nvarchar(20)not null,
imgurl text not null,
imgtext text not null,
)
go
--����Ʒ�Ʊ�
create table brands(
brand_id int primary key identity(1,1),
brand_name varchar(20) not null,--Ʒ������
)
go
--�������ӿڱ�
create table cpu_sockets(
sockets_id int primary key identity(1,1),
sockets_name varchar(20) not null,--�ӿ��ͺ�
)
go
--��������Ϣ��
create table cpu_info(
cpu_id int primary key identity(1,1),
cpu_Name varchar(20) not null,--����
cpu_core int not null,--������
cpu_threads int not null,--�߳���
cpu_base_frequency numeric(4,2) not null,--��׼Ƶ��
cpu_turbo_frequency numeric(4,2) not null,--����Ƶ��
cpu_tdp int not null,--ɢ����ƹ���
cpu_brandId int foreign key references brands(brand_id),--Ʒ��
cpu_sockets_id int foreign key references cpu_sockets(sockets_id),--�ӿ�(���)
)
go
--������Ϣ��
create table mainboard_info(
mainboard_id int primary key identity(1,1),--������
mainboard_name varchar(30) not null,--�����ͺ�
mainboard_sizeType varchar(20) not null,--����ߴ�����
mainboard_brand int foreign key references brands(brand_id),--����Ʒ��
mainboard_sockets_id int foreign key references cpu_sockets(sockets_id)--CPU���
)
go
--�ڴ��
create table mem(
mem_id int primary key identity(1,1),
mem_brand_id int foreign key references brands(brand_id), --Ʒ��
mem_capacity int not null,--����
mem_frenquence int not null,--Ƶ��
)
go
--�Կ���
create table GPU(
gpu_id int primary key identity(1,1),
gpu_brand int foreign key references brands(brand_id),--Ʒ��
gpu_model varchar(50) not null,--�ͺ�
gpu_size_lenth int not null,--�ߴ�-��
)
go
--Ӳ�̱�
create table disks(
disks_id int primary key identity(1,1),
disks_brand_id int foreign key references brands(brand_id),--Ʒ��
disks_model varchar(50) not null,--�ͺ�
disks_connection varchar(20) not null,--�ӿ�
)
go
--��Դ��
create table powers(
powers_id int primary key identity(1,1),
powers_brands int foreign key references brands(brand_id),--Ʒ��
powers_warts int not null,--����ع���
powers_size varchar(20) not null,--�ߴ�
)
--==����¼��===========================================
--�û���
go 
insert into UserInfo(user_uname,user_password)values('admin','123456')
insert into UserInfo(user_uname,user_password)values('123456','123456')

--��̳
go
insert into Talk(talk_title,talk_content,user_uid)
values
('�Կ��ǿ���ô��','618�������Կ�������������ǿ󿨣���װ���˲�����ϻ����������͸���ʹ��û�����⣬�򿪴�����Ϸ�ͺ�������',1)

--����
go
insert into Hot(Title,imgurl,imgtext)values
('������','AMD����9_3900X������.png',' AMD ����9 3900X������(r9)7nm 12��24�߳� 3.8GHz 105W AM4�ӿ� ��װCPU 100-100000023CBXAMD ����9 3900X������ 64����128�̣߳��ܻ���288MB��Ĭ����Ƶ2.9GHz����߼���Ƶ��4.3GHz��TDP 280W���۸�3990��Ԫ����������2��7�ţ��򵥵����۾���̫ǿ���ˡ��ܾ���ǰIntel����ƽ̨�ͷ�Ϊ��2���ȼ���һ����������ͨ�û���ƽ̨����оƬ��һ����Z��H��B��ͷ��Ʃ��Z97��Z170�ȣ�����һ��ƽ̨���������ѵ�ƽ̨����оƬ��һ����X��ͷ��Ʃ��X79��X99�ȣ�������ͨƽ̨��CPU��������һ�㶼��С�ڷ�����ƽ̨����AMD��Ryzen������������Ҳ����˶��ֵĲ��ԣ�����ƽ̨��Ryzenϵ�д������������߶˵�ƽ̨��Threadripper���߳�˺���ߣ�ϵ�д�������ͬ���ģ�����ƽ̨Ҳ�ǲ��ú����������������֣�AMD�¼ܹ�����CCXģ�黯��ƣ��ڶѺ��������Intel�����ף�����ں�����������ѹ��Intel�ġ�Ӣ�ض�����Զ�AMD���µ�32��Threadripper 3970X���Եúܳ����������������٣��ؼ��Ǽ۸��AMD�Ĺ�һ��أ������ܺͳɱ�����û�ж������ƣ������Threadripper 3990X�͸�������Ϊ���ˣ�HEDTƽ̨�ϣ�IntelĿǰû�п��Ժ�AMD����Ĳ�Ʒ��'),
('�Կ�','΢���Կ�.png','΢�� RTX3090Ti �羺�Կ�'),
('��ʾ��','������ʾ��.png','���ǣ�SAMSUNG��23.8Ӣ�� FreeSync �ɱڹ� '),
('�ڴ���','�������ڴ���.png','Э�� ɢ��Ƭ ̨ʽ���ڴ���'),
('����','�޼�����.png','�޼�����'),
('���','�޼�����.png','�޼����'),
('Ӳ��','ϣ�ݻ�еӲ�� SATA.png','ϣ�ݻ�еӲ��'),
('��Դ','EVESKY��Դ.png','EVESKY��Դ'),
('����','��˶����.png','��˶����'),
('ɢ����','èͷӥɢ����.png','èͷӥɢ����'),
('����','èͷӥɢ����.png','����'),
('��˷�','΢���Կ�.png','��˷�')

go

--����Ʒ�Ʊ�
--cpu
insert into brands(brand_name) values('AMD')
insert into brands(brand_name) values('Intel')
--����/�Կ�
insert into brands(brand_name) values('��˶')
insert into brands(brand_name) values('����')
insert into brands(brand_name) values('΢��')
--�Կ�
insert into brands(brand_name) values('�߲ʺ�')
--�ڴ�
insert into brands(brand_name) values('����')
insert into brands(brand_name) values('֥��')
insert into brands(brand_name) values('��ʿ��')
insert into brands(brand_name) values('������')
--Ӳ��
insert into brands(brand_name) values('����')
insert into brands(brand_name) values('����')
insert into brands(brand_name) values('��������')
--ɢ��
insert into brands(brand_name) values('èͷӥ')
insert into brands(brand_name) values('��������')
insert into brands(brand_name) values('���ݷ���')
--��Դ
insert into brands(brand_name) values('����')
insert into brands(brand_name) values('����')
insert into brands(brand_name) values('���ѿ�')
insert into brands(brand_name) values('ȫ��')

--�������ӿڱ�
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

--��������Ϣ��
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


--������Ϣ��
go
insert into mainboard_info(mainboard_name,mainboard_sizeType,mainboard_sockets_id)
values
('B550M Motar','M-ATX',(select sockets_id from cpu_sockets where sockets_name='SocketAM4')),
('B560M Motar','M-ATX',(select sockets_id from cpu_sockets where sockets_name='LGA1700')),
('B450M Motar','M-ATX',(select sockets_id from cpu_sockets where sockets_name='SocketAM4')),
('B460M Motar','M-ATX',(select sockets_id from cpu_sockets where sockets_name='LGA1700'))

--�ڴ��
go
insert into mem(mem_brand_id,mem_capacity,mem_frenquence)
values
((select brand_id from brands where brand_name='����'),8,3200 ),
((select brand_id from brands where brand_name='����'),16,3600 ),
((select brand_id from brands where brand_name='֥��'),8,3200 ),
((select brand_id from brands where brand_name='֥��'),16,3600 ),
((select brand_id from brands where brand_name='��ʿ��'),8,3200 ),
((select brand_id from brands where brand_name='��ʿ��'),16,3600 ),
((select brand_id from brands where brand_name='������'),8,3200 ),
((select brand_id from brands where brand_name='������'),16,3600 )

--�Կ���
go
insert into GPU(gpu_brand,gpu_model,gpu_size_lenth)
values
((select brand_id from brands where brand_name='��˶'),'��˶TUF-RTX3090-24G-GAMING',30),
((select brand_id from brands where brand_name='΢��'),'΢��GeForce RTX 3090 SUPRIM X 24G',34),
((select brand_id from brands where brand_name='�߲ʺ�'),'�߲ʺ�iGame GeForce RTX 3090 kudan',32)


--Ӳ�̱�
go
insert into disks(disks_brand_id,disks_model,disks_connection)
values
((select brand_id from brands where brand_name='����'),'SAMSUNG 980 PRO','m.2'),
((select brand_id from brands where brand_name='����'),'ZhiTai TiPro7000','m.2'),
((select brand_id from brands where brand_name='��������'),'Western Digital SN850','m.2')

--��Դ��
go
insert into powers(powers_brands,powers_warts,powers_size)
values
((select brand_id from brands where brand_name='����'),500,'ATX'),
((select brand_id from brands where brand_name='����'),1000,'ATX'),
((select brand_id from brands where brand_name='���ѿ�'),1000,'ATX'),
((select brand_id from brands where brand_name='ȫ��'),850,'ATX')

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
