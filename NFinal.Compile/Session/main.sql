/*
Navicat SQLite Data Transfer

Source Server         : Session
Source Server Version : 30706
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30706
File Encoding         : 65001

Date: 2015-05-10 13:02:57
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for "main"."session_id"
-- ----------------------------
DROP TABLE "main"."session_id";
CREATE TABLE "session_id" (
"id"  BLOB(16) NOT NULL,
"time"  INTEGER NOT NULL
);

-- ----------------------------
-- Table structure for "main"."session_val"
-- ----------------------------
DROP TABLE "main"."session_val";
CREATE TABLE "session_val" (
"id"  BLOB(16) NOT NULL,
"key"  TEXT NOT NULL,
"val"  TEXT
);

-- ----------------------------
-- Indexes structure for table session_id
-- ----------------------------
CREATE UNIQUE INDEX "main"."session_id_index"
ON "session_id" ("id" COLLATE BINARY ASC);

-- ----------------------------
-- Indexes structure for table session_val
-- ----------------------------
CREATE INDEX "main"."session_val_index"
ON "session_val" ("id" COLLATE BINARY ASC);
