DELETE FROM dbo.RoleFunction
DELETE FROM [FUNCTION]
DELETE FROM dbo.RoleEmployee
DELETE FROM [ROLE]
DELETE FROM dbo.AccessibilityType
DELETE FROM dbo.AlertConfiguration
DELETE FROM dbo.TicketEmployee
DELETE FROM dbo.ProductTicket
DELETE FROM dbo.Ticket
DELETE FROM dbo.Customer
DELETE FROM dbo.Employee
DELETE FROM dbo.EmployeeCategory
DELETE FROM dbo.ProductProvider
DELETE FROM dbo.Product
DELETE FROM dbo.Provider
DELETE FROM dbo.ProviderCalification
DELETE FROM dbo.TicketStatus

DECLARE @contador int

SET @contador = 0
WHILE (@contador < 5)
	BEGIN
 		SET @contador = @contador + 1
		DECLARE @iName INT
		DECLARE @iDescription INT
		
		SET @iName = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iDescription = ((500000 + 1) - 100000) * RAND() + 100000
		INSERT INTO EmployeeCategory
		(
		Name,
		Description,
		AuditUserName
		)
		VALUES
		(
		CONVERT(VARCHAR(50),@iName),
		CONVERT(VARCHAR(50),@iDescription),
		NULL
		)
	END

SET @contador = 0
WHILE (@contador < 50)
	BEGIN
 		SET @contador = @contador + 1

		DECLARE @iUsername INT
		DECLARE @iFileNumber INT
		DECLARE @iFirstname INT
		DECLARE @iLastname INT
		DECLARE @iPhone INT
		DECLARE @iEmergencyPhone INT

		SET @iUsername = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iFileNumber = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iFirstname = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iLastname = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iPhone = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iEmergencyPhone = ((500000 + 1) - 100000) * RAND() + 100000

		DECLARE @iMinCategoryId INT
		DECLARE @iMaxCategoryId INT
		DECLARE @iCategoryId INT
		
		SET @iMinCategoryId = (SELECT MIN(ID) FROM dbo.EmployeeCategory)
		SET @iMaxCategoryId = (SELECT MAX(ID) FROM dbo.EmployeeCategory)
		SET @iCategoryId = ((@iMaxCategoryId + 1) - @iMinCategoryId) * RAND() + @iMinCategoryId

		INSERT INTO dbo.Employee
		(
		CategoryId,
		Username,
		Password,
		FileNumber,
		Firstname,
		Lastname,
		Phone,
		EmergencyPhone,
		Email,
		AuditUserName
		)
		VALUES
		(
		@iCategoryId,
		CONVERT(VARCHAR(50),@iUsername),
		'1234',
		CONVERT(VARCHAR(50),@iFileNumber),
		CONVERT(VARCHAR(50),@iFirstname),
		CONVERT(VARCHAR(50),@iLastname),
		'4-' + CONVERT(VARCHAR(50),@iPhone),
		'4-' + CONVERT(VARCHAR(50),@iEmergencyPhone),
		CONVERT(VARCHAR(50),@iUsername) + '@BAGGE.COM',
		CONVERT(VARCHAR(50),@iUsername)
		)
	END

SET @contador = 0
WHILE (@contador < 15)
	BEGIN
 		SET @contador = @contador + 1
		DECLARE @iDays INT
		SET @iDays = ((30 + 1) - 1) * RAND() + 1

		INSERT INTO AlertConfiguration
		(
		Days,
		SendEmailToEmployees,
		SendEmailToCreator
		)
		VALUES
		(
		@iDays,
		1,
		1
		)
	END

