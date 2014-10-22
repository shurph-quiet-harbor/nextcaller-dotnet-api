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

Code example
-------

    string consumerKey = "XXXXX";
    string consumerSecret = "YYYYY";
    string profileId = "ZZZZZZZZZZ";
    
    Client client = new Client(consumerKey, consumerSecret);
    Profile profile = client.GetProfileById(profileId);


Client initialization
-------------

    string consumerKey = "XXXXX";
    string consumerSecret = "YYYYY";
    
    Client client = new Client(consumerKey, consumerSecret);

*Parameters*:

    string consumerKey - consumer key
    string consumerSecret - consumer secret

API Items
-------------

## Get profiles by phone ##

    IList<Profile> profiles = client.GetProfilesByPhone(number);
	
	string firstName = profiles[0].FirstName;
    
*Parameters*:
    
    string number - phone number
	
*Returns*:

	IList<Profile> profiles - list of profiles, associated with a particular phone number
	
## Get profile by id ##

    Profile profile = client.GetProfileById(profileId);
	
	string firstName = profile.FirstName;
    
*Parameters*:
    
    string profileId - id of a profile
	
*Returns*:

	Profile profile - profile with a specific id
	
### Update profile by id ###

	ProfilePostRequest postProfile = new ProfilePostRequest
	{
		FirstName = "NewFirstName",
		LastName = "NewLastName"
	};
	
    client.PostProfile(postProfile, profileId);
    
*Parameters*:

    ProfilePostRequest postProfile - profile to post
    string profileId - id of profile to update 

### Get profiles by phone in json ###

    string responseInJson = client.GetProfilesByPhoneJson(number);
	
*Parameters*:
    
    string number - phone number

*Returns*:

	string response - server response content in json
	
### Get profile by id in json ###

    string responseInJson = client.GetProfileByIdJson(profileId);
    
*Parameters*:
    
    string profileId - id of a profile
	
*Returns*:

	string response - server response content in json
	
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
