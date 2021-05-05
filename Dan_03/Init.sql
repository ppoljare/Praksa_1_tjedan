DROP TABLE Student;
DROP TABLE Fakultet;
GO

CREATE TABLE Fakultet (
	FakultetID CHAR(4) PRIMARY KEY,
	Naziv VARCHAR(50) NOT NULL
);

CREATE TABLE Student (
	StudentID CHAR(4) PRIMARY KEY,
	FakultetID CHAR(4) NOT NULL,
	Ime VARCHAR(20) NOT NULL,
	Prezime VARCHAR(20) NOT NULL,
	
	CONSTRAINT student_fakultet_fk FOREIGN KEY (FakultetID)
		REFERENCES Fakultet(FakultetID) ON DELETE CASCADE
);

GO

INSERT INTO Fakultet VALUES ('F001', 'MathOS');
INSERT INTO Fakultet VALUES ('F002', 'FERIT');
GO

INSERT INTO Student VALUES ('S001', 'F001', 'Petar', 'Poljarevic');
INSERT INTO Student VALUES ('S002', 'F002', 'Ivan', 'Paradzik');
INSERT INTO Student VALUES ('S003', 'F001', 'Marko', 'Senk');
INSERT INTO Student VALUES ('S004', 'F001', 'Ante', 'Romic');
INSERT INTO Student VALUES ('S005', 'F002', 'Robert', 'Dumancic');
INSERT INTO Student VALUES ('S006', 'F001', 'Luka', 'Strapac');
GO
