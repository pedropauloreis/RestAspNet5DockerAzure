CREATE TABLE `usersroles` (
  `id` BIGINT NOT NULL AUTO_INCREMENT,
  `rolesid` BIGINT NOT NULL,
  `usersid` BIGINT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fkroles_idx` (`rolesid` ASC),
  INDEX `fkusers_idx` (`usersid` ASC),
  INDEX `usersroles` (`rolesid` ASC, `usersid` ASC),
  CONSTRAINT `fkroles`
    FOREIGN KEY (`rolesid`)
    REFERENCES `roles` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fkusers`
    FOREIGN KEY (`usersid`)
    REFERENCES `users` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);