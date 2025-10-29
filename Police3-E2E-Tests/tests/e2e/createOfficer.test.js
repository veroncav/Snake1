const { test, expect } = require('@playwright/test');
require('dotenv').config();

test('Admin cannot create new user (button not present)', async ({ page }) => {
    await page.goto(process.env.BASE_URL + '/login2.php');

    await page.fill('input[name="kasutaja"]', process.env.ADMIN_USER);
    await page.fill('input[name="parool"]', process.env.ADMIN_PASS);
    await page.click('input[type="submit"]');

    await page.goto(process.env.BASE_URL + '/kasutajahaldus.php');

    // кнопки "Lisa kasutaja" нет
    await expect(page.locator('text=Lisa kasutaja')).not.toBeVisible();
});
