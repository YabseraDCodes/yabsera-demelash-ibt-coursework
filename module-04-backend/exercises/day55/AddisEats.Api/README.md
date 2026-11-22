# AddisEats API

This API serves a small in-memory list of Addis Ababa restaurants. Start it with `dotnet run` and use the requests in `AddisEats.Api.http`.

## Restaurant routes

| Method and route | Result |
|---|---|
| `GET /api/v1/restaurants?area=Bole&page=1&limit=5` | Search by area and page through the results. `area` is optional; `page` defaults to `1`; `limit` defaults to `10`. |
| `GET /api/v1/restaurants/{id:int}` | Get one restaurant, or `404 Not Found`. Non-integer IDs fail the route constraint with `404`. |
| `GET /api/v1/restaurants/{id:int}/menu` | Get a menu, `[]` for a restaurant with no items, or `404 Not Found` if the restaurant does not exist. |

`RestaurantDto` leaves out `OwnerPhone` and `CommissionRate`, so those private restaurant details are not sent in API responses.

The menu route returns `404` when the restaurant does not exist, but `200` with `[]` when it exists and has no menu items.

The API allows the Module 3 frontend origins listed in `Cors:AllowedOrigins` in `appsettings.json`, including `http://localhost:5173`.

## Exercise checks

- The API returns 12 restaurants across Bole (4), Piassa (3), Kazanchis (3), and Arat Kilo (2). The DTO contains `Id`, `Name`, `Area`, `DeliveryFee`, and `Menu`; it leaves out `OwnerPhone` and `CommissionRate`.
- With `limit=5`, page 1 returned IDs `1–5` and page 2 returned IDs `6–10`.
- `GET /api/v1/restaurants/12/menu` returned `200 OK` with `[]`. `GET /api/v1/restaurants/99/menu` returned `404 Not Found`.
- A CORS preflight from `http://localhost:5173` returned `204 No Content` and `Access-Control-Allow-Origin: http://localhost:5173`.


The four sample requests, including the missing-restaurant `404`, are in `AddisEats.Api.http`.
