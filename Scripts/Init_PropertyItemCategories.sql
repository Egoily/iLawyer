INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (1, 'Phone','电话','Phone',1,1, NULL);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (2, 'Email','邮箱','Email',1,1, NULL);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (3, 'Address','地址','AddressMarker',1,1, NULL);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (4, 'Certificate','证件','Certificate',1,1, NULL);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (5, 'Person','重要人物','FaceProfile',1,1, NULL);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (6, 'DateTime','重要日期','Timetable',2,1, NULL);

INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (10, 'MainPhone','主要','Phone',1,1, 1);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (11, 'Mobile','个人手机','Phone',1,1, 1);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (12, 'WorkMobile','工作手机','Phone',1,1, 1);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (13, 'HomePhone','家庭电话','Phone',1,1, 1);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (14, 'OfficePhone','办公电话','Phone',1,1, 1);

INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (20, 'MainEmail','主要','Email',1,1, 2);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (21, 'PersonalEmail','个人邮箱','Email',1,1,2);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (22, 'RegisterEmail','注册邮箱','Email',1,1,2);

INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (30, 'MainAddress','主要','AddressMarker',1,1,3);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (31, 'HomeAddress','家庭地址','AddressMarker',1,1,3);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (32, 'OfficeAddress','办公地址','AddressMarker',1,1,3);

INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (41, 'IDCard','身份证','Certificate',1,1,4);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (42, 'Passport','护照','Certificate',1,1,4);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (43, 'OfficialCard','军官证','Certificate',1,1,4);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (44, 'BusinessLicence','营业执照','Certificate',1,1,4);

INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (51, 'LegalPerson','法人','FaceProfile',1,1, 5);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (52, 'HeadPerson','负责人','FaceProfile',1,1, 5);

INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (61, 'RegisterDate','注册日期','Timetable',2,1, 6);
INSERT INTO PropertyItemCategories(Id,Code,Name,Icon,PickerType,IsEnabled,ParentId) VALUES (62, 'HeadPerson','负责人','Timetable',2,1, 6);
