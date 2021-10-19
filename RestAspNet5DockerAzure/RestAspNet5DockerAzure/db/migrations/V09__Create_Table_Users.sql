CREATE TABLE `users` (
	`id` BIGINT NOT NULL AUTO_INCREMENT,
	`user_name` VARCHAR(50) NOT NULL DEFAULT '0',
	`password` VARCHAR(130) NOT NULL DEFAULT '0',
	`full_name` VARCHAR(120) NOT NULL,
	`refresh_token` VARCHAR(500) NULL DEFAULT '0',
	`refresh_token_expiry_time` DATETIME NULL,
	`enabled` BIT(1) NOT NULL DEFAULT b'0',
	PRIMARY KEY (`id`),
	UNIQUE `user_name` (`user_name`)
);