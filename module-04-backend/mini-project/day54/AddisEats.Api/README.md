# AddisEats API

An in-memory ASP.NET Core Web API for restaurants, menus, and orders. Run the project and open `/swagger` in Development to explore the documented endpoints.

## Route table

| Verb | Route | Route constraints | Query parameters | Status codes |
|---|---|---|---|---|
| `GET` | `/api/v1/restaurants` | None | `area` optional; `areas` repeatable; `maxFeeEtb` optional; `page` defaults to `1`; `limit` defaults to `10` (maximum `100`) | `200`, `400` |
| `GET` | `/api/v1/restaurants/{id:int}` | `id` must be an integer | None | `200`, `404` |
| `GET` | `/api/v1/restaurants/{id:int}/menu` | `id` must be an integer | None | `200`, `404` |
| `GET` | `/api/v1/menu` | None | `category` optional | `200` |
| `GET` | `/api/v1/menu/{id:int}` | `id` must be an integer | None | `200`, `404` |
| `POST` | `/api/v1/menu` | None | None | `201`, `400` |
| `GET` | `/api/v1/orders` | None | None | `200` |
| `GET` | `/api/v1/orders/{id:int}` | `id` must be an integer | None | `200`, `404` |
| `POST` | `/api/v1/orders` | None | None | `201`, `400` |
| `PATCH` | `/api/v1/orders/{id:int}/status` | `id` must be an integer | None | `200`, `400`, `404` |

All collection endpoints return the same `{ "items": [...] }` envelope. JSON uses camelCase, omits null properties, and serializes enum values as strings. Restaurant search combines `area` and `areas` when both are supplied and filters by each restaurant's delivery fee. Pagination defaults live on `RestaurantQuery`; `page` and `limit` are nullable to distinguish omitted values from invalid values.

Orders are stored in memory. Creating an order returns `201 Created` with a `Location` header pointing to `GET /api/v1/orders/{id}`. See `AddisEats.Api.http` for sample requests, including a non-integer ID that fails the route constraint.

## Project layout

- `Controllers` — HTTP endpoints
- `DTOs` — response and collection envelope types
- `Mappers` — model-to-DTO mapping
- `Models` — restaurant, menu item, and order entities
- `Services` — business logic
