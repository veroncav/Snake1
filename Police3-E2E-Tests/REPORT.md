# Police3 E2E Test Report

## What works
1. Admin login succeeds.
2. Regular user login works; admin-only UI elements are hidden.
3. Admin can assign and remove admin roles.
4. Admin can edit user roles.
5. Admin can remove users from the system.
6. All tests are fully automated and independent.

## Notes / Limitations
- Locators rely on button text and input names; if the UI changes, tests may need updates.
- No direct database access; everything is tested via UI.
- Playwright captures screenshots and videos for failed tests in `playwright-report`.

## How to verify
1. Install dependencies (`npm install`, `npx playwright install`).
2. Set environment variables (`.env`).
3. Run tests (`npm run test:e2e`).
4. Open HTML report (`npx playwright show-report`) to view results.
