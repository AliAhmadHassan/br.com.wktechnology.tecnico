CREATE TABLE `category`(
	`id` bigint AUTO_INCREMENT NOT NULL,
	`name` varchar(50) NOT NULL,
	`description` varchar(50),
	primary key ( `id` ASC )
    );

CREATE TABLE `product`(
	`id` bigint AUTO_INCREMENT NOT NULL,
	`name` varchar(50) NOT NULL,
	`description` varchar(50),
	`unit_price` decimal(12, 4) NOT NULL,
	`amount` decimal(12, 4) NOT NULL,
    `idcategory` bigint,
	primary key ( `id` ASC )
);

ALTER TABLE `product` ADD CONSTRAINT `fk_product_category_idcategory` FOREIGN KEY(`idcategory`) REFERENCES `category` (`id`);

LOCK TABLES `category` WRITE;
INSERT INTO `category` VALUES (1,'teste','teste description'),(4,'teste 2','Descrição do teste novo teste');
UNLOCK TABLES;

LOCK TABLES `product` WRITE;
INSERT INTO `product` VALUES (1,'Prod1','Descricao Novo Teste',201.0000,5.0000,1),(3,'prod2','',100.0000,2.0000,4),(4,'prod2',NULL,100.0000,2.0000,4),(6,'prod5','string',100.0000,5.0000,1),(7,'prod5',NULL,100.0000,5.0000,1),(8,'prod5',NULL,100.0000,5.0000,1),(9,'prod5',NULL,100.0000,5.0000,1),(10,'teste 10',NULL,100.0000,5.0000,1),(11,'Teste 11',NULL,50.0000,2.0000,4);
UNLOCK TABLES;

DELIMITER $$
CREATE PROCEDURE `spi_category` (name_in  varchar(50), description_in varchar(50), out id_out bigint)
BEGIN
	INSERT INTO `category`(`name`, `description`)
    values (name_in, description_in);
    
    SET id_out = LAST_INSERT_ID();
END$$

DELIMITER ;


DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `spu_category`(id_in bigint, name_in varchar(50), description_in varchar(50))
BEGIN
	UPDATE `category`
    SET  `name` = name_in
		,`description` = description_in
	WHERE `id` = id_in;
END$$




CREATE DEFINER=`root`@`localhost` PROCEDURE `spd_category`(id_in bigint)
BEGIN
	DELETE FROM `category`
	WHERE `id` = id_in;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sps_category_by_id`(id_in bigint)
BEGIN
	SELECT * 
    FROM `category`
	WHERE `id` = id_in;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sps_category`()
BEGIN
	SELECT * 
    FROM `category`;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spi_product`(name_in varchar(50), description_in varchar(50), unit_price_in decimal(12, 4), amount_in decimal(12, 4), idcategory_in bigint, out id_out bigint)
BEGIN
	INSERT INTO `product` (`name`, `description`, `unit_price`, `amount`, `idcategory`)
	values (name_in, description_in, unit_price_in, amount_in, idcategory_in);
    
    SET id_out = last_insert_id();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spu_product`(id_in bigint, name_in varchar(50), description_in varchar(50), unit_price_in decimal(12, 4), amount_in decimal(12, 4), idcategory_in bigint)
BEGIN
	UPDATE `product` 
    SET  `name` = name_in
		,`description` = description_in
        ,`unit_price` = unit_price_in
        ,`amount` = amount_in
        ,`idcategory` = idcategory_in
	WHERE `id` = id_in;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `spd_product`(id_in bigint)
BEGIN
	DELETE FROM `product` 
    WHERE `id` = id_in;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sps_product_by_id`(id_in bigint)
BEGIN
	SELECT `product`.*, '' product_split
		,`category`.*, '' category_split		
    FROM `product`
		INNER JOIN `category` on `product`.`idcategory` = `category`.`id`
    WHERE `product`.`id` = id_in;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sps_product`()
BEGIN
	SELECT `product`.*, '' product_split
		,`category`.*, '' category_split		
    FROM `product`
		INNER JOIN `category` on `product`.`idcategory` = `category`.`id`;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sps_product_by_idcategory`(idcategory_in bigint)
BEGIN
	SELECT `product`.*, '' product_split
		,`category`.*, '' category_split		
    FROM `product`
		INNER JOIN `category` on `product`.`idcategory` = `category`.`id`
    WHERE `product`.`idcategory` = idcategory_in;
END$$

DELIMITER ;