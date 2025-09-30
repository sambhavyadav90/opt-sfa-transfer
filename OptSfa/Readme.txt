1.  Every evening push code in "Development" branch
2.  Every morning pull code from "Development" branch
3.  Before pushing code, make sure to pull the latest changes from "Development" branch to avoid conflicts.
4.  If there are any conflicts, resolve them locally before pushing your changes.
5.  Always write clear and concise commit messages to describe the changes made.
6.  Do not push code directly to the "Main" branch; always use the "Development" branch for all changes.
7.  Before merging "Development" branch into "Main" branch, ensure that all tests pass and code reviews are completed.
8.  Before starting new work, always pull the latest changes from the "Development" branch to ensure you are working with the most up-to-date codebase.
9.  Keep your local repository clean by regularly deleting branches that have been merged.
10. For any major changes or new features, create a separate feature branch from "Development" and merge it back after completion and review.
11. Always communicate with your team about significant changes or updates to avoid overlapping work.


** Project Naming Convention For API
	{Solution}.{ProjectType}.{Api}
	{Solution}.{ProjectType}.{Application}
	{Solution}.{ProjectType}.{Data}
	{Solution}.{ProjectType}.{Domain}

** Project Naming Convention For GUI
	{Solution}.{ProjectType}.{Gui}

** Working with API
	** Domain
		1. Model
		2. ViewModel
		3. Interfaces
	
	** Data
		1. Context => DbSet and Table Mapping
		2. Repository => CRUD Operations

	** Application
		1. Interfaces => Business Logic
		2. Services => Data Transfer Objects	

	** API
		1. Controller => Action(HttpGet, HttpPost)

	** NOTES
		Register Services in Program.cs
		Register Repository in Program.cs