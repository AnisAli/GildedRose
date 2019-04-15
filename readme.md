# GildedRose

# Assumptions
GildedRose is providing the APIs to the merchants, and merchants are actually the users for GildedRose, not the actual customer who are buying the products from Gilded Rose Inventory via merchant provided interface.

**API**: GildedRose's REST API, which the merchant can use to read product list and checkout.
**User**: A GildedRose account holder, usually a merchant.
**Checkout**: For demo purpose, Checkout API allow to buy only one product at a time with custom quantity.

# Running using Docker 
Docker image found on https://hub.docker.com/r/anisnoorali/gildedrose-api

Pull Image from Dockerhub
```sh
docker pull anisnoorali/gildedrose-api:latest
```
API running on port 5000 inside the container, spin the container using the following command
```sh
docker run -it -p 5000:5000 anisnoorali/gildedrose-api
```


# Authentication!
Authentication for the Buy Product (Checkout) API is performed via OAuth2 using OpenIddict. In order to access checkout API, we have to authenticate first. For the demo purpose, the API only accepts the password grandtype, which required a username and password but in the real world, we can use multiple authentication providers (like google, facebook, etc) for authentication.

To retrieve an access token, send a POST request to `/api/v1/auth/connect/token` with the grant_type=password parameter and the user credentials

htttp://localhost:{port}/api/v1/auth/connect/token
Content-Type: application/x-www-form-urlencoded
```
grant_type=password
username=test
password=test
```
**CURL Command for getting access token**
```code
curl -X POST \
  http://localhost:5000/api/v1/auth/connect/token \
  -H 'Content-Type: application/x-www-form-urlencoded' \
  -d 'grant_type=password&username=test&password=test'
```
If the credentials are valid, you'll get a JSON response containing the access token:

```
{
    "token_type": "Bearer",
    "access_token": "CfDJ8Gc6d7Yxn1JNshZ0tVYAN-ncNb6m8M7SUOT....",
    "expires_in": 3600
}
```
To send an checkout authenticated request, simply attach the bearer token to the Authorization header.


 # Product List API
Product list API returns the list of the inventory with optional parameters PageSize, PageNumber and SearchText. The merchant can leverage this API to display the products in paging and also pass the search filter to get a particular product using the product name.


**CURL Command for getting Product List**

```code
curl -X GET  http://localhost:5000/api/v1/store/products
or
curl -X GET http://localhost:4444/api/v1/store/products?pagesize=10&pagenumber=1
or
http://localhost:4444/api/v1/store/products?pagesize=10&pagenumber=1&searchtext=blue
```

**Response**
http://localhost:{port}/api/v1/store/products?pagesize=2&pagenumber=1
```
{
    "totalProducts": 2,
    "products": [
        {
            "productId": 1,
            "name": "Blue Bags",
            "description": "Fancy Blue Bags",
            "price": 12
        },
        {
            "productId": 2,
            "name": "Green T-Shirt",
            "description": "Fancy Green T-Shirt",
            "price": 100
        }
    ]
}
```

# Buy(Checkout) API
Check out API require product id and quantity for purchasing- If the product will be in Stock- API will response back the order Id and the details of the product by modifying the inventory.

Method Type: POST
Authentication: Required

**CURL Command for Checkout API**
```code
curl -X POST \
  http://localhost:5000/api/v1/store/checkout \
  -H 'Authorization: Bearer CfDJ8JbInJOOlZtNkF7lCeq_6lDHwS...' \
  -H 'Content-Type: application/json' \
  -d '{
	"productId":1,
	"quantity": 3
}'
```
**Request Body**
```
  {
	"productId":1,
	"quantity": 3
 }
```
**Response**
```
{
    "orderId": "f10d2660-cb58-48d8-afca-384666fec30c",
    "product": {
        "productId": 1,
        "name": "Blue Bags",
        "description": "Fancy Blue Bags",
        "price": 12
    },
    "timeStamp": "2019-04-15T02:42:14.0015996Z"
}
```

# Tools used
 *  Dotnet Core 2.1 (OpenIddict)
 *  Docker 
 *  Git
 *  Fluent Assertion and Xunit in Testing 

# Design Choices

### API versioning
I chose version controlling as well in the API endpoints, because it will be easier to maintain different versions of the API, as these APIs are public and consumed by the different merchant, thus in order to support merchants existing applications this would be the better choice to have versions APIs.

### Authentication
I chose  OAuth 2.0 because it is a common, widely supported, and standardized authentication method. With OAuth 2.0 client credentials, authenticating a client app is a two-step process: first, the client sends its API credentials to an authorization server that returns an access token. Second, the client sends a request to the API with that access token and the API verifies it and either authorizes the call or rejects it with a 401 Unauthorized. 

I have added one auth controller which act as an Authorization provider endpoint (/connect/token) to get the access token, we can use other third-party authorization providers to get the access-token.

### EntityFramework
I used InMemoryDBContext for data storage purchase instead of using just one List. This API will work with the actual database as well by just modifying the startup.cs file.

## Data Format
JSON is a lightweight and versatile format. It is relatively easier to parse, serialize/deserialize by clients.

## Testing
I have a number of unit tests for services and controller, and also added the integration tests for testing endpoints by using Mocks and In-Memory database.




