# Website FlowerShop

## Information
- **Tools:** SQL Server, Visual Studio
- **Framework:** ASP.NET Core, Entity Framework (Entity Framework Code First)

## Run project
**NOTE:** To run the project, please install and use Visual Studio version 17.8.x or later.

1. Clone the project
    ```
    git clone https://github.com/PhucPhuc1304/LapTrinhWeb-HoaTuoiBaSanh.git
    ```

2. Edit **ConnectionStrings** in `appsinstall.json`
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=LAPTOP-HINEBV60;Database=HoaTuoiBaSanh;User Id=sa;Password=123;TrustServerCertificate=True;MultipleActiveResultSets=True"
    }
    ```

3. Run the `Enable-Migrations` command in Package Manager Console
    ```
    Enable-Migrations
    ```

4. Applying database migrations
    ```
    Update-Database
    ```

5. Run project
