const { test, expect } = require('@playwright/test');
require('dotenv').config();

test('Admin can remove admin role', async ({ page }) => {
    await page.goto(process.env.BASE_URL + '/login2.php');

    await page.fill('input[name="kasutaja"]', process.env.ADMIN_USER);
    await page.fill('input[name="parool"]', process.env.ADMIN_PASS);
    await page.click('input[type="submit"]');

    await page.goto(process.env.BASE_URL + '/kasutajahaldus.php');

    // выбрать пользователя
    const row = page.locator('tr').filter({ hasText: process.env.USER_USER }).first();
    await expect(row).toBeVisible();

    const removeAdmin = row.locator('text=Eemalda admin');
    if (await removeAdmin.count() > 0) {
        await removeAdmin.click();
        await expect(page.locator('text=Kasutaja')).toBeVisible();
    }
});
