# AddisEats.TopUp

A simple C# console application that processes five TeleBirr top-ups and calculates bonuses based on the selected network.

## What the program does

- Collects the customer's name
- Collects the network (Ethio Telecom or Safaricom)
- Collects a top-up amount in ETB
- Validates the amount so invalid values do not crash the program
- Applies:
  - 5% bonus for Ethio Telecom
  - 3% bonus for Safaricom
  - 0% for any other network
- Adds an extra 20 ETB bonus for top-ups of 1,000 ETB or more
- Prints a formatted summary for all five customers
- Shows a grand total at the end

## Sample Output

```text
Customer         Network           Amount      Bonus       Total
---------------------------------------------------------------
Abebe            Ethio Telecom   1000.00      70.00     1070.00
Sara             Safaricom        800.00      24.00      824.00
---------------------------------------------------------------