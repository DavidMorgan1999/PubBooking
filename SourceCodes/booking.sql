CREATE DATABASE IF NOT EXISTS booking;

USE booking;

CREATE TABLE IF NOT EXISTS users 
(
	id 			int 		 NOT NULL AUTO_INCREMENT,
	username		varchar(100) 	 NOT NULL,
	password		varchar(100)	 NOT NULL,
	title			varchar(10)	 NOT NULL,
	fname			varchar(100)	 NOT NULL,
	sname			varchar(100)	 NOT NULL,
	email			varchar(255)	 NOT NULL,
	phoneno			varchar(20)	 NOT NULL,
	PRIMARY KEY (id)
 ); 


CREATE TABLE IF NOT EXISTS pubs 
(
	id 			int 		 NOT NULL AUTO_INCREMENT,
	username		varchar(100) 	 NOT NULL,
	password		varchar(100)	 NOT NULL,
	name			varchar(100)	 NOT NULL,
	street			varchar(100)	 NOT NULL,
	town			varchar(100)	 NOT NULL,
	postcode		varchar(10)	 NOT NULL,
	email			varchar(255)	 NOT NULL,
	phoneno			varchar(20)	 NOT NULL,
	PRIMARY KEY (id)
 ); 

CREATE TABLE IF NOT EXISTS seating 
(
	id 			int 		 NOT NULL AUTO_INCREMENT,
	pubId			int 		 NOT NULL,
	maxNo			int		 NOT NULL,
	tablename			varchar(100)	 NOT NULL,
	description			varchar(255)	 NOT NULL,
	PRIMARY KEY (id),
    	FOREIGN KEY (pubId) REFERENCES pubs(id)
 ); 

CREATE TABLE IF NOT EXISTS seatbookings 
(
	id 			int 		 NOT NULL AUTO_INCREMENT,
	userId			int 	 NOT NULL,
	tableId			int	 NOT NULL,
	bookingtime			varchar(30)	 NOT NULL,
	bookingdate			varchar(20)	 NOT NULL,
	PRIMARY KEY (id),
     	FOREIGN KEY (userId) REFERENCES users(id),
    	FOREIGN KEY (tableId) REFERENCES seating(id)
 ); 

CREATE TABLE IF NOT EXISTS day
(
	id 			int 		 NOT NULL AUTO_INCREMENT,
	pubId			int 	 NOT NULL,
	open			varchar(20)	 NOT NULL,
	close			varchar(20)	 NOT NULL,
	day			varchar(10)	 NOT NULL,
	PRIMARY KEY (id),
    	FOREIGN KEY (pubId) REFERENCES pubs(id)
 ); 

INSERT IGNORE INTO users(username,password,title,fname,sname,email,phoneno) VALUES ("Test","PASSWORD","Mr","Test","Testerson","test@gmail.com","07732232182");

INSERT IGNORE INTO pubs(username,password,name,street,town,postcode,email,phoneno) VALUES ("Happylanding","1234","The Happy Landing","4 Main Street","Eglinton","BT47 3PQ","happylanding@hotmail.com","02871811523");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (1,"10:30","22:00","Monday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (1,"10:45","22:15","Tuesday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (1,"10:30","22:00","Wednesday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (1,"11:00","22:30","Thursday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (1,"10:30","22:00","Friday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (1,"10:30","22:00","Saturday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (1,"17:00","22:00","Sunday");

INSERT IGNORE INTO pubs(username,password,name,street,town,postcode,email,phoneno) VALUES ("bbr","pass","Badgers Bar and Restaurant", "18 Orchard St", "Londonderry", "BT48 6EG" ,"badgers@hotmail.com","02871363306");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (2,"10:30","22:00","Monday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (2,"10:00","22:00","Tuesday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (2,"10:30","22:30","Wednesday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (2,"10:30","22:00","Thursday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (2,"10:30","22:00","Friday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (2,"10:30","22:00","Saturday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (2,"18:30","20:00","Sunday");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (1, 5, "1", "First on left");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (1, 4, "2", "First on right");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (1, 1, "3", "Seat at bar");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (1, 1, "4", "Seat at bar");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (1, 1, "5", "Seat at bar");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (1, 10, "6", "Booth at back");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (2, 6, "1", "Circular table in centre");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (2, 4, "2", "First on right");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (2, 3, "3", "Second on right");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (2, 10, "4", "Long table on left");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (2, 1, "5", "Seat at bar");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (2, 10, "6", "Circular table in side room");

INSERT IGNORE INTO pubs(username,password,name,street,town,postcode,email,phoneno) VALUES ("gainsbar","pass","Gainsborough Bar", "6 Shipquay Pl", "Londonderry", "BT48 6DF" ,"gainsbar@hotmail.com","02871453283");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (3,"10:30","22:00","Monday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (3,"10:00","22:00","Tuesday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (3,"10:30","22:30","Wednesday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (3,"10:30","22:00","Thursday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (3,"10:30","22:00","Friday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (3,"10:30","22:00","Saturday");

INSERT IGNORE INTO day(pubId, open, close, day) VALUES (3,"10:30","20:00","Sunday");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (3, 2, "1", "First on left");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (3, 4, "2", "First on right");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (3, 4, "3", "In side room");

INSERT IGNORE INTO seating(pubId, maxNo, tablename, description) VALUES (3, 8, "4", "Booth");

INSERT IGNORE INTO seatbookings(userId, tableId, bookingtime, bookingdate) VALUES (1,1,"09:30","11/12/2020");