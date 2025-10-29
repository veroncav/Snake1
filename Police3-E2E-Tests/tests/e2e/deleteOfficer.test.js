/**
 * Исходное состояние: Админ авторизован.
 * Действие: Создаёт временного офицера, затем удаляет через UI.
 * Ожидаемый результат: Запись исчезает из списка.
 */
const { test, expect } = require('@playwright/test');
require('dotenv').config();

test('Admin creates and deletes officer', async ({ page }) => {
  await page.goto(process.env.BASE_URL + '/login2.php');
  await page.fill('input[name="kasutaja"]', process.env.ADMIN_USER);
    await page.fill('input[name="parool""]', process.env.ADMIN_PASS);
  await page.click('button[type="submit"]');
  await page.goto(process.env.BASE_URL + '/officers.php');

  // create
  const addOpts = ['text=Add Officer','text=Add','button#addOfficer','button:has-text("Add")'];
  let addFound=null;
  for (const s of addOpts) { if (await page.locator(s).count()>0) { addFound=s; break; } }
  if (!addFound) throw new Error('Add not found');
  await page.click(addFound);

  const testName = 'Delete_Officer_' + Date.now();
  const firstNameField = ['input[name="first_name"]','input[name="firstname"]','input[name="fname"]'];
  for (const f of firstNameField) { if (await page.locator(f).count()>0) { await page.fill(f,testName); break; } }
  const submitButtons = ['button[type="submit"]','text=Save','text=Create','button:has-text("Save")'];
  for (const s of submitButtons) { if (await page.locator(s).count()>0) { await page.click(s); break; } }

  // ensure created
  await expect(page.locator(`text=${testName}`)).toBeVisible();

  // delete
  const row = page.locator(`tr:has-text("${testName}")`);
  const delBtns = ['button.deleteOfficer','text=Delete','button:has-text("Delete")'];
  for (const d of delBtns) { if (await row.locator(d).count()>0) { await row.locator(d).first().click(); break; } }
  await expect(page.locator(`text=${testName}`)).not.toBeVisible();
});
