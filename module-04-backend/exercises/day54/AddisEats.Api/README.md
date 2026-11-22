# AddisEats.Api

Run the project with `dotnet run` from this folder. Open the local URL printed in the terminal and add `/swagger` to view Swagger.

## Exercise notes

- `GET /api/v1/health-checks` returns HTTP 200 with a UTC time and environment name. `SystemClock` returns `DateTime.UtcNow` through `IClock`; I used `CapturedClock` for the lifetime checks.
- For the lifetime check, each clock captured its creation time. Transient returned `18:36:16.2997633Z` and then `18:36:16.3543941Z`; scoped returned `18:36:46.2667518Z` and then `18:36:46.3204483Z`; singleton returned `18:37:25.5556266Z` both times. Transient creates a new instance each time it is requested, while scoped reuses one instance for each request. Singleton reuses the same instance across all requests, so its captured value does not change.
- `IPaymentProvider` now uses `CbeBirrProvider`; changing its registration switched from TeleBirr to CBE Birr without changing `PaymentsController`.
- `GET /api/v1/delivery/fees` returns `FreeAboveEtb` and `DefaultFeeEtb` from the `Delivery` section of `appsettings.json`.
- **7.** Removing the `IPaymentProvider` registration produces: `Unable to resolve service for type 'AddisEats.Api.Services.IPaymentProvider' while attempting to activate 'AddisEats.Api.Controllers.PaymentsController'.` It says the controller needs an `IPaymentProvider` that dependency injection cannot find.
- **8.** Authorization must run after authentication so it can check the user that authentication established; reversing the order means authorization sees the request before that user is set.

## Capstone routing and API exercises

The restaurant and order exercises below run in this project alongside the earlier dependency-injection exercises. They use the same controller style: attribute routes, primary constructors, expression-bodied reads, and small service classes.

1. **ID constraint.** Restaurant detail uses `[HttpGet("{id:int}")]`. `GET /api/v1/restaurants/abc` returned `404 Not Found`; the request did not reach the action.
2. **Mismatched route token.** I temporarily changed the route to `{restaurantId:int}` and left the action parameter named `id`. `GET /api/v1/restaurants/1` returned `404 Not Found`. Route templates are checked at runtime, so C# does not compare a route token with an action parameter at compile time.
3. **Bound query object.** `RestaurantQuery` has nullable `Area`, `MaxFeeEtb`, `Page`, and `Limit` properties. `RestaurantsController` binds it with `[FromQuery]` and passes the same object to `IRestaurantService.GetAll`.
4. **Multiple areas.** `?areas=Bole&areas=Piassa` returned four restaurants across both areas. Omitting `areas` returned all eight seeded restaurants; `null` and an empty array both skip that filter.
5. **Ambiguous routes.** I temporarily used `{id}` and `{restaurantId}` for two GET actions. The host started; requesting `/api/v1/restaurants/1` produced this routing exception:

   ```text
   Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches:
   AddisEats.Api.Controllers.RestaurantsController.GetOne (AddisEats.Api)
   AddisEats.Api.Controllers.RestaurantsController.GetByName (AddisEats.Api)
   ```

   The build also emitted ASP0023 warnings for both route templates. I fixed the numeric detail route with the `:int` constraint and first gave name lookup the distinct path `/api/v1/restaurants/by-name/{name}`. Exercise 2 below moves that name search to a query filter, so the final routes are unambiguous.
6. **Swagger response schema.** Before the typed return, Swagger listed `GetOne`'s 404 response but no 200 response schema. It now returns `ActionResult<RestaurantDto>` and declares the 200 response type; Swagger's OpenAPI document references `#/components/schemas/RestaurantDto` for that response. The computer-use helper stopped before I could capture the requested UI screenshots, so this OpenAPI result is recorded here as text instead.
7. **Orders.** `POST /api/v1/orders` returned `201 Created` with a `Location` header for the new order. A GET to that Location returned the created order.
8. **JSON settings.** Before configuration, the created order body was `{"Id":1,"RestaurantId":1,"MenuItemIds":[1,2],"Status":0,"Notes":null}`. After configuration it was `{"id":1,"restaurantId":1,"menuItemIds":[1,2],"status":"Pending"}`. The latter uses camelCase, omits the null `notes` property, and writes the enum as a string.

The runnable requests are in `AddisEats.Api.http`.

## Exercise 1 — Route table

| Method | Path | Purpose | Success | Every failure |
|---|---|---|---:|---|
| GET | `/api/v1/restaurants` | Search restaurants and return a page. | 200 | 400 invalid query, page, or money amount; 500 server error |
| GET | `/api/v1/restaurants/{id}` | Get one restaurant by ID. | 200 | 400 invalid query; 404 restaurant not found; 500 server error |
| GET | `/api/v1/orders/{id}` | Get one order by ID. | 200 | 400 invalid query; 404 order not found; 500 server error |
| POST | `/api/v1/orders` | Create an order. | 201 | 400 invalid query or order; 500 server error |
| GET | `/api/v1/delivery-fees` | Read the delivery fee settings. | 200 | 400 invalid query; 500 server error |
| GET | `/api/v1/health-checks` | Read the API time and environment. | 200 | 400 invalid query; 500 server error |
| GET | `/api/v1/payment-providers` | Read the active payment provider. | 200 | 400 invalid query; 500 server error |
| GET | `/api/v1/protected-resources` | Check access to a signed-in resource. | 200 | 400 invalid query; 401 not signed in; 403 not allowed; 500 server error |

