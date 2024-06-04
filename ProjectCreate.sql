CREATE TABLE "Employee" (
    "Id" UUID PRIMARY KEY,
    "FirstName" VARCHAR(255),
    "LastName" VARCHAR(255),
    "Position" VARCHAR(255),
    "Salary" DOUBLE PRECISION
);
drop table "Employee" 

CREATE TABLE "Project" (
    "Id" UUID PRIMARY KEY,
    "ProjectName" VARCHAR(255),
    "StartDate" DATE,
    "EndDate" DATE,
    "EmployeeId" UUID,
    CONSTRAINT "FK_EmployeeID" FOREIGN KEY ("EmployeeId") REFERENCES "Employee"("Id") on delete CASCADE
);
