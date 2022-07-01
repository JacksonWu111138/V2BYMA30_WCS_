------------Sno_Max -------------
INSERT INTO Sno_Max (Sno_Type,Month_Flag,Init_Sno,Max_Sno,Sno_Len) 
VALUES 
('CMDSNO', 'Y', 1, 19997, 5),
('CMDSUO', 'N', 20001, 29997, 5);

INSERT INTO UnitModeDef (StockerID, In_enable) VALUES
('1', 'Y'),
('2', 'Y'),
('3', 'Y'),
('4', 'Y');

insert into Teach_Loc (DeviceID,Loc) VALUES
('1','0200303'),
('2','0200303'),
('3','0200303');

--STK 1 (PCBA)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('1', 'M1-01', 3, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('1', 'M1-06', 3, 2, 6, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('1', 'LeftFork', 4, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('1', 'Shelf', 0, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('1', 'Teach', 0, 2, 2, ' ');

insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('1', 'M1-06','1', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('1', 'M1-06','1', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('1', 'M1-06','1', 'M1-01', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('1', 'Shelf','1', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('1', 'Teach','1', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('1', 'Shelf','1', 'M1-01', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('1', 'Teach','1', 'M1-01', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('1', 'LeftFork','1', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('1', 'LeftFork','1', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('1', 'LeftFork','1', 'M1-01', ' ');


--STK 2 (PCBA)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('2', 'M1-11', 3, 1, 11, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('2', 'M1-16', 3, 2, 16, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('2', 'LeftFork', 4, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('2', 'Shelf', 0, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('2', 'Teach', 0, 2, 2, ' ');

insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('2', 'M1-16','2', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('2', 'M1-16','2', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('2', 'M1-16','2', 'M1-11', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('2', 'Shelf','2', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('2', 'Teach','2', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('2', 'Shelf','2', 'M1-11', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('2', 'Teach','2', 'M1-11', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('2', 'LeftFork','2', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('2', 'LeftFork','2', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('2', 'LeftFork','2', 'M1-11', ' ');


--STK 3 (箱式倉1)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('3', 'B1-001', 3, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('3', 'B1-004', 3, 2, 4, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('3', 'B1-007', 3, 3, 7, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('3', 'B1-010', 3, 4, 10, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('3', 'B1-081', 3, 5, 81, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('3', 'B1-084', 3, 6, 84, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('3', 'B1-087', 3, 7, 87, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('3', 'B1-090', 3, 8, 90, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('3', 'LeftFork', 4, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('3', 'Shelf', 0, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('3', 'Teach', 0, 2, 2, ' ');

insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-007','3', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-007','3', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-007','3', 'B1-004', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-007','3', 'B1-084', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-010','3', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-010','3', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-087','3', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-087','3', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-087','3', 'B1-084', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-087','3', 'B1-004', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-090','3', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'B1-090','3', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'Shelf','3', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'Teach','3', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'Shelf','3', 'B1-001', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'Shelf','3', 'B1-004', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'Shelf','3', 'B1-081', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'Shelf','3', 'B1-084', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'Teach','3', 'B1-004', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'Teach','3', 'B1-084', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'LeftFork','3', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'LeftFork','3', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'LeftFork','3', 'B1-001', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'LeftFork','3', 'B1-004', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'LeftFork','3', 'B1-081', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('3', 'LeftFork','3', 'B1-084', ' ');


--STK 4 (箱式倉2)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('4', 'B1-013', 3, 1, 13, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('4', 'B1-016', 3, 2, 16, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('4', 'B1-019', 3, 3, 19, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('4', 'B1-022', 3, 4, 22, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('4', 'B1-093', 3, 5, 93, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('4', 'B1-096', 3, 6, 96, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('4', 'B1-099', 3, 7, 99, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('4', 'B1-102', 3, 8, 102, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('4', 'LeftFork', 4, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('4', 'Shelf', 0, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('4', 'Teach', 0, 2, 2, ' ');

insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-019','4', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-019','4', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-019','4', 'B1-016', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-019','4', 'B1-096', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-022','4', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-022','4', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-099','4', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-099','4', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-099','4', 'B1-096', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-099','4', 'B1-016', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-102','4', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'B1-102','4', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'Shelf','4', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'Teach','4', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'Shelf','4', 'B1-013', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'Shelf','4', 'B1-016', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'Shelf','4', 'B1-093', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'Shelf','4', 'B1-096', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'Teach','4', 'B1-016', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'Teach','4', 'B1-096', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'LeftFork','4', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'LeftFork','4', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'LeftFork','4', 'B1-013', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'LeftFork','4', 'B1-016', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'LeftFork','4', 'B1-093', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('4', 'LeftFork','4', 'B1-096', ' ');


--STK 5 (箱式倉3)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('5', 'B1-025', 3, 1, 25, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('5', 'B1-028', 3, 2, 28, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('5', 'B1-031', 3, 3, 31, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('5', 'B1-034', 3, 4, 34, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('5', 'B1-105', 3, 5, 105, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('5', 'B1-108', 3, 6, 108, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('5', 'B1-111', 3, 7, 111, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('5', 'B1-114', 3, 8, 114, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('5', 'LeftFork', 4, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('5', 'Shelf', 0, 1, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('5', 'Teach', 0, 2, 2, ' ');

insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-031','5', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-031','5', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-031','5', 'B1-028', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-031','5', 'B1-108', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-034','5', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-034','5', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-111','5', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-111','5', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-111','5', 'B1-108', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-111','5', 'B1-028', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-114','5', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'B1-114','5', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'Shelf','5', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'Teach','5', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'Shelf','5', 'B1-025', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'Shelf','5', 'B1-028', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'Shelf','5', 'B1-105', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'Shelf','5', 'B1-108', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'Teach','5', 'B1-028', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'Teach','5', 'B1-108', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'LeftFork','5', 'Teach', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'LeftFork','5', 'Shelf', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'LeftFork','5', 'B1-025', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'LeftFork','5', 'B1-028', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'LeftFork','5', 'B1-105', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('5', 'LeftFork','5', 'B1-108', ' ');

--AGV (3F)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('6', 'A1-04', 3, 1, 4, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('6', 'A1-08', 3, 2, 8, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('6', 'A1-12', 3, 3, 12, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('6', 'LO4-04', 3, 4, 4, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('6', 'A1-01', 3, 5, 1, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('6', 'A1-05', 3, 6, 5, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('6', 'A1-09', 3, 7, 9, ' ');
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT) values('6', 'LO4-01', 3, 8, 1, ' ');

insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-04','6', 'A1-01', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-04','6', 'A1-05', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-04','6', 'A1-09', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-04','6', 'LO4-01', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-08','6', 'A1-01', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-08','6', 'A1-05', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-08','6', 'A1-09', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-08','6', 'LO4-01', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-12','6', 'A1-01', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-12','6', 'A1-05', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-12','6', 'A1-09', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'A1-12','6', 'LO4-01', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'LO4-04','6', 'A1-01', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'LO4-04','6', 'A1-05', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'LO4-04','6', 'A1-09', ' ');
insert into Routdef(DeviceID, HostPortID, NextDeviceID, NextHostPortID, TrnDT) values('6', 'LO4-04','6', 'LO4-01', ' ');