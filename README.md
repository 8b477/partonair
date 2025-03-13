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
- **Loki** Gestion des logs
- **Grafana** Visualition et analyse de log

--------
--------

# <a name="docker">Cheat Sheet</a>

- **First build**: `docker-compose up --build`
- **Start (background)**: `docker-compose up -d`
- **Check active containers**: `docker ps`
- **Stop containers**: `docker-compose down` (ou `docker stop {ID_Container}`)
- **Remove all ☣️**: `docker system prune -f`
- **Remove containers**: `docker rm {ID_Container1} {ID_Container2}`
- **Remove images**: `docker rmi {ID_Image1} {ID_Image2}`
- **Check logs**: `docker-compose logs -f`
- **Check target container logs**: `docker logs {ID_Container}`

URl Swagger UI : http://localhost:5000/swagger/index.html
URL Grafana : http://localhost:3000
Tous les logs de connexion et ports exposés sont consultables par service dans le fichier : *docker-compose.yml*