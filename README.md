# NOTE
This project has no connections to IMDB site, it is also made for non commercial purposes.
# Movie-Project
There are few ways to start project :
1. Without DB Scripts 
	- Open Project and set IMDB as Startup project
	- Uncommend lines in startup.cs that say 
			//dBContext.Database.EnsureDeleted();
            //dBContext.Database.EnsureCreated();
	- Run Project
	- Set IMDB.Web as Startup to See Web display of project and start project
	- Set IMDB as startup project to see Swagger display of APIs
2. Make sure at least once project was run when IMDB was startup project, and check if connection is http://localhost:32150 on Front End and http://localhost:51346/ on BackEnd, if not 
	- SET connection url of Front End in Startup where Cors is defined(WithOrigins)
	- Set connection url of backend in app.js where serviceBase is defined
3. Second was of generating data for project is run both Database scripts provided with project, first DBScript then DBData
4. To use EmailSender you need to supply credentials inside EmailSender along with Email address, this should be changed in future to require same inside config file instead

TODO :

This is draft project, and as such it is not finished. Things that need to be changed in this project :
1. Exception Handler is needed, preferably custom with database inserts. Error Handler on frontend also needed to notify in case of failed operations.
2. Method logging is needed for C,U and D operations, esspecially "D"
3. Soft Delete should be implemented
4. Further Database optimization is needed : Genre and Movies should have one more table Called GenreMovies that would connect these two
5. Further decoupling on Server and Client side is needed, on server side Microservice Architecture would be more beneficial
6. Single Sign On would be prefered option with Token Authentification
7. All Microservices should be decoupled into separate projects for easier maintanence.
8. On FrontEnd switch to Angular instead of AngularJS would be prefered option and further optimization needed.
9. Finishing controllers both frontend and backend
10. Data validation and verification needed on entire project.
11. Front end needs review of files, HTML should only have HTML, rest should be in separate CSS and JS files.
12. Resource optimisation needed, not all resources included are used.
13. Change Insert Methods so that they insert only the main object and not its sub elements. Or find alternative.
14. Implement Rating with Angular Star Rating and limit it per account.
