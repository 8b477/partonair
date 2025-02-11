------
------
# <div align="center"> 🛠️ Outils et Technos utilisés dans le Projet</div> 
------- 
------- 

## 🖥️ Backend
- **ASP.NET Core API**
- **Framework** : .NET 8  
- **Langage** : C# 12  
- **MediatR**
- **CQRS**
- **UnitOfWork**
- **Problem Details : [RFC 9457](https://www.rfc-editor.org/rfc/rfc9457)**
- **FluentValidation**

## 🗃️ Base de données
- **SQL Server**  
- **Entity Framework Core (ORM)**  
- **T-SQL**
- **Modélisation : [Looping](https://www.looping-mcd.fr)** 

## 🔒 Sécurité
- **JWT**
- **OAuth 2.0** 
- **BCrypt**

## 🧪 Tests
- **xUnit**

## 🐳 Conteneurisation
- **Docker** - [Cheat Sheet](#docker)

## 📦 Packages NuGet principaux

### 🪄 Couche API
- MediatR  
- Microsoft.EntityFrameworkCore.SqlServer  
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets  
- Swashbuckle.AspNetCore  
- Microsoft.AspNetCore.Authentication.JwtBearer  

### 🧠 Couche Application
- MediatR  
- FluentValidation.AspNetCore  
- BCrypt.Net-Next
  
### 🗃️ Couche Infrastructure
- Microsoft.EntityFrameworkCore  
- Microsoft.EntityFrameworkCore.SqlServer  

### 🧪 Couche Tests
- xUnit.net

--------
--------

# Cheat Sheet

- **First build**: `docker-compose up --build`
- **Start (background)**: `docker-compose up -d`
- **Check active containers**: `docker ps`
- **Stop containers**: `docker-compose down` (ou `docker stop {ID_Container}`)
- **Remove all ☣️**: `docker system prune -f`
- **Remove containers**: `docker rm {ID_Container1} {ID_Container2}`
- **Remove images**: `docker rmi {ID_Image1} {ID_Image2}`
