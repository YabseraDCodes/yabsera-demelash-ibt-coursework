# AddisEats API

An in-memory ASP.NET Core Web API for Addis Eats restaurants and menus. The restaurant service starts with 12 seeded restaurants across Bole, Piassa, Kazanchis, and Arat Kilo, each with six menu items. Run the project and open `/swagger` to explore the endpoints.

## Restaurant endpoints

| Method and route | What it does | Success | Failure |
|---|---|---|---|
| `GET /api/v1/restaurants?area=Bole&page=1&limit=5` | Lists restaurants, optionally filtered by area and paged. `page` defaults to `1`; `limit` defaults to `10` and must be between `1` and `100`. | `200 OK` | `400 Bad Request` for invalid paging values. |
| `GET /api/v1/restaurants/{id}` | Returns one restaurant, including its menu. | `200 OK` | `404 Not Found` when the ID does not exist. |
| `GET /api/v1/restaurants/{id}/menu` | Returns that restaurant's menu. | `200 OK` (an empty menu is `[]`) | `404 Not Found` when the restaurant does not exist. |

The restaurant controller uses `IRestaurantService`, registered as scoped. Restaurant responses use `RestaurantDTO` in `DTOs`, mapped from the `Restaurant` model by `Mappers/RestaurantMapper`.

## Project layout

- `Controllers` — HTTP endpoints
- `DTOs` — response data transfer objects
- `Mappers` — model-to-DTO mapping
- `Models` — restaurant and menu item entities
- `Services` — restaurant and menu business logic, grouped by feature

## CORS

The `Frontend` CORS policy allows `http://localhost:3000`, `http://localhost:5173`, and `https://localhost:5173`. Edit `Cors:AllowedOrigins` in `appsettings.json` to match the frontend origin.

## Existing menu endpoints

| Method and route | What it does |
|---|---|
| `GET /api/v1/menu?category=Main` | Lists menu items, optionally filtered by category. |
| `GET /api/v1/menu/{id}` | Returns one menu item, or `404 Not Found`. |
| `POST /api/v1/menu` | Creates a menu item from `name`, `category`, and `price`. The price must be greater than zero ETB. |

The menu API's default result limit is configured with `MenuOptions:DefaultPageSize` in `appsettings.json`. Example requests for the restaurant endpoints are in `AddisEats.Api.http`.
