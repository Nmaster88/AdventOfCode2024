# Advent of Code 2025 - C# Solutions

This repository contains my solutions for the **Advent of Code 2025** challenges, implemented in **C#** using the .NET platform. At this stage, it includes solutions for **Day 1** through **Day 6**.

## Table of Contents

- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Days and Solutions](#days-and-solutions)
- [Dependencies](#dependencies)
- [How to Run](#how-to-run)
- [License](#license)


## Project Structure

The project follows a simple structure for clarity and maintainability:

```plaintext
AdventOfCode2025/
├── Day1.cs/
│   ├──File/input.txt
├── Day1Tests.cs    
│   
├── Day2.cs/
│   ├──File/input.txt
├── Day2Tests.cs  
├── ...
├── AdventOfCode2024.sln
└── README.md
```

## Key Components

- **DayX**: Contains the solution and input for each day's challenge.
- **Tests**: Unit tests for verifying the correctness of solutions.

---

## Getting Started

### Prerequisites

- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) or higher installed on your machine.

### Clone the Repository

```bash
git clone https://github.com/yourusername/AdventOfCode2024.git
cd AdventOfCode2024
```

### Build the Solution

Restore dependencies and build the solution using the command:

```bash
dotnet build
```

---

## Days and Solutions

| Day | Challenge                              | Solution Status |
|-----|----------------------------------------|-----------------|
| 1   | Day 1 Challenge                        | ✅ Completed    |
| 2   | Day 2 Challenge                        | ✅ Completed    |
| 3   | Day 3 Challenge                        | ✅ Completed    |
| 4   | Day 4 Challenge                        | ✅ Completed    |
| 5   | Day 5 Challenge                        | ✅ Completed    |
| 6   | Day 6 Challenge                        | ✅ Completed    |

---

## Dependencies

The following libraries are used in the project:

- xUnit: For unit testing.
- FluentAssertions: For expressive assertions in tests.
- System.Linq: For functional-style operations on collections.

Install additional NuGet packages as needed using the command:

```bash
dotnet add package <package-name>
```

---

## How to Run

### Running a Solution

Execute a specific day's solution by running:

```bash
dotnet run --project DayX
```

Replace DayX with the specific day you want to run, such as Day1 or Day2.

### Running Tests

Run all tests using the command:

```bash
dotnet test
```

---

## License

This project is licensed under the MIT License. See the LICENSE file for details.

---
