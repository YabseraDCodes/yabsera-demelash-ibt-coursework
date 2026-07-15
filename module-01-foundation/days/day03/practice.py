#Unique cities

cities = ["Sydeny", "Melbourne", "Brisbane", "New Yokr", "Manchester", "Sydney", "Melbourne"]
unique_cities = set(cities)
count = 0
print("Unique cities: ", end=" ")
for city in unique_cities:
    print(city, end=" ")
    count = count +1
print(f"\nTotal number of unique cities: {count}\n\n")

#Price report

groceries = {"Milk": 170, "Egg": 22, "Bread": 15, "Pepper": 100, "Onion": 70}
print("Price report: ", end="")
for item, price in groceries.items():
    print(f"{item} : {price} ETB", end=" , ")


#Tax Compensation

prices = [100, 250, 400, 80] 
  
taxed = [round(p * 1.15) for p in prices]
print("\n\nTaxed prices: ", taxed, "\n")


#write and read
with open("notes.txt", "a") as f: 
    f.write("Mr Abe\n")
    f.write("Mr Kebe\n")
    f.write("Mrs Alemitu\n")

print("Names from notes.txt:")
with open("notes.txt") as f:
    for line in f:
        print(line.strip())

#safe division
try: 
    user_input = int(input("\n\nPlease inter a number: ")) 
    result = 1000 / user_input 
except ZeroDivisionError: 
    print("Can not divide by zero") 
except ValueError: 
    print("Please enter an integer") 
else: 
    print(result)   # runs only if no error 
finally: 
    print("Done") 