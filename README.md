# EssentialJS2WebApplication1
Web Application For File Manager
Configure Essential JS 2 in the application
1. Add the Syncfusion.EJ2.AspNet.Core NuGet package to the new application by using the Nuget Package Manager. Right-click the project and select Manage NuGet Packages….

2. Search the Syncfusion.EJ2.AspNet keyword in the Browse tab and install Syncfusion.EJ2.AspNet.Core NuGet package in the application.
   aspnetcore2.x install nuget packages
   The Essential JS 2 package will be included in the project, after the installation process is completed.

   Note: The Syncfusion.EJ2.AspNet.Core NuGet package has dependencies, Newtonsoft.Json for JSON serialization and Syncfusion.Licensing for validating Syncfusion license key.

3. Open ~/Views/_ViewImports.cshtml file and import the Syncfusion.EJ2 package.

   @addTagHelper *, Syncfusion.EJ2
4. Add the client-side resources through CDN or local npm package in the <head> element of the ~/Views/Shared/_Layout.cshtml layout page.
   <head>
      <div markdown="1">
       <!-- Syncfusion Essential JS 2 Styles -->
       <link rel="stylesheet" href="https://cdn.syncfusion.com/ej2/material.css" />

       <!-- Syncfusion Essential JS 2 Scripts -->
       <script src="https://cdn.syncfusion.com/ej2/dist/ej2.min.js"></script>
      </div>
   </head>
5. Add the Essential JS 2 Script Manager at the end of <body> element in the ~/Views/Shared/_Layout.cshtml layout page.
   <body>
      <div markdown="1">
       <!-- Syncfusion Essential JS 2 ScriptManager -->
       <ejs-scripts></ejs-scripts>
      </div>
   </body>

6. Now, add the Syncfusion Essential JS 2 components in any web page (cshtml) in the Views folder.
   For example, the calendar component is added in the ~/Views/Home/Index.cshtml page.
   <div markdown="1">
       <ejs-calendar id="calendar"></ejs-calendar>
   </div>
   Run the application. The Essential JS 2 calendar component will render in the web browser.
