CREATE TABLE user_login(
   user_id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
   user_name VARCHAR(50),
   user_password VARCHAR(50)
);

CREATE TABLE department(
   department_id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
   department_name VARCHAR(50)
);

CREATE TABLE site(
   site_id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
   site_name VARCHAR(50) NOT NULL,
   site_type VARCHAR(50) NOT NULL
);

CREATE TABLE employee(
   employee_id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
   firstname VARCHAR(50) NOT NULL,
   lastname VARCHAR(50) NOT NULL,
   landline_phone VARCHAR(10),
   mobile_phone VARCHAR(10),
   e_mail VARCHAR(50),
   site_id INT NOT NULL,
   department_id INT NOT NULL,
   FOREIGN KEY(site_id) REFERENCES site(site_id),
   FOREIGN KEY(department_id) REFERENCES department(department_id)
);
