CREATE TABLE [dbo].[Evaluations]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
	[EvaluationCommentary] NVARCHAR(MAX) NOT NULL,
	[EvaluationCreatedAt] DATETIME NOT NULL,
	[EvaluationUpdatedAt] DATETIME,
	[EvaluationValue] INT NOT NULL,
	CONSTRAINT CHK_EvaluationValue_Range CHECK ([EvaluationValue] >= 1 AND [EvaluationValue] <= 5),
	[FK_Owner] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	[FK_Sender] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [Contacts](Id) NOT NULL
)
