USE LibraryWebsiteDatabase

SELECT * FROM SACH
SELECT * FROM MUONTRA
SELECT * FROM JOIN_BOOKBORROW
SELECT * FROM JOIN_LISTTAGBOOK
SELECT * FROM TAG
SELECT * FROM TRANG

INSERT INTO TRANG
VALUES ('001', 1, 'Chapter 1', '"But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure?"')
INSERT INTO TRANG
VALUES ('002', 2, 'Chapter 2', '"On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains."')

INSERT INTO SACH
VALUES ('',
		'',
		'' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + '' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + '' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + '',
		'',
		'',
		'20150320',
		'Digital',
		NULL,
		'AVAILABLE',
		'~/Images/Books/Kulti.jpeg')

--Table
CREATE TABLE TAG (
	idTag INT NOT NULL IDENTITY(1,1),
	nameTag NVARCHAR(30) NOT NULL UNIQUE,
	typeTag NVARCHAR(30) NOT NULL,
	PRIMARY KEY (idTag)
)

CREATE TABLE SACH (
	idBook CHAR(4) NOT NULL UNIQUE,
	nameBook NVARCHAR(100) NOT NULL,
	descriptionBook NVARCHAR(MAX) NOT NULL,
	authorBook NVARCHAR(100) NOT NULL,
	publisherBook NVARCHAR(100) NOT NULL,
	dateBook DATE NOT NULL,
	formatBook NVARCHAR(20) NOT NULL,
	noteBook NVARCHAR(200), 
	statusBook NVARCHAR(100) NOT NULL,
	imageBook VARCHAR(200) NOT NULL,
	PRIMARY KEY (idBook)
)

CREATE TABLE TRANG (
	idChapter CHAR(5) NOT NULL,
	numberChapter INT NOT NULL,
	titleChapter NVARCHAR(100) NOT NULL,
	contentChapter NVARCHAR(MAX) NOT NULL
	PRIMARY KEY (idChapter)
)

CREATE TABLE JOIN_LISTTAGBOOK (
	idBook CHAR(4) NOT NULL,
	idTag INT NOT NULL,
	PRIMARY KEY (idBook, idTag),
	FOREIGN KEY (idBook) REFERENCES SACH(idBook),
	FOREIGN KEY (idTag) REFERENCES TAG(idTag)
)

CREATE TABLE SUUTAP (
	idCollection CHAR(4) NOT NULL UNIQUE,
	nameCollection NVARCHAR(100) NOT NULL,
	descriptionCollection NVARCHAR(MAX) NOT NULL,
	imageCollection VARCHAR(200) NOT NULL,
	PRIMARY KEY (idCollection)
)

CREATE TABLE JOIN_LISTCOLLECTION (
	idCollection CHAR(4) NOT NULL,
	idBook CHAR(4) NOT NULL,
	PRIMARY KEY (idCollection, idBook),
	FOREIGN KEY (idCollection) REFERENCES SUUTAP(idCollection),
	FOREIGN KEY (idBook) REFERENCES SACH(idBook)
)

CREATE TABLE ACCOUNT_USER (
	idUser INT NOT NULL IDENTITY(1,1),
	nameUser VARCHAR(100) NOT NULL,
	emailUser VARCHAR(100) NOT NULL UNIQUE,
	passwordUser VARCHAR(100) NOT NULL,
	PRIMARY KEY (idUser)
)

CREATE TABLE THETHUVIEN (
	idCard CHAR(12) NOT NULL UNIQUE,
	nameCard NVARCHAR(100) NOT NULL,
	emailCard NVARCHAR(100) NOT NULL,
	addressCard NVARCHAR(200) NOT NULL,
	phoneCard VARCHAR(100) NOT NULL,
	dateCard DATE NOT NULL,
	startCard DATE NOT NULL,
	expireCard DATE NOT NULL,
	statusCard NVARCHAR(100) NOT NULL,
	PRIMARY KEY (idCard)
)

CREATE TABLE DOCGIA (
	idMember INT NOT NULL IDENTITY(1,1),
	statusMember NVARCHAR(100) NOT NULL,
	idCard CHAR(12) NOT NULL UNIQUE,
	idUser INT NOT NULL,
	PRIMARY KEY (idMember),
	FOREIGN KEY (idCard) REFERENCES THETHUVIEN(idCard),
	FOREIGN KEY (idUser) REFERENCES ACCOUNT_USER(idUser)
)

