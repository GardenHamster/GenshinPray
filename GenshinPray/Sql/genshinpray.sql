/*
Navicat MySQL Data Transfer

Source Server         : 127.0.0.1
Source Server Version : 50728
Source Host           : 127.0.0.1:3306
Source Database       : genshinpray

Target Server Type    : MYSQL
Target Server Version : 50728
File Encoding         : 65001

Date: 2021-08-24 01:01:42
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for authorize
-- ----------------------------
DROP TABLE IF EXISTS `authorize`;
CREATE TABLE `authorize` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(16) NOT NULL,
  `dailyCall` int(11) NOT NULL DEFAULT '300',
  `createDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `expireDate` datetime NOT NULL,
  `isDisable` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `unique_autCode` (`code`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of authorize
-- ----------------------------
INSERT INTO `authorize` VALUES ('1', '123', '301', '2021-08-22 15:40:35', '2021-08-22 15:40:32', '0');

-- ----------------------------
-- Table structure for goods
-- ----------------------------
DROP TABLE IF EXISTS `goods`;
CREATE TABLE `goods` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `goodsName` varchar(255) NOT NULL,
  `goodsType` int(11) NOT NULL,
  `rareType` int(11) NOT NULL,
  `createDate` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of goods
-- ----------------------------

-- ----------------------------
-- Table structure for member
-- ----------------------------
DROP TABLE IF EXISTS `member`;
CREATE TABLE `member` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `authId` int(11) NOT NULL COMMENT '群ID',
  `memberId` bigint(20) NOT NULL COMMENT '群员ID',
  `role180Surplus` int(11) NOT NULL DEFAULT '180' COMMENT '原神角色池剩余多少发五星大保底',
  `role90Surplus` int(11) NOT NULL DEFAULT '90' COMMENT '原神角色池剩余多少发五星保底',
  `role20Surplus` int(11) NOT NULL DEFAULT '20' COMMENT '原神角色池剩余多少发十连大保底',
  `role10Surplus` int(11) NOT NULL DEFAULT '10' COMMENT '原神角色池剩余多少发十连保底',
  `arm180Surplus` int(11) NOT NULL DEFAULT '180' COMMENT '原神角色池剩余多少发五星大保底',
  `arm90Surplus` int(11) NOT NULL DEFAULT '90' COMMENT '原神角色池剩余多少发五星保底',
  `arm20Surplus` int(11) NOT NULL DEFAULT '20' COMMENT '原神角色池剩余多少发十连大保底',
  `arm10Surplus` int(11) NOT NULL DEFAULT '10' COMMENT '原神角色池剩余多少发十连保底',
  `perm180Surplus` int(11) NOT NULL DEFAULT '180' COMMENT '原神角色池剩余多少发五星大保底',
  `perm90Surplus` int(11) NOT NULL DEFAULT '90' COMMENT '原神角色池剩余多少发五星保底',
  `perm20Surplus` int(11) NOT NULL DEFAULT '20' COMMENT '原神角色池剩余多少发十连大保底',
  `perm10Surplus` int(11) NOT NULL DEFAULT '10' COMMENT '原神角色池剩余多少发十连保底',
  `rolePrayTimes` int(11) NOT NULL,
  `armPrayTimes` int(11) NOT NULL,
  `permPrayTimes` int(11) NOT NULL,
  `totalPrayTimes` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of member
-- ----------------------------

-- ----------------------------
-- Table structure for member_goods
-- ----------------------------
DROP TABLE IF EXISTS `member_goods`;
CREATE TABLE `member_goods` (
  `id` int(11) NOT NULL,
  `memberId` int(11) NOT NULL,
  `goodsName` varchar(255) NOT NULL,
  `goodsType` int(11) NOT NULL,
  `rareType` int(11) NOT NULL,
  `createDate` datetime NOT NULL,
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
  `pondType` int(11) NOT NULL,
  `goodsId` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pond_goods
-- ----------------------------
