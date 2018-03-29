CREATE DATABASE `KvKickstart`;

CREATE TABLE `KvKickstart`.`presentors`
(
	`id` int(8) AUTO_INCREMENT NOT NULL,
    `name` varchar(128) NOT NULL,
    `company` varchar(128) NOT NULL,
    `email` varchar(128),
    `twitter` varchar(128),
    `linkedIn` varchar(128),

    PRIMARY KEY(id)
);

CREATE TABLE `KvKickstart`.`presentations`
(
	`id` int(8) AUTO_INCREMENT NOT NULL,
    `title` varchar(256) NOT NULL,
    `city` varchar(64) NOT NULL,
    `time` time NOT NULL,
    `presenterId` int(8) NOT NULL,
    `building` varchar(64) NOT NULL,
    `distance` varchar(32) NOT NULL,
	 `content` text,
    `impresentationsageUrl` varchar(32) NOT NULL,

    PRIMARY KEY(id),
    FOREIGN KEY (`presenterId`) REFERENCES KvKickstart.`presentors`(`id`)
);