SET @contador = 0
WHILE (@contador < 250)
	BEGIN
		SET @contador = @contador + 1
		
		DECLARE @iCUIT INT
		DECLARE @iAddress INT
		DECLARE @iZipCode INT
		DECLARE @iCity INT
		DECLARE @iMobilePhone INT

		SET @iName =((500000 + 1) - 100000) * RAND() + 100000
		SET @iCUIT =((500000 + 1) - 100000) * RAND() + 100000
		SET @iAddress =((500000 + 1) - 100000) * RAND() + 100000
		SET @iZipCode =((500000 + 1) - 100000) * RAND() + 100000
		SET @iCity =((500000 + 1) - 100000) * RAND() + 100000
		SET @iPhone =((500000 + 1) - 100000) * RAND() + 100000
		SET @iMobilePhone =((500000 + 1) - 100000) * RAND() + 100000
	
		DECLARE @iMinDistrictId INT
		DECLARE @iMaxDistrictId INT
		DECLARE @iDistrictId INT
		
		SET @iMinDistrictId = (SELECT MIN(ID) FROM dbo.District)
		SET @iMaxDistrictId = (SELECT MAX(ID) FROM dbo.District)
		SET @iDistrictId = ((@iMaxDistrictId + 1) - @iMinDistrictId) * RAND() + @iMinDistrictId

		INSERT INTO Customer
		(
		Name,
		CUIT,
		DistrictId,
		Address,
		Floor,
		Departament,
		ZipCode,
		City,
		Phone,
		MobilePhone,
		Email,
		Subscription,
		AuditUserName
		)
		VALUES
		(
		CONVERT(VARCHAR(50),@iName),
		CONVERT(VARCHAR(50),@iCUIT),
		@iDistrictId,
		CONVERT(VARCHAR(50),@iAddress),
		'1',
		'A',
		CONVERT(VARCHAR(50),@iZipCode),
		CONVERT(VARCHAR(50),@iCity),
		'4-' + CONVERT(VARCHAR(50),@iPhone),
		'15-' + CONVERT(VARCHAR(50),@iMobilePhone),
		CONVERT(VARCHAR(50),@iName) + '@' + CONVERT(VARCHAR(50),@iName) + '.COM',
		NULL,
		NULL
		)
	END

SET @contador = 0
WHILE (@contador < 3)
	BEGIN
 		SET @contador = @contador + 1

		SET @iName = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iDescription = ((500000 + 1) - 100000) * RAND() + 100000

		INSERT INTO ProviderCalification
		(
		Name,
		Description
		)
		VALUES
		(
		CONVERT(VARCHAR(50),@iName),
		CONVERT(VARCHAR(50),@iDescription)
		)
	END

SET @contador = 0
WHILE (@contador < 4)
	BEGIN
 		SET @contador = @contador + 1

		SET @iName = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iDescription = ((500000 + 1) - 100000) * RAND() + 100000

		INSERT INTO TicketStatus
		(
		Name,
		Description
		)
		VALUES
		(
		CONVERT(VARCHAR(50),@iName),
		CONVERT(VARCHAR(50),@iDescription)
		)
	END

SET @contador = 0
WHILE (@contador < 7)
	BEGIN
 		SET @contador = @contador + 1
		SET @iName = ((500000 + 1) - 100000) * RAND() + 100000
		INSERT INTO dbo.Role
		(
		Name,
		AuditUserName
		)
		VALUES
		(
		CONVERT(VARCHAR(50),@iName),
		NULL)		
	END

SET @iName = ((500000 + 1) - 100000) * RAND() + 100000
INSERT INTO dbo.AccessibilityType
(
ID,
Name
)
VALUES
(
1,
CONVERT(VARCHAR(50),@iName)
)		
SET @iName = ((500000 + 1) - 100000) * RAND() + 100000
INSERT INTO dbo.AccessibilityType
(
ID,
Name
)
VALUES
(
2,
CONVERT(VARCHAR(50),@iName)
)		
SET @iName = ((500000 + 1) - 100000) * RAND() + 100000
INSERT INTO dbo.AccessibilityType
(
ID,
Name
)
VALUES
(
3,
CONVERT(VARCHAR(50),@iName)
)		

SET @contador = 0
WHILE (@contador < 10)
	BEGIN
 		SET @contador = @contador + 1
		
		DECLARE @iAssembly INT
		DECLARE @iClassFullQualifiedName INT
		DECLARE @iMemberName INT
		DECLARE @iMinAccessibilityTypeId INT
		DECLARE @iMaxAccessibilityTypeId INT
		DECLARE @iAccessibilityTypeId INT
		
		SET @iName = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iAssembly = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iClassFullQualifiedName = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iMemberName = ((500000 + 1) - 100000) * RAND() + 100000

		SET @iMinAccessibilityTypeId = (SELECT MIN(ID) FROM dbo.AccessibilityType)
		SET @iMaxAccessibilityTypeId = (SELECT MAX(ID) FROM dbo.AccessibilityType)

		SET @iAccessibilityTypeId = ((@iMaxAccessibilityTypeId + 1) - @iMinAccessibilityTypeId) * RAND() + @iMinAccessibilityTypeId

		INSERT INTO [Function]
		(Name,
		Assembly,
		ClassFullQualifiedName,
		MemberName,
		MemberType,
		AccessibilityTypeId,
		ConstraintType,
		Value,
		AuditUserName)
		VALUES
		(CONVERT(VARCHAR(50),@iName),
		CONVERT(VARCHAR(255),@iAssembly),
		CONVERT(VARCHAR(255),@iClassFullQualifiedName),
		CONVERT(VARCHAR(255),@iMemberName),
		'A',
		@iAccessibilityTypeId,
		'P',
		1,
		NULL)
	END

