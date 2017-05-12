# UrlShortening
The purpose of this project is to provide a service for url shortening.
I have used nosql database (DynamoDB) so that service can scaled up and also used caching. Repository is extendable to any kind.
Also cacheing is extendable. Currently used non distributed cache but can easily extend to redis cache.
A running service can be found at:
"http://urlshorter.azurewebsites.net/"

How to get a short URL:
url: http://urlshorter.azurewebsites.net/st
method: post
body: {url : 'your url here'}
Response: You will find "ShortUrl=http://urlshorter.azurewebsites.net/st/5d4bf6" along with the original URL

Use this short url. You can test anytime pasting this short url to browser.


