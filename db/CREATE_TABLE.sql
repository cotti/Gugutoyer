CREATE TABLE `palavras` (
  `palavra_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `palavra` varchar(100) NOT NULL,
  `usado` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`palavra_id`),
  UNIQUE KEY `palavra_id_UNIQUE` (`palavra_id`),
  KEY `usado_INDEX` (`usado`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=320140 DEFAULT CHARSET=utf8;

CREATE TABLE `filtro` (
  `filtro_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `palavra_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`filtro_id`),
  KEY `palavra_id_fk` (`palavra_id`),
  CONSTRAINT `palavra_id_fk` FOREIGN KEY (`palavra_id`) REFERENCES `palavras` (`palavra_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=301 DEFAULT CHARSET=utf8;
