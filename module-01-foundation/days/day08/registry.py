from datetime import datetime

class BankConfig:
    _instance = None

    def __new__(cls):
        if cls._instance is None:
            cls._instance = super().__new__(cls)

            cls._instance.interest_rate = 0.07
            cls._instance.overdraft_limit = 1000

        return cls._instance
    
class AlertService:
    def send_alert(self, owner, message):
        # print(f"[ALERT] {owner}: {message}") 
        pass

class AuditLog(AlertService):
    def log(self, account, action, amount=0, status="SUCCESS"):
        print(
            "[AUDIT] "
            f"[{datetime.now():%Y-%m-%d %H:%M:%S}] "
            f"{action:<12}"
            f"Owner: {account.owner:<20}"
            f"Acc#: {account.account_number:<8}"
            f"Amount: {amount}"
            f"Balance: {account.balance:<10}"
            f"Status: {status}"
        )
    def send_alert(self, owner, message):
        pass

class SMSAlert(AlertService):
    def send_alert(self, owner, message):
        print(f"[SMS] {owner}: {message}")    
    def log(self, owner, action, amount=0, status="SUCCESS"):
        pass      

class Account: 
    def __init__(self, owner, account_number, balance=0): 
        self.owner = owner 
        self.account_number = account_number
        self.__balance = balance
        # self.alert = AlertService()
        self.observers = []
        self.history = []
    

    def subscribe(self, observer):
        self.observers.append(observer)

    def _notify(self, message, action="Transaction", amount=0):
        for observer in self.observers:
            if isinstance(observer, SMSAlert):
                observer.send_alert(self.owner, message)

            if isinstance(observer, AuditLog):
                observer.log(self, action, amount)

    @property
    def balance(self):
        return self.__balance
    
    def statement(self):
        self._notify(f"Account Owner: {self.owner}: {self.balance} ETB")
    
    
    def _change_balance(self, amount):
        self.__balance += amount

    def deposit(self, amount):
        self._change_balance(amount)
        self.history.append({
        "type": "deposit",
        "amount": amount
        })
        self._notify(f"Dear customer you have deposited: {amount} ETB. Your current balance is: {self.__balance} ETB.", "DEPOSIT", amount)


    def withdraw(self, amount):
        if amount <= 0:
            self._notify(f"Please enter a valid amount to withdraw.")
        elif amount > self.__balance:
            self._notify(f"Insufficient Balance")
        else:
            self.__balance -= amount
            self.history.append({
            "type": "withdraw",
            "amount": amount
            })
            self._notify(f"Dear customer you have withdrawn: {amount} ETB. Your remaining balance is: {self.__balance} ETB.", "WITHDRAW", amount)
    
    def undo_last(self):
        if not self.history:
            print("No transaction to undo.")
            return

        transaction = self.history.pop()

        if transaction["type"] in ("deposit", "interest"):
            self._change_balance(-transaction["amount"])

        elif transaction["type"] == "withdraw":
            self._change_balance(transaction["amount"])

        self._notify(
            f"Last transaction ({transaction['type']}) of "
            f"{transaction['amount']} ETB has been undone.",
            "UNDO",
            transaction["amount"]
        )

    def total_transactions(self, history=None):
        if history is None:
            history = self.history
        if history == []:
            return 0
        return 1 + self.total_transactions(history[1:])

class SavingsAccount(Account):
    def __init__(self, owner, account_number, balance):
        super().__init__(owner, account_number, balance)
        config = BankConfig()
        self.interest_rate = config.interest_rate
        
    def add_interest(self):
        interest = self.balance * self.interest_rate
        self._change_balance(interest)
        self.history.append({
            "type": "withdraw",
            "amount": interest
            })
        self._notify(f"Interest of {interest:.2f} ETB has been added to your account.", "INTEREST", interest)

    def statement(self):
        self._notify(f"Account Type: SavingAcccount -- Account Owner: {self.owner}: {self.balance} ETB")

