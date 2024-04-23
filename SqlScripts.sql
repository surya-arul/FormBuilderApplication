-- Creating database 
CREATE DATABASE FormBuilderDB;
GO

-- Selecting database 
USE FormBuilderDB;
GO

-- Creating table (tblSurveys) 
CREATE TABLE tblSurveys (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    OpenDate Date NOT NULL,
    EndDate Date NOT NULL
);

-- Creating table (tblInputs) 
CREATE TABLE tblInputs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
	SurveyId INT NOT NULL,
    InputType VARCHAR(100) NOT NULL,
    InternalName VARCHAR(100) NOT NULL,
    Label VARCHAR(100) NULL,
    ShouldHideLabel BIT NOT NULL,
    DefaultValue VARCHAR(100) NULL,
    IsAutofocus BIT NOT NULL,
	Placeholder VARCHAR(100) NULL,
	IsRequired BIT NOT NULL,
	OptionData VARCHAR(MAX) NULL,
    FOREIGN KEY (SurveyId) REFERENCES tblSurveys(Id) ON DELETE CASCADE -- Foreign key with cascade
);