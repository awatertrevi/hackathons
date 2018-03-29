# Web Service
### Credits
 - Matthew Smeets
 - Luke Paris

### Usage
#### Requesting resources
**Authentication for patients**
```
 [url]: /api/login/patient
 [request]:
 {
 	"email" : string,
 	"password" : string
 }
```

**Authentication for doctors**
```
 [url]: /api/login/doctor
 [request]:
 {
 	"email" : string,
 	"password" : string
 }
```

**Get doctor details by id**
```
 [url]: /api/doctor
 [request]:
 {
 	"id" : int
 }
```

**Get patient details by id**
```
 [url]: /api/patient
 [request]:
 {
 	"id" : int
 }
```

**Get all appointments related to a patient by id**
```
 [url]: /api/patient/appointments
 [request]:
 {
 	"id" : int
 }
```

**Get an appointment by id**
```
 [url]: /api/appointment
 [request]:
 {
 	"id" : int
 }
```

#### Adding resources

**Add a patient**
```
 [url]: /api/patient/add
 [request]:
 {
 	"firstname" : string,
 	"lastname" : string,
 	"email" : string,
 	"password" : string,
 	"phone" : string,
 	"birthday" : date,
 	"insurance" : string,
 	"risk" : int,
 	"zip" : string,
 	"house" : string
 }
```

**Add a patient**
```
 [url]: /api/patient/add
 [request]:
 {
 	"firstname" : string,
 	"lastname" : string,
 	"email" : string,
 	"password" : string,
 	"phone" : string,
 	"birthday" : date,
 	"insurance" : string,
 	"risk" : int,
 	"zip" : string,
 	"house" : string
 }
```

 **Add an appointment**
```
 [url]: /api/appointment/add
 [request]:
 {
 	"name" : string,
 	"subject" : string,
 	"symptoms" : string,
 	"date" : date
 }
```

#### Response format

```
{
    "success" : boolean,
    "message" : string,
    "body" : object
}
```

