Console.WriteLine("Yabsera Demelash");
Console.WriteLine(DateTime.Today.ToString("dd MMM yyyy"));

string RestuarantName = "YD Restuarant";
decimal price = 100.50m;
double rating = 4.5;
int numberOfReviews = 150;
long customerId = 754635243L;
char grade = 'A';
bool isOpen = true;
DateTime establishedDate = new DateTime(2015, 5, 20);

Console.WriteLine($"Restuarant Name: {RestuarantName}");
Console.WriteLine($"Price: {price}");
Console.WriteLine($"Rating: {rating}");
Console.WriteLine($"Number of Reviews: {numberOfReviews}");
Console.WriteLine($"Customer ID: {customerId}");
Console.WriteLine($"Grade: {grade}");
Console.WriteLine($"Is Open: {isOpen}");
Console.WriteLine($"Established Date: {establishedDate.ToString("dd MMM yyyy")}");