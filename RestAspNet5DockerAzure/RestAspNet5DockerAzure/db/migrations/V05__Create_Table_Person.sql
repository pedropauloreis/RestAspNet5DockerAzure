CREATE TABLE `persons` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `first_name` varchar(80) NOT NULL,
  `last_name` varchar(80) NOT NULL,
  `address` varchar(100),
  `gender` varchar(1),
  `departmentid` bigint,
  PRIMARY KEY (`id`),
  INDEX `departmentFK_idx` (`departmentid` ASC),
  CONSTRAINT `departmentFK`
  FOREIGN KEY (`departmentid`)
  REFERENCES `departments` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION
);


