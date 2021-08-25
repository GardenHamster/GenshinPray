/*
Navicat MySQL Data Transfer

Source Server         : 127.0.0.1
Source Server Version : 50728
Source Host           : 127.0.0.1:3306
Source Database       : genshinpray

Target Server Type    : MYSQL
Target Server Version : 50728
File Encoding         : 65001

Date: 2021-08-26 00:16:25
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for authorize
-- ----------------------------
DROP TABLE IF EXISTS `authorize`;
CREATE TABLE `authorize` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(16) NOT NULL COMMENT '授权码',
  `dailyCall` int(11) NOT NULL DEFAULT '300' COMMENT '每日可调用次数',
  `createDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '添加时间',
  `expireDate` datetime NOT NULL COMMENT '过期时间',
  `isDisable` tinyint(1) NOT NULL DEFAULT '0' COMMENT '是否被禁用',
  PRIMARY KEY (`id`),
  UNIQUE KEY `unique_autCode` (`code`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of authorize
-- ----------------------------
INSERT INTO `authorize` VALUES ('1', '123', '300', '2021-08-22 15:40:35', '2021-08-22 15:40:32', '0');

-- ----------------------------
-- Table structure for goods
-- ----------------------------
DROP TABLE IF EXISTS `goods`;
CREATE TABLE `goods` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `goodsName` varchar(255) NOT NULL COMMENT '物品名称',
  `goodsType` int(11) NOT NULL COMMENT '物品类型',
  `rareType` int(11) NOT NULL COMMENT '稀有类型',
  `isPerm` tinyint(1) NOT NULL COMMENT '是否常驻',
  `createDate` datetime NOT NULL COMMENT '添加日期',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=91 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of goods
-- ----------------------------
INSERT INTO `goods` VALUES ('1', '弹弓', '12', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('2', '神射手之誓', '12', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('3', '鸦羽弓', '12', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('4', '翡玉法球', '11', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('5', '讨龙英杰谭', '11', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('6', '魔导绪论', '11', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('7', '黑缨枪', '10', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('8', '以理服人', '9', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('9', '沐浴龙血的剑', '9', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('10', '铁影阔剑', '9', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('11', '飞天御剑', '8', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('12', '黎明神剑', '8', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('13', '冷刃', '8', '3', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('14', '弓藏', '12', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('15', '祭礼弓', '12', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('16', '绝弦', '12', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('17', '西风猎弓', '12', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('18', '昭心', '11', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('19', '祭礼残章', '11', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('20', '流浪乐章', '11', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('21', '西风秘典', '11', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('22', '西风长枪', '10', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('23', '匣里灭辰', '10', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('24', '雨裁', '9', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('25', '祭礼大剑', '9', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('26', '钟剑', '9', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('27', '西风大剑', '9', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('28', '匣里龙吟', '8', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('29', '祭礼剑', '8', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('30', '笛剑', '8', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('31', '西风剑', '8', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('32', '砂糖', '3', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('33', '菲谢尔', '4', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('34', '芭芭拉', '2', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('35', '烟绯', '1', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('36', '罗莎莉亚', '6', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('37', '辛焱', '1', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('38', '迪奥娜', '6', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('39', '重云', '6', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('40', '诺艾尔', '7', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('41', '班尼特', '1', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('42', '凝光', '7', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('43', '行秋', '2', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('44', '北斗', '4', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('45', '香菱', '1', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('46', '雷泽', '4', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('47', '早柚', '3', '4', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('48', '刻晴', '4', '5', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('49', '莫娜', '2', '5', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('50', '七七', '6', '5', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('51', '迪卢克', '1', '5', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('52', '琴', '3', '5', '1', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('53', '宵宫', '1', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('54', '神里绫华', '6', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('55', '枫原万叶', '3', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('56', '达达利亚', '2', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('57', '温迪', '3', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('58', '胡桃', '1', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('59', '可莉', '1', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('60', '优菈', '6', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('61', '魈', '3', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('62', '钟离', '7', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('63', '阿贝多', '7', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('64', '甘雨', '6', '5', '0', '2021-08-25 23:00:59');
INSERT INTO `goods` VALUES ('70', '飞雷之弦振', '12', '5', '0', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('71', '雾切之回光', '8', '5', '0', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('72', '天空之刃', '8', '5', '1', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('73', '狼的末路', '9', '5', '1', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('74', '阿莫斯之弓', '12', '5', '1', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('75', '天空之卷', '11', '5', '1', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('76', '天空之傲', '9', '5', '1', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('77', '风鹰剑', '8', '5', '1', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('78', '和璞鸢', '10', '5', '1', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('79', '四风原典', '11', '5', '1', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('80', '天空之翼', '12', '5', '1', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('81', '天空之脊', '10', '5', '1', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('82', '尘世之锁', '11', '5', '0', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('83', '无工之剑', '9', '5', '0', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('84', '贯虹之槊', '10', '5', '0', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('85', '斫峰之刃', '8', '5', '0', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('86', '磐岩结绿', '8', '5', '0', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('87', '护摩之杖', '10', '5', '0', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('88', '终末嗟叹之诗', '12', '5', '0', '2021-08-25 23:36:38');
INSERT INTO `goods` VALUES ('89', '松籁响起之时', '9', '5', '0', '2021-08-25 23:36:38');

-- ----------------------------
-- Table structure for member
-- ----------------------------
DROP TABLE IF EXISTS `member`;
CREATE TABLE `member` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `authId` int(11) NOT NULL COMMENT '授权ID',
  `memberCode` varchar(32) NOT NULL COMMENT '成员编号',
  `role180Surplus` int(11) NOT NULL DEFAULT '180' COMMENT '角色池剩余多少发五星大保底',
  `role90Surplus` int(11) NOT NULL DEFAULT '90' COMMENT '角色池剩余多少发五星保底',
  `role20Surplus` int(11) NOT NULL DEFAULT '20' COMMENT '角色池剩余多少发十连大保底',
  `role10Surplus` int(11) NOT NULL DEFAULT '10' COMMENT '角色池剩余多少发十连保底',
  `arm180Surplus` int(11) NOT NULL DEFAULT '180' COMMENT '武器池剩余多少发五星大保底',
  `arm90Surplus` int(11) NOT NULL DEFAULT '90' COMMENT '武器池剩余多少发五星保底',
  `arm20Surplus` int(11) NOT NULL DEFAULT '20' COMMENT '武器池剩余多少发十连大保底',
  `arm10Surplus` int(11) NOT NULL DEFAULT '10' COMMENT '武器池剩余多少发十连保底',
  `perm180Surplus` int(11) NOT NULL DEFAULT '180' COMMENT '常驻池剩余多少发五星大保底',
  `perm90Surplus` int(11) NOT NULL DEFAULT '90' COMMENT '常驻池剩余多少发五星保底',
  `perm20Surplus` int(11) NOT NULL DEFAULT '20' COMMENT '常驻池剩余多少发十连大保底',
  `perm10Surplus` int(11) NOT NULL DEFAULT '10' COMMENT '常驻池剩余多少发十连保底',
  `rolePrayTimes` int(11) NOT NULL COMMENT '角色池祈愿次数',
  `armPrayTimes` int(11) NOT NULL COMMENT '武器池祈愿次数',
  `permPrayTimes` int(11) NOT NULL COMMENT '常驻池祈愿次数',
  `totalPrayTimes` int(11) NOT NULL COMMENT '总祈愿次数',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of member
-- ----------------------------

-- ----------------------------
-- Table structure for member_goods
-- ----------------------------
DROP TABLE IF EXISTS `member_goods`;
CREATE TABLE `member_goods` (
  `id` int(11) NOT NULL,
  `authId` int(11) NOT NULL COMMENT '授权ID',
  `membeCode` varchar(32) NOT NULL COMMENT '成员编号',
  `goodsName` varchar(255) NOT NULL COMMENT '物品名称',
  `goodsType` int(11) NOT NULL COMMENT '物品类型',
  `rareType` int(11) NOT NULL COMMENT '稀有类型',
  `createDate` datetime NOT NULL COMMENT '获得时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of member_goods
-- ----------------------------

-- ----------------------------
-- Table structure for pond_goods
-- ----------------------------
DROP TABLE IF EXISTS `pond_goods`;
CREATE TABLE `pond_goods` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `pondType` int(11) NOT NULL COMMENT '蛋池类型',
  `goodsId` int(11) NOT NULL COMMENT '物品id',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pond_goods
-- ----------------------------
INSERT INTO `pond_goods` VALUES ('1', '1', '53');
INSERT INTO `pond_goods` VALUES ('2', '1', '38');
INSERT INTO `pond_goods` VALUES ('3', '1', '47');
INSERT INTO `pond_goods` VALUES ('4', '1', '37');
INSERT INTO `pond_goods` VALUES ('5', '2', '70');
INSERT INTO `pond_goods` VALUES ('6', '2', '72');
INSERT INTO `pond_goods` VALUES ('7', '2', '29');
INSERT INTO `pond_goods` VALUES ('8', '2', '24');
INSERT INTO `pond_goods` VALUES ('9', '2', '23');
INSERT INTO `pond_goods` VALUES ('10', '2', '19');
INSERT INTO `pond_goods` VALUES ('11', '2', '17');
