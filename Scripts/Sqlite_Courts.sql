
PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for Courts
-- ----------------------------
DROP TABLE IF EXISTS "Courts";
CREATE TABLE Courts (Id  integer primary key autoincrement, Name TEXT, Rank TEXT, Province TEXT, City TEXT, County TEXT, Address TEXT, ContactNo TEXT);

-- ----------------------------
-- Records of Courts
-- ----------------------------
INSERT INTO "Courts" VALUES (1, '中华人民共和国最高人民法院', 'Supreme', '北京市', '市辖区', '东城区', '北京市东城区东交民巷27号', '010-85120527');
INSERT INTO "Courts" VALUES (2, '广东省高级人民法院', 'Superior', '广东省', '广州市', '天河区', '广州市天河区员村一横路9号', '12368');
INSERT INTO "Courts" VALUES (3, '广州市中级人民法院', 'Intermediate', '广东省', '广州市', '越秀区', '广州市越秀区仓边路28号', '020-83210000 、12368');
INSERT INTO "Courts" VALUES (4, '广州互联网法院', 'Grassroots', '广东省', '广州市', '海珠区', '广州海珠区新港东路148号环球贸易中心48—53层', '020-31950083、020-31950526');
INSERT INTO "Courts" VALUES (5, '广州市越秀区人民法院', 'Grassroots', '广东省', '广州市', '越秀区', '广州市越秀区寺右新马路寺右北一街三巷一号', '020-87365002');
INSERT INTO "Courts" VALUES (6, '广州市海珠区人民法院', 'Grassroots', '广东省', '广州市', '海珠区', '广州市海珠区逸景路333号', '020-84430623');
INSERT INTO "Courts" VALUES (7, '广州市荔湾区人民法院', 'Grassroots', '广东省', '广州市', '荔湾区', '广州市荔湾区龙溪大道273号', '020-83005930');
INSERT INTO "Courts" VALUES (8, '广州市天河区人民法院', 'Grassroots', '广东省', '广州市', '天河区', '广州市天河区明镜路1号', '020-83008667');
INSERT INTO "Courts" VALUES (9, '广州市白云区人民法院', 'Grassroots', '广东省', '广州市', '白云区', '广州市白云区机场路1668号', '020-83008000');
INSERT INTO "Courts" VALUES (10, '广州市黄埔区人民法院', 'Grassroots', '广东省', '广州市', '黄埔区', '广州市黄埔区大沙地东路363号', '020-83009953');
INSERT INTO "Courts" VALUES (11, '广州市花都区人民法院', 'Grassroots', '广东省', '广州市', '花都区', '广州市花都区新华路26号', '020-86816535');
INSERT INTO "Courts" VALUES (12, '广州市番禺区人民法院', 'Grassroots', '广东省', '广州市', '番禺区', '广州市番禺区桥兴大道731－733号', '020-39122773');
INSERT INTO "Courts" VALUES (13, '广州市南沙区人民法院', 'Grassroots', '广东省', '广州市', '南沙区', '广州市南沙区港前大道北99号', '020-83006950');
INSERT INTO "Courts" VALUES (14, '广州市从化区人民法院', 'Grassroots', '广东省', '广州市', '从化区', '广州市从化区城郊街河滨北路628号', '020-87921121');
INSERT INTO "Courts" VALUES (15, '广州市增城区人民法院', 'Grassroots', '广东省', '广州市', '增城区', '广东省增城区荔城街健生西路５号', '020-826620772');
