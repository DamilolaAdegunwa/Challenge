 
# Title
[Challenge front-end y back-end.](https://wcaangularclient.azurewebsites.net/admin/users)
 

## Getting Started 
- Clone the git repo — `git clone https://github.com/ZuccatoAgustin/Challenge.git` 
## Prerequisites 
### Front-end 

            Set up the Development Environment
            Install Node.js® and npm if they are not already on your machine.
            
            Angular CLI is a command line interface for the latest Angular. Install it before start with the Angular app.
            
            npm install --global @angular/cli@latest
            
            If you have Angular CLI installed previously, update it to the latest Angular CLI. Remove the older version and re-install it.

            npm uninstall --global @angular/cli
            npm install --global @angular/cli@latest

### Back-end
[net core 2.1](https://dotnet.microsoft.com/download/dotnet-core/2.1)

## Build
### Front-end
   Run in PS or CMD =>    `AngularClient\ClientApp\ng serve`
### Back-end
   Open  "WebAppChallenge.sln" in Visual Estudio  and start project WAC.WebService.Admin (WAC.WebService.Admin.csproj)
   
## Deployment
### Front-end
 - `npm  run build-prod`  
 ### back-end
 
   ##### documentation
 - [Metronicbuild](https://keenthemes.com/metronic/?page=docs&section=angular%2Fdeployment#docs)
 -  [Continuously build, test, and deploy](https://azure.microsoft.com/en-us/services/devops/pipelines/)

## Technologies
 
* [Metronic](https://keenthemes.com/metronic/) 
* [Bootstrap](https://getbootstrap.com/) 
* [Angular material](https://material.angular.io/)
    * A paging control of the angular material library that has its own test case was used for this website  [MatPaginator](https://github.com/angular/material2/blob/master/src/lib/paginator/paginator.spec.ts) 
