# AddisEats Menu API

Menu items are kept in memory. Run the project in Development and open `/swagger` to try the endpoints.

## Endpoints

| Method and route | What it does | Success | Failure |
|---|---|---|---|
| `GET /api/v1/menu` | Returns menu items. Add `?category=Main` to filter by category. | `200 OK` | — |
| `GET /api/v1/menu/{id}` | Returns one menu item. | `200 OK` | `404 Not Found` when the ID does not exist. |
| `POST /api/v1/menu` | Creates an item from a JSON body containing `name`, `category`, and `price`. | `201 Created` with a `Location` header pointing to the new item. | `400 Bad Request` when the ETB price is zero or negative. |

Example request body:

```json
{
  "name": "Kitfo",
  "category": "Main",
  "price": 600.00
}
```

The default result limit is configured with `MenuOptions:DefaultPageSize` in `appsettings.json`.
