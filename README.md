PortfolioManagment system
✔ Store & manage investment records in a SQL Server database.
✔ Provide API endpoints for retrieving, adding, and deleting investments.
✔ Use Entity Framework Core for seamless database management.
✔ Swagger UI for easy API testing.
✔ Future-proof design with authentication & stock data integration planned.
+ React.js Frontend + Tailwind css + Integrating Stock Market Data into the Portfolio Management System (free Alpha Vantage api)

How It Works (Flow)
1 User enters investment details (ticker, shares, price, date).
2 React sends a POST request to /api/Portfolio/AddInvestment.
3 ASP.NET Core saves the data in the SQL Server database.
4 Response sent back confirming success or failure.


Portfolio Management System (Backend) - Features & Functionality
This ASP.NET Core Web API project serves as the backend for a Portfolio Management System, allowing users to manage their investments. It provides functionalities such as storing investments in a database, retrieving portfolio details, adding new investments, and deleting investments.

1. Features of the Backend System
 User Investment Management
The API allows users to:
 Add new investments (e.g., buying stocks).
 Retrieve all investments for a specific user.
 Delete an investment from the portfolio.
 
2. Backend Architecture Overview
The system follows a structured architecture using ASP.NET Core Web API with Entity Framework Core (EF Core) and SQL Server for data storage.

 Technologies Used:

ASP.NET Core Web API --> Backend framework
Entity Framework Core --> ORM for database interaction
SQL Server → Database storage
Swagger → API testing interface
JWT (Planned) → Secure authentication
React.js Frontend + Tailwind css 
Integrating Stock Market Data into the Portfolio Management System (free Alpha Vantage api)

--------------------------------------------------------------------------------------------------------------------------------------

3. Detailed Explanation of Each Component
 Step 1: Project Setup
The project is created using ASP.NET Core Web API.
Required dependencies such as Entity Framework Core, SQL Server, and Newtonsoft.Json are installed.
 Step 2: Database Configuration
The connection to the SQL Server database is set up inside appsettings.json:

json

"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=PortfolioDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
 What this does:
This ensures the API can connect to the SQL Server database named PortfolioDB.

 Step 3: Database Models & DbContext
A C# model (Investment.cs) is created to represent investment records:

csharp

public class Investment
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Ticker { get; set; }
    public int Shares { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime PurchaseDate { get; set; }
}
 What this does:
This defines the data structure, including:

Id → Unique identifier for each investment.
UserId → Identifies the user who owns the investment.
Ticker → The stock symbol (e.g., AAPL for Apple, TSLA for Tesla).
Shares → Number of shares owned.
PurchasePrice → The price at which the shares were bought.
PurchaseDate → The date of purchase.
🔹 Step 4: Database Context
A DbContext (PortfolioDbContext) is used for database operations:

csharp

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }
    public DbSet<Investment> Investments { get; set; }
}
 What this does:

Defines the Investments table in the database.
Allows interaction with the database using EF Core.
 Step 5: API Controllers
The API exposes endpoints for managing investments via the PortfolioController.

✔️ Get Portfolio by User
 Endpoint: GET /api/Portfolio/GetPortfolio/{userId}
 What it does: Returns all investments for a given userId.

csharp

[HttpGet("GetPortfolio/{userId}")]
public async Task<ActionResult<IEnumerable<Investment>>> GetPortfolio(string userId)
{
    return await _context.Investments.Where(i => i.UserId == userId).ToListAsync();
}
✔️ Add a New Investment
 Endpoint: POST /api/Portfolio/AddInvestment
 What it does: Adds a new investment record.

csharp

[HttpPost("AddInvestment")]
public async Task<ActionResult> AddInvestment(Investment investment)
{
    _context.Investments.Add(investment);
    await _context.SaveChangesAsync();
    return Ok();
}

 Delete an Investment
 Endpoint: DELETE /api/Portfolio/DeleteInvestment/{id}
 What it does: Deletes an investment by ID.

csharp

[HttpDelete("DeleteInvestment/{id}")]
public async Task<ActionResult> DeleteInvestment(int id)
{
    var investment = await _context.Investments.FindAsync(id);
    if (investment == null) return NotFound();
    _context.Investments.Remove(investment);
    await _context.SaveChangesAsync();
    return Ok();
}
 Step 6: Running Database Migrations
 What this does:
Creates the database schema automatically.

powershell

dotnet ef migrations add InitialCreate
dotnet ef database update
 Step 7: API Testing with Swagger
Once the application is running (F5 in Visual Studio), you can test the API endpoints in Swagger at:
 http://localhost:5000/swagger

Available API Endpoints in Swagger
GET /api/Portfolio/GetPortfolio/{userId} → Fetch user’s investments.
POST /api/Portfolio/AddInvestment → Add new investment.
DELETE /api/Portfolio/DeleteInvestment/{id} → Remove investment.
