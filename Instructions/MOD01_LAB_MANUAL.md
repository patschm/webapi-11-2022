
# Module 1: Querying and Manipulating Data Using Entity Framework

# Lab: Creating a Data Access Layer using Entity Framework 

#### Scenario

In this lab, you will use the Entity Framework Core to connect to an SQL Database. 

#### Objectives

After completing this lab, you will be able to:

- Create a DAL layer.
- Create an entity data model by using Entity Framework Core.

### Exercise 1: Creating a data model

#### Scenario

In this exercise, you will create the data access layer and connect to the database by using Entity Framework Core
to perform CRUD operations on the SQL Express database.

#### Task 1: Create a class library for the data model

1. Open a command prompt.
2. Browse to the following path **[Drive:]\Allfiles\Mod01\LabFiles\Starter**, and then create a new **Class Library** using dotnet tools and name it **ProductReviews.DAL.EntityFramework**.
3. Use the command prompt to install the  **Microsoft.EntityFrameworkCore** package.
4. Use the command prompt to install the  **Microsoft.EntityFrameworkCore.SqlServer** package.
5. Open the project with Microsoft Visual Studio Code.
6. Add a new folder to the project and name it **Entities**.

#### Task 2: Create data model entities using the code-first approach

1. Create a new class and name it **ProductGroup**, and then add the following properties:
    ```cs
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    ```
2. Create a new class and name it **Product**, and then add the following properties:
    ```cs
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int? BrandId { get; set; }
    public int? ProductGroupId { get; set; }
    public Brand Brand { get; set; }
    public ProductGroup ProductGroup { get; set; }
    public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    ```
3. Create a new class and name it **Brand**, and then add the following properties:
    ```cs 
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    ```
4. Create a new class and name it **Review**, and then add the following properties:
    ```cs
    public int Id { get; set; }
    public string Author { get; set; }
    public string Email { get; set; }
    public string Text { get; set; }
    public int Score { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    ```

#### Task 3: Create a DbContext

1. Create a new folder and name it **Database**.

2. Create a new class that inherits from **DbContext** and name it **ProductReviewsContext**.

3. Add **DbSet** property to each of the models that you created in the previous task.

4. Paste the following code for the constructor:
    ```cs
    private void InitialDBContext()
    {
        //DbInitializer.Initialize(this);
    }
    // Constructor with options
    public ProductReviewsContext(DbContextOptions<ProductReviewsContext> options)
            : base(options)
    {
        InitialDBContext();
    }
    ```

6. Build the project and correct any errors.

>**Results** Your application now has a functioning data access layer. 



### Exercise 2: Query your database

#### Scenario

In this exercise, you will use the DAL class library to create a new console application
to display all the data from the database.


#### Task 1: Copy DbInitializer with dummy data

1. Copy the file **[Drive:]\AllFiles\Mod01\Assets\DbInitializer.cs** into the **Database** folder.

3. Modify the code to reflect your classes and namespaces.
   
3. In the **Initialize** method:

    - Make sure that the database was created. 
    - If the database was created for the first time, use the **Seed** method.

4. In the **Seed** method Add **ProductGroups**, **Products**, **Brands** and **Reviews** to the context.

4. Uncomment the method **DbInitializer.Initialize(this)** in **InitializeContext**. (ProductReviewsContext)

    

#### Task 2: Write a LINQ query to query the data

1. Close the Visual Studio Code window.
2. Switch to the command prompt.
4. In the **[Drive:]\Allfiles\Mod01\LabFiles\Starter** path, create a new **Console Application** and name it **ProductReviews.DatabaseConsole**.
5. Open the folder in Visual Studio Code.
8. In **ProductReviews.DatabaseConsole.csproj**, add a reference to the **ProductReviews.DAL.EntityFramework** project.
9. In the **ProductReviews.DatabaseConsole** folder, navigate to **Program.cs**, and locate the **Main** method (not there in .NET6. Program.cs is the main).
7. Configure the SqlServer database with connectionstring **Server=.\SQLEXPRESS;Database=Mod1DB;Trusted_Connection=True;MultipleActiveResultSets=True;**
8. Create a new **ProductReviewsContext** instance.
9. Display the following info on the screen:
   - The name of the ProductGroup.
   - The product name and brand name for the products belonging to the ProductGroup.
10. From the command prompt, run the **ProductReviews.DatabaseConsole**application.

   >**Results** Your application can now display all of the data from the database by using LINQ queries.



# Lab: Manipulating Data

#### Scenario

In this lab, you will create a repository with CRUD methods and inject two kinds of database configurations to work with SQL Express and SQLite.

