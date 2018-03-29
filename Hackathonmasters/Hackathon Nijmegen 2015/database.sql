-- Create user
DROP USER heathpath;
CREATE USER 'heathpath'@'localhost' IDENTIFIED BY '';

-- Create database
DROP DATABASE IF EXISTS `Heathpath`;
CREATE DATABASE IF NOT EXISTS `Heathpath`;
USE `Heathpath`;

-- Permissions
GRANT INSERT ON `Heathpath`.* TO 'Heathpath'@'localhost';
GRANT SELECT ON `Heathpath`.* TO 'Heathpath'@'localhost';
GRANT DELETE ON `Heathpath`.* TO 'Heathpath'@'localhost';
GRANT UPDATE ON `Heathpath`.* TO 'Heathpath'@'localhost';

CREATE TABLE `Heathpath`.`doctors` (
   id int(8) NOT NULL AUTO_INCREMENT,
   name varchar(64) NOT NULL,
   email varchar(64) NOT NULL,
   password char(40) NOT NULL,
   phone char(10) NOT NULL,

   PRIMARY KEY(id)
);

CREATE TABLE `Heathpath`.`patients` (
   id int(8) NOT NULL AUTO_INCREMENT,
   name varchar(64) NOT NULL,
   email varchar(64) NOT NULL,
   password char(40) NOT NULL,
   phone char(10) NOT NULL,
   birthday date NOT NULL,
   insurance varchar(64) NOT NULL,
   risk numeric(15,2) NOT NULL DEFAULT 0,
   zip char(6),
   house varchar(8),

   PRIMARY KEY(id)
);

CREATE TABLE `Heathpath`.`patientRelatives` (
   id int(8) NOT NULL AUTO_INCREMENT,
   patientId int(8) NOT NULL,
   name varchar(64) NOT NULL,
   email varchar(64) NOT NULL,
   phone char(10) DEFAULT NULL,

   PRIMARY KEY(id),
	FOREIGN KEY(`patientId`) REFERENCES `Heathpath`.`patients`(`id`)
);

CREATE TABLE `Heathpath`.`appointments` (
   id int(8) NOT NULL AUTO_INCREMENT,
   doctorId int(8) NOT NULL,
   patientId int(8) NOT NULL,
   subject varchar(64) NOT NULL,
   symptoms mediumtext NOT NULL,
   appointmentDate datetime NOT NULL,

   PRIMARY KEY(id),
	FOREIGN KEY(`doctorId`) REFERENCES `Heathpath`.`doctors`(`id`),
	FOREIGN KEY(`patientId`) REFERENCES `Heathpath`.`patients`(`id`)
);

CREATE TABLE `Heathpath`.`appointmentResults`
(
   id int(8) NOT NULL AUTO_INCREMENT,
   appointmentId int(8) NOT NULL,
   prescription mediumtext NOT NULL,
   nextAppointment date DEFAULT NULL,

   PRIMARY KEY(id),
	FOREIGN KEY(`appointmentId`) REFERENCES `Heathpath`.`appointments`(`id`)
);

CREATE TABLE `Heathpath`.`appointmentResultQuestions`
(
   id int(8) NOT NULL AUTO_INCREMENT,
   appointmentId int(8) NOT NULL,
   question mediumtext NOT NULL,
   answer mediumtext NOT NULL,

   PRIMARY KEY(id),
	FOREIGN KEY(`appointmentId`) REFERENCES `Heathpath`.`appointmentResults`(`id`)
);