CREATE TABLE THUTHU (
	idLibrarian INT NOT NULL IDENTITY(1,1),
	roleLibrarian NVARCHAR(30) NOT NULL,
	hireLibrarian DATE NOT NULL,
	statusLibrarian NVARCHAR(100) NOT NULL,
	idUser INT NOT NULL,
	PRIMARY KEY (idLibrarian),
	FOREIGN KEY (idUser) REFERENCES ACCOUNT_USER(idUser)
)	

CREATE TABLE MUONTRA (
	idBorrow INT NOT NULL IDENTITY(1,1),
	dateBorrow DATE NOT NULL,
	statusBorrow NVARCHAR(100) NOT NULL,
	idCard CHAR(12) NOT NULL,
	idLibrarian INT,
	PRIMARY KEY (idBorrow),
	FOREIGN KEY (idCard) REFERENCES THETHUVIEN(idCard),
	FOREIGN KEY (idLibrarian) REFERENCES THUTHU(idLibrarian)
)

CREATE TABLE JOIN_BOOKBORROW (
	idBorrow INT NOT NULL,
	idBook CHAR(4) NOT NULL,
	startDate DATE NOT NULL,
	returnDate DATE NOT NULL,
	statusBookBorrow NVARCHAR(100) NOT NULL,
	PRIMARY KEY (idBorrow, idBook),
	FOREIGN KEY (idBorrow) REFERENCES MUONTRA(idBorrow),
	FOREIGN KEY (idBook) REFERENCES SACH(idBook)
)

declare @sql nvarchar(max) = (
    select 
        'alter table ' + quotename(schema_name(schema_id)) + '.' +
        quotename(object_name(parent_object_id)) +
        ' drop constraint '+quotename(name) + ';'
    from sys.foreign_keys
    for xml path('')
);
exec sp_executesql @sql;

DECLARE @sql NVARCHAR(max)=''

SELECT @sql += ' Drop table ' + QUOTENAME(TABLE_SCHEMA) + '.'+ QUOTENAME(TABLE_NAME) + '; '
FROM   INFORMATION_SCHEMA.TABLES
WHERE  TABLE_TYPE = 'BASE TABLE'

Exec Sp_executesql @sql

--Type
CREATE TYPE IntList AS TABLE (
    Id INT
);


--Procedure 
--TAG
CREATE PROC AddTag @name NVARCHAR(30), @type NVARCHAR(30)
AS
BEGIN
	INSERT INTO TAG
	VALUES (@name, @type);
END;

CREATE PROC RemoveTag @id INT
AS
BEGIN
	DELETE FROM TAG
	WHERE idTag = @id;
END;

CREATE PROC EditTag @id INT, @name NVARCHAR(30), @type NVARCHAR(30)
AS
BEGIN
	UPDATE TAG
	SET nameTag = @name,
		typeTag = @type
	WHERE idTag = @id
END;

--SACH
CREATE PROC AddBook @id CHAR(4), @name NVARCHAR(100), @description NVARCHAR(MAX), @author NVARCHAR(100), @publisher NVARCHAR(100), @date DATE, @format NVARCHAR(20), @note NVARCHAR(200), @image VARCHAR(200)
AS
BEGIN
	INSERT INTO SACH
	VALUES (@id, @name, @description, @author, @publisher, @date, @format, @note, 'AVAILABLE', @image)
END;

CREATE PROC DeleteBook @id CHAR(4)
AS
BEGIN
	DELETE FROM SACH
	WHERE idBook = @id;
END;

CREATE PROC EditBook @id CHAR(4), @name NVARCHAR(100), @description NVARCHAR(MAX), @author NVARCHAR(100), @publisher NVARCHAR(100), @date DATE, @format NVARCHAR(20), @note NVARCHAR(200), @status NVARCHAR(100), @image VARCHAR(200)
AS
BEGIN
    UPDATE SACH
    SET 
        nameBook = @name,
        descriptionBook = @description,
        authorBook = @author,
        publisherBook = @publisher,
        formatBook = @format,
		dateBook = @date,
		noteBook = @note,
		statusBook = @status,
        imageBook = @image
    WHERE idBook = @id;
END; 

CREATE PROC SearchBook @search NVARCHAR(100)
AS
BEGIN
	SELECT *
	FROM SACH
	WHERE 
		idBook LIKE '%' + @search + '%' OR 
		nameBook LIKE N'%' + @search + '%' OR
		authorBook LIKE N'%' + @search + '%' OR
		publisherBook LIKE N'%' + @search + '%' OR
		formatBook LIKE N'%' + @search + '%' OR
		CONVERT(VARCHAR, dateBook, 120) LIKE '%' + @search + '%' OR
		statusBook LIKE '%' + @search + '%';
END;

