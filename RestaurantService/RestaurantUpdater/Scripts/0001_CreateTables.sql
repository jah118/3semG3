CREATE TABLE Restaurant_Tables (
	id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL
	,noOfSeats INT NOT NULL
	,table_number INT NOT NULL
	);

CREATE TABLE Food_Category (
	id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL
	,name NVARCHAR(32) NOT NULL
	);

CREATE TABLE Food (
	id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL
	,name NVARCHAR(32) NOT NULL
	,description NVARCHAR(64)
	,food_category_id INT NOT NULL
	,FOREIGN KEY (food_category_id) REFERENCES food_category(id) ON DELETE NO ACTION
);

CREATE TABLE Price (
	id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL
	,price_value DECIMAL(19, 4) NOT NULL
	,food_id INT NOT NULL
	,FOREIGN KEY (food_id) REFERENCES Food(id) ON DELETE CASCADE ON UPDATE CASCADE
	,SysStartTime DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL
	,SysEndTime DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL
	,PERIOD FOR SYSTEM_TIME(SysStartTime, SysEndTime)
	)
	WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.Price_History));

CREATE TABLE Zip_id (
	zip_code NVARCHAR(6) PRIMARY KEY
	,city NVARCHAR(50) NOT NULL
	);

CREATE TABLE Location (
	id INT PRIMARY KEY identity(1, 1)
	,address NVARCHAR(50) NOT NULL
	,zip_code NVARCHAR(6)
	,FOREIGN KEY (zip_code) REFERENCES Zip_id(zip_code) ON DELETE SET NULL ON UPDATE CASCADE
	);

CREATE TABLE Person (
	id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL
	,phone NVARCHAR(20) UNIQUE NOT NULL
	,email NVARCHAR(100)
	,first_name NVARCHAR(50) NOT NULL
	,last_name NVARCHAR(50) NOT NULL
	,location_id INT NOT NULL
	,FOREIGN KEY (location_id) REFERENCES Location(id) ON DELETE no action ON UPDATE no action
	);

CREATE TABLE Employee_Title (
	id INT PRIMARY KEY IDENTITY(1, 1)
	,title NVARCHAR(30) UNIQUE NOT NULL
	);

CREATE TABLE Employee (
	id INT PRIMARY KEY identity(1, 1)
	,person_id INT 
	,title_id INT NOT NULL
	,salary DECIMAL DEFAULT 0
	,FOREIGN KEY (title_id) REFERENCES Employee_Title(id) ON DELETE NO ACTION
	,FOREIGN KEY (person_id) REFERENCES Person(id) ON DELETE SET NULL ON UPDATE NO ACTION
	);

CREATE TABLE Customer (
	id INT PRIMARY KEY identity(1, 1)
	,person_id INT 
	,FOREIGN KEY (person_id) REFERENCES Person(id) ON DELETE SET NULL ON UPDATE NO ACTION
	);

CREATE TABLE Reservation (
	id INT PRIMARY KEY identity(1, 1)
	,reservation_date DATETIME NOT NULL		
	,customer_id INT NOT NULL
	,reservationTime DATETIME2 NOT NULL
	,noOfPeople INT NOT NULL
	,deposit BIT DEFAULT 0
	,note NVARCHAR(200)
	,FOREIGN KEY (customer_id) REFERENCES Customer(id) ON DELETE NO ACTION
	);

CREATE TABLE Reservations_Tables (
	reservation_id INT NOT NULL
	,restaurant_tables_id INT 
	,FOREIGN KEY (restaurant_tables_id) REFERENCES Restaurant_Tables(id)  ON DELETE CASCADE 
	,FOREIGN KEY (reservation_id) REFERENCES Reservation(id)  ON DELETE CASCADE
	,PRIMARY KEY (
		reservation_id
		,restaurant_tables_id
		)
	);

CREATE TABLE PaymentCondition (
	id INT PRIMARY KEY identity(1, 1)
	,condition NVARCHAR(20) NOT NULL  CHECK(
		condition IN (
			'Bestilt'
			,'Begyndt'
			,'Leveret'
			,'Betalt'
			,'Annulleret'
			)
		)
	);

CREATE TABLE Restaurant_Order (
	orderNo INT PRIMARY KEY IDENTITY(1, 1) NOT NULL
	,reservation_id INT NOT NULL
	,employee_id INT NOT NULL
	,orderDate DATETIME NOT NULL
	,paymentCondition_id INT NOT NULL
	,FOREIGN KEY (employee_id) REFERENCES Employee(id) ON DELETE NO ACTION
	,FOREIGN KEY (reservation_id) REFERENCES Reservation(id) ON DELETE NO ACTION
	,FOREIGN KEY (paymentCondition_id) REFERENCES PaymentCondition(id) ON DELETE NO ACTION 
	);

CREATE TABLE OrderLine (
	quantity INT NOT NULL
	,food_id INT NOT NULL
	,order_Number INT NOT NULL
	,FOREIGN KEY (food_id) REFERENCES Food(id) ON DELETE NO ACTION
	,FOREIGN KEY (order_Number) REFERENCES Restaurant_Order(orderNo) ON DELETE NO ACTION
	,PRIMARY KEY (
		food_id
		,order_Number
		)
	);