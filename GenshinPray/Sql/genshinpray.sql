/*
Navicat MySQL Data Transfer

Source Server         : 127.0.0.1
Source Server Version : 50728
Source Host           : 127.0.0.1:3306
Source Database       : genshinpray

Target Server Type    : MYSQL
Target Server Version : 50728
File Encoding         : 65001

Date: 2021-09-05 15:55:58
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for authorize
-- ----------------------------
DROP TABLE IF EXISTS `authorize`;
CREATE TABLE `authorize` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键,自增',
  `Code` varchar(16) NOT NULL COMMENT '授权码',
  `DailyCall` int(11) NOT NULL COMMENT '每日可调用次数',
  `CreateDate` datetime NOT NULL COMMENT '添加时间',
  `ExpireDate` datetime NOT NULL COMMENT '过期时间',
  `IsDisable` tinyint(4) NOT NULL COMMENT '是否被禁用',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `unique_autCode` (`Code`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for goods
-- ----------------------------
DROP TABLE IF EXISTS `goods`;
CREATE TABLE `goods` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键,自增',
  `GoodsName` varchar(50) NOT NULL COMMENT '物品名称',
  `GoodsType` int(11) NOT NULL COMMENT '物品类型',
  `GoodsSubType` int(11) NOT NULL COMMENT '物品子类型',
  `RareType` int(11) NOT NULL COMMENT '稀有类型',
  `IsPerm` tinyint(4) NOT NULL COMMENT '是否常驻',
  `CreateDate` datetime NOT NULL COMMENT '添加日期',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=90 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for member
-- ----------------------------
DROP TABLE IF EXISTS `member`;
CREATE TABLE `member` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键,自增',
  `AuthId` int(11) NOT NULL COMMENT '授权码ID',
  `MemberCode` varchar(32) NOT NULL COMMENT '成员编号',
  `Role180Surplus` int(11) NOT NULL COMMENT '角色池剩余多少发五星大保底',
  `Role90Surplus` int(11) NOT NULL COMMENT '角色池剩余多少发五星保底',
  `Role20Surplus` int(11) NOT NULL COMMENT '角色池剩余多少发十连大保底',
  `Role10Surplus` int(11) NOT NULL COMMENT '角色池剩余多少发十连保底',
  `Arm180Surplus` int(11) NOT NULL COMMENT '武器池剩余多少发五星大保底',
  `Arm90Surplus` int(11) NOT NULL COMMENT '武器池剩余多少发五星保底',
  `Arm20Surplus` int(11) NOT NULL COMMENT '武器池剩余多少发十连大保底',
  `Arm10Surplus` int(11) NOT NULL COMMENT '武器池剩余多少发十连保底',
  `Perm180Surplus` int(11) NOT NULL COMMENT '常驻池剩余多少发五星大保底',
  `Perm90Surplus` int(11) NOT NULL COMMENT '常驻池剩余多少发五星保底',
  `Perm20Surplus` int(11) NOT NULL COMMENT '常驻池剩余多少发十连大保底',
  `Perm10Surplus` int(11) NOT NULL COMMENT '常驻池剩余多少发十连保底',
  `RolePrayTimes` int(11) NOT NULL COMMENT '角色池祈愿次数',
  `ArmPrayTimes` int(11) NOT NULL COMMENT '武器池祈愿次数',
  `PermPrayTimes` int(11) NOT NULL COMMENT '常驻池祈愿次数',
  `TotalPrayTimes` int(11) NOT NULL COMMENT '总祈愿次数',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for member_goods
-- ----------------------------
DROP TABLE IF EXISTS `member_goods`;
CREATE TABLE `member_goods` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键,自增',
  `AuthId` int(11) NOT NULL COMMENT '授权码ID',
  `GoodsName` varchar(50) NOT NULL COMMENT '物品名称',
  `GoodsType` int(11) NOT NULL COMMENT '物品类型',
  `RareType` int(11) NOT NULL COMMENT '稀有类型',
  `CreateDate` datetime NOT NULL COMMENT '添加日期',
  `MemberCode` varchar(32) NOT NULL COMMENT '成员编号',
  `GoodsSubType` int(11) NOT NULL COMMENT '物品子类型',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for pond_goods
-- ----------------------------
DROP TABLE IF EXISTS `pond_goods`;
CREATE TABLE `pond_goods` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键,自增',
  `AuthId` int(11) NOT NULL COMMENT '授权码ID',
  `PondType` int(11) NOT NULL COMMENT '蛋池类型',
  `GoodsId` int(11) NOT NULL COMMENT '物品ID',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for pray_record
-- ----------------------------
DROP TABLE IF EXISTS `pray_record`;
CREATE TABLE `pray_record` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键,自增',
  `AuthId` int(11) NOT NULL COMMENT '授权码ID',
  `MemberCode` varchar(32) NOT NULL COMMENT '成员编号',
  `PrayCount` int(11) NOT NULL COMMENT '祈愿次数',
  `CreateDate` datetime NOT NULL COMMENT '添加日期',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
