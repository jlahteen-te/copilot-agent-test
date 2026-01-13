# copilot-agent-test

Finnish SSN (Social Security Number) Validator - A .NET console application for validating Finnish social security numbers.

## Build Status

✅ **The code builds successfully!**

## Prerequisites

- .NET 10.0 SDK or later

## Building the Project

To build the entire solution:

```bash
dotnet build
```

To build individual projects:

```bash
# Build the main application
dotnet build FiSsnValidator/FiSsnValidator.csproj

# Build the test project
dotnet build FiSsnValidator.Tests/FiSsnValidator.Tests.csproj
```

## Running Tests

To run all tests:

```bash
dotnet test
```

All 17 tests pass successfully.

## Running the Application

To validate a Finnish SSN:

```bash
dotnet run --project FiSsnValidator -- <Finnish SSN>
```

Example:

```bash
dotnet run --project FiSsnValidator -- 131052-308T
```

## Project Structure

- `FiSsnValidator/` - Main console application
- `FiSsnValidator.Tests/` - Unit tests using xUnit