

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for ProjectCauses
-- ----------------------------
DROP TABLE IF EXISTS ProjectCauses;
CREATE TABLE ProjectCauses (
"Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Name"  TEXT NOT NULL,
"OrderNo"  INTEGER DEFAULT 0
);

-- ----------------------------
-- Records of ProjectCauses
-- ----------------------------
INSERT INTO ProjectCauses VALUES (1, '协议起草', 1);
INSERT INTO ProjectCauses VALUES (2, '协商', 2);
INSERT INTO ProjectCauses VALUES (3, '商铺买卖风险防范', 3);
INSERT INTO ProjectCauses VALUES (4, '诉前联调（民间借贷）', 4);
INSERT INTO ProjectCauses VALUES (5, '追缴社保', 5);
INSERT INTO ProjectCauses VALUES (6, '离婚纠纷', 6);
INSERT INTO ProjectCauses VALUES (7, '机动车交通事故责任纠纷', 7);
INSERT INTO ProjectCauses VALUES (8, '非因工死亡待遇纠纷', 8);
INSERT INTO ProjectCauses VALUES (9, '劳动争议纠纷', 9);
INSERT INTO ProjectCauses VALUES (10, '意外伤害保险合同纠纷', 10);
INSERT INTO ProjectCauses VALUES (11, '委托代理合同纠纷', 11);