#### Objectives

After completing this lab, you will be able to:

- Create a ProductGroup repository and populate it with CRUD methods.
- Test the queries with SQLite database.

### Exercise 1: Create repository methods

#### Scenario

In this exercise, you will create a productgroup repository.

#### Task 1: Create a method to add entities

1. Open a command prompt.

2. To change directory to the startup project, run the following command:
    ```bash
    cd [Drive:]\Allfiles\Mod01\LabFiles\Starter
    ```
    
3. Copy the folder **[Drive:]\\AllFiles\Mod01\Assets\ProductReviews.Interfaces** into the starters folder

4. Create a new **Class Library** with name **ProductReviews.Repository.EntityFramework**.

5. Add a reference to **ProductReviews.Interfaces**

6. Add a reference to **ProductReviews.DAL.EntityFramework**

7. Open the folder in Visual Studio Code.

8. Create a new class **ProductGroupRepository** and implement  the interface **IProductGroupRepository**

9. In **ProductGroupRepository** create a field for your **ProductReviewsContext** and initialize it through the constructor (as an argument)

9. Implement the interface methods
>**Results** You created a repository with the **GetAsync, AddAsync, UpdateAsync, DeleteAsync** methods.



#### Exercise 2: Test the model using InMemory Database Using Sqlite

#### Scenario

In this exercise, you will inject  an **InMemory** Database into the repository,

#### Task 1: Create test code

1. Open a command prompt.

2. To change the directory to the **Starter** folder, run the following command:
   ```bash
    cd [Drive:]\Allfiles\Mod01\LabFiles\Starter
   ```
   
3. Create a new **xUnit Test Project** and name it **ProductReviews.Repositories.Tests**.

4. Add package **Microsoft.EntityFrameworkCore.Sqlite**

5. Add a reference to **ProductReviews.Repository.EntityFramework**.

6. Add a reference to **ProductReviews.DAL.EntityFramework**.

7. From the command prompt, open Visual Studio Code.

8. Install the Visual Studio Code Extension **.NET Core Test Explorer**

9. Locate the **UnitTest1.cs** file and rename it **ProductGroupRepositoryTests**.

10. In **ProductGroupRepositoryTests** add the following code

```csharp
private static SqliteConnection CreateInMemoryDatabase()
{
    var connection = new SqliteConnection("Filename=:memory:");
    connection.Open();
    return connection;
}

 private ProductReviewsContext CreateContext()
 {
     var optionsBuilder = new DbContextOptionsBuilder<ProductReviewsContext>();
     optionsBuilder.UseSqlite<ProductReviewsContext>(CreateInMemoryDatabase());
     return new ProductReviewsContext(optionsBuilder.Options);
 }
```

11. Change **public void TestMethod1()** to **public async Task TestPagingAsync()**.
12. Create an instance of the **ProductGroupRepository** and call **GetAsync(1, 10)**
13. Check if the result is not null.
14. Check if the result contains 10 items.
15. Run the test

#### Task 2: Add more tests

1. Add the following test.

```csharp
[Fact]
public async Task TestGetByIdAsync()
{
    var repo = new ProductGroupRepository(CreateContext());
    var result = await repo.GetByIdAsync(1);

    Assert.NotNull(result);
    Assert.IsType<ProductGroup>(result);
}
[Fact]
public async Task TestInsertAsync()
{
    var repo = new ProductGroupRepository(CreateContext());
    var tmp = new ProductGroup { Name = "Test"};
    var result = await repo.AddAsync(tmp);
    
    Assert.NotNull(result);
    Assert.True(result.Id > 0);
}
[Fact]
public async Task TestUpdateAsync()
{
    var repo = new ProductGroupRepository(CreateContext());
    var tmp = await repo.GetByIdAsync(1);
    tmp.Name = "Test";
    var result = await repo.UpdateAsync(tmp);
    
    Assert.NotNull(result);
    Assert.True(result.Name == "Test");
}
[Fact]
public async Task TestDeleteAsync()
{
    var repo = new ProductGroupRepository(CreateContext());
    await repo.DeleteAsync(1);
    var result = await repo.GetByIdAsync(1);
    
    Assert.Null(result);
}
[Fact]
public async Task TestProductsAsync()
{
    var repo = new ProductGroupRepository(CreateContext());
    var tmp = await repo.GetByIdAsync(1);
    var result = await repo.GetProductsAsync(tmp.Id);

    Assert.NotNull(result);
    Assert.True(result.Count > 0);
}
```

2. Run the unit test from within Visual Studio Code using the extension **.NET Core Test Explorer**

