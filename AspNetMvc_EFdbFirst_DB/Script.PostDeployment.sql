/*
后期部署脚本模板							
--------------------------------------------------------------------------------------
 此文件包含将附加到生成脚本中的 SQL 语句。		
 使用 SQLCMD 语法将文件包含到后期部署脚本中。			
 示例:      :r .\myfile.sql								
 使用 SQLCMD 语法引用后期部署脚本中的变量。		
 示例:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
MERGE INTO　Course AS Target
Using (VALUES (1,N'经济学',3),(2,N'文学',3),(3,N'化学',4))
AS Source(CourseID,Title,Credits) ON Target.CourseID=Source.CourseID
WHEN NOT matched BY target THEN INSERT(Title,Credits) VALUES(Title,Credits);

Merge into Student AS Target
Using(VALUES (1,N'张扬',N'李','2018-09-01'),(1,N'长风',N'古','2017-09-01'),(1,N'淳罡',N'李','2019-09-01'))
AS Source(StudentID,LastName,FirstName,EnrollmentDate) ON Target.StudentID=Source.StudentID
When NOT Matched BY Target Then Insert(LastName,FirstName,EnrollmentDate) VALUES(LastName,FirstName,EnrollmentDate);

Merge into Enrollment As Target
Using(Values(1,2.00,1,1),(2,3.50,1,2),(3,4.00,2,3),(4,1.80,2,1),(5,3.20,3,1),(6,4.00,3,2))
As Source(EnrollmentID,Grade,CourseID,StudentID)
On Target.EnrollmentID=Source.EnrollmentID
When Not Matched By Target Then Insert(Grade,CourseID,StudentID) Values(Grade,CourseID,StudentID);