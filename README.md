# EasyDb
A database design and Micro ORM written in c#.

# Installation
Prerequisites:
  - You must have SQLExpress or MS SQL installed.
  
To install:
  - Copy the scripts folder locally and run the scripts therein on your desired server.
  - Copy the EasyDb folder to a location of choice on your PC.
  - Amend the app.config connection string to use the server where you installed the database.
  
# Features
EasyDb allows you to point to any server you can connect to as a trusted user and then select an existing database you wish to work with. So far you can generate code against a given table to:
  - Create a Delete all SP
  - Create a Select all SP
  - Create an Insert New SP
  - Create an Edit existing SP (based off the Id column)
  - Generate a c# POCO class
  - Generate a c# View Model class
  - Generate SQL to insert the existing data in a table (for installations)
  - Create c# code to add parameters to a command
  
Creation of new and editing of existing tables is also supported via SQL generation.
Bulk output of all of the above is also available.

# Unsupported
Right now the feature set is limited. The following features are unsupported but will likely be added in future versions:
  - schemas
  - users
  - working off existing SPs
  - creation of new templates for code generation
  - scripting of the database itself
  - Use clause
  
As of now the Templates are inflexible and certain features are hard coded. A future release will address this and essentially allow users to generate any desired code based on the database tables.