SET @contador = 0
WHILE (@contador < 20)
	BEGIN
 		SET @contador = @contador + 1

		DECLARE @iMinEmployeeCreatorId INT
		DECLARE @iMaxEmployeeCreatorId INT
		DECLARE @iEmployeeCreatorId INT
		DECLARE @iTicketStatusId INT
		DECLARE @dEstimatedDuration DECIMAL(18,2)
		DECLARE @dBudget DECIMAL(18,2)
		DECLARE @ID_CLIENTE_MIN INT
		DECLARE @ID_CLIENTE_MAX INT
		DECLARE @iMinTicketStatusId INT
		DECLARE @iMaxTicketStatusId INT
		DECLARE @iCustomerId INT
		DECLARE @iExecutionDate int
		DECLARE @iCustomerETA int

		SET @iDescription = ((500000 + 1) - 100000) * RAND() + 100000
		SET @dEstimatedDuration = ((5.5 + 1) - 1.5) * RAND() + 1.5
		SET @dBudget = ((500000.5 + 1) - 100000.5) * RAND() + 100000.5


		SET @iMinEmployeeCreatorId = (SELECT MIN(ID) FROM dbo.Employee)
		SET @iMaxEmployeeCreatorId = (SELECT MAX(ID) FROM dbo.Employee)

		SET @iEmployeeCreatorId = ((@iMaxEmployeeCreatorId + 1) - @iMinEmployeeCreatorId) * RAND() + @iMinEmployeeCreatorId


		SET @ID_CLIENTE_MIN = (SELECT MIN(ID) FROM dbo.CUSTOMER)
		SET @ID_CLIENTE_MAX = (SELECT MAX(ID) FROM dbo.CUSTOMER)

		SET @iCustomerId = ((@ID_CLIENTE_MAX + 1) - @ID_CLIENTE_MIN) * RAND() + @ID_CLIENTE_MIN

		SET @iMinTicketStatusId = (SELECT MIN(ID) FROM dbo.TicketStatus)
		SET @iMaxTicketStatusId = (SELECT MAX(ID) FROM dbo.TicketStatus)

		SET @iTicketStatusId = ((@iMaxTicketStatusId + 1) - @iMinTicketStatusId) * RAND() + @iMinTicketStatusId

		SET @iExecutionDate = ((4 + 1) - 1) * RAND() + 1
		SET @iCustomerETA = ((16 + 1) - 9) * RAND() + 9

		INSERT INTO Ticket
		(
		CustomerId,
		TicketStatusId,
		CreationDate,
		ExecutionDate,
		CustomerETA,
		EstimatedDuration,
		RealDuration,
		Description,
		Budget,
		EmployeeCreatorId,
		AuditUserName)
		VALUES
		(
		@iCustomerId,
		@iTicketStatusId,
		GETDATE(),
		GETDATE() + @iExecutionDate,
		CONVERT(VARCHAR(2),@iCustomerETA) + ':00:00.00',
		@dEstimatedDuration,
		NULL,
		CONVERT(VARCHAR(50),@iDescription),
		@dBudget,
		@iEmployeeCreatorId,
		NULL)
		
	END

SET @contador = 0
WHILE (@contador < 30)
	BEGIN
 		SET @contador = @contador + 1
		
		DECLARE @iMinTicketId INT
		DECLARE @iMaxTicketId INT
		DECLARE @iTicketId INT
		DECLARE @iMinEmployeeId INT
		DECLARE @iMaxEmployeeId INT
		DECLARE @iEmployeeId INT

		SET @iMinTicketId = (SELECT MIN(ID) FROM dbo.Ticket)
		SET @iMaxTicketId = (SELECT MAX(ID) FROM dbo.Ticket)

		SET @iTicketId = ((@iMaxTicketId + 1) - @iMinTicketId) * RAND() + @iMinTicketId

		SET @iMinEmployeeId = (SELECT MIN(ID) FROM dbo.Employee)
		SET @iMaxEmployeeId = (SELECT MAX(ID) FROM dbo.Employee)

		SET @iEmployeeId = ((@iMaxEmployeeId + 1) - @iMinEmployeeId) * RAND() + @iMinEmployeeId

		INSERT INTO TicketEmployee
		(TicketId,
		EmployeeId)
		VALUES
		(@iTicketId,
		@iEmployeeId)
	END

