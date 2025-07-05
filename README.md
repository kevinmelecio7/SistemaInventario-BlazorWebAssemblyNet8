
<h1 align="left">Physical Inventory Application V2💻📦</h1>
<p align="left">✨ My name is Kevin, and I like having a repository of the projects I´ve done since high school.
  <br>
  📚 I´m a Computer Systems Engineer. I like to program web and mobile applications with business solution or just for fun.
  <br>
  🎯 Goals: Learn new technologies to create innovative solutions
</p>

###

<p align="center">
  <img src="https://media3.giphy.com/media/v1.Y2lkPTc5MGI3NjExcjU2enZnbXVlcjh3d3R4NGE3eWNhYW4wNXZ4dHl2NjhwbXE5bnk1eSZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/ECCDnScTsxs0MQ3qDt/giphy.gif" alt="GIF" width="500" height="300">
</p>

###

## 🌐 Description Project
<p align="left">The application assists with the digital recording process of the physical count of all item in a warehouse or business to verify their actual cuantity and compare it with the accounting records or the application used by the company.   <ins><b>The aplication includes a robust authentication and authorization system.</b></ins> There are two types of access: Administrator and User</p>
<a target="_blank" align="center">
  <img align="right" top="800" height="200" width="200" alt="GIF" src="https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExYWt5MzFwYnk2MTMybTE1ZG0wYzEzMnN2cHRxemZzeXFxcjVtb3YwayZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/YqEtJ56h8z82FsxHAZ/giphy.gif">
</a>
<p align="left">The application process begins with the Administrator role, who must enable a "Period". To create the period, simply select the month and year in wich the Physical Inventory will be taken, must also add an "Identifier" for the inventory. The "identifier" is important for the administrator because it allows them to distinguish the inventories performed. Some examples of "Identifiers" are: Week Number, Storage Sections, Material Sections, Company plant, etc. The Administrator must also upload the accounting records the company wishes to count using 3 Excel Files to compare what is currently in the system with what is counted in the inventory. The 3 Excel files that the Administrator must upload to the applications: <ins>Storage Bin</ins> (locations), <ins>Materials</ins> and <ins>Initial Load</ins> (values of company is stock of materialsat the beginning of an accounting period).</p>

<p align="left">The user role will perfom the physical count and record it in the application. The counting proccess begins with the identification of the material and location; this information is obteined through labels containing 2D codes. The usermust write or scanner to read the material and location codes. Then, the user will count and the quantity counted in pieces will be recorded. If the user's record is not included in the Initial Load fileby the Administrator, it is saved as an "Additional" item in the report to be considered in the final report. </p>

<p align="left">Only the Administrator role will be able to view, modify and delete user's records. This view is a report that show the inventory differences between Initial Load file by Admistrator uploaded and the Inventory count by applictation.</p>
<a target="_blank" align="center">
  <img align="right" top="100" height="50" width="100" alt="GIF" src="https://unaaldia.hispasec.com/wp-content/uploads/2013/09/950a5-logo_sap-jpg.gif">
</a>
<p align="left">As result, the Administrator can download 3 types of reports (The administrator can download the report of several inventories, the administrator must indicate the "Identifies" that want to download):
  <br/>1- Detailed Report for each user record - Excel File
  <br/>2- Total Report by material and location - Excel File
  <br/>3- Total Report by material and location for SAP - Text File
  <br/>The generation of the lastest report is configured to procedure a single text file for each block of 330 processed materials. This file helps the company's administrators upload the file to the SAP system
</p>



## 🖼️ Screenshots

### 🔐 Interactive login with validation
<p align="left">
  <img src="https://raw.githubusercontent.com/kevinmelecio7/SistemaInventario-BlazorWebAssemblyNet8/master/Login.png" alt="Home Page" width="200">
</p>

### 🏠 Home page Administrator
<p align="left">
  <img src="https://raw.githubusercontent.com/kevinmelecio7/SistemaInventario-BlazorWebAssemblyNet8/master/Home%20Page%20Administrator.png" alt="Home Page" width="400">
</p>


### 📱 Home page User
<p align="left">
  <img src="https://raw.githubusercontent.com/kevinmelecio7/SistemaInventario-BlazorWebAssemblyNet8/master/Home%20Page%20User.png" alt="Home Page" width="400">
</p>

### 📊 Dashboard with dynamic statistics
<p align="left">
  <img src="https://raw.githubusercontent.com/kevinmelecio7/SistemaInventario-BlazorWebAssemblyNet8/master/Dashboard.png" alt="Home Page" width="400">
</p>


<h2 align="left">🚀 Technologies</h2>

###

[![Programming](https://skillicons.dev/icons?i=cs,net,visualstudio)](https://skillicons.dev)
<!--<div align="left">
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" alt="csharp logo"  />
  <img width="2" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/bootstrap/bootstrap-original.svg" alt="bootstrap logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/visualstudio/visualstudio-plain.svg" alt="visualstudio logo"  />
</div>-->
<p align="left">Built with .NET 8 using the <ins><b>Blazor Server Model</b></ins> for direct access to databases, files, etc. The logic and data are server-siide, ideal for apps that require server-side logic. </p>
<p align="left">The application uses  <ins><b>JWT (JSON Web Token)</b></ins> for authentication and authorization in the web application and secure transmission of information. </p>
<p align="left">For the application design (front-end) use <ins><b>Radzen</b></ins>. It is a tool and set of UI (user interface) components for applications like Blazor. Radzen contains a free and open-source collection of components such as Forms, Layouts, Data, etc. It saves design time and visual logic, making it ideal for dashboard, businnes apps, administratives apps, etc.</p>
<br/>

###


## 📦 Características

- 🔐 Authentication with JWT
- 📱 Responsive design
- 🧩 Reusable components
- ⚡ REST services for API consumption
- 📈 Visual dashboard with graphs and tables
- 📁 File management (Excel, Text File)
- 🎨 Custom design with Radzen

<h2 align="left">⚙️ Documentation and Instalation</h2>

###

<div align="left">
  <p align="left">The installation documentation and explanations are a little extensive, so I preferred to use the documents, which help you understand the system's procedures. I hope this helps you 😁 </p>
  <p align="left"> User Manual (https://goo.su/cFUY)</p>
  <p align="left"> Code Documentation (https://goo.su/IOOwV)</p>
</div>
<br/>

