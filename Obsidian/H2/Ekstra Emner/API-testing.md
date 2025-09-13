# 🧪 API Testing - Komplet Guide

> **API Testing** er en kritisk del af moderne softwareudvikling. Denne guide dækker alt fra grundlæggende koncepter til avancerede testing strategier med fokus på praktisk implementering.

## 📋 Indholdsfortegnelse

- [🎯 Hvad er API Testing?](#-hvad-er-api-testing)
- [🔍 Typer af API Testing](#-typer-af-api-testing)
- [🛠️ Værktøjer til API Testing](#️-værktøjer-til-api-testing)
- [📊 Testing Strategier](#-testing-strategier)
- [🧪 Test Cases og Scenarier](#-test-cases-og-scenarier)
- [🔐 Autentificering og Authorization](#-autentificering-og-authorization)
- [📈 Performance Testing](#-performance-testing)
- [🔄 Automation og CI/CD](#-automation-og-cicd)
- [📝 Dokumentation og Reporting](#-dokumentation-og-reporting)
- [🎯 Best Practices](#-best-practices)
- [🔍 Fejlfinding](#-fejlfinding)

---

## 🎯 Hvad er API Testing?

**API Testing** er processen med at teste Application Programming Interfaces (APIs) for at sikre, at de fungerer korrekt, er pålidelige, performante og sikre.

### Hvorfor er API Testing vigtigt?

- **Tidlig Fejlfinding**: Find fejl før de når produktionen
- **Integration Sikkerhed**: Sikrer at forskellige systemer kommunikerer korrekt
- **Performance Garanti**: Validerer at API'en kan håndtere forventet belastning
- **Sikkerhed**: Tester for sårbarheder og sikkerhedsbrud
- **Kontrakt Validering**: Sikrer at API'en overholder sine specifikationer

### API Testing vs. UI Testing

| Aspekt | API Testing | UI Testing |
|--------|-------------|------------|
| **Niveau** | Backend/Service | Frontend/User Interface |
| **Hastighed** | Hurtig | Langsom |
| **Stabilitet** | Høj | Lav (UI ændringer) |
| **Coverage** | Bred | Begrænset |
| **Maintenance** | Lav | Høj |

---

## 🔍 Typer af API Testing

### 1. **Functional Testing**
Test af API'ens funktionalitet og forventet adfærd.

```javascript
// Eksempel: Test af GET endpoint
test("GET /api/users should return user list", async () => {
  const response = await request(app)
    .get('/api/users')
    .expect(200);
  
  expect(response.body).toHaveProperty('data');
  expect(Array.isArray(response.body.data)).toBe(true);
});
```

### 2. **Integration Testing**
Test af hvordan API'en integrerer med andre systemer.

```javascript
// Eksempel: Test af database integration
test("User creation should save to database", async () => {
  const userData = { name: "Test User", email: "test@example.com" };
  
  const response = await request(app)
    .post('/api/users')
    .send(userData)
    .expect(201);
  
  // Verify in database
  const savedUser = await User.findById(response.body.id);
  expect(savedUser).toBeTruthy();
  expect(savedUser.email).toBe(userData.email);
});
```

### 3. **Performance Testing**
Test af API'ens performance under forskellige belastninger.

```javascript
// Eksempel: Load testing
test("API should handle 100 concurrent requests", async () => {
  const requests = Array(100).fill().map(() => 
    request(app).get('/api/users')
  );
  
  const responses = await Promise.all(requests);
  const avgResponseTime = responses.reduce((sum, res) => 
    sum + res.responseTime, 0) / responses.length;
  
  expect(avgResponseTime).toBeLessThan(1000); // Under 1 sekund
});
```

### 4. **Security Testing**
Test af API'ens sikkerhed og sårbarheder.

```javascript
// Eksempel: SQL Injection test
test("API should prevent SQL injection", async () => {
  const maliciousInput = "'; DROP TABLE users; --";
  
  const response = await request(app)
    .post('/api/users/search')
    .send({ query: maliciousInput })
    .expect(400);
  
  expect(response.body.error).toContain('Invalid input');
});
```

### 5. **Contract Testing**
Test af at API'en overholder sine kontrakter/specifikationer.

```javascript
// Eksempel: Schema validation
test("Response should match OpenAPI schema", async () => {
  const response = await request(app)
    .get('/api/users/1')
    .expect(200);
  
  const isValid = validateResponse(response.body, userSchema);
  expect(isValid).toBe(true);
});
```

---

## 🛠️ Værktøjer til API Testing

### 1. **Bruno** ⭐ (Anbefalet)
- **File-based**: Gemmer tests som filer
- **Git Integration**: Native version control
- **Offline**: Fungerer uden internet
- **Gratis**: Open source

```bru
// Bruno test eksempel
tests {
  test("Status skal være 200 OK", function() {
    expect(res.status).to.equal(200);
  });
  
  test("Response skal indeholde data", function() {
    expect(res.body).to.have.property("data");
  });
}
```

### 2. **Postman**
- **Cloud-based**: Online workspace
- **Team Collaboration**: Deling og samarbejde
- **Collection Runner**: Automatiseret test execution
- **Betalt**: Premium features kræver subscription

### 3. **REST Assured** (Java)
```java
@Test
public void testGetUser() {
    given()
        .baseUri("https://api.example.com")
        .pathParam("id", 1)
    .when()
        .get("/users/{id}")
    .then()
        .statusCode(200)
        .body("id", equalTo(1))
        .body("name", notNullValue());
}
```

### 4. **Supertest** (Node.js)
```javascript
const request = require('supertest');
const app = require('../app');

describe('User API', () => {
  test('GET /users should return users', async () => {
    const response = await request(app)
      .get('/users')
      .expect(200);
    
    expect(response.body).toHaveProperty('users');
  });
});
```

### 5. **Newman** (Postman CLI)
```bash
# Kør Postman collection via CLI
newman run collection.json -e environment.json
```

---

## 📊 Testing Strategier

### 1. **Test Pyramid for APIs**

```
    /\
   /  \     E2E Tests (Få)
  /____\    
 /      \   Integration Tests (Nogle)
/________\  
/          \ Unit Tests (Mange)
/____________\
```

### 2. **API Testing Levels**

#### **Unit Level**
- Test individuelle endpoints
- Mock eksterne dependencies
- Hurtige og isolerede tests

#### **Integration Level**
- Test API med database
- Test med eksterne services
- Test data flow mellem komponenter

#### **System Level**
- Test hele API'et som en enhed
- Test med rigtige data
- Test performance og sikkerhed

### 3. **Testing Approaches**

#### **Black Box Testing**
- Test uden kendskab til intern implementering
- Fokus på input/output forhold
- Test baseret på API specifikation

#### **White Box Testing**
- Test med kendskab til intern kode
- Test alle code paths
- Fokus på intern logik

#### **Gray Box Testing**
- Kombination af black og white box
- Test med begrænset kendskab til intern struktur
- Fokus på integration og data flow

---

## 🧪 Test Cases og Scenarier

### 1. **Positive Test Cases**
Test af forventet adfærd med gyldige inputs.

```javascript
// Eksempel: Successful user creation
test("Create user with valid data", async () => {
  const userData = {
    name: "John Doe",
    email: "john@example.com",
    password: "securePassword123"
  };
  
  const response = await request(app)
    .post('/api/users')
    .send(userData)
    .expect(201);
  
  expect(response.body).toHaveProperty('id');
  expect(response.body.name).toBe(userData.name);
  expect(response.body.email).toBe(userData.email);
});
```

### 2. **Negative Test Cases**
Test af fejlhåndtering med ugyldige inputs.

```javascript
// Eksempel: Invalid email format
test("Create user with invalid email", async () => {
  const userData = {
    name: "John Doe",
    email: "invalid-email",
    password: "securePassword123"
  };
  
  const response = await request(app)
    .post('/api/users')
    .send(userData)
    .expect(400);
  
  expect(response.body).toHaveProperty('error');
  expect(response.body.error).toContain('email');
});
```

### 3. **Boundary Value Testing**
Test af grænseværdier og edge cases.

```javascript
// Eksempel: Test minimum/maximum values
test("Create user with boundary values", async () => {
  const testCases = [
    { name: "A", email: "a@b.co", password: "123456" }, // Minimum
    { name: "A".repeat(100), email: "test@example.com", password: "12345678901234567890" } // Maximum
  ];
  
  for (const userData of testCases) {
    const response = await request(app)
      .post('/api/users')
      .send(userData)
      .expect(201);
    
    expect(response.body).toHaveProperty('id');
  }
});
```

### 4. **Error Handling Tests**
Test af forskellige fejlscenarier.

```javascript
// Eksempel: Test various error scenarios
describe('Error Handling', () => {
  test('Should return 404 for non-existent user', async () => {
    const response = await request(app)
      .get('/api/users/99999')
      .expect(404);
    
    expect(response.body).toHaveProperty('error');
  });
  
  test('Should return 401 for unauthorized access', async () => {
    const response = await request(app)
      .get('/api/users')
      .expect(401);
    
    expect(response.body).toHaveProperty('error');
  });
  
  test('Should return 500 for server error', async () => {
    // Mock database error
    jest.spyOn(User, 'findAll').mockRejectedValue(new Error('Database error'));
    
    const response = await request(app)
      .get('/api/users')
      .expect(500);
    
    expect(response.body).toHaveProperty('error');
  });
});
```

---

## 🔐 Autentificering og Authorization

### 1. **JWT Token Testing**

```javascript
// Eksempel: Test JWT authentication
describe('JWT Authentication', () => {
  let authToken;
  
  beforeAll(async () => {
    // Login to get token
    const response = await request(app)
      .post('/api/auth/login')
      .send({
        email: 'test@example.com',
        password: 'password123'
      });
    
    authToken = response.body.token;
  });
  
  test('Should access protected route with valid token', async () => {
    const response = await request(app)
      .get('/api/users/profile')
      .set('Authorization', `Bearer ${authToken}`)
      .expect(200);
    
    expect(response.body).toHaveProperty('user');
  });
  
  test('Should reject request with invalid token', async () => {
    const response = await request(app)
      .get('/api/users/profile')
      .set('Authorization', 'Bearer invalid-token')
      .expect(401);
    
    expect(response.body).toHaveProperty('error');
  });
});
```

### 2. **Role-Based Access Control (RBAC)**

```javascript
// Eksempel: Test role-based access
describe('Role-Based Access', () => {
  let adminToken, userToken;
  
  beforeAll(async () => {
    // Get admin token
    const adminResponse = await request(app)
      .post('/api/auth/login')
      .send({ email: 'admin@example.com', password: 'admin123' });
    adminToken = adminResponse.body.token;
    
    // Get user token
    const userResponse = await request(app)
      .post('/api/auth/login')
      .send({ email: 'user@example.com', password: 'user123' });
    userToken = userResponse.body.token;
  });
  
  test('Admin should access admin-only endpoint', async () => {
    const response = await request(app)
      .get('/api/admin/users')
      .set('Authorization', `Bearer ${adminToken}`)
      .expect(200);
    
    expect(response.body).toHaveProperty('users');
  });
  
  test('User should not access admin-only endpoint', async () => {
    const response = await request(app)
      .get('/api/admin/users')
      .set('Authorization', `Bearer ${userToken}`)
      .expect(403);
    
    expect(response.body).toHaveProperty('error');
  });
});
```

### 3. **API Key Testing**

```javascript
// Eksempel: Test API key authentication
describe('API Key Authentication', () => {
  test('Should access with valid API key', async () => {
    const response = await request(app)
      .get('/api/data')
      .set('X-API-Key', 'valid-api-key')
      .expect(200);
    
    expect(response.body).toHaveProperty('data');
  });
  
  test('Should reject with invalid API key', async () => {
    const response = await request(app)
      .get('/api/data')
      .set('X-API-Key', 'invalid-key')
      .expect(401);
    
    expect(response.body).toHaveProperty('error');
  });
});
```

---

## 📈 Performance Testing

### 1. **Load Testing**

```javascript
// Eksempel: Load testing med Artillery
const { expect } = require('chai');

module.exports = {
  config: {
    target: 'https://api.example.com',
    phases: [
      { duration: '30s', arrivalRate: 10 }, // 10 requests per second
      { duration: '1m', arrivalRate: 20 },  // 20 requests per second
      { duration: '30s', arrivalRate: 0 }   // Ramp down
    ]
  },
  scenarios: [
    {
      name: 'Get users',
      weight: 70,
      flow: [
        {
          get: {
            url: '/api/users'
          }
        }
      ]
    },
    {
      name: 'Create user',
      weight: 30,
      flow: [
        {
          post: {
            url: '/api/users',
            json: {
              name: 'Test User',
              email: 'test@example.com'
            }
          }
        }
      ]
    }
  ]
};
```

### 2. **Stress Testing**

```javascript
// Eksempel: Stress testing
test('API should handle stress load', async () => {
  const concurrentRequests = 1000;
  const requests = Array(concurrentRequests).fill().map(() => 
    request(app).get('/api/users')
  );
  
  const startTime = Date.now();
  const responses = await Promise.all(requests);
  const endTime = Date.now();
  
  const totalTime = endTime - startTime;
  const avgResponseTime = responses.reduce((sum, res) => 
    sum + res.responseTime, 0) / responses.length;
  
  // Assertions
  expect(totalTime).toBeLessThan(10000); // Under 10 seconds
  expect(avgResponseTime).toBeLessThan(1000); // Under 1 second average
  expect(responses.every(res => res.status === 200)).toBe(true);
});
```

### 3. **Memory Leak Testing**

```javascript
// Eksempel: Memory leak detection
test('API should not have memory leaks', async () => {
  const initialMemory = process.memoryUsage().heapUsed;
  
  // Perform many requests
  for (let i = 0; i < 1000; i++) {
    await request(app).get('/api/users');
  }
  
  // Force garbage collection
  if (global.gc) {
    global.gc();
  }
  
  const finalMemory = process.memoryUsage().heapUsed;
  const memoryIncrease = finalMemory - initialMemory;
  
  // Memory increase should be reasonable (less than 50MB)
  expect(memoryIncrease).toBeLessThan(50 * 1024 * 1024);
});
```

---

## 🔄 Automation og CI/CD

### 1. **GitHub Actions Integration**

```yaml
# .github/workflows/api-tests.yml
name: API Tests

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

jobs:
  api-tests:
    runs-on: ubuntu-latest
    
    services:
      postgres:
        image: postgres:13
        env:
          POSTGRES_PASSWORD: postgres
        options: >-
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5
    
    steps:
    - uses: actions/checkout@v2
    
    - name: Setup Node.js
      uses: actions/setup-node@v2
      with:
        node-version: '18'
    
    - name: Install dependencies
      run: npm ci
    
    - name: Run database migrations
      run: npm run migrate
      env:
        DATABASE_URL: postgres://postgres:postgres@localhost:5432/test
    
    - name: Run API tests
      run: npm run test:api
      env:
        NODE_ENV: test
        DATABASE_URL: postgres://postgres:postgres@localhost:5432/test
    
    - name: Run Bruno tests
      run: |
        npm install -g @usebruno/cli
        bruno test Bruno/API
```

### 2. **Docker Integration**

```dockerfile
# Dockerfile for API testing
FROM node:18-alpine

WORKDIR /app

# Copy package files
COPY package*.json ./
RUN npm ci --only=production

# Copy source code
COPY . .

# Install Bruno
RUN npm install -g @usebruno/cli

# Copy Bruno collection
COPY Bruno/ ./Bruno/

# Run tests
CMD ["bruno", "test", "Bruno/API"]
```

### 3. **Test Data Management**

```javascript
// Eksempel: Test data setup og cleanup
describe('User API', () => {
  let testUsers = [];
  
  beforeAll(async () => {
    // Setup test data
    testUsers = await createTestUsers(5);
  });
  
  afterAll(async () => {
    // Cleanup test data
    await cleanupTestUsers(testUsers);
  });
  
  beforeEach(async () => {
    // Reset database state
    await resetDatabase();
  });
  
  test('Should get all users', async () => {
    const response = await request(app)
      .get('/api/users')
      .expect(200);
    
    expect(response.body.users).toHaveLength(testUsers.length);
  });
});
```

---

## 📝 Dokumentation og Reporting

### 1. **API Documentation**

```javascript
// Eksempel: OpenAPI/Swagger dokumentation
/**
 * @swagger
 * /api/users:
 *   get:
 *     summary: Get all users
 *     tags: [Users]
 *     security:
 *       - bearerAuth: []
 *     responses:
 *       200:
 *         description: List of users
 *         content:
 *           application/json:
 *             schema:
 *               type: object
 *               properties:
 *                 users:
 *                   type: array
 *                   items:
 *                     $ref: '#/components/schemas/User'
 *       401:
 *         description: Unauthorized
 *       500:
 *         description: Server error
 */
app.get('/api/users', authenticateToken, async (req, res) => {
  // Implementation
});
```

### 2. **Test Reporting**

```javascript
// Eksempel: Test reporting med Jest
const { generateReport } = require('jest-html-reporters');

afterAll(async () => {
  // Generate HTML report
  await generateReport({
    pageTitle: 'API Test Report',
    outputPath: './test-reports/api-tests.html',
    includeFailureMsg: true,
    includeSuiteFailure: true
  });
});
```

### 3. **Coverage Reporting**

```javascript
// jest.config.js
module.exports = {
  collectCoverage: true,
  coverageDirectory: 'coverage',
  coverageReporters: ['text', 'lcov', 'html'],
  collectCoverageFrom: [
    'src/**/*.js',
    '!src/**/*.test.js',
    '!src/**/*.spec.js'
  ],
  coverageThreshold: {
    global: {
      branches: 80,
      functions: 80,
      lines: 80,
      statements: 80
    }
  }
};
```

---

## 🎯 Best Practices

### 1. **Test Organization**

```
tests/
├── unit/           # Unit tests
│   ├── controllers/
│   ├── services/
│   └── models/
├── integration/    # Integration tests
│   ├── api/
│   └── database/
├── e2e/           # End-to-end tests
└── fixtures/      # Test data
    ├── users.json
    └── hotels.json
```

### 2. **Naming Conventions**

```javascript
// ✅ Gode test navne
describe('UserController', () => {
  describe('GET /api/users', () => {
    it('should return all users when authenticated', () => {});
    it('should return 401 when not authenticated', () => {});
    it('should return 500 when database error occurs', () => {});
  });
});

// ❌ Dårlige test navne
describe('Test', () => {
  it('test1', () => {});
  it('should work', () => {});
});
```

### 3. **Test Data Management**

```javascript
// Eksempel: Test data factory
class UserFactory {
  static create(overrides = {}) {
    return {
      name: 'Test User',
      email: 'test@example.com',
      password: 'password123',
      role: 'user',
      ...overrides
    };
  }
  
  static createAdmin(overrides = {}) {
    return this.create({
      role: 'admin',
      ...overrides
    });
  }
}

// Brug i tests
test('Should create user', async () => {
  const userData = UserFactory.create({ name: 'John Doe' });
  const response = await request(app)
    .post('/api/users')
    .send(userData)
    .expect(201);
});
```

### 4. **Error Handling**

```javascript
// Eksempel: Proper error handling i tests
test('Should handle database connection error', async () => {
  // Mock database error
  jest.spyOn(User, 'findAll').mockRejectedValue(
    new Error('Database connection failed')
  );
  
  const response = await request(app)
    .get('/api/users')
    .expect(500);
  
  expect(response.body).toHaveProperty('error');
  expect(response.body.error).toContain('Database');
  
  // Restore mock
  jest.restoreAllMocks();
});
```

### 5. **Performance Considerations**

```javascript
// Eksempel: Performance optimerede tests
describe('Performance Tests', () => {
  test('API should respond within 100ms', async () => {
    const startTime = Date.now();
    
    await request(app)
      .get('/api/users')
      .expect(200);
    
    const responseTime = Date.now() - startTime;
    expect(responseTime).toBeLessThan(100);
  });
  
  test('API should handle 100 concurrent requests', async () => {
    const requests = Array(100).fill().map(() => 
      request(app).get('/api/users')
    );
    
    const responses = await Promise.all(requests);
    const failedRequests = responses.filter(res => res.status !== 200);
    
    expect(failedRequests).toHaveLength(0);
  });
});
```

---

## 🔍 Fejlfinding

### 1. **Almindelige API Test Fejl**

#### **Connection Refused**
```
Error: connect ECONNREFUSED 127.0.0.1:3000
```
**Løsning:**
- Tjek at API serveren kører
- Verificer port og host
- Tjek firewall indstillinger

#### **Timeout Errors**
```
Error: timeout of 5000ms exceeded
```
**Løsning:**
- Øg timeout værdi
- Tjek API performance
- Verificer database queries

#### **Authentication Errors**
```
Error: 401 Unauthorized
```
**Løsning:**
- Tjek token format
- Verificer token expiration
- Tjek authentication flow

### 2. **Debug Tips**

```javascript
// Eksempel: Debug logging
test('Debug API response', async () => {
  const response = await request(app)
    .get('/api/users')
    .expect(200);
  
  // Debug output
  console.log('Response status:', response.status);
  console.log('Response headers:', response.headers);
  console.log('Response body:', response.body);
  console.log('Response time:', response.responseTime);
  
  expect(response.body).toHaveProperty('users');
});
```

### 3. **Test Environment Setup**

```javascript
// Eksempel: Test environment konfiguration
const setupTestEnvironment = async () => {
  // Setup test database
  await setupTestDatabase();
  
  // Seed test data
  await seedTestData();
  
  // Setup mocks
  setupMocks();
  
  // Start test server
  const app = await startTestServer();
  
  return app;
};

const cleanupTestEnvironment = async () => {
  // Cleanup test database
  await cleanupTestDatabase();
  
  // Restore mocks
  restoreMocks();
  
  // Stop test server
  await stopTestServer();
};
```

---

## 🎯 Konklusion

API Testing er en kritisk komponent i moderne softwareudvikling der sikrer:

- ✅ **Kvalitet**: APIs fungerer som forventet
- ✅ **Pålidelighed**: Konsistent adfærd under forskellige forhold
- ✅ **Sikkerhed**: Beskyttelse mod sårbarheder
- ✅ **Performance**: Optimal hastighed og ressourceforbrug
- ✅ **Integration**: Korrekt kommunikation mellem systemer

### Nøgle Takeaways:

1. **Start tidligt** - Test fra dag ét af udviklingen
2. **Automatiser** - Integrer tests i CI/CD pipeline
3. **Dokumenter** - Hold test dokumentation opdateret
4. **Monitorer** - Overvåg API performance kontinuerligt
5. **Iterer** - Forbedre tests baseret på feedback

**Med de rigtige værktøjer og strategier kan API testing være både effektivt og effektivt!** 🚀

---

*Denne guide dækker moderne API testing praksis. For specifikke værktøjer som Bruno, se [Bruno.md](./Bruno.md)*