SET @contador = 0
WHILE (@contador < 100)
	BEGIN
 		SET @contador = @contador + 1

		SET @iName = ((500000 + 1) - 100000) * RAND() + 100000
		SET @iDescription = ((500000 + 1) - 100000) * RAND() + 100000

		INSERT INTO Product
		(
		Name,
		Description,
		AuditUserName
		)
		VALUES
		(
		CONVERT(VARCHAR(50),@iName),
		CONVERT(VARCHAR(50),@iDescription),
		NULL
		)
	END

SET @contador = 0
WHILE (@contador < 50)
	BEGIN
 		SET @contador = @contador + 1

		DECLARE @iProductId INT
		DECLARE @iMinProductId INT
		DECLARE @iMaxProductId INT

		DECLARE @dEstimatedQuantity DECIMAL(18,2)

		SET @iMinTicketId = (SELECT MIN(ID) FROM dbo.Ticket)
		SET @iMaxTicketId = (SELECT MAX(ID) FROM dbo.Ticket)

		SET @iTicketId = ((@iMaxTicketId + 1) - @iMinTicketId) * RAND() + @iMinTicketId

		SET @iMinProductId = (SELECT MIN(ID) FROM dbo.Product)
		SET @iMaxProductId = (SELECT MAX(ID) FROM dbo.Product)

		SET @iProductId = ((@iMaxProductId + 1) - @iMinProductId) * RAND() + @iMinProductId

		SET @dEstimatedQuantity = ((50.5 + 1) - 1.5) * RAND() + 1.5

		INSERT INTO ProductTicket
		(ProductId,
		TicketId,
		EstimatedQuantity)
		VALUES
		(@iProductId,
		@iTicketId,
		@dEstimatedQuantity)	
	END

SET @contador = 0
WHILE (@contador < 20)
	BEGIN
 		SET @contador = @contador + 1

		DECLARE @iCompany INT
		DECLARE @iSecondaryPhone INT
		DECLARE @iFaxPhone INT 
		DECLARE @iContactName INT
		DECLARE @iCalificationId INT
		DECLARE @iMinCalificationId INT
		DECLARE @iMaxCalificationId INT

		SET @iCUIT =((500000 + 1) - 100000) * RAND() + 100000
		SET @iCompany =((500000 + 1) - 100000) * RAND() + 100000
		SET @iAddress =((500000 + 1) - 100000) * RAND() + 100000
		SET @iMinDistrictId = (SELECT MIN(ID) FROM dbo.District)
		SET @iMaxDistrictId = (SELECT MAX(ID) FROM dbo.District)
		SET @iDistrictId = ((@iMaxDistrictId + 1) - @iMinDistrictId) * RAND() + @iMinDistrictId
		SET @iMinCalificationId = (SELECT MIN(ID) FROM dbo.ProviderCalification)
		SET @iMaxCalificationId = (SELECT MAX(ID) FROM dbo.ProviderCalification)
		SET @iCalificationId = ((@iMaxCalificationId + 1) - @iMinCalificationId) * RAND() + @iMinCalificationId
		SET @iZipCode =((500000 + 1) - 100000) * RAND() + 100000
		SET @iCity =((500000 + 1) - 100000) * RAND() + 100000
		SET @iPhone =((500000 + 1) - 100000) * RAND() + 100000
		SET @iSecondaryPhone =((500000 + 1) - 100000) * RAND() + 100000
		SET @iFaxPhone =((500000 + 1) - 100000) * RAND() + 100000
		SET @iContactName =((500000 + 1) - 100000) * RAND() + 100000
		
		INSERT INTO Provider
		(
		CUIT,
		Company,
		Address,
		DistrictId,
		Floor,
		Departament,
		ZipCode,
		City,
		Phone,
		SecondaryPhone,
		FaxPhone,
		Email,
		WebSite,
		ContactName,
		CalificationId,
		AuditUserName
		)
		VALUES
		(
		CONVERT(VARCHAR(50),@iCUIT),
		CONVERT(VARCHAR(50),@iCompany),
		CONVERT(VARCHAR(50),@iAddress),
		@iDistrictId,
		'1',
		'A',
		CONVERT(VARCHAR(50),@iZipCode),
		CONVERT(VARCHAR(50),@iCity),
		'4-' + CONVERT(VARCHAR(50),@iPhone),
		'4-' + CONVERT(VARCHAR(50),@iSecondaryPhone),
		'4-' + CONVERT(VARCHAR(50),@iFaxPhone),
		CONVERT(VARCHAR(50),@iCompany)+ '@' + CONVERT(VARCHAR(50),@iCompany) + '.COM',
		'WWW.' + CONVERT(VARCHAR(50),@iCompany)+ '.COM',
		CONVERT(VARCHAR(50),@iContactName),
		@iCalificationId,
		NULL)
	END

