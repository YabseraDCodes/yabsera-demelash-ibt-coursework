# AddisEats.Api

Run the project with `dotnet run` from this folder. Open the local URL printed in the terminal and add `/swagger` to view Swagger.

## Exercise notes

- `GET /api/v1/health` returns HTTP 200 with a UTC time and environment name. `SystemClock` returns `DateTime.UtcNow` through `IClock`; I used `CapturedClock` for the lifetime checks.
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

   The build also emitted ASP0023 warnings for both route templates. I fixed the numeric detail route with the `:int` constraint and gave name lookup the distinct literal path `/api/v1/restaurants/by-name/{name}`. The final routes are unambiguous.
6. **Swagger response schema.** Before the typed return, Swagger listed `GetOne`'s 404 response but no 200 response schema. It now returns `ActionResult<RestaurantDto>` and declares the 200 response type; Swagger's OpenAPI document references `#/components/schemas/RestaurantDto` for that response. The computer-use helper stopped before I could capture the requested UI screenshots, so this OpenAPI result is recorded here as text instead.
7. **Orders.** `POST /api/v1/orders` returned `201 Created` with `Location: http://127.0.0.1:5195/api/v1/orders/1`. A GET to that Location returned the created order.
8. **JSON settings.** Before configuration, the created order body was `{"Id":1,"RestaurantId":1,"MenuItemIds":[1,2],"Status":0,"Notes":null}`. After configuration it was `{"id":1,"restaurantId":1,"menuItemIds":[1,2],"status":"Pending"}`. The latter uses camelCase, omits the null `notes` property, and writes the enum as a string.

The runnable requests are in `AddisEats.Api.http`.
