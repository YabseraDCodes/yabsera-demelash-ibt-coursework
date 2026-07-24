# inventory.py

# Dictionary to store stock
stock = {}

# Read stock from file
try:
    with open("stock.txt", "r") as file:
        for line in file:
            item, qty = line.strip().split(",")
            stock[item] = int(qty)
except FileNotFoundError:
    print("No stock file yet — starting empty")

# Function to adjust stock quantity
def adjust(item, amount):
    stock[item] = stock.get(item, 0) + amount

# Example updates
adjust("Paracetamol", 5)     # Add 5
adjust("Amoxicillin", -2)    # Remove 2
adjust("Vitamin C", 8)       # Add new item

# Print current inventory
print("\nCurrent Inventory")
print("-" * 30)
for item, qty in stock.items():
    print(f"{item}: {qty}")

# Print low-stock items
print("\nLow Stock Items")
print("-" * 30)
low_stock = [item for item, qty in stock.items() if qty < 10]

if low_stock:
    for item in low_stock:
        print(item)
else:
    print("No low-stock items.")

# Save updated stock back to the file
with open("stock.txt", "w") as file:
    for item, qty in stock.items():
        file.write(f"{item},{qty}\n")

print("\nStock has been saved to stock.txt")