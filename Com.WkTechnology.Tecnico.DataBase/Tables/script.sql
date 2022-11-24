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





