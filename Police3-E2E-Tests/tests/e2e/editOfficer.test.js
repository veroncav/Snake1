const { test, expect } = require('@playwright/test');
require('dotenv').config();

test('Admin edits a user role', async ({ page }) => {
    await page.goto(process.env.BASE_URL + '/login2.php');

    await page.fill('input[name="kasutaja"]', process.env.ADMIN_USER);
    await page.fill('input[name="parool"]', process.env.ADMIN_PASS);
    await page.click('input[type="submit"]');

    await page.goto(process.env.BASE_URL + '/kasutajahaldus.php');

    // выбрать первого пользователя (не себя)
    const row = page.locator('tr').filter({ hasText: process.env.USER_USER }).first();
    await expect(row).toBeVisible();

    const assignAdmin = row.locator('text=Määra admin');
    if (await assignAdmin.count() > 0) {
        await assignAdmin.click();
        await expect(page.locator('text=Admin')).toBeVisible();
    }

    const removeAdmin = row.locator('text=Eemalda admin');
    if (await removeAdmin.count() > 0) {
        await removeAdmin.click();
        await expect(page.locator('text=Kasutaja')).toBeVisible();
    }
});
