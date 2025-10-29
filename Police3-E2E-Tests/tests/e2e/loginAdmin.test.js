/**
 * Исходное состояние: Админ не авторизован.
 * Действие: Вводит логин/пароль и нажимает Login.
 * Ожидаемый результат: Попадает на welcome.php, видит приветствие.
 */
const { test, expect } = require('@playwright/test');
require('dotenv').config();

test('Admin successful login', async ({ page }) => {
    await page.goto(process.env.BASE_URL + '/login2.php');

    await expect(page.locator('input[name="kasutaja"]')).toBeVisible();
    await page.fill('input[name="kasutaja"]', process.env.ADMIN_USER);
    await page.fill('input[name="parool"]', process.env.ADMIN_PASS);
    await page.click('input[type="submit"]');

    await expect(page).toHaveURL(process.env.BASE_URL + '/welcome.php');
    await expect(page.locator('text=Tere tulemast')).toBeVisible();
});