The `500` response means the API failed unexpectedly. It is listed for every route because any route can hit a server fault. Unknown query keys fail with `400 unknown_query_parameter` on every route.

## Exercise 2 — IDs and filters

| Path part | Kind | Meaning |
|---|---|---|
| `/api/v1` | Fixed prefix | API name and version; not an ID or filter. |
| `/restaurants`, `/orders`, `/delivery-fees`, `/health-checks`, `/payment-providers`, `/protected-resources` | Resource collection | Plural, lowercase resource names. |
| `{id}` in `/restaurants/{id}` and `/orders/{id}` | Identifier | Names one restaurant or order. |
| `name`, `area`, `areas`, `maxFeeEtb`, `page`, `limit` | Query filters | Narrow or page the restaurant list; none live in the path. |

I changed `GET /restaurants/by-name/{name}` to `GET /restaurants?name=...`. A name is a search filter, so it belongs in the query. The name search now returns a list like the other restaurant filters; no matching names return an empty list with `200`.

## Exercise 3 — Verbs in paths

There are no verb segments in the final routes. The old `/restaurants/by-name/{name}` route had no verb either, but it put a search filter in the path. Here are three noun-based choices for that same search:

| Approach | Route | Choice |
|---|---|---|
| Filter the collection | `GET /restaurants?name=Habesha` | **Chosen.** A name narrows the restaurant collection and can be combined with the other filters. |
| Address the resource by its known ID | `GET /restaurants/{id}` | Works when the caller already knows the ID, but it does not search by name. |
| Create a search request resource | `POST /restaurant-searches` with `{ "name": "Habesha" }` | Makes sense for a complex saved search, but is too much for one simple filter. |

## Exercise 4 — Error catalogue

| Code | Status | Message |
|---|---:|---|
| `invalid_request` | 400 | Check the request fields and try again. |
| `unknown_query_parameter` | 400 | Remove or rename the unknown query parameter. |
| `invalid_pagination` | 400 | Page must be at least 1; limit must be from 1 to 100. |
| `invalid_order` | 400 | Use a positive restaurant ID and at least one positive menu item ID. |
| `invalid_money_amount` | 400 | The ETB amount must be zero or more. |
| `unauthenticated` | 401 | Sign in with a valid token and try again. |
| `forbidden` | 403 | Your account cannot make this change. |
| `restaurant_not_found` | 404 | No restaurant has that ID. |
| `order_not_found` | 404 | No order has that ID. |
| `conflict` | 409 | The order cannot be changed from its current state. |
| `server_error` | 500 | The API could not finish the request. Try again later. |

## Exercise 5 — Status choices

| Situation | Status | Why |
|---|---:|---|
| A search returns no rows | 200 | The search worked; its result is an empty list. |
| Someone cancels an order that is already delivered | 409 | The order exists, but its current state does not allow cancellation. |
| A request has a negative ETB total | 400 | The caller sent a value that is not allowed. |
| A valid courier token tries to edit a menu | 403 | The courier is signed in but does not have menu editing rights. |

## Exercise 6 — Twelve route fixes

The class handout was not in the repo, so these twelve examples are a clearly marked sample set, not a claim about the handout's exact routes. Each rewrite follows the same API rules used above.

| Bad sample route | Rewritten route | Rule broken by the sample |
|---|---|---|
| `GET /getRestaurants` | `GET /restaurants` | Verb in path; camelCase. |
| `GET /restaurant/{id}` | `GET /restaurants/{id}` | Singular collection name. |
| `GET /restaurants/find/{name}` | `GET /restaurants?name={name}` | Verb and filter in path. |
| `POST /createOrder` | `POST /orders` | Verb in path; camelCase. |
| `GET /orders/get/{id}` | `GET /orders/{id}` | Verb in path. |
| `PUT /updateOrder/{id}` | `PUT /orders/{id}` | Verb in path; camelCase. |
| `DELETE /deleteOrder/{id}` | `DELETE /orders/{id}` | Verb in path; camelCase. |
| `GET /restaurants/{id}/menu` | `GET /restaurants/{id}/menu-items` | Singular collection name. |
| `POST /restaurants/{id}/addMenuItem` | `POST /restaurants/{id}/menu-items` | Verb in path; camelCase. |
| `GET /orders/byDate/{date}` | `GET /orders?created-date={date}` | Filter in path; camelCase. |
| `GET /payment/getProvider` | `GET /payment-providers` | Verb in path; singular resource; camelCase. |
| `GET /getDeliveryFee/{area}` | `GET /delivery-fees?area={area}` | Verb in path; filter in path; singular name. |

## Exercise 7 — Pick a style

**Public mobile API — REST.** It uses plain HTTP methods and paths, which are easy to inspect and cache. It also works well for a public API that needs a stable contract for many app versions.

**Service-to-service call — gRPC.** It gives both services a typed contract and generated client code. It is a good fit when the services are controlled by the same team and need fast calls.

**Dashboard with six resources on one screen — GraphQL.** The dashboard can ask for the fields it needs from all six resources in one request. That can avoid extra calls and avoid sending unused fields.

## Exercise 8 — Unknown query keys

The API rejects unknown query keys with `400 unknown_query_parameter`. This catches misspellings such as `?areaa=Bole` early instead of quietly returning a result that ignores the caller's filter. The restaurant list accepts only `name`, `area`, `areas`, `maxFeeEtb`, `page`, and `limit`; routes with no query filters accept none.
