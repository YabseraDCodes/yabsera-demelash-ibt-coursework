| Method | Path | Purpose | Success Status | Failure Status |
|----------|----------|----------|----------|----------|
| GET | /api/v1/restaurants | Get all available restaurants. | 200 OK | 400 Bad Request |
| GET | /api/v1/restaurants/{id} | View details for a specific restaurant. | 200 OK | 404 Not Found |
| GET | /api/v1/restaurants?city=Bole&cuisine=Ethiopian | Search restaurants using location and cuisine filters. | 200 OK | 400 Bad Request |
| GET | /api/v1/restaurants/{id}/menus | See the menu for a restaurant. | 200 OK | 404 Not Found |
| GET | /api/v1/menus/{id} | View information about a menu item. | 200 OK | 404 Not Found |
| POST | /api/v1/orders | Place a new food order. | 201 Created | 400 Bad Request |
| GET | /api/v1/orders/{id} | Check the details of an existing order. | 200 OK | 404 Not Found |
| GET | /api/v1/orders | View all orders belonging to the logged-in customer. | 200 OK | 401 Unauthorized |
| PATCH | /api/v1/orders/{id} | Update an order status such as preparing or delivered. | 200 OK | 403 Forbidden |
| DELETE | /api/v1/orders/{id} | Cancel an order before it is delivered. | 204 No Content | 404 Not Found |
| POST | /api/v1/payments | Submit payment for an order. | 201 Created | 400 Bad Request |
| GET | /api/v1/payments/{id} | Check payment information and status. | 200 OK | 404 Not Found |
| POST | /api/v1/auth/login | Sign in and receive an access token. | 200 OK | 401 Unauthorized |
| POST | /api/v1/auth/register | Create a customer account. | 201 Created | 400 Bad Request |
| POST | /api/v1/auth/logout | Sign out of the current session. | 204 No Content | 401 Unauthorized |