class CurrentAccount(Account):
    def __init__(self, owner, account_number, balance):
        super().__init__(owner, account_number, balance)
        config = BankConfig()

        self.overdraft = config.overdraft_limit

    def withdraw(self, amount):  # override
        if amount > self.balance + self.overdraft:
            self._notify(f"Insufficient funds, including overdraft limit.")
        else:
            self._change_balance(-amount)
            self.history.append({
            "type": "withdraw",
            "amount": amount
            })
            self._notify(f"Dear customer you have withdrawn: {amount} ETB. Your remaining balance is: {self.balance} ETB.", "WITHDRAW", amount)
    
    def statement(self):
        self._notify(f"Account Type: CurrentAcccount -- Account Owner: {self.owner}: {self.balance} ETB")

def binary_search(sorted_accounts, target_number):
    low = 0
    high = len(sorted_accounts) - 1

    while low <= high:
        mid = (low + high) // 2           
        mid_account = sorted_accounts[mid]

        if mid_account.account_number == target_number:
            return mid_account            
        elif mid_account.account_number < target_number:
            low = mid + 1                 
        else:
            high = mid - 1               

    return None    

class AccountRegistry:
    def __init__(self):
        self.accounts = {}      # dictionary

    def add(self, account):
        self.accounts[account.account_number] = account

    def find(self, account_number):
        return self.accounts.get(account_number)

    def list_all(self):
        return sorted(
            self.accounts.values(),
            key=lambda acc: acc.account_number
        )
    
    def print_all(self):
        for acc in self.list_all():
            print(f"Account_number: {acc.account_number:<8} Owner: {acc.owner:<20} Balance: {acc.balance}")

    def top_by_balance(self, n):

        return sorted(
            self.accounts.values(),
            key=lambda acc: acc.balance,
            reverse=True)[:n]
    
    def find_by_number(self, account_number):
        sorted_accounts = self.list_all()
        return binary_search(sorted_accounts, account_number)


registry = AccountRegistry()

class AccountFactory:

    @staticmethod
    def create(account_type, owner, account_number, balance=0):
        if account_type == "savings":
            account = SavingsAccount(owner, account_number, balance)
        elif account_type == "current":
            account = CurrentAccount(owner, account_number, balance)
        else:
            account = Account(owner, account_number)
        
        registry.add(account)

        return account

sms = SMSAlert()
audit = AuditLog()

pedro = AccountFactory.create("normal", "Pedro Porro", 5678)
yamal = AccountFactory.create("normal", "Lamine Yamal", 1234)
sanches = AccountFactory.create("current", "Sanches", 12454, 1000)
martinez = AccountFactory.create("savings", "Martinez", 14545, 2000)

for account in [pedro, yamal, sanches, martinez]:
    account.subscribe(sms)
    account.subscribe(audit)

print("\n--- Account Transactions ---")

pedro.deposit(2000)
pedro.withdraw(500)
pedro.deposit(300)

yamal.deposit(10000)
yamal.withdraw(1500)

sanches.deposit(100)
sanches.withdraw(1200)

martinez.withdraw(500)
martinez.add_interest()
martinez.deposit(1000)

accounts = [pedro, yamal, sanches, martinez]

print("\n--- Account Statements ---")

for account in accounts:
    account.statement()

print("\n--- Registry find() demo ---")
found = registry.find(5678)
print(f"Found account: {found.owner}, Balance: {found.balance}")

not_found = registry.find(9999)
print(f"Searching missing account: {not_found}")  

print("\n--- Registry list_all() demo ---")
for acc in registry.list_all():
    print(f"Account_number: {acc.account_number:<8} Owner: {acc.owner:<20} Balance: {acc.balance}")

print("\n--- History Stack demo ---")
print("Pedro's history after deposits/withdrawals:", pedro.history)


print("\n--- UNDO implementation demo ---")
print("Balance before undo:", pedro.balance)
print("History before undo:", pedro.history)

pedro.undo_last()

print("Balance after undo:", pedro.balance)
print("History after undo:", pedro.history)

print("\n--- top_by_balance() demo ---")
for acc in registry.top_by_balance(2):
    print(f"Account_number: {acc.account_number:<8} Owner: {acc.owner:<20} Balance: {acc.balance}")

print("\n--- find_by_number() binary search demo ---")
found = registry.find_by_number(1234)
print(f"Found using binary search: {found.owner}, Balance: {found.balance}")

missing = registry.find_by_number(9999)
print(f"Searching missing account using binary search: {missing}")

print("\n--- total_transactions() recursive demo ---")
for acc in accounts:
    print(f"{acc.owner:<20} total transactions: {acc.total_transactions()}")