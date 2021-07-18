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

```
.github/workflows/main_wx-exercise-api.yml
```

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

### ✅ Exercise 3
---

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