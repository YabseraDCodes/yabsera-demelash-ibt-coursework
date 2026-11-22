# AddisEats API

An in-memory API for restaurants, menu items, and orders. Start the project in Development and open `/swagger` to try every route.

## Route table

IDs belong in the path. Search and page filters belong in the query. Collections return `{ "items": [...] }`, including when there are no matches (with status `200`). `limit` defaults to `10` and can be from `1` to `100`; `page` defaults to `1` and must be at least `1`.

| Method | Path | Query | Success | Failure |
|---|---|---|---|---|
| GET | `/api/v1/restaurants` | `area`, repeatable `areas`, `maxFeeEtb`, `page`, `limit` | `200` list | `400 invalid_request` or `invalid_pagination` |
| GET | `/api/v1/restaurants/{id}` | — | `200` restaurant | `404 restaurant_not_found` |
| GET | `/api/v1/restaurants/{id}/menu-items` | — | `200` menu item list | `404 restaurant_not_found` |
| GET | `/api/v1/menu-items` | `category` | `200` list | `400 invalid_request` |
| GET | `/api/v1/menu-items/{id}` | — | `200` menu item | `404 menu_item_not_found` |
| POST | `/api/v1/menu-items` | — | `201` created item and `Location` | `400 invalid_request` or `invalid_menu_item` |
| GET | `/api/v1/orders` | `page`, `limit` | `200` list | `400 invalid_pagination` |
| GET | `/api/v1/orders/{id}` | — | `200` order | `404 order_not_found` |
| POST | `/api/v1/orders` | — | `201` created order and `Location` | `400 invalid_request` or `invalid_order` |
| PATCH | `/api/v1/orders/{id}/statuses` | — | `204` no body | `400 invalid_request` or `invalid_order_status`; `404 order_not_found` |

`PATCH /orders/{id}/statuses` uses the `statuses` noun to name the order's status resource; PATCH changes only that part of the order. No route uses an action word. Restaurant menus are nested one level under a restaurant.

## Error response

Every error is JSON with the same ProblemDetails fields. `code` is always present; `errors` is added for field validation:

```json
{
  "type": "https://addiseats.api/problems/invalid-pagination",
  "title": "Page or limit is not valid.",
  "status": 400,
  "detail": "Page must be at least 1 and limit must be between 1 and 100.",
  "instance": "/api/v1/restaurants",
  "code": "invalid_pagination"
}
```

| Code | Status | Meaning |
|---|---:|---|
| `invalid_request` | 400 | The body or a value could not be read. |
| `invalid_pagination` | 400 | Page or limit is outside its allowed range. |
| `invalid_menu_item` | 400 | A menu item needs a name, category, and price above zero. |
| `invalid_order` | 400 | An order needs valid restaurant and menu item IDs. |
| `invalid_order_status` | 400 | The status value is not supported. |
| `restaurant_not_found` | 404 | The restaurant ID does not exist. |
| `menu_item_not_found` | 404 | The menu item ID does not exist. |
| `order_not_found` | 404 | The order ID does not exist. |
| `not_found` | 404 | No route matched the request path. |
| `http_error` | 4xx | Another HTTP error occurred. |
| `server_error` | 500 | Something went wrong inside the API. |

Bad input gets a `400` response. Missing items and paths get `404`. Unexpected server problems get `500`; they do not include internal exception details. Empty lists and successful reads are `200`, creates are `201`, and a successful status update is `204` with no response body.

## Project layout

- `Controllers` — HTTP routes
- `DTOs` — response shapes
- `Mappers` — restaurant response mapping
- `Models` — restaurant, menu item, and order data
- `Services` — in-memory data and business rules
