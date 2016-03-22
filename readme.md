nextcaller-dotnet-api
=====================

[![Build status](https://ci.appveyor.com/api/projects/status/ewpq1rs09lghcm08?svg=true)](https://ci.appveyor.com/project/Nextcaller/nextcaller-dotnet-api)

This is a .net wrapper around the Nextcaller API.
The library supports .net versions ≥ 3.5.

Dependencies
------------

* Newtonsoft.Json (≥ 6.0.5)

Installation
------------

*install using Package Manager Console in Visual Studio*:

    PM> Install-Package NextCallerApi

*install using nuget.exe*:

	$ nuget install NextCallerApi

NextCallerClient
=====================

Client initialization
-------------

    string username = "XXXXX";
    string password = "YYYYY";
	bool sandbox = true;
    
    NextCallerClient client = new NextCallerClient(username, password, sandbox);

*Parameters*:

    string username - username
    string password - password
	bool sandbox - use sandbox or no

API Items
-------------

## Get profiles by phone ##

	string number = "2020327123";
    IList<Profile> profiles = client.GetProfilesByPhone(number);
    
*Parameters*:
    
    string number - phone number
	
*Returns*:

	IList<Profile> profiles - list of profiles, associated with a particular phone number
	
## Get profile by id ##

	string profileId = "xxxx";
    Profile profile = client.GetProfileById(profileId);
    
*Parameters*:
    
    string profileId - id of a profile
	
*Returns*:

	Profile profile - profile with a specific id
	
## Update profile by id ##

	string profileId = "xxxx";
	ProfileToPost profile = new ProfileToPost
	{
		FirstName = "NewFirstName",
		LastName = "NewLastName"
	};
	
    client.UpdateProfileById(profileId, profile);
    
*Parameters*:

    ProfileToPost profile - profile info to post
    string profileId - id of profile to update 

NextCallerPlatformClient
=====================

Client initialization
-------------

    string username = "XXXXX";
    string password = "YYYYY";
	bool sandbox = true;
    
    NextCallerPlatformClient client = new NextCallerPlatformClient(username, password, sandbox);

*Parameters*:

    string username - username
    string password - password
	bool sandbox - use sandbox or no

API Items
-------------

## Get profiles by phone ##

	string number = "2020327123";
	string platformUsername = "xxxx";
    IList<Profile> profiles = client.GetProfilesByPhone(number, platformUsername);
    
*Parameters*:
    
    string number - phone number
	string platformUsername - platform username of a caller
	
*Returns*:

	IList<Profile> profiles - list of profiles, associated with a particular phone number
	
## Get profile by id ##

	string profileId = "xxxx";
	string platformUsername = "yyyyyy";
    Profile profile = client.GetProfileById(profileId, platformUsername);
    
*Parameters*:
    
    string profileId - id of a profile
	string platformUsername - platform username of a caller

*Returns*:

	Profile profile - profile with a specific id
	
## Update profile by id ##

	string profileId = "xxxx";
	string platformUsername = "yyyyyy";
	ProfileToPost profile = new ProfileToPost
	{
		FirstName = "NewFirstName",
		LastName = "NewLastName"
	};
	
    client.UpdateProfileById(profileId, profile, platformUsername);
    
*Parameters*:

    ProfileToPost profile - profile info to post
    string profileId - id of profile to update 
	string platformUsername - platform username of a caller

## Get platform statistics ##

    PlatformStatistics stats= client.GetPlatformStatistics();
    
*Parameters*:
    
	no parameters
	
*Returns*:

	PlatformStatistics stats - platform statistics
	
## Get platform user ##

	string platformUsername = "xxxx";
    PlatformUser user = client.GetPlatformUser(platformUsername);
    
*Parameters*:
    
	string platformUsername - platform username of a user to get
	
*Returns*:

	PlatformUser user - platform user info
	
## Update platform user ##

	string platformUsername = "xxxx";
	PlatformUserToPost user = new PlatformUserToPost
				{
					Email = "email@email.com",
					FirstName = "fedor"
				};
    client.UpdatePlatformUser(platformUsername, user);
    
*Parameters*:
    
	string platformUsername - platform username of a user to update
	PlatformUserToPost user - platform user info to post
	
### Json API ###

	All API items can be used with raw json data;

### Exceptions ###
	
	## BadResponseException ##
	
	When the NextCallerApi call failed for some reason
	and response was successfully parsed according to the 
	https://nextcaller.com/documentation/#/errors/curl error response reference,
	BadResponseException is thrown. It contains request, response,
	response content as a string and error object.
	
	try
	{
		// NextCaller API call
	}
	catch (BadResponseException e)
	{

		HttpWebRequest request = e.Request;
		HttpWebResponse response = e.Response;
		string content = e.Content;
		
		Error parsedError = e.Error;
		string errorMessage = parsedError.Code;
		string errorCode = parsedError.Code;
		string type = parsedError.Type;
		Dictionary<string, string[]> description = parsedError.Description;

		string emailErrorMessage = description["email"].FirstOrDefault();
	}

	
	## FormatException ##
	
	When the NextCallerApi call failed for some reason
	and response was not parsed according to the 
	https://nextcaller.com/documentation/#/errors/curl error response reference,
	FormatException is thrown. It contains request, response and
	response content as a string.
	
	try
	{
		// NextCaller API call
	}
	catch (FormatException e)
	{

		HttpWebRequest request = e.Request;
		HttpWebResponse response = e.Response;
		string content = e.Content;

	}
	
### Nuget package ###

	The library is packed, pushed to nuget.org and available via package 
	manager console or nuget command line tool. 
	Package contains versions for .net 3.5 or higher
	along with code samples.

### Mono usage ###

	The library was tested on Mono 3.8.0. To use it on previous releases one 
	has to make sure that chosen Mono release supports 3.5 .net version.
	
	
### Known mono issues ###

	If you see a WebException indicating there was an invalid certificate received 
	from the server then you should try running the following in console:

		$ mozroots --import --sync
