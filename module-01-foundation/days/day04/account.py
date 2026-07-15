class Account: 
    def __init__(self, owner, account_number): 
        self.owner = owner 
        self.account_number = account_number
        self.__balance = 0

    @property
    def balance(self):
        return self.__balance
    
    def deposit(self, amount):
        self.__balance += amount
        print(f"Dear customer you have deposited: {amount} ETB. Your current balance is: {self.__balance} ETB.")


    def withdraw(self, amount):
        if amount <= 0:
            print("Please enter a valid amount to withdraw.")
        elif amount > self.__balance:
            print("Insufficient Balance")
        else:
            self.__balance -= amount
            print(f"Dear customer you have withdrawn: {amount} ETB. Your remaining balance is: {self.__balance} ETB.")
    

pedro = Account("Pedro Porro", 5678)
pedro.deposit(2000)
pedro.withdraw(500)

yamal = Account("Lamine Yamal", "1234")
yamal.deposit(1000)
yamal.withdraw(1500)
