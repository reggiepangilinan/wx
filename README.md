# 🏋️‍♀️ WX Exercises  

> A live version of the api is accessible here. https://wx-exercise-api.azurewebsites.net/swagger


## ⚡ Running the app locally 

Open solution `Wx.Exercises.sln` and hit `F5` is you're using VS2019 or higher

If you're using VSCode
```
cd Wx.Exercises
dotnet run --project Wx.Exercises.Api
```

then navigate to `https://localhost:5001/swagger` to view documentation and test endpoints

## 🧪 Running the tests

```
cd Wx.Exercises
dotnet test Wx.Exercises.Tests
```

## 📦 CI/CD is configured using GitHub Actions

You can check my step by step progress in here
https://github.com/reggiepangilinan/wx/actions

If you want to checkout the steps
https://github.com/reggiepangilinan/wx/blob/main/.github/workflows/main_wx-exercise-api.yml


## Framework + Packages used
- Net5
- MediatR
- xUnit
- Refit
- Serilog
- Swashbuckle


## 💡 Answers

### ✅ Exercise 1
---
### 👩‍💻 Curl 
```
curl -X POST --header 'Content-Type: application/json-patch+json' --header 'Accept: application/json' -d '{ \ 
   "token": "ef6abd87-34e2-4500-8649-f8f8418b7355", \ 
   "url": "https://wx-exercise-api.azurewebsites.net/api/Exercises" \ 
 }' 'http://dev-wooliesx-recruitment.azurewebsites.net/api/Exercise/exercise1'
```

### 📃 Response Body 
```
[
  {
    "passed": true,
    "url": "https://wx-exercise-api.azurewebsites.net/api/Exercises/user",
    "message": "Name returned correctly: Reggie Pangilinan."
  }
]
```
![image](https://user-images.githubusercontent.com/7448059/126067302-68495e67-653e-47e1-b662-7f3d77338780.png)


### ✅ Exercise 2
---
### 👩‍💻 Curl 
```
curl -X POST --header 'Content-Type: application/json-patch+json' --header 'Accept: application/json' -d '{ \ 
   "token": "ef6abd87-34e2-4500-8649-f8f8418b7355", \ 
   "url": "https://wx-exercise-api.azurewebsites.net/api/Exercises" \ 
 }' 'http://dev-wooliesx-recruitment.azurewebsites.net/api/Exercise/exercise2'
```

### 📃 Response Body 
```
[
  {
    "passed": true,
    "url": "https://wx-exercise-api.azurewebsites.net/api/Exercises/sort",
    "message": "Ascending Sort Passed"
  },
  {
    "passed": true,
    "url": "https://wx-exercise-api.azurewebsites.net/api/Exercises/sort",
    "message": "Descending Sort Passed"
  },
  {
    "passed": true,
    "url": "https://wx-exercise-api.azurewebsites.net/api/Exercises/sort",
    "message": "High Sort Passed"
  },
  {
    "passed": true,
    "url": "https://wx-exercise-api.azurewebsites.net/api/Exercises/sort",
    "message": "Low Sort Passed"
  },
  {
    "passed": true,
    "url": "https://wx-exercise-api.azurewebsites.net/api/Exercises/sort",
    "message": "Recommended Sort Passed"
  }
]
```
![image](https://user-images.githubusercontent.com/7448059/126067333-2db6334c-0b75-44b5-991b-c484deb0f1e4.png)


### ✅ Exercise 3
---
> This endpoint works intermittently. I used the endpoint provided.
### 👩‍💻 Curl 
```
curl -X POST --header 'Content-Type: application/json-patch+json' --header 'Accept: application/json' -d '{ \ 
   "token": "ef6abd87-34e2-4500-8649-f8f8418b7355", \ 
   "url": "https://wx-exercise-api.azurewebsites.net/api/Exercises" \ 
 }' 'http://dev-wooliesx-recruitment.azurewebsites.net/api/Exercise/exercise3'
```

### 📃 Response Body 
```
[
  {
    "passed": true,
    "url": "https://wx-exercise-api.azurewebsites.net/api/Exercises/trolleyTotal",
    "message": "Trolley total (313.63919932564653) returned correctly."
  },
  {
    "passed": true,
    "url": "https://wx-exercise-api.azurewebsites.net/api/Exercises/trolleyTotal",
    "message": "Trolley total (14) returned correctly."
  }
]
```

![image](https://user-images.githubusercontent.com/7448059/126067344-b0521790-31a1-4212-ada8-57befb34eb31.png)
