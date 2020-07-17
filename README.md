# HSAEnrollmentApplication
Console application written in C# to serve as the intake mechanism for a flat comma delimited input file application to process enrollments.

# About The Project
This is a console application that will ask you to share a path to a csv file of application enrollment data. You will also be asked if you want to set a date to compare dob and enrollment data.

- Data will then be validated. 
 FirstName,LastName,DOB (valid date mmddyyyy),PlanType (HSA,HRA,FSA),EffectiveDate (valid date mmddyyyy)
- The program will close if the data does not pass validation.
- Data will be Assessed for acceptance status (DOB 18yo +, EffectiveDate within 30 days of today or entered comparission date)
- Data will be read into a DataTable in memory
- Data will be displayed in the console with acceptance staus

# To The Run Project
Open This Git Repository
Click on “Clone or download” and copy the URL

Open Computer Terminal
Navigate to desired folder where you would like the project to be saved

`cd <desired folder name or path>
git clone <paste git URL you copied>`

Navigate to the newly created project folder

`cd <HSAEnrollmentApplication or path>`

Run the project and build dependencies

`dotnet run <path of project locally>.dll`
