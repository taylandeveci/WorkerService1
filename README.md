# WorkerLogger – .NET Worker Service for Pokémon API Monitoring

`WorkerLogger` is a .NET 8 Worker Service that runs in the background and periodically interacts with the Pokémon Web API to fetch data and log it locally. It's designed for continuous, automated monitoring or batch processing in a background service context.

## Features

- ⏱️ Periodic polling of the Pokémon Web API (e.g., every 15 seconds)
- 📄 Fetches Pokémon data automatically using `HttpClient`
- 📝 Logs the results with timestamps
- 💾 Writes logs to a local `.txt` file (`log.txt`)
- ⚙️ Runs as a background service integrated into a .NET solution

## Technologies Used

- ✅ .NET 8 Worker Service Template
- ✅ `HttpClient` for REST calls
- ✅ `ILogger<T>` for structured logging
- ✅ File I/O for custom persistent logging

## Setup Instructions

1. Ensure the API (`WebApplication1`) is running and accessible at `https://localhost:7295`.
2. In Visual Studio:
   - Right-click the **Solution** → **Set Startup Projects** → Select `Multiple startup projects`
   - Set both `WebApplication1` and `WorkerLogger` to **Start**
3. Run the solution with `F5`. Both projects will start.

## Sample Output

