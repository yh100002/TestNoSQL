
<Prerequisites For Development>
	1. Frameworks
		1.1. ASP.NET MVC 
		1.2. Ninject : Nuget Package
		1.3. Moq : Nuget Package
		1.4. MongoDB Server 3.2.9 : 
		1.5. MongoDB Driver : Nuget Package
		1.6. MongRepository : Nuget Package
		1.7. .Net Framework 4.6

	2. Tools
		2.1. Visual Studio 2015 Professional
		2.2. Robomongo 0.9.0

<Building the Project>
	1. After installation of Nuget Packages above, you are able to build the solution.
	2. After that, import the data and check for it
	3. Build Solution and Run

<Preparation Of MongoDB>
	1. Install the MongoDB 3.2.9 from the site
		: https://www.mongodb.com/download-center

	2. For convinience and development, you had better install the MongoDB client program such as Robomongo
		: https://robomongo.org/

	3. Import the given data from Cossover
		: https://techtrial.s3.amazonaws.com/System/SoftwareEngineerGeneric_PublishedBooks.zip
		: Run the given mongo_import.bat and check for the imported data in the MongoDB server through robomongo
		: Use the default port number equal to 27017
		: There should be like below.
			[Crossover]
				|
				|
				|
				[Book]
				[User]
	4. Connection String
		: mongodb://localhost/Crossover

<Assumptions of Features>
	1. The Login feature was implemented with simple concept and no secure methods using just server session variables.
	2. For MongoDB data access and handling, I usally use the Mongorepository for convinience. 
	3. Focusing on the concept of the requirements
	4. To keep user's demands, I save demanded books as array into User collection.