CREATE PROC SearchBookUser @search NVARCHAR(100)
AS
BEGIN
	SELECT *
	FROM (
		SELECT 
			*,
			ROW_NUMBER() OVER (
				PARTITION BY LEFT(idBook, 2)
				ORDER BY idBook
			) AS rn
		FROM SACH
		WHERE nameBook LIKE @search + '%'  
	) AS x
	WHERE x.rn = 1;  
END;

CREATE PROC FilterBook @tagids IntList READONLY
AS
BEGIN
	SET NOCOUNT ON;

    SELECT b.*
    FROM SACH b
    JOIN JOIN_LISTTAGBOOK bt ON b.idBook = bt.idBook
    WHERE bt.idTag IN (SELECT Id FROM @TagIds)
    GROUP BY b.idBook, b.nameBook, b.descriptionBook, b.authorBook, b.publisherBook, b.dateBook, b.formatBook, b.noteBook, b.statusBook, b.imageBook
    HAVING COUNT(DISTINCT bt.idTag) = (SELECT COUNT(*) FROM @TagIds);
END;

--TRANG
CREATE PROC AddChapter @id CHAR(5), @number INT, @title NVARCHAR(100), @content NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO TRANG
	VALUES (@id, @number, @title, @content)
END;

CREATE PROC DeleteChapter @id CHAR(5)
AS
BEGIN
	DELETE FROM TRANG
	WHERE idChapter = @id
END;

CREATE PROC EditChapter @id CHAR(5), @number INT, @title NVARCHAR(100), @content NVARCHAR(MAX)
AS
BEGIN
	UPDATE TRANG
	SET
		idChapter = @id,
		numberChapter = @number,
		titleChapter = @title
	WHERE idChapter = @id;
END;

CREATE PROC SearchChapter @search NVARCHAR(100)
AS
BEGIN
	SELECT *
	FROM TRANG
	WHERE 
		idChapter LIKE '%' + @search + '%' OR 
		(TRY_CAST(@search AS INT) IS NOT NULL AND numberChapter = TRY_CAST(@search AS INT)) OR
		titleChapter LIKE N'%' + @search + '%' 
END;

--SUUTAP
CREATE PROC AddCollection @id CHAR(4) OUTPUT, @name NVARCHAR(100), @description NVARCHAR(200), @image VARCHAR(200)
AS
BEGIN
	INSERT INTO SUUTAP
	VALUES (@id, @name, @description, @image)
END;

CREATE PROC DeleteCollection @id CHAR(4)
AS
BEGIN
	DELETE FROM JOIN_LISTCOLLECTION
	WHERE idCollection = @id;

	DELETE FROM SUUTAP
	WHERE idCollection = @id;
END;

CREATE PROC AddToCollection @idcollection CHAR(4), @idbook CHAR(4)
AS
BEGIN
	INSERT INTO JOIN_LISTCOLLECTION
	VALUES (@idcollection, @idbook);
END;

CREATE PROC RemoveFromCollection @idcollection CHAR(4), @idbook CHAR(4)
AS
BEGIN
	DELETE FROM JOIN_LISTCOLLECTION
	WHERE idCollection = @idcollection AND idBook = @idbook;
END;

CREATE PROC EditCollection @id CHAR(4), @name NVARCHAR(100), @description NVARCHAR(200), @image VARCHAR(200)
AS
BEGIN
	UPDATE SUUTAP
	SET
		nameCollection = @name,
		descriptionCollection = @description,
		imageCollection = @image
	WHERE idCollection = @id;
END;

CREATE PROC SearchCollection @search NVARCHAR(100)
AS
BEGIN
	SELECT *
	FROM SUUTAP
	WHERE 
		idCollection LIKE '%' + @search + '%' OR 
		nameCollection LIKE N'%' + @search + '%';
END;

--MUONTRA
CREATE PROC AddBorrow @idborrow INT OUTPUT, @idcard CHAR(12), @idlibrarian INT
AS
BEGIN
	INSERT INTO MUONTRA
	VALUES (GETDATE(), 'ACTIVE', @idcard, @idlibrarian);

	SET @idborrow = SCOPE_IDENTITY();
END;

CREATE PROC DeleteBorrow @id INT
AS
BEGIN
	DELETE FROM JOIN_BOOKBORROW
	WHERE idBorrow = @id;

	DELETE FROM MUONTRA
	WHERE idBorrow = @id;
END;

CREATE PROC EditBorrow @idborrow INT, @status NVARCHAR(100)
AS
BEGIN
    UPDATE MUONTRA
    SET statusBorrow = @status
    WHERE idBorrow = @idborrow;
END;

