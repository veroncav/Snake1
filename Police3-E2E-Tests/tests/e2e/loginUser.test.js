/**
 * Исходное состояние: Обычный пользователь не авторизован.
 * Действие: Вводит логин/пароль и нажимает Login.
 * Ожидаемый результат: Попадает на welcome.php, но не видит админских кнопок.
 */
const { test, expect } = require('@playwright/test');
require('dotenv').config();

test('Regular user limited access', async ({ page }) => {
    await page.goto(process.env.BASE_URL + '/login2.php');

    await expect(page.locator('input[name="kasutaja"]')).toBeVisible();
    await page.fill('input[name="kasutaja"]', process.env.USER_USER);
    await page.fill('input[name="parool"]', process.env.USER_PASS);
    await page.click('input[type="submit"]');

    await expect(page).toHaveURL(process.env.BASE_URL + '/welcome.php');
    // admin features should not be visible for a regular user
    await expect(page.locator('text=Create Officer')).not.toBeVisible();
});
