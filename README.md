# How Long to Pay It Off

## Overview

The **"How Long to Pay It Off"** project is a financial calculator for loan repayment scenarios. It helps users understand their loan repayment dynamics by calculating the future value of the loan, the effective annual interest rate, and provides a detailed payment table for various scenarios.

Project Inspired by [AndyMath](https://www.tiktok.com/@andymath.com/video/7391900516457942303?_t=8oCJOCPEe5h&_r=1)
   
## Features

- **Calculate Future Value**: Determines the future value of the loan based on given payments.
- **Calculate Effective Annual Interest Rate**: Computes the annual interest rate required for the loan repayment scenario.
- **Generate Payment Table**: Provides a table of different payment scenarios showing how long it will take to pay off the loan.
- **Customizable Culture**: Supports different cultures for formatting currency and numbers.

## Getting Started

To use this project, you need to set up the project and configure it properly. Follow the steps below:

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- An IDE such as [Visual Studio](https://visualstudio.microsoft.com/) or [Rider](https://www.jetbrains.com/rider/).

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/abenteuerzeit/how-long-to-repay-debt.git
    ```

2. Navigate to the project directory:
    ```bash
    cd how-long-to-repay-debt
    ```

3. Build the project:
    ```bash
    dotnet build
    ```

### Configuration

Create an `appsettings.json` file in the root directory of the project with the following structure:

```json
{
  "LoanSettings": {
    "InitialLoan": 70000,
    "Payment": 500,
    "YearsPaid": 23,
    "RemainingLoan": 60000
  }
}
```

### Usage

1. Open the `Program.cs` file and ensure the configuration is correctly set up to read from `appsettings.json`.

2. Run the application:
    ```bash
    dotnet run
    ```

    ![image](https://github.com/user-attachments/assets/8b359f61-11c6-49d4-b490-2ec1be64e8bd)


## Contributing

If you would like to contribute to this project, please fork the repository and submit a pull request with your proposed changes.

## License

This project is licensed under the EUPL v1.2 License. See the [LICENSE](https://joinup.ec.europa.eu/sites/default/files/custom-page/attachment/eupl_v1.2_en.pdf) file for details.

## Contact

For any questions or suggestions, please reach out to [Adrian Mróz](mailto:dr.adrian.mroz@outlook.com).
