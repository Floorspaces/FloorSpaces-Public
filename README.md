# Floorspaces (Created Winter-Sprint '24) (Next.js, .NET, Supabase, Unity WebGL + Azure Web Services)

```

Frontend: `npm install` & `npm run start` (Check package.json for confirmation or reconfig)

Backend: `dotnet restore` & `dotnet run`

SQL Database: Either edit an appsettings.json to place in the backend root folder (setup a Supabase project), or reconfigure this as your own. The schema we used should correspond to the file "schema.svg" but check with us on this, it might be an outdated image. Using other Azure services, rewriting this to work with an Azure DB or switching to a different platform altogether is the way to go.

NoSQL Bucket Storage: we used IBM Cloud's free tier bucket storage (built on top of AWS S3) (up to 25gb free) to store Unity related files in the cloud, reconfigure the key settings or switch this storage configuration altogether.

Unity Interfaces: check your bucket storage configurations, then you're good to go!

```

Floorspaces is a digital twin concept using Unity & web development architecture to build a web app that can draw and represent **3D maps with business & user data.** This idea of "digital twin" apps has been around
for a few years now, little did we know at the time of beginning the project. There's certainly tons of features that can be expounded upon to create a unique product and user experience for your application if you'd
like to create business floor models; you could extend this with the Unity XR library (among others) to pair your 3D live business map to meeting spaces held in and modeled for VR. MappedIn, a great example of this 
"digital twin" concept we've found recently, allows you to generate maps with AI using the camera on your phone to scan your venue's surroundings. Some of the most developed software in this space has been targeted towards
real estate insterests, essentially you can take the emerging field of digital twin applications and likely integrate it into your own industry somehow.

As two senior students in the University of Akron Computer Science program, we looked to create something new and marketable for our senior design capstone project. Floorspaces both offered something of value to us, as well as offering (A LOT) of future work ahead of us to work on as a side project. One day we hoped to make fluid the business data, architecture UI, and 3D map. We were able to present this deliverable on design day to Senior Seminar, Computer Graphics, (classes) and also the engineering design showcase. (Hosted in Akron)

![image](https://github.com/Floorspaces/FloorSpaces-Public/assets/93809439/adc0a0bf-a0da-4ded-91ec-145a4320c9ac)

After presenting our working prototype to these classes and the showcase using Vercel, Azure, & IBM Cloud, we were accepted to join this Summer's I-Corps Cohort in Akron (A startup incubator connected to the UA Research Foundation) as well as for a Fitzgerald Institute for Entrepreneurial Studies’ Morgan Startup Grant. After learning of the competition out on the market for such products, with limited market, we declined the grant & cohort membership this Summer seeing the long road ahead of us to develop a competitive graphics application with just 2 developers. Using this concept (the prototype here is in the repo) you can either make your own digital twin platform or try integrating it as a feature into a large application. You can also take the Unity interfaces and port them to any device like desktop, mobile, VR, or game consoles.

-- Floorspaces Development Team, @nicklim011213 @eseer-divad