CREATE PROC AddBookToBorrow @idborrow INT, @idbook CHAR(4)
AS
BEGIN
	INSERT INTO JOIN_BOOKBORROW
	VALUES (@idborrow, @idbook, GETDATE(), DATEADD(DAY, 21, GETDATE()), 'PENDING');
END;

CREATE PROC RemoveBookFromBorrow @idborrow INT, @idbook CHAR(4)
AS
BEGIN
	DELETE FROM JOIN_BOOKBORROW
	WHERE idBorrow = @idborrow AND idBook = @idBook
END;

CREATE PROC EditBookBorrow @idborrow INT, @idbook CHAR(4), @status NVARCHAR(100)
AS
BEGIN
	UPDATE JOIN_BOOKBORROW
	SET statusBookBorrow = @status
	WHERE idBorrow = @idborrow AND idBook = @idBook
END;

CREATE PROC SearchBorrow @search VARCHAR(12)
AS
BEGIN
	SELECT *
	FROM MUONTRA
	WHERE 
		(TRY_CAST(@search AS INT) IS NOT NULL AND idLibrarian = TRY_CAST(@search AS INT)) OR 
		idCard LIKE '%' + @search + '%'
END;

--THETHUVIEN
CREATE PROC AddCard @id CHAR(12), @name NVARCHAR(100), @email NVARCHAR(100), @address NVARCHAR(100), @phone VARCHAR(100), @date DATE
AS
BEGIN
	INSERT INTO THETHUVIEN
	VALUES (@id, @name, @email, @address, @phone, @date, GETDATE(), DATEADD(YEAR, 3, GETDATE()), 'CREATED');
END;

CREATE PROC DeleteCard @id CHAR(12)
AS
BEGIN
	DELETE FROM THETHUVIEN
	WHERE idCard = @id;
END;

CREATE PROC EditCard @id CHAR(12), @name NVARCHAR(100), @email NVARCHAR(100), @address NVARCHAR(100), @phone VARCHAR(100), @date DATE, @status NVARCHAR(100)
AS
BEGIN
	UPDATE THETHUVIEN
	SET
		nameCard = @name,
		emailCard = @email,
		addressCard = @address,
		phoneCard = @phone,
		dateCard = @date,
		statusCard = @status
		WHERE idCard = @id;
END;

CREATE PROC SearchCard @search NVARCHAR(200)
AS
BEGIN
	SELECT *
	FROM THETHUVIEN
	WHERE 
		idCard LIKE '%' + @search + '%' OR 
		nameCard LIKE N'%' + @search + '%' OR
		emailCard LIKE '%' + @search + '%' OR
		addressCard LIKE N'%' + @search + '%' OR
		phoneCard LIKE '%' + @search + '%' OR
		CONVERT(VARCHAR, dateCard, 120) LIKE '%' + @search + '%' OR
		statusCard LIKE '%' + @search + '%';
END;

--DOCGIA
CREATE PROC AddMember @idcard CHAR(12), @iduser INT
AS
BEGIN
	INSERT INTO DOCGIA
	VALUES ('CREATED', @idcard, @iduser);
END;

CREATE PROC DeleteMember @id INT
AS
BEGIN
	DELETE FROM DOCGIA
	WHERE idMember = @id;
END;

CREATE PROC EditMember @id INT, @status NVARCHAR(100), @idcard CHAR(12), @iduser INT
AS
BEGIN
	UPDATE DOCGIA
	SET
		statusMember = @status,
		idCard = @idcard,
		idUser = @iduser
	WHERE idMember = @id;
END;

CREATE PROC SearchMember @search VARCHAR(100)
AS
BEGIN
	SELECT *
	FROM DOCGIA
	WHERE 
		statusMember LIKE '%' + @search + '%' OR 
		(TRY_CAST(@search AS INT) IS NOT NULL AND idUser = TRY_CAST(@search AS INT)) OR 
		(TRY_CAST(@search AS INT) IS NOT NULL AND idMember = TRY_CAST(@search AS INT)) OR
		idCard LIKE '%' + @search + '%'; 
END;

--THUTHU
CREATE PROC AddLibrarian @role NVARCHAR(30), @date DATE, @iduser INT
AS
BEGIN
	INSERT INTO THUTHU
	VALUES (@role, @date, 'CREATED', @iduser);
END;
select * from TAG
CREATE PROC DeleteLibrarian @id INT
AS
BEGIN
	DELETE FROM THUTHU
	WHERE idLibrarian = @id;
END;

