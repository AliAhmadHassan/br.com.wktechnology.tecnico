DELIMITER $$
USE `wktechnology`$$
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