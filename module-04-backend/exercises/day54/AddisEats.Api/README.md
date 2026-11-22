# AddisEats.Api

Run the project with `dotnet run` from this folder. Open the local URL printed in the terminal and add `/swagger` to view Swagger.

## Exercise notes

- `GET /api/v1/health` returns HTTP 200 with a UTC time and environment name. `SystemClock` returns `DateTime.UtcNow` through `IClock`; I used `CapturedClock` for the lifetime checks.
- For the lifetime check, each clock captured its creation time. Transient returned `18:36:16.2997633Z` and then `18:36:16.3543941Z`; scoped returned `18:36:46.2667518Z` and then `18:36:46.3204483Z`; singleton returned `18:37:25.5556266Z` both times. Transient creates a new instance each time it is requested, while scoped reuses one instance for each request. Singleton reuses the same instance across all requests, so its captured value does not change.
- `IPaymentProvider` now uses `CbeBirrProvider`; changing its registration switched from TeleBirr to CBE Birr without changing `PaymentsController`.
- `GET /api/v1/delivery/fees` returns `FreeAboveEtb` and `DefaultFeeEtb` from the `Delivery` section of `appsettings.json`.
- **7.** Removing the `IPaymentProvider` registration produces: `Unable to resolve service for type 'AddisEats.Api.Services.IPaymentProvider' while attempting to activate 'AddisEats.Api.Controllers.PaymentsController'.` It says the controller needs an `IPaymentProvider` that dependency injection cannot find.
- **8.** Authorization must run after authentication so it can check the user that authentication established; reversing the order means authorization sees the request before that user is set.
