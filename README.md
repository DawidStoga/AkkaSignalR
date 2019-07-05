# A Reactive system ![GitHub release](https://img.shields.io/github/release/ajeetx/ko.signalr.selfhost.akka.svg?style=for-the-badge)![Maintenance](https://img.shields.io/maintenance/yes/2019.svg?style=for-the-badge)

| ![GitHub Release Date](https://img.shields.io/github/release-date/ajeetx/ko.signalr.selfhost.akka.svg?style=plastic) | ![Website](https://img.shields.io/website-stable-offline-green-red/http/ajeetx.github.io/ko.signalr.selfhost.akka.svg?label=status&style=plastic)|[![Build Status](https://travis-ci.org/AJEETX/ko.signalr.selfhost.akka.png?branch=master&style=for-the-badge)](https://travis-ci.org/AJEETX/ko.signalr.selfhost.akka)|[![.Net Framework](https://img.shields.io/badge/DotNet-4.5.2-blue.svg?style=plastic)](https://www.microsoft.com/en-au/download/details.aspx?id=42642) | ![GitHub language count](https://img.shields.io/github/languages/count/ajeetx/ko.signalr.selfhost.akka.svg?style=plastic)| ![GitHub top language](https://img.shields.io/github/languages/top/ajeetx/ko.signalr.selfhost.akka.svg) |![GitHub repo size in bytes](https://img.shields.io/github/repo-size/ajeetx/ko.signalr.selfhost.akka.svg) 
| ---          | ---        | ---      | ---       | --- | --- | --- |

>  akka.net based highly scalable reactive web-application


<img width="1469" alt="reactivesystem" src="https://user-images.githubusercontent.com/16511837/30899573-bfa516e0-a3a3-11e7-9783-1cfd3a4934fd.png">

## Purpose of statement
```
The project intends to have 
 (a) an http-service-based continuous long-running task with cancellation
 (b) Pushing real-time progress/update through Signalr
```

#### Application list and details

| App Name| Project Type | Comments|
| --- | --- | --- |
| Akka.DB| Console |self-hosted akka.net[Actor-model], Run independently, Database interaction|
| Akka.Service | Console  | self-hosted akka.net[Actor-model], Run independently, business logic|
| Infrastructure| Class Library |Common / shared resources project|
| SelfHostedAPI | Console  | self-hosted web-api with signalr push, Run independently|
| WebAPIClient | MVC  | front-end website, uses knockooutjs with signalr,Run independently |


 ## Configuration / Setup :
 > Environment:  VS2015 + MSSql Server
```
1. Clone/download the repository from git
2. Create Database by name="AkkaData" in Sql Server
3. Restore nugets
4. Build Solution
5. If Build goes good, then we run the projects in sequence.
```

##### Steps to run the applications  [in sequence]
> 1. Run the Akka.DB console project [in non admin mode, in case of issue]

	Database actor system is up-and running

> 2. Then run Akka.Service console project

	Service actor system is up-and running

> 3. Run the SelfHostedapi console project in admin mode

	Self hosted api is up-and running

> 4. Run WebAPIClient

	home.html page displays the data.

![Alt text](/page.png?raw=true "Home page")
	
	
  > Please do CRUD operation and have play around.

### Support or Contact

Having any trouble? Check out our [documentation](https://github.com/AJEETX/ko.signalr.selfhost.akka/edit/master/README.md) or [contact support](mailto:ajeetkumar@email.com) and we’ll help you sort it out.

|  Counter    | Contributor | Disclaimer
| ---  | --- | --- |
| [![HitCount](http://hits.dwyl.io/ajeetx//ko.signalr.selfhost.akka/projects/1.svg)](http://hits.dwyl.io/ajeetx//ko.signalr.selfhost.akka/projects/1)| ![GitHub contributors](https://img.shields.io/github/contributors/ajeetx/ko.signalr.selfhost.akka.svg?style=plastic)|![license](https://img.shields.io/github/license/ajeetx/ko.signalr.selfhost.akka.svg?style=plastic)
