# Module 4 - Day 51 Exercises

**Topic:** Introduction to HTTP and REST APIs
**Day:** 51  


---

### Exercise 1 - Write a paragraph explaining what a server is to someone who has never programmed, without using the words "computer" or "machine". Keep it under 120 words. 
A server is a service that litsen for requests made by things such as website or apps and gives a back what it was asked for such as information or data. For example when you try to open a video in youtube, your device is sending a request to youtube's server asking for the video and the server responds by delivering you the video you wanted.

---

### Exercise 2 - Open any website with the browser network tab visible. Pick three requests and record, for each: the method, the full path, the status code, and whether it carried a request body. 

**Website:** 

| Request | Method | Full Path | Status Code | Request Body? |
|---|---|---|---|---|
| 1 | GET | https://poki.com/ | 200 | No |
| 2 | GET | https://a.poki-cdn.com/images/bg-diamante.svg | 200 | No |
| 3 | GET | https://a.poki-cdn.com/t2.js | 200 | No |

---

### Exercise 3 - For one of those three requests, list four request headers and four response headers, and write one line on what each is doing.

## Request: GET https://a.poki-cdn.com/images/bg-diamante.svg

#### Four Request Headers

| Request Header | What it is doing |
|---------------|------------------|
| **Referer: https://poki.com/** | Tells the server that the request originated from the Poki website. |
| **User-Agent: Mozilla/5.0... Edg/158.0** | Identifies the browser and operating system making the request. |
| **Sec-CH-UA: "Chromium", "Not.A/Brand", "Microsoft Edge"** | Provides browser brand and version information to the server. |
| **Sec-CH-UA-Platform: "Windows"** | Indicates that the client device is running Windows. |

#### Four Response Headers

| Response Header | What it is doing |
|----------------|------------------|
| **Content-Type: image/svg+xml** | Tells the browser that the returned resource is an SVG image. |
| **Content-Encoding: gzip** | Indicates that the response was compressed to reduce transfer size. |
| **Cache-Control: public, max-age=31536000** | Allows browsers and caches to store the file for up to one year. |
| **Last-Modified: Tue, 27 Jul 2021 12:23:53 GMT** | Shows the date and time when the file was last modified on the server. |

#### Summary

This request uses the **GET** method to retrieve an SVG image (`bg-diamante.svg`) from Poki's CDN. The server responds with **200 OK**, and the headers provide information about the client, content type, compression, and caching behavior.

---

### Exercise 4- Write out a full HTTP request, by hand, that would create an order for two portions of menu item 7 at restaurant 14 in Bole, paid by TeleBirr. Include the method line, four headers, and a JSON body. 

``` HTTP
POST /api/orders HTTP/1.1
Host: bole-food.example.com
Content-Type: application/json
Accept: application/json
Authorization: Bearer abc123token

{
  "restaurantId": 14,
  "location": "Bole",
  "paymentMethod": "TeleBirr",
  "items": [
    {
      "menuItemId": 7,
      "quantity": 2
    }
  ]
}
```

---

### Exercise 5 - Choose the correct status code for each of these eight outcomes and justify each in one line: (a) a list of restaurants is returned; (b) an order is created; (c) an order is cancelled and nothing is returned; (d) the qty field arrives as "two"; (e) the auth token has expired; (f) a courier attempts to edit a menu; (g) someone requests order 99999, which does not exist; (h) your code throws an unhandled exception.

| Scenario | Status Code | Justification |
|----------|------------|---------------|
| (a) Restaurants list returned | **200 OK** | The request was successful and returned the restaurant list. |
| (b) Order created | **201 Created** | A new order was successfully created. |
| (c) Order cancelled, nothing returned | **204 No Content** | The cancellation succeeded without returning a response body. |
| (d) Quantity arrives as "two" | **400 Bad Request** | The quantity field contains an invalid value because a number was expected. |
| (e) Authentication token expired | **401 Unauthorized** | The authentication credentials are no longer valid. |
| (f) Courier attempts to edit a menu | **403 Forbidden** | The courier is authenticated but does not have permission to edit the menu. |
| (g) Order 99999 doesn't exist | **404 Not Found** | The requested order cannot be found. |
| (h) Unhandled exception | **500 Internal Server Error** | An unexpected error occurred on the server. |

---

### Exercise 6 - Find an API you use — any public one — and write down three things its contract promises you. Then name one thing it does not document that you would want to know before depending on it.

### API Chosen: OpenWeather API
**Official**PI Documentation:** https://openweathermap.org/api
### Three things its contract promises

1. Responses are returned in **JSON format**, making them easy for applications to process.
2. Weather data can be requested using a **city name, ZIP code, or geographic coordinates**.
3. The API provides weather information such as **temperature, humidity, wind speed, and weather conditions**.

#### One thing it does not clearly document that I would want to know

Before depending on the API, I would want detailed information about its **service reliability and uptime guarantees (SLA)**, including how often outages occur and what level of availability is guaranteed.