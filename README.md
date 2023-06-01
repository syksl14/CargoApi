
# CargoApi
Cargo status query using siri with personal ups cargo api. 

It allows you to query your UPS cargo by giving commands with Apple ecosystem and other clients. It can be personalized. It can be used with shortcuts, software and other methods on iOS/iPadOS/MacOS.

> This service is designed to be developed with other cargo integrations. It is offered free of charge/not for sale. You need hosting or server to run the api.

## Demo
Cargo query via API with shortcuts on ios. 
[Shortcut Download](https://www.icloud.com/iclouddrive/071cmdlanPKDZ3JC2608XKzqA#Kargom_Nerede)

## Screenshots

![Kestirmeler](https://depo.selahattinyuksel.net/img/IMG_2686.PNG)
![Örnek](https://depo.selahattinyuksel.net/img/IMG_2687.PNG)
  
  
## Features

- User authentication with token
- Json supported api service with flexible code
- It has been developed on Asp Net Core structure.
- Support for all platforms

  
## API Usage

#### Login api service

```http
  POST /cargo/login
```
JSON Body
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `username` | `string` | **Required** for authentication. |
| `password` | `string` | **Required** for authentication. |

Return token key.

#### Cargo query

```http
  GET /cargo/ups/{Waybill}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `Waybill`      | `string` | **Required**. Your UPS shipping tracking code. |

| Header Name | Value     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `Authorization`      | `Bearer token....` | The key returned through authentication is sent over the header.  |


## Feedback

If you have any feedback, please contact us at iletisim@selahattinyuksel.net.

  
