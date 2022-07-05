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
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('1', 'M1-01', 3, 1, 1, ' ', 2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('1', 'M1-06', 3, 2, 6, ' ', 1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('1', 'LeftFork', 4, 1, 1, ' ', 0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('1', 'Shelf', 0, 1, 1, ' ', 0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('1', 'Teach', 0, 2, 2, ' ', 0);


--STK 2 (PCBA)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('2', 'M1-11', 3, 1, 11, ' ', 2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('2', 'M1-16', 3, 2, 16, ' ', 1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('2', 'LeftFork', 4, 1, 1, ' ', 0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('2', 'Shelf', 0, 1, 1, ' ', 0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('2', 'Teach', 0, 2, 2, ' ', 0);


--STK 3 (箱式倉1)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('3', 'B1-001', 3, 1, 1, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('3', 'B1-004', 3, 2, 4, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('3', 'B1-007', 3, 3, 7, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('3', 'B1-010', 3, 4, 10, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('3', 'B1-081', 3, 5, 81, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('3', 'B1-084', 3, 6, 84, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('3', 'B1-087', 3, 7, 87, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('3', 'B1-090', 3, 8, 90, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('3', 'LeftFork', 4, 1, 1, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('3', 'Shelf', 0, 1, 1, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('3', 'Teach', 0, 2, 2, ' ',0);


--STK 4 (箱式倉2)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('4', 'B1-013', 3, 1, 13, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('4', 'B1-016', 3, 2, 16, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('4', 'B1-019', 3, 3, 19, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('4', 'B1-022', 3, 4, 22, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('4', 'B1-093', 3, 5, 93, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('4', 'B1-096', 3, 6, 96, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('4', 'B1-099', 3, 7, 99, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('4', 'B1-102', 3, 8, 102, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('4', 'LeftFork', 4, 1, 1, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('4', 'Shelf', 0, 1, 1, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('4', 'Teach', 0, 2, 2, ' ',0);


--STK 5 (箱式倉3)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('5', 'B1-025', 3, 1, 25, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('5', 'B1-028', 3, 2, 28, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('5', 'B1-031', 3, 3, 31, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('5', 'B1-034', 3, 4, 34, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('5', 'B1-105', 3, 5, 105, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('5', 'B1-108', 3, 6, 108, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('5', 'B1-111', 3, 7, 111, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('5', 'B1-114', 3, 8, 114, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('5', 'LeftFork', 4, 1, 1, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('5', 'Shelf', 0, 1, 1, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('5', 'Teach', 0, 2, 2, ' ',0);


--AGV (3F)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('63', 'A1-04', 3, 1, 4, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('63', 'A1-08', 3, 2, 8, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('63', 'A1-12', 3, 3, 12, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('63', 'LO4-04', 3, 4, 4, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('63', 'A1-01', 3, 5, 1, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('63', 'A1-05', 3, 6, 5, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('63', 'A1-09', 3, 7, 9, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('63', 'LO4-01', 3, 8, 1, ' ',2);


--AGV (5F)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'A2-04', 3, 1, 4, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'A2-08', 3, 2, 8, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'A2-12', 3, 3, 12, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'A2-16', 3, 4, 16, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'A2-20', 3, 5, 20, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'LO5-04', 3, 6, 4, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'A2-01', 3, 7, 1, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'A2-05', 3, 8, 5, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'A2-09', 3, 9, 9, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'A2-13', 3, 10, 13, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'A2-17', 3, 11, 17, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('65', 'LO5-01', 3, 12, 1, ' ',2);


--AGV (6F)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'A3-04', 3, 1, 4, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'A3-08', 3, 2, 8, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'A3-12', 3, 3, 12, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'A3-16', 3, 4, 16, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'A3-20', 3, 5, 20, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'LO6-04', 3, 6, 4, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'A3-01', 3, 7, 1, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'A3-05', 3, 8, 5, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'A3-09', 3, 9, 9, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'A3-13', 3, 10, 13, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'A3-17', 3, 11, 17, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('66', 'LO6-01', 3, 12, 1, ' ',2);


--AGV (8F)
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'M1-05', 3, 1, 5, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'M1-15', 3, 2, 15, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'A4-04', 3, 3, 4, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'A4-08', 3, 4, 8, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'A4-12', 3, 5, 12, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'A4-16', 3, 6, 16, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'A4-20', 3, 7, 20, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'E1-08', 3, 8, 8, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'E2-35', 3, 9, 35, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'E2-36', 3, 10, 36, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'E2-37', 3, 11, 37, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'E2-38', 3, 12, 38, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'E2-39', 3, 13, 39, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'E2-44', 3, 14, 44, ' ',0);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'B1-070', 3, 15, 70, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'B1-074', 3, 16, 74, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'B1-078', 3, 17, 78, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'LO2-04', 3, 18, 4, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'LO3-01', 3, 19, 1, ' ',1);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'M1-10', 3, 20, 10, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'M1-20', 3, 21, 20, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'A4-01', 3, 22, 1, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'A4-05', 3, 23, 5, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'A4-09', 3, 24, 9, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'A4-13', 3, 25, 13, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'A4-17', 3, 26, 17, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'E1-01', 3, 27, 1, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'B1-071', 3, 28, 71, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'B1-075', 3, 29, 75, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'B1-079', 3, 30, 79, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'LO2-01', 3, 31, 1, ' ',2);
insert into PortDef (DeviceID, HostPortID, PortType, PortTypeIndex, PLCPortID, TrnDT, Direction) values('68', 'LO3-04', 3, 32, 4, ' ',2);
