# Backend NBP API

The Backend NBP API application is written in C# using ASP.NET Core. The application uses the public API of the National Bank of Poland (NBP) to retrieve exchange rate data.

## Functions

The application offers the following functions:

1. Retrieve the average exchange rate for the specified date.
2. Retrieve the minimum and maximum average exchange rate of a currency for the last N quotes.
3. Retrieve the largest difference between the bid and ask rates for the last N quotes.

## Installation and startup

1. make sure you have [.NET SDK] installed.
2. clone the repository: `git clone https://github.com/yourusername/BackendNbpApi.git`.
3. navigate to the project directory: `cd BackendNbpApi`.
4. build the project: `dotnet build`.
5. run the application: `dotnet run`.

The application will be available at: `http://localhost:5190` (or `https://localhost:7207` in case of HTTPS).

## Usage examples
### Gets the average exchange rate for a given currency and date.

GET /Currency/average-exchange-rate?currencyCode=USD&date=2022-04-21

### Gets the minimum and maximum average exchange rate for a given currency and the last N quotations.

GET /Currency/min-max-average?currencyCode=USD&numberOfQuotations=10

### Gets the major difference between the buy and ask rate for a given currency and the last N quotations.
GET /Currency/major-difference?currencyCode=USD&numberOfQuotations=12
