class Account: 
    def __init__(self, owner, account_number, balance=0): 
        self.owner = owner 
        self.account_number = account_number
        self.__balance = balance

    @property
    def balance(self):
        return self.__balance
    
    def statement(self):
        print(f"Account Owner: {self.owner}: {self.balance} ETB")
    
    
    def _change_balance(self, amount):
        self.__balance += amount

    def deposit(self, amount):
        self._change_balance(amount)
        print(f"Dear customer you have deposited: {amount} ETB. Your current balance is: {self.__balance} ETB.")


    def withdraw(self, amount):
        if amount <= 0:
            print("Please enter a valid amount to withdraw.")
        elif amount > self.__balance:
            print("Insufficient Balance")
        else:
            self.__balance -= amount
            print(f"Dear customer you have withdrawn: {amount} ETB. Your remaining balance is: {self.__balance} ETB.")

class SavingsAccount(Account):
    def __init__(self, owner, account_number, balance, interest_rate=0.07):
        super().__init__(owner, account_number, balance)
        self.interest_rate = interest_rate

    def add_interest(self):
        interest = self.balance * self.interest_rate
        self.deposit(interest)
        print(f"Interest of {interest} ETB has been added to your account.")

    def statement(self):
        print(f"Account Type: SavingAcccount -- Account Owner: {self.owner}: {self.balance} ETB")

class CurrentAccount(Account):
    def __init__(self, owner, account_number, balance, overdraft=1000):
        super().__init__(owner, account_number, balance)
        self.overdraft = overdraft

    def withdraw(self, amount):  # override
        if amount > self.balance + self.overdraft:
            print("Insufficient funds, including overdraft limit.")
        else:
            self._change_balance(-amount)
            print(f"Dear customer you have withdrawn: {amount} ETB. Your remaining balance is: {self.balance} ETB.")
    
    def statement(self):
        print(f"Account Type: CurrentAcccount -- Account Owner: {self.owner}: {self.balance} ETB")


            
           

pedro = Account("Pedro Porro", 5678)
pedro.deposit(2000)
pedro.withdraw(500)

yamal = Account("Lamine Yamal", 1234)
yamal.deposit(10000)
yamal.withdraw(1500)

sanches = CurrentAccount("sanches", 12454, 1000)
sanches.withdraw(1200)
print(sanches.balance)

martinez = SavingsAccount("sanches", 14545, 2000)
martinez.withdraw(500)

accounts = [pedro, yamal, sanches, martinez]

for account in accounts:
    account.statement()