CREATE PROC EditLibrarian @idlibrarian INT, @role NVARCHAR(30), @date DATE, @status NVARCHAR(100), @iduser INT
AS
BEGIN
	UPDATE THUTHU
	SET
		roleLibrarian = @role,
		hireLibrarian = @date,
		statusLibrarian = @status,
		idUser = @iduser
	WHERE idLibrarian = @idlibrarian;
END;

CREATE PROC SearchLibrarian @search VARCHAR(100)
AS
BEGIN
	SELECT *
	FROM THUTHU
	WHERE 		
		(TRY_CAST(@search AS INT) IS NOT NULL AND idLibrarian = TRY_CAST(@search AS INT)) OR
		roleLibrarian LIKE '%' + @search + '%' OR 
		CONVERT(VARCHAR, hireLibrarian, 120) LIKE '%' + @search + '%' OR 
		statusLibrarian LIKE '%' + @search + '%' OR 
		(TRY_CAST(@search AS INT) IS NOT NULL AND idUser = TRY_CAST(@search AS INT));
END;

--ACCOUNT_USER
CREATE PROC AddUser @id INT = 0 OUTPUT, @name VARCHAR(100), @email VARCHAR(100), @password VARCHAR(100)
AS
BEGIN
	INSERT INTO ACCOUNT_USER
	VALUES (@name, @email, @password)

	SET @id = SCOPE_IDENTITY();
END;

CREATE PROC DeleteUser @id INT
AS
BEGIN
	DELETE FROM DOCGIA
	WHERE idUser = @id;

	DELETE FROM THUTHU
	WHERE idUser = @id;

	DELETE FROM ACCOUNT_USER
	WHERE idUser = @id;
END;

CREATE PROC EditUser @id INT, @name VARCHAR(100), @email VARCHAR(100), @password VARCHAR(100)
AS
BEGIN
	UPDATE ACCOUNT_USER
	SET
		nameUser = @name,
		emailUser = @email,
		passwordUser = @password
	WHERE idUser = @id;
END;

CREATE PROC SearchUser @search NVARCHAR(100)
AS
BEGIN
	SELECT *
	FROM ACCOUNT_USER
	WHERE 
		(TRY_CAST(@search AS INT) IS NOT NULL AND idUser = TRY_CAST(@search AS INT)) OR 
		nameUser LIKE '%' + @search + '%' OR 
		emailUser LIKE '%' + @search + '%' OR 
		passwordUser LIKE '%' + @search + '%'; 
END;

CREATE PROC CheckUser @email NVARCHAR(255), @password NVARCHAR(255), @status INT OUTPUT, @role NVARCHAR(30) OUTPUT
AS
BEGIN
    SET @status = 0;

    IF EXISTS (
        SELECT 1 
		FROM ACCOUNT_USER
        WHERE emailUser = @email AND passwordUser = @password
    )
    BEGIN
        SET @status = 1;
		SET @role = (
			SELECT roleLibrarian
			FROM THUTHU
			WHERE idUser = (
				SELECT idUser
				FROM ACCOUNT_USER
				WHERE emailUser = @email
				)
			)
        RETURN;
    END;

    IF EXISTS (
        SELECT 1 FROM ACCOUNT_USER
        WHERE emailUser = @email
    )
    BEGIN
        SET @status = 2;
        RETURN;
    END;
END;

--ALL
CREATE PROC UpdateField @table CHAR(20), @id CHAR(12), @field CHAR(20), @newdata CHAR(200)
AS
BEGIN
	DECLARE @runupdate NVARCHAR(MAX), @idname CHAR(12), @fieldname CHAR(20);

	SELECT @idname = COLUMN_NAME
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = @table AND COLUMN_NAME LIKE 'id%';

	SELECT @fieldname = COLUMN_NAME
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = @table AND COLUMN_NAME = @field;

	SET @runupdate = 'UPDATE ' + QUOTENAME(@table) + 
					 ' SET ' + QUOTENAME(@fieldname) + ' = @p_newdata' +
					 ' WHERE ' + QUOTENAME(@idname) + ' = @p_id';
	EXEC sp_executesql @runupdate, N'@p_newdata CHAR(200), @p_id CHAR(12)', @p_newdata = @newdata, @p_id = @id;
END;

EXEC sp_MSForEachTable 'DISABLE TRIGGER ALL ON ?'
GO
EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
GO
EXEC sp_MSForEachTable 'SET QUOTED_IDENTIFIER ON; DELETE FROM ?'
GO
EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'
GO
EXEC sp_MSForEachTable 'ENABLE TRIGGER ALL ON ?'
GO

EXEC sp_MSforeachtable @command1 = 'DBCC CHECKIDENT(''?'', RESEED, 0)'
