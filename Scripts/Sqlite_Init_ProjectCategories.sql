PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for ProjectCategories
-- ----------------------------
DROP TABLE IF EXISTS ProjectCategories;
CREATE TABLE ProjectCategories (Code TEXT not null, Name TEXT, OrderNo INT, ParentId TEXT, primary key (Code), constraint PK_ProjectCategories_ParentId foreign key (ParentId) references ProjectCategories);

-- ----------------------------
-- Records of ProjectCategories
-- ----------------------------
INSERT INTO ProjectCategories(Code,Name,OrderNo,ParentId) VALUES ('CivilSuit','民事诉讼',1, NULL);
INSERT INTO ProjectCategories(Code,Name,OrderNo,ParentId) VALUES ('CriminalSuit','刑事诉讼',2, NULL);
INSERT INTO ProjectCategories(Code,Name,OrderNo,ParentId) VALUES ('AdministrativeSuit','行政诉讼',3, NULL);
INSERT INTO ProjectCategories(Code,Name,OrderNo,ParentId) VALUES ('ArbitrateCase','仲裁案件',4, NULL);