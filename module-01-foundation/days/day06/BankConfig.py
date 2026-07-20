from datetime import datetime
from logging import config

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
        self._notify(f"Dear customer you have deposited: {amount} ETB. Your current balance is: {self.__balance} ETB.", "DEPOSIT", amount)


    def withdraw(self, amount):
        if amount <= 0:
            self._notify(f"Please enter a valid amount to withdraw.")
        elif amount > self.__balance:
            self._notify(f"Insufficient Balance")
        else:
            self.__balance -= amount
            self._notify(f"Dear customer you have withdrawn: {amount} ETB. Your remaining balance is: {self.__balance} ETB.", "WITHDRAW", amount)

class SavingsAccount(Account):
    def __init__(self, owner, account_number, balance):
        super().__init__(owner, account_number, balance)
        config = BankConfig()
        self.interest_rate = config.interest_rate
        
    def add_interest(self):
        interest = self.balance * self.interest_rate
        self._change_balance(interest)
        self._notify(f"Interest of {interest:.2f} ETB has been added to your account.", "INTREST", interest)

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
            self._notify(f"Dear customer you have withdrawn: {amount} ETB. Your remaining balance is: {self.balance} ETB.", "WITHDRAW", amount)
    
    def statement(self):
        self._notify(f"Account Type: CurrentAcccount -- Account Owner: {self.owner}: {self.balance} ETB")

class AccountFactory:
    @staticmethod
    def create(account_type, owner, account_number, balance=0):
        if account_type == "savings":
            return SavingsAccount(owner, account_number, balance)
        elif account_type == "current":
            return CurrentAccount(owner, account_number, balance)
        else:
            return Account(owner, account_number)


sms = SMSAlert()
audit = AuditLog()

pedro = AccountFactory.create("normal", "Pedro Porro", 5678)
yamal = AccountFactory.create("normal", "Lamine Yamal", 1234)
sanches = AccountFactory.create("current", "Sanches", 12454, 1000)
martinez = AccountFactory.create("savings", "Martinez", 14545, 2000)

for account in [pedro, yamal, sanches, martinez]:
    account.subscribe(sms)
    account.subscribe(audit)

pedro.deposit(2000)
pedro.withdraw(500)

yamal.deposit(10000)
yamal.withdraw(1500)

sanches.deposit(100)
sanches.withdraw(1200)

martinez.withdraw(500)
martinez.add_interest()

accounts = [pedro, yamal, sanches, martinez]

for account in accounts:
    account.statement()