SET @contador = 0
WHILE (@contador < 10)
	BEGIN
 		SET @contador = @contador + 1

		DECLARE @iMinRoleId INT
		DECLARE @iMaxRoleId INT
		DECLARE @iRoleId INT
		DECLARE @iMinFunctionId INT
		DECLARE @iMaxFunctionId INT
		DECLARE @iFunctionId INT

		SET @iMinRoleId = (SELECT MIN(ID) FROM dbo.Role)
		SET @iMaxRoleId = (SELECT MAX(ID) FROM dbo.Role)
		SET @iRoleId = ((@iMaxRoleId + 1) - @iMinRoleId) * RAND() + @iMinRoleId

		SET @iMinFunctionId = (SELECT MIN(ID) FROM [Function])
		SET @iMaxFunctionId = (SELECT MAX(ID) FROM [Function])
		SET @iFunctionId = ((@iMaxFunctionId + 1) - @iMinFunctionId) * RAND() + @iMinFunctionId

		INSERT INTO RoleFunction
		(RoleId,
		FunctionId)
		VALUES
		(
		@iRoleId,
		@iFunctionId)
	END

SET @contador = 0
WHILE (@contador < 50)
	BEGIN
 		SET @contador = @contador + 1

		DECLARE @iMinProviderId INT
		DECLARE @iMaxProviderId INT
		DECLARE @iProviderId INT
		DECLARE @dUnitaryPrice DECIMAL(18,2)

		SET @dUnitaryPrice = ((500.5 + 1) - 100.5) * RAND() + 100.5
		SET @iMinProductId = (SELECT MIN(ID) FROM dbo.Product)
		SET @iMaxProductId = (SELECT MAX(ID) FROM dbo.Product)
		SET @iProductId = ((@iMaxProductId + 1) - @iMinProductId) * RAND() + @iMinProductId
		SET @iMinProviderId = (SELECT MIN(ID) FROM Provider)
		SET @iMaxProviderId = (SELECT MAX(ID) FROM Provider)
		SET @iProviderId = ((@iMaxProviderId + 1) - @iMinProviderId) * RAND() + @iMinProviderId

		INSERT INTO ProductProvider
		(ProductId,
		ProviderId,
		UnitaryPrice,
		AuditUserName)
		VALUES
		(@iProductId,
		@iProviderId,
		@dUnitaryPrice,
		NULL)
	END

SET @contador = 0
WHILE (@contador < 10)
	BEGIN
 		SET @contador = @contador + 1
		
		SET @iMinRoleId = (SELECT MIN(ID) FROM dbo.Role)
		SET @iMaxRoleId = (SELECT MAX(ID) FROM dbo.Role)
		SET @iRoleId = ((@iMaxRoleId + 1) - @iMinRoleId) * RAND() + @iMinRoleId

		SET @iMinEmployeeId = (SELECT MIN(ID) FROM dbo.Employee)
		SET @iMaxEmployeeId = (SELECT MAX(ID) FROM dbo.Employee)
		SET @iEmployeeId = ((@iMaxEmployeeId + 1) - @iMinEmployeeId) * RAND() + @iMinEmployeeId

		INSERT INTO RoleEmployee
		(RoleId,
		EmployeeId)
		VALUES
		(@iRoleId,
		@iEmployeeId)
	END
/*
SELECT * FROM EmployeeCategory
SELECT * FROM Employee
SELECT * FROM AlertConfiguration
SELECT * FROM Customer
SELECT * FROM dbo.ProviderCalification
SELECT * FROM TicketStatus
SELECT * FROM [ROLE]
SELECT * FROM dbo.AccessibilityType
SELECT * FROM [Function]
SELECT * FROM dbo.Ticket
SELECT * FROM dbo.TicketEmployee
SELECT * FROM dbo.Product
SELECT * FROM dbo.ProductTicket
SELECT * FROM dbo.Provider
SELECT * FROM dbo.RoleFunction
SELECT * FROM dbo.ProductProvider
SELECT * FROM dbo.RoleEmployee
*/











